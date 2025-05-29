using VietTravelBE.Dtos;

namespace VietTravelBE.Core.Interface
{
    public interface IBookingService
    {
        Task<BookingDto> CreateBookingAsync(BookingAndPayCreateDto request);
    }
}
