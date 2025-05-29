using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<Payment> GetByBookingIdAsync(int id);
    }
}
