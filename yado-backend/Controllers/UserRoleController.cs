using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/user-role")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleController(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        [AllowAnonymous]
        [HttpPost("{userId}/{roleId}")]
        public async Task<IActionResult> AddUserRoleAsync(Guid userId, int roleId)
        {
            await _userRoleRepository.AddUserRoleAsync(userId, roleId);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}/{roleId}")]
        public async Task<IActionResult> DeleteUserRoleAsync(Guid userId, int roleId)
        {
            await _userRoleRepository.RemoveUserRoleAsync(userId, roleId);
            return NoContent();
        }

        [Authorize]
        [HttpGet("get-roles-user/{userId}")]
        public async Task<IActionResult> GetRolesForUserAsync(Guid userId)
        {
            var roles = await _userRoleRepository.GetRolesForUserAsync(userId);
            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-users-role/{userId}")]
        public async Task<IActionResult> GetUsersByRoleAsync(int roleId, int page = 1, int pageSize = 10)
        {
            var users = await _userRoleRepository.GetUsersByRoleAsync(roleId, page, pageSize);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
    }
}
