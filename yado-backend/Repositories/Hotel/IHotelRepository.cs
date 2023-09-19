using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Repositories
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllTopHotelsReviewAsync();
        Task<Hotel> GetHotelByIdAsync(Guid id);
        Task<IEnumerable<Hotel>> GetAllHotelsByUserIdAsync(Guid userId);
        Task<IEnumerable<HotelSummaryDto>> GetAllHotelsByPopulationIdAsync(int populationId, int page, int pageSize);
        Task<IEnumerable<HotelSummaryDto>> GetHotelsByParametersAsync(ParameterDto parameters, int populationId, int page, int pageSize);
        Task<bool> InsertHotelAsync(Hotel hotel);
        Task<bool> UpdateHotelByIdAsync(Guid id, Hotel updatedHotel);
        Task<bool> DeleteHotelByIdAsync(Guid id);
    }
}
