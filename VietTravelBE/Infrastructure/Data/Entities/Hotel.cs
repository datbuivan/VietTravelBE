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
        [Precision(18, 2)]
        public decimal PriceOneNight { get; set; }
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [StringLength(2000)]
        public string TitleIntroduct { get; set; }
        [StringLength(2000)]
        public string ContentIntroduct { get; set; }
        public string Pictures { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
        public ICollection<Room>? Rooms { get; set; } = new List<Room>();
        public ICollection<Evaluate>? Evaluates { get; set; } = new List<Evaluate>();
        public ICollection<TourPackage> TourPackages { get; set; } = new List<TourPackage>();


    }
}
