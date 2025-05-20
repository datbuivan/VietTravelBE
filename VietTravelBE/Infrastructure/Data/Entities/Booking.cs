using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("booking")]
    public class Booking : BaseEntity
    {
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingType Type { get; set; }
        public int Adults { get; set; }
        public int Children {  get; set; }
        public int TotalPeople { get; set; }

        public int? HotelId { get; set; }
        public Hotel? Hotel {  get; set; }
        public DateTime? HotelCheckInDate { get; set; }
        public DateTime? HotelCheckOutDate { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int? TourId { get; set; }
        public Tour? Tour { get; set; }

        public int? TourStartDateId { get; set; }
        public TourStartDate? TourStartDate { get; set; }

        public Payment Payment { get; set; }
    }
    public enum BookingType
    {
        Tour,
        Hotel
    }
}
