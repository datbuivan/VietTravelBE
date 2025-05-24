using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface IScheduleRepository : IGenericRepository<TourSchedule>
    {
        Task<IReadOnlyList<TourSchedule>> GetScheduleByTourId(int Id);
    }
}
