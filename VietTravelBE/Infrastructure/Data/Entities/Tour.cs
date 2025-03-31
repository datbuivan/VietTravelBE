using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("tour")]
    public class Tour : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        [Required]
        [StringLength(1000)]
        public string? Pictures { get; set; }
        [Required]
        [StringLength(2000)]
        public string? TitleIntroduct { get; set; }
        [Required]
        [StringLength(2000)]
        public string? ContentIntroduct { get; set; }
        public int? NumberOfEvaluate { get; set; }
        public float? MediumStar { get; set; }
        [Precision(18, 2)]
        public decimal? PriceTourGuide { get; set; }
        [Precision(18, 2)]
        public decimal? PriceBase { get; set; }
        [Precision(18, 2)]
        public decimal? TotalPrice { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; } = new City();
        public ICollection<Schedule>? Schedules { get; set; } = new List<Schedule>();
        public ICollection<TourPackage>? TourPackages { get; set; } = new List<TourPackage>();



    }
}
