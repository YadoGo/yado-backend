using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Favorite>> GetAllFavoritesByUserId(Guid userId)
        {
            return await _dbContext.Favorites.Where(f => f.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Favorite>> GetAllFavoritesByHotelId(Guid hotelId)
        {
            return await _dbContext.Favorites.Where(f => f.HotelId == hotelId).ToListAsync();
        }

        public async Task<bool> AddFavoriteAsync(Favorite favorite)
        {
            try
            {
                var existingFavorite = await _dbContext.Favorites
                    .FirstOrDefaultAsync(f => f.UserId == favorite.UserId && f.HotelId == favorite.HotelId);

                if (existingFavorite != null)
                {
                    return false;
                }

                _dbContext.Favorites.Add(favorite);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveFavoriteAsync(Favorite favorite)
        {
            try
            {
                var existingFavorite = await _dbContext.Favorites
                    .FirstOrDefaultAsync(f => f.UserId == favorite.UserId && f.HotelId == favorite.HotelId);

                if (existingFavorite != null)
                {
                    _dbContext.Favorites.Remove(existingFavorite);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> FavoriteExistsAsync(Guid userId, Guid hotelId)
        {
            return await _dbContext.Favorites
                .AnyAsync(f => f.UserId == userId && f.HotelId == hotelId);
        }

        public async Task<int> GetFavoriteCountByUserId(Guid userId)
        {
            return await _dbContext.Favorites.CountAsync(f => f.UserId == userId);
        }

        public async Task<int> GetFavoriteCountByHotelId(Guid hotelId)
        {
            return await _dbContext.Favorites.CountAsync(f => f.HotelId == hotelId);
        }

    }
}
