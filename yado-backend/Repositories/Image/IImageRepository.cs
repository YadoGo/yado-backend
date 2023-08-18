using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IImageRepository
    {
        Task<bool> InsertImage(Image image);
        Task<IEnumerable<Image>> GetAllImagesByHotelUuid(string hotelUuid);
        Task<bool> DeleteImageById(int imageId);
    }
}
