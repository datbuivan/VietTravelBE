using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("schedule")]
    public class Schedule : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string? Content { get; set; }
        public int? TicketEnable { get; set; }
        [Precision(18, 2)]
        public decimal? PriceTicketKid { get; set; }
        [Precision(18, 2)]
        public decimal? PriceTicketAdult { get; set; }
        public string? Pictures { get; set; }
        public int TourId { get; set; }
        public Tour? Tour { get; set; }
        public ICollection<ScheduleTourPackage>? ScheduleTourPackages { get; set; } = new List<ScheduleTourPackage>();
    }
}
