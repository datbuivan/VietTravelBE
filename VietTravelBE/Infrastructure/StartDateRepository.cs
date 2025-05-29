using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class StartDateRepository : GenericRepository<TourStartDate>, IStartDateRepository
    {
        private readonly DataContext _context;
        public StartDateRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TourStartDate>> GetStartDateByTourId(int id)
        {
           return await _context.Set<TourStartDate>().Where(x => x.TourId == id).ToListAsync();
        }

        public async Task<bool> StartDateExistsAsync(int id)
        {
            return await _context.Set<TourStartDate>().AnyAsync(s => s.Id == id);
        }
    }
}
