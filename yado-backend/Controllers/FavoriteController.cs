using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yado_backend.Models;
using yado_backend.Models.Dtos;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/favorites")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMapper _mapper;

        public FavoriteController(IFavoriteRepository favoriteRepository, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;

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

        [HttpGet("user/{userId}/count")]
        [ResponseCache(CacheProfileName = "CacheProfile1day")]
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
        [ResponseCache(CacheProfileName = "CacheProfile1day")]
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

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteRequestDto favoriteRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var favorite = _mapper.Map<Favorite>(favoriteRequestDto);

            var result = await _favoriteRepository.AddFavoriteAsync(favorite);

            if (result)
            {
                return Ok(new { Message = "Hotel added to favorites successfully." });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Failed to add hotel to favorites." });
            }
        }

        [Authorize]
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFavorite([FromBody] FavoriteRequestDto favoriteRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var favorite = _mapper.Map<Favorite>(favoriteRequestDto);

            var result = await _favoriteRepository.RemoveFavoriteAsync(favorite);

            if (result)
            {
                return Ok(new { Message = "Hotel removed from favorites successfully." });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Failed to remove hotel from favorites." });
            }
        }

        [Authorize]
        [HttpGet("exists")]
        public async Task<IActionResult> FavoriteExists(Guid userId, Guid hotelId)
        {
            var exists = await _favoriteRepository.FavoriteExistsAsync(userId, hotelId);

            return Ok(new { Exists = exists });
        }
    }
}
