using Microsoft.AspNetCore.Mvc;
using yado_backend.Models;
using System.Collections.Generic;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userRepository.GetAllUsers());
        }

        [HttpGet("{UUID}")]
        public async Task<IActionResult> GetUserDetails(string UUID)
        {
            return Ok(await _userRepository.GetUserDetails(UUID));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if( user == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _userRepository.InsertUser(user);

            return Created("created", created);

        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userRepository.UpdateUser(user);

            return NoContent();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string UUID)
        {
            await _userRepository.DeleteUserByUUID(UUID);

            return NoContent();
        }
    }
}

