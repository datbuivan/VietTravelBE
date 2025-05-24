using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface ITourFavoriteRepository : IGenericRepository<TourFavorite>
    {
        Task<List<TourFavorite>> GetByUserIdAsync(string userId);
        Task<bool> ExistsAsync(string userId, int tourId);
        Task<TourFavorite> GetByUserIdAndTourIdAsync(string userId, int tourId);
    }
}
