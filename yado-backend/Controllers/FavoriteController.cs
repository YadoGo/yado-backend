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

        [HttpGet("user/{userUuid}")]
        public async Task<IActionResult> GetAllFavoritesByUserUuid(string userUuid)
        {
            var favorites = await _favoriteRepository.GetAllFavoritesByUserUuid(userUuid);
            return Ok(favorites);
        }

        [HttpGet("hotel/{hotelUuid}")]
        public async Task<IActionResult> GetAllFavoritesByHotelUuid(string hotelUuid)
        {
            var favorites = await _favoriteRepository.GetAllFavoritesByHotelUuid(hotelUuid);
            return Ok(favorites);
        }

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
