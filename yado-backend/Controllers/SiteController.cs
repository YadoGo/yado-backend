using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yado_backend.Models;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/sites")]
    public class SiteController : ControllerBase
    {
        private readonly ISiteRepository _siteRepository;

        public SiteController(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }

        [AllowAnonymous]
        [HttpGet("{hotelId}")]
        [ResponseCache(CacheProfileName = "CacheProfile60sec")]
        public async Task<IActionResult> GetAllSitesByHotelId(Guid hotelId)
        {
            var sites = await _siteRepository.GetAllSitesByHotelId(hotelId);
            return Ok(sites);
        }

        [Authorize(Roles = "Hotel Manager")]
        [HttpPost]
        public async Task<IActionResult> InsertSite(Site site)
        {
            var success = await _siteRepository.InsertSite(site);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = "Hotel Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateSiteById(Site site)
        {
            var success = await _siteRepository.UpdateSiteById(site);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpDelete("{siteId}")]
        public async Task<IActionResult> DeleteSiteById(Guid siteId)
        {
            var success = await _siteRepository.DeleteSiteById(siteId);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }     
    }
}
