using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class ImageRepository : GenericRepository<Image>,IImageRepository
    {
        private readonly DataContext _context;
        public ImageRepository(DataContext context): base(context)
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

        public async Task<List<Image>> GetImagesByEntityIdAsync(int entityId, ImageType imageType)
        {
            var images = await _context.Images
                .Where(i => i.EntityId == entityId && i.ImageType == imageType)
                .ToListAsync();
            if (images == null)
            {
                throw new ApiException(404, "images not found");
            }
            return images;
        }

        public async Task DeleteAsync(Image image)
        {
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
