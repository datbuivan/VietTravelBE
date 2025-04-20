using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("city")]
    public class City : BaseEntity
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
        public int? RegionId { get; set; }
        public Region? Region { get; set; }
        public ICollection<Tour>? Tours { get; set; } = new List<Tour>();
        public ICollection<Hotel>? Hotels { get; set; } = new List<Hotel>();

    }
}
