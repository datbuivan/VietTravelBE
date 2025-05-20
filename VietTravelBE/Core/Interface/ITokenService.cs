using System.Security.Claims;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(AppUser user);
        string GenerateRefreshToken();
        //ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
    