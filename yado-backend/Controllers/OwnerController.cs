﻿using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ResponseCache(CacheProfileName = "CacheProfile120sec")]
        public async Task<IActionResult> GetAllOwners()
        {
            var owners = await _ownerRepository.GetAllOwners();
            return Ok(owners);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{ownerId}")]
        public async Task<IActionResult> DeleteOwner(Guid ownerId)
        {
            var success = await _ownerRepository.DeleteOwnerByID(ownerId);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }

        [Authorize(Roles = "Hotel Manager,Admin")]
        [HttpGet("{ownerId}/hotels")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelsByOwnerId(Guid ownerId)
        {
            var hotels = await _ownerRepository.GetHotelsByOwnerId(ownerId);
            return Ok(hotels);
        }

    }
}
