using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class TourFavoriteRepository : GenericRepository<TourFavorite>, ITourFavoriteRepository
    {
        private readonly DataContext _context;
        public TourFavoriteRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TourFavorite>> GetByUserIdAsync(string userId)
        {
            return await _context.Set<TourFavorite>()
                .Include(f => f.Tour)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(string userId, int tourId)
        {
            return await _context.Set<TourFavorite>()
                .AnyAsync(f => f.UserId == userId && f.TourId == tourId);
        }

        public async Task<TourFavorite> GetByUserIdAndTourIdAsync(string userId, int tourId)
        {
            return await _context.Set<TourFavorite>()
                .FirstOrDefaultAsync(f => f.UserId == userId && f.TourId == tourId)
                ?? throw new Exception("TourFavorite not found");
        }
    }
}
