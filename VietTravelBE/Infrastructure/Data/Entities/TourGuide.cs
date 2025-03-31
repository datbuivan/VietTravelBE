using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("tourguide")]
    public class TourGuide : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string Sex { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        [Required]
        public int CityId { get; set; }
        public City? City { get; set; }

    }
}
