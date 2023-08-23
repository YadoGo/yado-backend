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
        [HttpGet("{uuid}")]
        [ResponseCache(CacheProfileName = "CacheProfile120sec")]
        public async Task<IActionResult> GetHotelByUuid(string uuid)
        {
            var hotel = await _hotelRepository.GetHotelByUuid(uuid);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        [Authorize(Roles = "2, 3")]
        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetAllHotelsByOwnerId(string ownerId)
        {
            var hotels = await _hotelRepository.GetAllHotelsByOwnerId(ownerId);
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

        [Authorize(Roles = "2")]
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
        [HttpPut("{uuid}")]
        public async Task<IActionResult> UpdateHotelByUuid(string uuid, Hotel hotel)
        {
            var success = await _hotelRepository.UpdateHotelByUuid(uuid, hotel);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }

        [Authorize(Roles = "2, 3")]
        [HttpDelete("{uuid}")]
        public async Task<IActionResult> DeleteHotelByUuid(string uuid)
        {
            var success = await _hotelRepository.DeleteHotelByUuid(uuid);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
