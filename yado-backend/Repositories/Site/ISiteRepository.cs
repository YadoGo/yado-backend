using System.Collections.Generic;
using System.Threading.Tasks;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface ISiteRepository
    {
        Task<IEnumerable<Site>> GetAllSitesByHotelUuid(string hotelUuid);

        Task<bool> InsertSite(Site site);

        Task<bool> DeleteSiteById(int siteId);

        Task<bool> UpdateSiteById(Site site);
    }
}
