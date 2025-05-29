namespace VietTravelBE.Dtos
{
    public class MonthlyRevenueDto
    {
        public int Month { get; set; }
        public decimal TourRevenue { get; set; }
        public decimal HotelRevenue { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
