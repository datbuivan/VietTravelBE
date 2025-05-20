using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _context;
        public ImageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Image> GetPrimaryImageByEntityIdAsync(int entityId, ImageType imageType)
        {
            var image =  await _context.Images
                .Where(i => i.EntityId == entityId && i.ImageType == imageType && i.IsPrimary)
                .FirstOrDefaultAsync();
            if (image == null)
            {
                throw new ApiException(404, "Primary image not found");
            }
            return image;
        }

        public async Task DeleteAsync(Image image)
        {
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
