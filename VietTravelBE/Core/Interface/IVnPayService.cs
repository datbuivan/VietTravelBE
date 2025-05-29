
namespace VietTravelBE.Core.Interface
{
    public interface IVnPayService 
    {
        Task<string> CreateVNPayPaymentUrlAsync(int bookingId);
        Task<(bool Success, string Message, object Data)> ProcessVnPayReturnAsync(IQueryCollection query);
        //Task<bool> HandleVNPayCallbackAsync(IQueryCollection query);
    }
}
