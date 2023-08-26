using System.Collections.Generic;
using System.Threading.Tasks;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Favorite>> GetAllFavoritesByUserId(Guid userId);
        Task<IEnumerable<Favorite>> GetAllFavoritesByHotelId(Guid hotelId);
        Task<bool> InsertFavorite(Favorite favorite);
        Task<bool> DeleteFavorite(Guid userId, Guid hotelId);
    }
}
