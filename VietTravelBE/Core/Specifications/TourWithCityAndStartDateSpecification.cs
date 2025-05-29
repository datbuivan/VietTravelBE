using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Specifications
{
    public class TourWithCityAndStartDateSpecification : BaseSpecification<Tour>
    {
        public TourWithCityAndStartDateSpecification(TourSpecParams tourParams):
        base(x => 
            (string.IsNullOrEmpty(tourParams.Search) || x.Name.ToLower().Contains(tourParams.Search)) &&
            (!tourParams.CityId.HasValue || x.CityId == tourParams.CityId) &&
            (!tourParams.MinPrice.HasValue || x.Price >= tourParams.MinPrice) &&
            (!tourParams.MaxPrice.HasValue || x.Price <= tourParams.MaxPrice) &&
            (!tourParams.StartDate.HasValue || x.TourStartDates.Any(d => d.StartDate >= tourParams.StartDate))
        )
        {
            AddInclude(x => x.City);
            AddInclude(x => x.TourStartDates);
            AddOrderBy(x => x.Name);
            //ApplyPaging(tourParams.PageSize * (tourParams.PageIndex - 1), tourParams.PageSize);

            if (!string.IsNullOrEmpty(tourParams.Sort))
            {
                switch (tourParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public TourWithCityAndStartDateSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.City); 
            AddInclude(x => x.TourStartDates);  
        }
    }
}
