using VietTravelBE.Dtos;

namespace VietTravelBE.Core.Interface
{
    public interface ITourScheduleService
    {
        Task<TourScheduleDto> CreateAsync(TourScheduleCreateDto tourScheduleCreateDto);
        Task<IReadOnlyList<TourScheduleDto>> GetByTourIdAsync(int tourId);
        Task<List<TourScheduleDto>> CreateMultipleAsync(List<TourScheduleCreateDto> dtos);

    }
}
