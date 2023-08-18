using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using yado_backend.Data;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext _dbContext;

        public FavoriteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Favorite>> GetAllFavoritesByUserUuid(string userUuid)
        {
            return await _dbContext.Favorites.Where(f => f.UserUuid == userUuid).ToListAsync();
        }

        public async Task<IEnumerable<Favorite>> GetAllFavoritesByHotelUuid(string hotelUuid)
        {
            return await _dbContext.Favorites.Where(f => f.HotelUuid == hotelUuid).ToListAsync();
        }

        public async Task<bool> InsertFavorite(Favorite favorite)
        {
            _dbContext.Favorites.Add(favorite);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteFavorite(string userUuid, string hotelUuid)
        {
            var favorite = await _dbContext.Favorites.FirstOrDefaultAsync(f =>
                f.UserUuid == userUuid && f.HotelUuid == hotelUuid);

            if (favorite != null)
            {
                _dbContext.Favorites.Remove(favorite);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }
    }
}
