using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("ticket")]
    public class Ticket : BaseEntity
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public int UserId { get; set; }
        //[Required]
        [JsonIgnore]
        public User? User { get; set; }
        public int TourPackageId { get; set; }
        public TourPackage? TourPackage { get; set; }
    }
}
