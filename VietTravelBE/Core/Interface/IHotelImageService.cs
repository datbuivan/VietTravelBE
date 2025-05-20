using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Core.Interface
{
    public interface IHotelImageService
    {
        Task<Image> AddPrimaryImageAsync(Hotel hotel, IFormFile primaryImage);
        Task<List<Image>> AddImagesAsync(Hotel hotel, List<IFormFile> images);
    }
}
