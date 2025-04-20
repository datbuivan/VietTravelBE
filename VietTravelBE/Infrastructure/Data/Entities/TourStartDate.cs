using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("tourstartdate")]
    public class TourStartDate: BaseEntity
    {
        public int AvailableSlots { get; set; }
        public DateTime StartDate { get; set; } // Ngày khởi hành
        public int TourId { get; set; }
        public Tour Tour { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
