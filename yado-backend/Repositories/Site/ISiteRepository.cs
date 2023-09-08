using System.Collections.Generic;
using System.Threading.Tasks;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface ISiteRepository
    {
        Task<IEnumerable<Site>> GetAllSitesByHotelId(Guid hotelId);

        Task<bool> InsertSite(Site site);

        Task<bool> DeleteSiteById(Guid siteId);

        Task<bool> UpdateSiteById(Site site);
    }
}
