using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new { Errors = result.Errors });

            await _userManager.AddToRoleAsync(user, "User");

            return Ok(new { Message = "Đăng ký thành công" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new { Message = "Email không tồn tại" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new { Message = "Mật khẩu không đúng" });

            var accessToken = await _tokenService.CreateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto model)
        {
            try
            {
                var (accessToken, refreshToken) = await _tokenService.RefreshToken(model.AccessToken, model.RefreshToken);

                return Ok(new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
    }
}
