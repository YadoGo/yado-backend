using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yado_backend.Models;
using yado_backend.Models.Dtos;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelController(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "CacheProfile120sec")]
        public async Task<IActionResult> GetHotelById(Guid id)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(id);
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
            var hotels = await _hotelRepository.GetAllHotelsByUserIdAsync(userId);
            return Ok(hotels);
        }

        [AllowAnonymous]
        [HttpGet("top-review")]
        [ResponseCache(CacheProfileName = "CacheProfile1day")]
        public async Task<IActionResult> GetAllTopHotelsReview()
        {
            var topHotels = await _hotelRepository.GetAllTopHotelsReviewAsync();
            return Ok(topHotels);
        }

        [AllowAnonymous]
        [HttpGet("population/{populationId}")]
        public async Task<IActionResult> GetAllHotelsByPopulationId(int populationId, int page, int pageSize)
        {
            var hotelEntities = await _hotelRepository.GetAllHotelsByPopulationIdAsync(populationId, page, pageSize);

            var hotelDtos = _mapper.Map<IEnumerable<HotelSummaryDto>>(hotelEntities);
            return Ok(hotelDtos);
        }


        [AllowAnonymous]
        [HttpGet("filter")]
        public async Task<IActionResult> GetHotelsByParameters([FromQuery] ParameterDto parameters, int populationId, int page, int pageSize)
        {
            var filteredHotels = await _hotelRepository.GetHotelsByParametersAsync(parameters, populationId, page, pageSize);

            return Ok(filteredHotels.ToList());
        }

        [Authorize(Roles = "Hotel Manager")]
        [HttpPost]
        public async Task<IActionResult> InsertHotel(Hotel hotel)
        {
            var success = await _hotelRepository.InsertHotelAsync(hotel);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = "Hotel Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotelById(Guid id, Hotel hotel)
        {
            var success = await _hotelRepository.UpdateHotelByIdAsync(id, hotel);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }

        [Authorize(Roles = "Hotel Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelById(Guid id)
        {
            var success = await _hotelRepository.DeleteHotelByIdAsync(id);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}