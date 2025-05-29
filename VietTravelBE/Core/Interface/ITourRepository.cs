using Microsoft.EntityFrameworkCore;

namespace VietTravelBE.Core.Interface
{
    public interface ITourRepository
    {
        Task<bool> TourExistsAsync(int id);

    }
}
