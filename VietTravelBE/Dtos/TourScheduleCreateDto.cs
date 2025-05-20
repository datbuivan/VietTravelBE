namespace VietTravelBE.Dtos
{
    public class TourScheduleCreateDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int DayNumber { get; set; }
        public int TourId { get; set; }
    }
}
