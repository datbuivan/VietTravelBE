using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Dtos
{
    public class RoomDto: BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }   // Tên phòng
        [Precision(18, 2)]
        public decimal Price { get; set; }   
        public int? Superficies { get; set; }    
        public int TypeRoom { get; set; }
        public bool FreeWifi { get; set; }  
        public bool BreakFast { get; set; }    
        public bool FreeBreakFast { get; set; } 
        public bool DrinkWater { get; set; }  
        public bool CoffeeAndTea { get; set; }  
        public bool Park { get; set; } 
        public int HotelId { get; set; }
    }
}
