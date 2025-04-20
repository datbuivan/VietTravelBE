using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("tourschedule")]
    public class TourSchedule : BaseEntity
    {
        public int DayNumber { get; set; }       // Ngày thứ mấy (1, 2, 3,...)
        public string Title { get; set; }        // Tiêu đề ngày: "Tham quan Vịnh Hạ Long"
        public string Description { get; set; }  // Mô tả chi tiết
        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
