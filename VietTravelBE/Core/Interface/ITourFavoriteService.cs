using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface ITourFavoriteService
    {
        Task<List<TourFavorite>> GetFavoritesAsync(string userId);
        Task<TourFavorite> AddFavoriteAsync(string userId, int tourId);
        Task SyncFavoritesAsync(string userId, List<TourFavoriteDto> favorites);
        Task RemoveFavoriteAsync(string userId, int tourId);
    }
}
