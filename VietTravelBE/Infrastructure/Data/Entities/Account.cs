using System.ComponentModel.DataAnnotations;
using VietTravelBE.Infrastructure.Data.Entities.Custom;
namespace VietTravelBE.Infrastructure.Data.Entities
{
    public class Account : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(25)]
        public string Password { get; set; }
    }
}
