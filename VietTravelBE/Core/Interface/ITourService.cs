using VietTravelBE.Dtos;

namespace VietTravelBE.Core.Interface
{
    public interface ITourService
    {
        Task<IReadOnlyList<TourDto>> GetTours();
        Task<TourDto> GetById(int id);
        Task<TourDto> CreateTour(TourCreateDto tourCreateDto);
        Task<TourDto> UpdateTour(int id, TourCreateDto dto);
    }
}
