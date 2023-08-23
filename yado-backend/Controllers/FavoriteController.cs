using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using yado_backend.Models;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/favorites")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteController(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        [AllowAnonymous]
        [HttpGet("user/{userUuid}")]
        [ResponseCache(CacheProfileName = "CacheProfile60sec")]
        public async Task<IActionResult> GetAllFavoritesByUserUuid(string userUuid)
        {
            var favorites = await _favoriteRepository.GetAllFavoritesByUserUuid(userUuid);
            return Ok(favorites);
        }

        [Authorize(Roles = "2,3")]
        [HttpGet("hotel/{hotelUuid}")]
        [ResponseCache(CacheProfileName = "CacheProfile60sec")]
        public async Task<IActionResult> GetAllFavoritesByHotelUuid(string hotelUuid)
        {
            var favorites = await _favoriteRepository.GetAllFavoritesByHotelUuid(hotelUuid);
            return Ok(favorites);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InsertFavorite(Favorite favorite)
        {
            var success = await _favoriteRepository.InsertFavorite(favorite);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize]
        [HttpDelete("{userUuid}/{hotelUuid}")]
        public async Task<IActionResult> DeleteFavorite(string userUuid, string hotelUuid)
        {
            var success = await _favoriteRepository.DeleteFavorite(userUuid, hotelUuid);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
