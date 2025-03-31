using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("room")]
    public class Room : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }   // Tên phòng
        [Precision(18, 2)]
        public decimal PriceOneNight { get; set; }   // Giá một đêm
        public int? Superficies { get; set; }    // Diện tích
        /*
         * Loại phòng
         *      Standard  1
         *      Superior  2
         *      Deluxe    3 
         *      Suite     4
         */
        public int TypeRoom { get; set; }
        public bool FreeWifi { get; set; }  // Wifi miễn phí
        public bool BreakFast { get; set; }    // Bữa sáng
        public bool FreeBreakFast { get; set; } // Miễn phí bữa sáng
        public bool DrinkWater { get; set; }    // Nước uống miễn phí
        public bool CoffeeAndTea { get; set; }  // Cafe và Trà
        public bool Park { get; set; } // Bãi đỗ xe
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
