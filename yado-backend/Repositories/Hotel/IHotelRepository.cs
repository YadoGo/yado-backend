using System.Collections.Generic;
using System.Threading.Tasks;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IHotelRepository
    {
        Task<Hotel> GetHotelByUuid(string uuid);
        Task<IEnumerable<Hotel>> GetAllHotelsByOwnerId(string ownerId);
        Task<IEnumerable<Hotel>> GetAllTopHotelsReview();
        Task<IEnumerable<Hotel>> GetAllHotelsByPopulationId(int populationId, int page, int pageSize);
        Task<IEnumerable<Hotel>> GetHotelsByParameters(int populationId, Parameter parameters);
        Task<bool> InsertHotel(Hotel hotel);
        Task<bool> UpdateHotelByUuid(string uuid, Hotel hotel);
        Task<bool> DeleteHotelByUuid(string uuid);
    }
}
