using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Dtos
{
    public class TourScheduleDto: BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int DayNumber { get; set; }
        public int TourId { get; set; }
    }
}
