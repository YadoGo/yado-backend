using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yado_backend.Models;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "CacheProfile120sec")]
        public async Task<IActionResult> GetHotelById(Guid id)
        {
            var hotel = await _hotelRepository.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("owner/{userId}")]
        public async Task<IActionResult> GetAllHotelsByOwnerId(Guid userId)
        {
            var hotels = await _hotelRepository.GetAllHotelsByUserId(userId);
            return Ok(hotels);
        }

        [AllowAnonymous]
        [HttpGet("top-review")]
        [ResponseCache(CacheProfileName = "CacheProfile1day")]
        public async Task<IActionResult> GetAllTopHotelsReview()
        {
            var topHotels = await _hotelRepository.GetAllTopHotelsReview();
            return Ok(topHotels);
        }

        [AllowAnonymous]
        [HttpGet("population/{populationId}")]
        public async Task<IActionResult> GetAllHotelsByPopulationId(int populationId, int page, int pageSize)
        {
            var hotels = await _hotelRepository.GetAllHotelsByPopulationId(populationId, page, pageSize);
            return Ok(hotels);
        }

        [AllowAnonymous]
        [HttpGet("{populationId}/filter")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelsByParameters(int populationId, [FromQuery] Parameter parameters)
        {
            var filteredHotels = await _hotelRepository.GetHotelsByParameters(populationId, parameters);

            return filteredHotels.ToList();
        }

        [Authorize(Roles = "Hotel Manager")]
        [HttpPost]
        public async Task<IActionResult> InsertHotel(Hotel hotel)
        {
            var success = await _hotelRepository.InsertHotel(hotel);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = "2, 3")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotelById(Guid id, Hotel hotel)
        {
            var success = await _hotelRepository.UpdateHotelById(id, hotel);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }

        [Authorize(Roles = "2, 3")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelById(Guid id)
        {
            var success = await _hotelRepository.DeleteHotelById(id);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
