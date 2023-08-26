using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using yado_backend.Data;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly AppDbContext _dbContext;

        public ImageRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Image>> GetAllImagesByHotelId(Guid hotelId)
        {
            return await _dbContext.Images
                .Where(img => img.HotelId == hotelId)
                .OrderBy(img => img.Position)
                .ToListAsync();
        }

        public async Task<bool> InsertImage(Image image)
        {
            _dbContext.Images.Add(image);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateImagePositions(IEnumerable<Image> images)
        {
            foreach (var image in images)
            {
                var existingImage = await _dbContext.Images.FindAsync(image.Id);
                if (existingImage != null)
                {
                    existingImage.Position = image.Position;
                }
            }

            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteImageById(int imageId)
        {
            var image = await _dbContext.Images.FindAsync(imageId);
            if (image != null)
            {
                _dbContext.Images.Remove(image);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}
