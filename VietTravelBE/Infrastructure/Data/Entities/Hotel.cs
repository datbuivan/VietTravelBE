using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("hotel")]
    public class Hotel : BaseEntity
    {
       
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [StringLength(2000)]
        public string TitleIntroduct { get; set; }
        [StringLength(2000)]
        public string ContentIntroduct { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<Room> Rooms { get; set; } 
        public ICollection<Review> Reviews { get; set; } 
        //public ICollection<Tour> Tours { get; set; } = new List<Tour>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        [NotMapped]
        public ICollection<Image> Images { get; set; }
    }
}
