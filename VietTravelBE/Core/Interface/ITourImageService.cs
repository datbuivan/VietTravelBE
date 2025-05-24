using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface ITourImageService 
    {
        Task<Image> AddPrimaryImageAsync(Tour tour, IFormFile primaryImage);
        Task<List<Image>> AddImagesAsync(Tour tour, List<IFormFile> images);
        Task DeleteImageAsync(string publicId);
    }
}
