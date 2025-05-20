using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class TourImageService : ITourImageService
    {
        private readonly IGenericRepository<Image> _repoImage;
        private readonly ICloudinaryService _cloudinary;
        private readonly IUnitOfWork _unit;

        public TourImageService(IGenericRepository<Image> repoImage,ICloudinaryService cloudinary, IUnitOfWork unit)
        {
            _repoImage = repoImage;
            _cloudinary = cloudinary;
            _unit = unit;
        }

        public async Task<Image> AddPrimaryImageAsync(Tour tour, IFormFile primaryImage)
        {
            if (primaryImage == null || primaryImage.Length == 0)
                throw new ArgumentException("Primary image is required.");

            var uploadResult = await _cloudinary.UploadImageAsync(primaryImage, "tours", tour.Id);

            var image = new Image
            {
                Url = uploadResult.Url,
                EntityId = tour.Id,
                ImageType = ImageType.Tour,
                IsPrimary = true,
                PublicId = uploadResult.PublicId
            };

            _repoImage.Add(image);
            await _unit.Complete();

            return image;
        }

        public async Task<List<Image>> AddImagesAsync(Tour tour, List<IFormFile> images)
        {
            var uploadedImages = new List<Image>();
            if (images == null || images.Count == 0) return uploadedImages;
            foreach (var file in images)
            {
                var uploadResult = await _cloudinary.UploadImageAsync(file, "tours", tour.Id);
                var image = new Image
                {
                    Url = uploadResult.Url,
                    EntityId = tour.Id,
                    ImageType = ImageType.Tour,
                    IsPrimary = false,
                    PublicId = uploadResult.PublicId

                };           
                

                uploadedImages.Add(image);
            }

            _repoImage.AddRange(uploadedImages);

            await _unit.Complete();
            return uploadedImages;

        }
    }
}
