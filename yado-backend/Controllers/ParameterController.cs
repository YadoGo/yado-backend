using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using yado_backend.Models;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [Route("api/parameter")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        private readonly IParameterRepository _parameterRepository;

        public ParameterController(IParameterRepository parameterRepository)
        {
            _parameterRepository = parameterRepository;
        }

        [HttpPost]
        public async Task<IActionResult> InsertParameter([FromBody] Parameter parameter)
        {
            if (await _parameterRepository.InsertParameter(parameter))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{hotelUuid}")]
        public async Task<IActionResult> UpdateParameterByHotelUuid(string hotelUuid, [FromBody] Parameter parameter)
        {
            if (await _parameterRepository.UpdateParameterByHotelUuid(hotelUuid, parameter))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{hotelUuid}")]
        public async Task<IActionResult> DeleteParameterByHotelUuid(string hotelUuid)
        {
            if (await _parameterRepository.DeleteParameterByHotelUuid(hotelUuid))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
