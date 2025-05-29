using VietTravelBE.Dtos;

namespace VietTravelBE.Core.Interface
{
    public interface IRevenueRepository
    {
        Task<List<RawRevenueDto>> GetRawRevenueDataAsync();
    }
}
