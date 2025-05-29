using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class TourRepository : GenericRepository<Tour>, ITourRepository
    {
        private readonly DataContext _context;
        public TourRepository(DataContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<bool> TourExistsAsync(int id)
        {
           return await _context.Set<Tour>().AnyAsync(t => t.Id == id);
        }
    }
}
