using CloudinaryDotNet.Actions;
using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class HotelImageService: IHotelImageService
    {
        private readonly IGenericRepository<Image> _repoImage;
        private readonly ICloudinaryService _cloudinary;
        private readonly IUnitOfWork _unit;
        public HotelImageService(IGenericRepository<Image> repoImage, ICloudinaryService cloudinary, IUnitOfWork unit)
        {
            _repoImage = repoImage;
            _cloudinary = cloudinary;
            _unit = unit;
        }

        public async Task<Image> AddPrimaryImageAsync(Hotel hotel, IFormFile primaryImage)
        {
            if (primaryImage == null || primaryImage.Length == 0)
                throw new ArgumentException("Primary image is required.");

            var uploadResult = await _cloudinary.UploadImageAsync(primaryImage, "hotels", hotel.Id);

            var image = new Image
            {
                Url = uploadResult.Url,
                EntityId = hotel.Id,
                ImageType = ImageType.Hotel,
                IsPrimary = true,
                PublicId = uploadResult.PublicId,
            };

            _repoImage.Add(image);
            await _unit.Complete();

            return image;
        }

        public async Task<List<Image>> AddImagesAsync(Hotel hotel, List<IFormFile> images)
        {
            if (hotel == null)
                throw new ArgumentNullException(nameof(hotel));
            if (images == null || images.Count == 0)
                return new List<Image>();

            var uploadedImages = new List<Image>();
            bool isFirstImage = true;
            foreach (var file in images)
            {
                var uploadResult = await _cloudinary.UploadImageAsync(file, "hotels", hotel.Id);

                if (uploadResult == null || string.IsNullOrEmpty(uploadResult.Url))
                    throw new InvalidOperationException($"Failed to upload image to Cloudinary for hotel {hotel.Id}");
                var image = new Image
                {
                    Url = uploadResult.Url,
                    EntityId = hotel.Id,
                    ImageType = ImageType.Hotel,
                    IsPrimary = isFirstImage,
                    PublicId = uploadResult.PublicId,

                };
                uploadedImages.Add(image);
                isFirstImage = false;
            }

            _repoImage.AddRange(uploadedImages);

            await _unit.Complete();
            return uploadedImages;

        }

        public async Task DeleteImageAsync(string publicId)
        {
             await _cloudinary.DeleteImageAsync(publicId);
        }


    }
}
