using VietTravelBE.Dtos;

namespace VietTravelBE.Core.Interface
{
    public interface IBookingPaymentService
    {
        Task<BookingPaymentResponseDto> CreateBookingAndPayAsync(BookingAndPayCreateDto request);
        Task<(bool Success, string Message, object Data)> ProcessCallbackAsync(IQueryCollection query);
    }
}
