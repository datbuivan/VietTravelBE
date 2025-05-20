using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Specifications
{
    public class TourScheduleByTourIdSpecification : BaseSpecification<TourSchedule>
    {
        public TourScheduleByTourIdSpecification(int tourId)
        : base(s => s.TourId == tourId)
        {
            AddOrderBy(s => s.DayNumber);
        }
    }
}
