using Microsoft.EntityFrameworkCore;
using VietTravelBE.Helpers;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Specifications
{
    public class Specification<T> : BaseSpecification<T> where T : class
    {
        public Specification(SpecParams specParams) : base(x =>
            (string.IsNullOrEmpty(specParams.Search) ||
             (typeof(T).GetProperty("Name") != null &&
            EF.Functions.Like(EF.Property<string>(x, "Name"), $"%{specParams.Search}%"))) &&
            (!specParams.CityId.HasValue ||
             (typeof(T).GetProperty("CityId") != null && 
             EF.Property<int>(x, "CityId") == specParams.CityId))
        )
        {
            if (typeof(T) == typeof(Hotel))
            {
                AddInclude("Rooms"); 
            }
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            if (typeof(T).GetProperty("Price") != null)
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(ExpressionHelper.GetPropertyExpression<T>("Price"));
                        break;
                    case "priceDesc":
                        AddOrderByDescending(ExpressionHelper.GetPropertyExpression<T>("Price"));
                        break;
                    default:
                        AddOrderBy(ExpressionHelper.GetPropertyExpression<T>("Price"));
                        break;
                }
            }
        }    
    }
}
