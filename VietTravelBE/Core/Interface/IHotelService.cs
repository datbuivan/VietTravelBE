using VietTravelBE.Core.Specifications;
using VietTravelBE.Dtos;

namespace VietTravelBE.Core.Interface
{
    public interface IHotelService
    {
        //Task<IReadOnlyList<HotelDto>> GetHotelsWithParam(SpecParams? specParams);
        Task<IReadOnlyList<HotelDto>> GetHotels();
        Task<HotelDto> CreateHotel(HotelCreateDto tourCreateDto);
        Task<HotelDto> UpdateHotel(int id, HotelCreateDto dto);
    }
}
