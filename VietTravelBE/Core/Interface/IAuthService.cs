using VietTravelBE.Dtos;
using VietTravelBE.Errors;

namespace VietTravelBE.Core.Interface
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto);
        Task RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        Task<UserDto> GetCurrentUserAsync();
        Task<bool> DeleteUserAsync(string userId);
        Task LogoutAsync(string userId);
        Task<(bool Success, string ErrorMessage)> ConfirmEmailAsync(string userId, string token);
    }
}
