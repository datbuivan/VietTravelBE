namespace VietTravelBE.Dtos
{
    public class RevenueReportDto
    {
        public int Year { get; set; }
        public List<MonthlyRevenueDto> MonthlyRevenues { get; set; }
    }
}
