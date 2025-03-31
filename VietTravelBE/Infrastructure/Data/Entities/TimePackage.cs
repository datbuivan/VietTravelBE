using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("timepackage")]
    public class TimePackage : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int HourNumber { get; set; }
        public ICollection<TourPackage>? TourPackages { get; set; } = new List<TourPackage>();
    }
}
