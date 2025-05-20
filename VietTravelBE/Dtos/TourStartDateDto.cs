using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Dtos
{
    public class TourStartDateDto: BaseEntity
    {
        public int AvailableSlots { get; set; }
        public DateTime StartDate { get; set; }
        public int TourId { get; set; }
    }
}
