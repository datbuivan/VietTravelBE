using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface IBookingRepository: IGenericRepository<Booking>
    {
        Task<List<Booking>> GetByUserIdAsync(string userId);
        Task<List<Booking>> GetByTypeAsync(BookingType type);
    }
}
