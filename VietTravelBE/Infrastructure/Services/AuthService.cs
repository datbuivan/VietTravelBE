using CloudinaryDotNet.Actions;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Extensions;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public AuthService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailService emailService,
            IConfiguration config,
            ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _config = config;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, loginDto.Password)))
            {
                return new ApiResponse<AuthResponseDto>(401, "Invalid email or password");
            }
            var accessToken = await _tokenService.GenerateJwtToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(Convert.ToDouble(_config["Token:AccessTokenExpiresMinutes"]!));
            await _userManager.UpdateAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            return new ApiResponse<AuthResponseDto>(200, "Login successful", new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserName = user.UserName,
                Roles = roles.ToArray(),
            });
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            var user = new AppUser 
            { 
                UserName = registerDto.Username,
                Email = registerDto.Email 
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new ApiException(400, $"Registration failed: {errors}");
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "USER");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);
            var clientAppUrl = _config["ClientAppUrl"];
            var confirmLink = $"{clientAppUrl}/Auth/confirm-email?userId={user.Id}&token={encodedToken}";
            var subject = "Xác nhận đăng ký - Việt Travel";
            var body = BuildConfirmationEmailBody(user.UserName, confirmLink);

            await _emailService.SendEmailAsync(user.Email, subject, body);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new Exception("Invalid or expired refresh token");
            }

            var accessToken = await _tokenService.GenerateJwtToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            if (accessToken == null || newRefreshToken == null)
                throw new Exception("Failed to create new token");

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                throw new Exception("Failed to update refresh token");

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken,
                UserName = user.UserName,
                Roles = roles.ToArray()
            };
        }
        

        public async Task<UserDto> GetCurrentUserAsync()
        {
            var userPrincipal = _httpContextAccessor.HttpContext?.User;
            if (userPrincipal == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var user = await _userManager.FindByEmailFromClaimsPrinciple(userPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException("User not found from claims.");

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = roles.ToArray()
            };
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task LogoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = DateTime.MinValue;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<(bool Success, string ErrorMessage)> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (false, "Không tìm thấy người dùng.");
            }
            if (user.EmailConfirmed)
            {
                
                return (false, "Email đã được xác nhận trước đó.");
            }
            
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return (false, $"Xác nhận email thất bại: {errorMessage}");

            }
            return (true, "Email đã được xác nhận thành công.");
        }

        private string BuildConfirmationEmailBody(string username, string confirmLink)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <body style='font-family: Arial, sans-serif;'>
                <h3>Xin chào {username},</h3>
                <p>Vui lòng nhấn vào nút dưới đây để xác nhận email của bạn:</p>
                <p><a href='{confirmLink}' style='
                    display: inline-block;
                    padding: 10px 20px;
                    background-color: #007bff;
                    color: white;
                    text-decoration: none;
                    border-radius: 5px;'>Xác nhận Email</a>
                </p>
                <p>Nếu nút không hoạt động, bạn có thể sao chép và dán liên kết sau vào trình duyệt:</p>
                <p>{confirmLink}</p>
                <p><strong>Việt Travel Team</strong></p>
                </body>
                </html>";
        }

    }
  }
