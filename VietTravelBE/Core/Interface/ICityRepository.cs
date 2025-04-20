using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<IReadOnlyList<City>> GetCitiesByRegionIdAsync(int regionId);
    }
}
