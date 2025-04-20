using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("user")]
    public class AppUser : IdentityUser<int>
    {
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<TourFavorite> TourFavorites { get; set; }
    }
}
