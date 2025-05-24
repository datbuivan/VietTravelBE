using VietTravelBE.Infrastructure;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface IImageRepository : IGenericRepository<Image> 
    {
        Task<Image> GetPrimaryImageByEntityIdAsync(int entityId, ImageType imageType);
        Task<List<Image>> GetImagesByEntityIdAsync(int entityId, ImageType imageType);
        Task DeleteAsync(Image image);
    }
}
