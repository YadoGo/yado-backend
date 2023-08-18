using System.Collections.Generic;
using System.Threading.Tasks;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Favorite>> GetAllFavoritesByUserUuid(string userUuid);
        Task<IEnumerable<Favorite>> GetAllFavoritesByHotelUuid(string hotelUuid);
        Task<bool> InsertFavorite(Favorite favorite);
        Task<bool> DeleteFavorite(string userUuid, string hotelUuid);
    }
}
