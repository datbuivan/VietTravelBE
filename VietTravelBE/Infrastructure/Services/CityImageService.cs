using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Services
{
    public class CityImageService : ICityImageService
    {
        private readonly IGenericRepository<Image> _repoImage;
        private readonly ICloudinaryService _cloudinary;
        private readonly IUnitOfWork _unit;
        public CityImageService(IGenericRepository<Image> repoImage, ICloudinaryService cloudinary, IUnitOfWork unit)
        {
            _repoImage = repoImage;
            _cloudinary = cloudinary;
            _unit = unit;
        }

        public async Task<Image> AddPrimaryImageAsync(City city, IFormFile primaryImage)
        {
            if (primaryImage == null || primaryImage.Length == 0)
                throw new ArgumentException("Primary image is required.");

            var uploadResult = await _cloudinary.UploadImageAsync(primaryImage, "cities", city.Id);

            if (uploadResult == null || string.IsNullOrEmpty(uploadResult.Url))
                throw new InvalidOperationException("Failed to upload image to Cloudinary.");

            var image = new Image
            {
                Url = uploadResult.Url,
                EntityId = city.Id,
                ImageType = ImageType.City,
                IsPrimary = true,
                PublicId = uploadResult.PublicId,
            };

            _repoImage.Add(image);
            await _unit.Complete();

            return image;
        }

        public async Task DeleteImageAsync(string publicId)
        {
            await _cloudinary.DeleteImageAsync(publicId);
        }
    }
}
