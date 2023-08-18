using Microsoft.AspNetCore.Mvc;
using yado_backend.Models;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/owners")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerController(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            var owners = await _ownerRepository.GetAllOwners();
            return Ok(owners);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOwner(Owner owner)
        {
            var success = await _ownerRepository.InsertOwner(owner);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{ownerId}")]
        public async Task<IActionResult> DeleteOwner(int ownerId)
        {
            var success = await _ownerRepository.DeleteOwnerByID(ownerId);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
