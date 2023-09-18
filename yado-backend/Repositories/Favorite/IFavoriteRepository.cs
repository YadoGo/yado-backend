using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Repositories
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Favorite>> GetAllFavoritesByUserId(Guid userId);
        Task<IEnumerable<Favorite>> GetAllFavoritesByHotelId(Guid hotelId);
        Task<bool> AddFavoriteAsync(Favorite favorite);
        Task<bool> RemoveFavoriteAsync(Favorite favorite);
        Task<bool> FavoriteExistsAsync(Guid userId, Guid hotelId);
        Task<int> GetFavoriteCountByUserId(Guid userId);
        Task<int> GetFavoriteCountByHotelId(Guid hotelId);
    }
}
