using System;
using Microsoft.AspNetCore.Mvc;
using yado_backend.Models.Dtos;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/parameters")]
    public class ParameterController : ControllerBase
    {
        private readonly IParameterRepository _parameterRepository;

        public ParameterController(IParameterRepository parameterRepository)
        {
            _parameterRepository = parameterRepository;
        }

        [HttpGet("{hotelId}")]
        public async Task<ActionResult<ParameterDto>> GetParametersByHotelId(Guid hotelId)
        {
            var parameterDto = await _parameterRepository.GetParametersByHotelIdAsync(hotelId);

            if (parameterDto == null)
            {
                return NotFound();
            }

            return Ok(parameterDto);
        }
    }
}

