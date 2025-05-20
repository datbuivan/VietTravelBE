using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface IImageRepository
    {
        Task<Image> GetPrimaryImageByEntityIdAsync(int entityId, ImageType imageType);
        Task DeleteAsync(Image image);
    }
}
