using VietTravelBE.Dtos;

namespace VietTravelBE.Core.Interface
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResultDto> UploadImageAsync(IFormFile file, string entityType, int entityId);
        Task DeleteImageAsync(string publicId);
    }
}
