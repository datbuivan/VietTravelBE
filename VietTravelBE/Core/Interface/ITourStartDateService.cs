using VietTravelBE.Dtos;

namespace VietTravelBE.Core.Interface
{
    public interface ITourStartDateService
    {
        Task<TourStartDateDto> CreateAsync(TourStartDateCreateDto dto);
        Task<IReadOnlyList<TourStartDateDto>> GetByTourIdAsync(int tourId);
        Task<List<TourStartDateDto>> CreateMultipleAsync(List<TourStartDateCreateDto> dtos);
    }
}
