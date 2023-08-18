using Microsoft.AspNetCore.Mvc;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/populations")]
    public class PopulationController : ControllerBase
    {
        private readonly IPopulationRepository _populationRepository;

        public PopulationController(IPopulationRepository populationRepository)
        {
            _populationRepository = populationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPopulations()
        {
            var populations = await _populationRepository.GetAllPopulations();
            return Ok(populations);
        }
    }
}
