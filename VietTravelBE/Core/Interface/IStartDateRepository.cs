using Microsoft.EntityFrameworkCore;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface IStartDateRepository: IGenericRepository<TourStartDate>
    {
        Task<IReadOnlyList<TourStartDate>> GetStartDateByTourId(int id);
        Task<bool> StartDateExistsAsync(int id);
    }
}
