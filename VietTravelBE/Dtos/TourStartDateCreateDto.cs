namespace VietTravelBE.Dtos
{
    public class TourStartDateCreateDto
    {
        public int AvailableSlots { get; set; }
        public DateTime StartDate { get; set; }
        public int TourId { get; set; }
    }
}
