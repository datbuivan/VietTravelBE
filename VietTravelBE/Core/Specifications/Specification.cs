using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Specifications
{
    public class Specification : BaseSpecification<Hotel>
    {
        public Specification(SpecParams specParams) : base(x =>
        (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
        (!specParams.CityId.HasValue || x.CityId == specParams.CityId)
    )
        {
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            switch (specParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(x => x.PriceOneNight);
                    break;
                case "priceDesc":
                    AddOrderByDescending(x => x.PriceOneNight);
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;
            }
        }

    }
}
