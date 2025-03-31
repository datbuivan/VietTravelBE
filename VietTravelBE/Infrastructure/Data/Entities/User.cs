using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("user")]
    public class User : Account
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [StringLength(10)]
        public string Sex { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string Role { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public string? UniCodeName { get; set; }
        public ICollection<Evaluate>? Evaluates { get; set; } = new List<Evaluate>();
    }
}
