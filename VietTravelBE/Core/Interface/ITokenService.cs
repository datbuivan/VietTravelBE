using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
        string GenerateRefreshToken();
        Task<(string accessToken, string refreshToken)> RefreshToken(string expiredToken, string refreshToken);
    }
}
    