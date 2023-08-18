using Microsoft.AspNetCore.Mvc;
using yado_backend.Models;
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

        [HttpPut("{UUID}")]
        public async Task<IActionResult> UpdateUser(string UUID, User updatedUser)
        {
            var success = await _userRepository.UpdateUser(UUID, updatedUser);
            if (success)
            {
                return NoContent(); 
            }
            return NotFound(); 
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string UUID)
        {
            await _userRepository.DeleteUserByUUID(UUID);

            return NoContent();
        }
    }
}

