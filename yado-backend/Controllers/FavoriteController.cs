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
        [HttpGet("user/{userId}")]
        [ResponseCache(CacheProfileName = "CacheProfile60sec")]
        public async Task<IActionResult> GetAllFavoritesByUserId(Guid userId)
        {
            var favorites = await _favoriteRepository.GetAllFavoritesByUserId(userId);
            return Ok(favorites);
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("hotel/{hotelId}")]
        [ResponseCache(CacheProfileName = "CacheProfile60sec")]
        public async Task<IActionResult> GetAllFavoritesByHotelId(Guid hotelId)
        {
            var favorites = await _favoriteRepository.GetAllFavoritesByHotelId(hotelId);
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
        [HttpDelete("{userId}/{hotelId}")]
        public async Task<IActionResult> DeleteFavorite(Guid userId, Guid hotelId)
        {
            var success = await _favoriteRepository.DeleteFavorite(userId, hotelId);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("user/{userId}/count")]
        public async Task<IActionResult> GetFavoriteCountByUserId(Guid userId)
        {
            try
            {
                int favoriteCount = await _favoriteRepository.GetFavoriteCountByUserId(userId);
                return Ok(favoriteCount);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("hotel/{hotelId}/count")]
        public async Task<IActionResult> GetFavoriteCountByHotelId(Guid hotelId)
        {
            try
            {
                int favoriteCount = await _favoriteRepository.GetFavoriteCountByHotelId(hotelId);
                return Ok(favoriteCount);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
