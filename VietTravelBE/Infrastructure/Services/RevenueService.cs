using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class RevenueService : IRevenueService
    {

        private readonly IRevenueRepository _revenueRepository;

        public RevenueService(IRevenueRepository revenueRepository)
        {
            _revenueRepository = revenueRepository ;
        }

        public async Task<List<RevenueReportDto>> GetAllRevenue()
        {
            var revenueData = await _revenueRepository.GetRawRevenueDataAsync();
            return BuildRevenueReports(revenueData);
        }

        public async Task<RevenueReportDto> GetRevenueByYear(int year)
        {
            var revenueData = await _revenueRepository.GetRawRevenueDataAsync();
            var groupedData = revenueData
                .Where(r => r.Year == year)
                .GroupBy(r => new { r.Year, r.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TourRevenue = g.Where(x => x.BookingType == BookingType.Tour).Sum(x => x.Amount),
                    HotelRevenue = g.Where(x => x.BookingType == BookingType.Hotel).Sum(x => x.Amount)
                })
                .GroupBy(r => r.Year)
                .Select(g => new RevenueReportDto
                {
                    Year = g.Key,
                    MonthlyRevenues = g.Select(m => new MonthlyRevenueDto
                    {
                        Month = m.Month,
                        TourRevenue = m.TourRevenue,
                        HotelRevenue = m.HotelRevenue,
                        TotalRevenue = m.TourRevenue + m.HotelRevenue
                    }).OrderBy(m => m.Month).ToList()
                })
               .FirstOrDefault();

            if (groupedData == null)
            {
                return new RevenueReportDto
                {
                    Year = year,
                    MonthlyRevenues = Enumerable.Range(1, 12).Select(m => new MonthlyRevenueDto
                    {
                        Month = m,
                        TourRevenue = 0,
                        HotelRevenue = 0,
                        TotalRevenue = 0
                    }).ToList()
                };
            }

            var monthlyRevenues = groupedData.MonthlyRevenues;

            for (int month = 1; month <= 12; month++)
            {
                if (!monthlyRevenues.Any(m => m.Month == month))
                {
                    monthlyRevenues.Add(new MonthlyRevenueDto
                    {
                        Month = month,
                        TourRevenue = 0,
                        HotelRevenue = 0,
                        TotalRevenue = 0
                    });
                }
            }

            groupedData.MonthlyRevenues = monthlyRevenues.OrderBy(m => m.Month).ToList();

            return groupedData;

        }




        private List<RevenueReportDto> BuildRevenueReports(List<RawRevenueDto> revenueData)
        {
            var groupedData = revenueData
                .GroupBy(r => new { r.Year, r.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TourRevenue = g.Where(x => x.BookingType == BookingType.Tour).Sum(x => x.Amount),
                    HotelRevenue = g.Where(x => x.BookingType == BookingType.Hotel).Sum(x => x.Amount)
                })
                .GroupBy(r => r.Year)
                .Select(g => new RevenueReportDto
                {
                    Year = g.Key,
                    MonthlyRevenues = g.Select(m => new MonthlyRevenueDto
                    {
                        Month = m.Month,
                        TourRevenue = m.TourRevenue,
                        HotelRevenue = m.HotelRevenue,
                        TotalRevenue = m.TourRevenue + m.HotelRevenue
                    }).OrderBy(m => m.Month).ToList()
                })
                .OrderBy(r => r.Year)
                .ToList();

            // Add đủ 12 tháng
            var result = new List<RevenueReportDto>();
            var years = groupedData.Select(r => r.Year).Distinct().OrderBy(y => y).ToList();

            foreach (var year in years)
            {
                var yearData = groupedData.FirstOrDefault(r => r.Year == year)
                    ?? new RevenueReportDto { Year = year, MonthlyRevenues = new List<MonthlyRevenueDto>() };

                var monthlyRevenues = yearData.MonthlyRevenues.ToList();

                
                for (int month = 1; month <= 12; month++)
                {
                    if (!monthlyRevenues.Any(m => m.Month == month))
                    {
                        monthlyRevenues.Add(new MonthlyRevenueDto
                        {
                            Month = month,
                            TourRevenue = 0,
                            HotelRevenue = 0,
                            TotalRevenue = 0
                        });
                    }
                }

                yearData.MonthlyRevenues = monthlyRevenues.OrderBy(m => m.Month).ToList();
                result.Add(yearData);
            }

            return result.OrderBy(r => r.Year).ToList();
        }
    }
}
