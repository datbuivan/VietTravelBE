using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Core.Interface
{
    public interface ICityImageService
    {
        Task<Image> AddPrimaryImageAsync(City city, IFormFile primaryImage);
        Task DeleteImageAsync(string publicId);
    }
}
