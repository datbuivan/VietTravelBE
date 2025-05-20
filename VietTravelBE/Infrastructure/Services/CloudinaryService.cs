using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;

namespace VietTravelBE.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;


        public CloudinaryService(IConfiguration config)
        {
            var cloudName = config["Cloudinary:CloudName"];
            var apiKey = config["Cloudinary:ApiKey"];
            var apiSecret = config["Cloudinary:ApiSecret"];

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResultDto> UploadImageAsync(IFormFile file, string entityType, int entityId)
        {
            if (file.Length <= 0) throw new ArgumentException("File is empty"); 
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = $"{entityType}/{entityId}",
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,

                Transformation = new Transformation()
                    .Width(1280)
                    .Height(720)
                    .Crop("fill")    // Crop vừa khít (bạn có thể đổi thành "limit" để giữ nguyên tỉ lệ)
                    .Quality("auto:good")  // ✅ Nén ảnh tự động theo chất lượng tốt nhất (auto compress)
                    .FetchFormat("auto")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ImageUploadResultDto
                {
                    Url = uploadResult.SecureUrl.AbsoluteUri,
                    PublicId = uploadResult.PublicId
                };
            }

            throw new Exception("Image upload failed.");
        }

        public async Task DeleteImageAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            if (deletionResult.Result != "ok")
            {
                throw new Exception("Failed to delete image from Cloudinary");
            }
        }
    }
}
