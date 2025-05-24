using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class ScheduleRepository :GenericRepository<TourSchedule>, IScheduleRepository
    {
        private readonly DataContext _context;
        public ScheduleRepository(DataContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TourSchedule>> GetScheduleByTourId(int id)
        {
            return await _context.Set<TourSchedule>().Where(x => x.TourId == id)
                .ToListAsync();  
        }
    }
}
