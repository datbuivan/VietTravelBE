using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("review")]
    public class Review : BaseEntity
    {
        
        public ReviewType Type { get; set; }
        public int Rating { get; set; } // 1-5 sao
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? TourId { get; set; }
        public Tour? Tour { get; set; }  
        public int? HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public int UserId { get; set; }
        [NotMapped]
        public AppUser User { get; set; }
    }
    public enum ReviewType
    {
        Tour,
        Hotel
    }
}
