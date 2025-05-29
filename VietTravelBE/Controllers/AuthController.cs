using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);

            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            await _authService.RegisterAsync(registerDto);
            return Ok(new ApiResponse<string>(200, "Registration successful, please confirm your email"));

        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.RefreshToken))
                {
                    return Unauthorized(new ApiResponse<string>(401, "Refresh token is required"));
                }

                var token = await _authService.RefreshTokenAsync(model.RefreshToken);
                if (token == null)
                {
                    return Unauthorized(new ApiResponse<AuthResponseDto>(401, "Invalid refresh token attempt."));
                }
                return Ok(new ApiResponse<AuthResponseDto>(200, "Refresh token successful.", token));
            }
            catch(SecurityTokenException ex)
            {
                return Unauthorized(new ApiResponse<AuthResponseDto>(401, ex.Message));
            }
        }

        [Authorize]
        [HttpGet("current-user")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _authService.GetCurrentUserAsync();
            return Ok(new ApiResponse<UserDto>(200, "Get Current User successfull", user));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _authService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse<string>(404, "User not found or delete failed."));
            }

            return Ok(new ApiResponse<string>(200, "User deleted successfully."));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var (success, errorMessage) = await _authService.ConfirmEmailAsync(userId, token);
            if (!success)
            {
                var errorUrl = "http://localhost:4200/verify-email?success=false&message=" + Uri.EscapeDataString("Xác nhận thất bại.");
                return Redirect(errorUrl);
            }
            var frontendUrl = "http://localhost:4200/verify-email?success=true&userId=" + userId;
            return Redirect(frontendUrl);
            
        }

    }
}
