using VietTravelBE.Dtos;

namespace VietTravelBE.Core.Interface
{
    public interface IRevenueService
    {
        Task<List<RevenueReportDto>> GetAllRevenue();
        Task<RevenueReportDto> GetRevenueByYear(int year);
    }
}
