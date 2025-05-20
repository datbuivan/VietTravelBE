namespace VietTravelBE.Core.Interface
{
    public interface IFileValidationService
    {
        bool ValidateFile(IFormFile file, out string errorMessage);
    }
}
