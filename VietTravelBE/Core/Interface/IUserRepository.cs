using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface IUserRepository
    {
        Task<AppUser> FindByEmailAsync(string email);
        Task<AppUser> FindByRefreshTokenAsync(string refreshToken);
        Task<AppUser> FindByIdAsync(string userId);
        Task UpdateAsync(AppUser user);
        Task<IList<string>> GetRolesAsync(AppUser user);
        Task<bool> UserExistsAsync(string userId);
    }
}
