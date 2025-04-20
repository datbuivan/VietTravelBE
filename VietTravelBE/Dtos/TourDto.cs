using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Dtos
{
    public class TourDto: BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string? Pictures { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        [Precision(18, 2)]
        public decimal YoungPrice { get; set; }
        [Precision(18, 2)]
        public decimal ChildPrice { get; set; }
        [Precision(18, 2)]
        public decimal? SingleRoomSurcharge { get; set; }
        public int CityId { get; set; }
        public List<TourStartDateDto>? TourStartDates { get; set; } 
    }
}
