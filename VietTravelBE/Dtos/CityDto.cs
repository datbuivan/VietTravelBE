using System.ComponentModel.DataAnnotations;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Dtos
{
    public class CityDto: BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Pictures { get; set; }

        [StringLength(2000)]
        public string TitleIntroduct { get; set; }

        [StringLength(2000)]
        public string ContentIntroduct { get; set; }

        public int RegionId { get; set; }
    }
}
