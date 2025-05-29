using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class HotelRepository: GenericRepository<Hotel>, IHotelRepository
    {
        private readonly DataContext _context;
        public HotelRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> HotelExistsAsync(int id)
        {
            return await _context.Set<Hotel>().AnyAsync(h => h.Id == id);
        }
    }
}
