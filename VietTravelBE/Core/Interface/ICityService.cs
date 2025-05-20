using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface ICityService
    {
        Task<IReadOnlyList<CityDto>> GetCities();
        Task<CityDto> GetById(int id);
        Task<CityDto> CreateCity(CityCreateDto dto);
        Task<CityDto> UpdateCity(int Id ,CityCreateDto dto);

        Task<IReadOnlyList<CityDto>> GetCitiesByRegionAsync(int regionId);
    }
}
