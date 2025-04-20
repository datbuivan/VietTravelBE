using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class CityService
    {
        private readonly ICityRepository _cityRepo;
        public CityService(ICityRepository cityRepo)
        {
            _cityRepo = cityRepo;
        }
        public async Task<IReadOnlyList<City>> GetCitiesByRegionAsync(int regionId)
        {
            return await _cityRepo.GetCitiesByRegionIdAsync(regionId);
        }
    }
}
