using Microsoft.EntityFrameworkCore;
using yado_backend.Data;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        private readonly AppDbContext _dbContext;

        public SiteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Site>> GetAllSitesByHotelId(Guid hotelId)
        {
            return await _dbContext.Sites
                .Where(site => site.HotelId == hotelId)
                .ToListAsync();
        }

        public async Task<bool> InsertSite(Site site)
        {
            _dbContext.Sites.Add(site);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteSiteById(Guid siteId)
        {
            var site = await _dbContext.Sites.FindAsync(siteId);
            if (site != null)
            {
                _dbContext.Sites.Remove(site);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<bool> UpdateSiteById(Site site)
        {
            var existingSite = await _dbContext.Sites.FindAsync(site.Id);
            if (existingSite != null)
            {
                existingSite.OriginUrl = site.OriginUrl;
                existingSite.NightlyPrice = site.NightlyPrice;

                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }
    }
}
