using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Dtos
{
    public class HotelDto: BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [Precision(18, 2)]
        public decimal PriceOneNight { get; set; }
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [StringLength(2000)]
        public string TitleIntroduct { get; set; }
        [StringLength(2000)]
        public string ContentIntroduct { get; set; }
        public string Pictures { get; set; }
        public int CityId { get; set; }
    }
}
