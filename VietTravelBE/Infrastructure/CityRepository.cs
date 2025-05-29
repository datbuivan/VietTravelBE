using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly DataContext _context;
        public CityRepository(DataContext context): base(context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<City>> GetCitiesByRegionIdAsync(int regionId)
        {
            return await _context.Set<City>().Where(c => c.RegionId == regionId)
                                   .ToListAsync();
        }

    }
}
