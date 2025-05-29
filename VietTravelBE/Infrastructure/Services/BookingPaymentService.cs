using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class BookingPaymentService : IBookingPaymentService
    {
        private readonly IBookingService _bookingService;
        private readonly IVnPayService _vpnPayService;
        private readonly IUnitOfWork _unit;

        public BookingPaymentService(IBookingService bookingService, IVnPayService vpnPayService, IUnitOfWork unit)
        {
            _bookingService = bookingService;
            _vpnPayService = vpnPayService;
            _unit = unit;
        }

        public async Task<BookingPaymentResponseDto> CreateBookingAndPayAsync(BookingAndPayCreateDto request)
        {
            using (var transaction = await _unit.BeginTransactionAsync())
            {
                try
                {
                    var bookingDto = await _bookingService.CreateBookingAsync(request);
                    var paymentUrl = await _vpnPayService.CreateVNPayPaymentUrlAsync(bookingDto.Id);

                    await transaction.CommitAsync();
                    return new BookingPaymentResponseDto
                    {
                        PaymentUrl = paymentUrl,
                        BookingId = bookingDto.Id
                    };
                }
                catch (Exception )
                {
                    await transaction.RollbackAsync();
                    throw ;
                }
            }
        }

        public async Task<(bool Success, string Message, object Data)> ProcessCallbackAsync(IQueryCollection query)
        {
             return await _vpnPayService.ProcessVnPayReturnAsync(query);
        }
    }
}
