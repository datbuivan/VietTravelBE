using VietTravelBE.Core.Interface;

namespace VietTravelBE.Infrastructure.Services
{
    public class FileValidationService : IFileValidationService
    {
        private readonly long _maxFileSize = 5 * 1024 * 1024; 
        private readonly string[] _validExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public bool ValidateFile(IFormFile file, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (file == null)
            {
                errorMessage = "No file uploaded.";
                return false;
            }

            if (file.Length > _maxFileSize)
            {
                errorMessage = $"File size exceeds the maximum limit of {_maxFileSize / (1024 * 1024)} MB.";
                return false;
            }

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!_validExtensions.Contains(extension))
            {
                errorMessage = $"Invalid file type. Only the following file types are allowed: {string.Join(", ", _validExtensions)}";
                return false;
            }

            var contentType = file.ContentType.ToLower();
            if (!contentType.StartsWith("image/"))
            {
                errorMessage = "Only image files are allowed.";
                return false;
            }
            return true;
        }
    }
}
