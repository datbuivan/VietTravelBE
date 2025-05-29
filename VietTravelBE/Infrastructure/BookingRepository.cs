using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        private readonly DataContext _context;
        public BookingRepository(DataContext context): base(context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetByUserIdAsync(string userId)
        {
            return await _context.Set<Booking>()
                .Where(b => b.UserId == userId)
                .Include(b => b.User)
                .Include(b => b.Hotel)
                .Include(b => b.Tour)
                .Include(b => b.TourStartDate)
                .Include(b => b.Payment)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByTypeAsync(BookingType type)
        {
            return await _context.Set<Booking>()
                .Where(b => b.Type == type)
                .Include(b => b.User)
                .Include(b => b.Hotel)
                .Include(b => b.Tour)
                .Include(b => b.TourStartDate)
                .Include(b => b.Payment)
                .ToListAsync();
        }
    }
}
