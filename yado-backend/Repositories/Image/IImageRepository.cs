using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllImagesByHotelUuid(string hotelUuid);
        Task<bool> InsertImage(Image image);
        Task<bool> UpdateImagePositions(IEnumerable<Image> images);
        Task<bool> DeleteImageById(int imageId);
    }
}
