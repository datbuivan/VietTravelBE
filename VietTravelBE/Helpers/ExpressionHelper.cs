using System.Linq.Expressions;

namespace VietTravelBE.Helpers
{
    public class ExpressionHelper
    {
        public static Expression<Func<T, object>> GetPropertyExpression<T>(string propertyName)
        {
            var param = Expression.Parameter(typeof(T), "x");
            Expression property = Expression.PropertyOrField(param, propertyName);

            
            UnaryExpression converted = Expression.Convert(property, typeof(object));
         

            return Expression.Lambda<Func<T, object>>(converted, param);
        }
    }
}
