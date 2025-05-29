using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Specifications
{
    public class TourWithFiltersForCountSpecificication : BaseSpecification<Tour>
    {
        public TourWithFiltersForCountSpecificication(TourSpecParams tourParams)
            :base(x =>
                (string.IsNullOrEmpty(tourParams.Search) || x.Name.ToLower().Contains(tourParams.Search)) &&
                (!tourParams.CityId.HasValue || x.CityId == tourParams.CityId) &&
                (!tourParams.MinPrice.HasValue || x.Price >= tourParams.MinPrice) &&
                (!tourParams.MaxPrice.HasValue || x.Price <= tourParams.MaxPrice) &&
                (!tourParams.StartDate.HasValue || x.TourStartDates.Any(d => d.StartDate >= tourParams.StartDate))
            )
        { }
    }
}
