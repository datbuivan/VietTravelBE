using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("tourfavorite")]
    public class TourFavorite: BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int TourId { get; set; }
        public Tour Tour { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
