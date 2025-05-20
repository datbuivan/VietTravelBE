using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("tour")]
    public class Tour : BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        [Precision(18, 2)]
        public decimal ChildPrice { get; set; }
        [Precision(18, 2)]
        public decimal SingleRoomSurcharge { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; } 
        public ICollection<TourSchedule>? TourSchedules { get; set; } = new List<TourSchedule>();
        //public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<TourFavorite> TourFavorites { get; set; }
        public ICollection<TourStartDate> TourStartDates { get; set; } = new List<TourStartDate>();
        [NotMapped]
        public ICollection<Image> Images { get; set; }

    }
}
