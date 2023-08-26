using System.Collections.Generic;
using System.Threading.Tasks;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IHotelRepository
    {
        Task<Hotel> GetHotelById(Guid id);
        Task<IEnumerable<Hotel>> GetAllHotelsByUserId(Guid userId);
        Task<IEnumerable<Hotel>> GetAllTopHotelsReview();
        Task<IEnumerable<Hotel>> GetAllHotelsByPopulationId(int populationId, int page, int pageSize);
        Task<IEnumerable<Hotel>> GetHotelsByParameters(int populationId, Parameter parameters);
        Task<bool> InsertHotel(Hotel hotel);
        Task<bool> UpdateHotelById(Guid id, Hotel hotel);
        Task<bool> DeleteHotelById(Guid id);
    }
}
