using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("region")]
    public class Region: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
