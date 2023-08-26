using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllImagesByHotelId(Guid hotelId);
        Task<bool> InsertImage(Image image);
        Task<bool> UpdateImagePositions(IEnumerable<Image> images);
        Task<bool> DeleteImageById(int imageId);
    }
}
