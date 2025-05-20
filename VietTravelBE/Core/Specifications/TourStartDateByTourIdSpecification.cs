using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Specifications
{
    public class TourStartDateByTourIdSpecification : BaseSpecification<TourStartDate>
    {
        public TourStartDateByTourIdSpecification(int tourId)
        :base(s => s.TourId == tourId)
        {
            AddOrderBy(s => s.StartDate);
        }
    }
}
