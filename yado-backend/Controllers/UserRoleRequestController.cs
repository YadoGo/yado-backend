using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yado_backend.Enums;
using yado_backend.Models.Dtos;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/user-role-requests")]
    public class UserRoleRequestController : ControllerBase
    {
        private readonly IUserRoleRequestRepository _userRoleRequestRepository;

        public UserRoleRequestController(IUserRoleRequestRepository userRoleRequestRepository)
        {
            _userRoleRequestRepository = userRoleRequestRepository;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateUserUserRoleRequest(Guid userId, [FromBody] UserUserRoleRequestDto userRoleRequestDto)
        {
            userRoleRequestDto.UserId = userId;

            var userRoleRequest = await _userRoleRequestRepository.CreateAsync(userRoleRequestDto);

            if (userRoleRequest != null)
            {
                return Ok(userRoleRequest);
            }

            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAdminUserRoleRequests(
            UserRoleRequestStatus? status = null,
            int page = 1,
            int pageSize = 10)
        {
            var adminUserRoleRequests = await _userRoleRequestRepository
                .GetAllAdminUserRoleRequestsAsync(status, page, pageSize);

            return Ok(adminUserRoleRequests);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{requestId}")]
        public async Task<IActionResult> UpdateUserRoleRequestStatus(Guid requestId, UserRoleRequestStatus status)
        {
            var success = await _userRoleRequestRepository.UpdateUserRoleRequestStatusAsync(requestId, status);

            if (success)
            {
                return Ok();
            }

            return NotFound();
        }

        [AllowAnonymous]
        [HttpDelete("cleanup")]
        [ResponseCache(CacheProfileName = "CacheProfile1day")]
        public async Task<IActionResult> DeleteExpiredCancelledUserRoleRequestsAsync()
        {
            await _userRoleRequestRepository.DeleteExpiredCancelledUserRoleRequestsAsync();
            return Ok("Expired cancelled user role requests have been deleted successfully.");
        }
    }
}

