using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Dtos
{
    public class BookingAndPayCreateDto
    {
        [Required]
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }

        [Required]
        public BookingType Type { get; set; }

        [Required]
        public string UserId { get; set; }

        public int Adults { get; set; }

        public int Children { get; set; }

        public int? HotelId { get; set; }
        public DateTime? HotelCheckInDate { get; set; }
        public DateTime? HotelCheckOutDate { get; set; }

        public int? TourId { get; set; }
        public int? TourStartDateId { get; set; }
    }
}
