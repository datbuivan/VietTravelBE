using Microsoft.EntityFrameworkCore;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        Task<bool> HotelExistsAsync(int id);
    }
}
