using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yado_backend.Models.Dtos;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/user-role-requests")]
    public class UserRoleRequestController : ControllerBase
    {
        private readonly IUserRoleRequestRepository _userRoleRequestRepository;
        private readonly IMapper _mapper;

        public UserRoleRequestController(IUserRoleRequestRepository userRoleRequestRepository, IMapper mapper)
        {
            _userRoleRequestRepository = userRoleRequestRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateUserUserRoleRequest([FromBody] UserUserRoleRequestDto userRoleRequestDto)
        {
            var userRoleRequest = await _userRoleRequestRepository.CreateAsync(userRoleRequestDto);

            if (userRoleRequest != null)
            {
                return Ok(userRoleRequest);
            }

            return BadRequest();
        }

        [Authorize(Roles = "User")]
        [HttpGet("{userId}")]
        [ResponseCache(CacheProfileName = "CacheProfile1day")]
        public async Task<IActionResult> GetUserUserRoleRequest(Guid userId)
        {
            var userRoleRequest = await _userRoleRequestRepository.GetUserUserRoleRequestAsync(userId);

            if (userRoleRequest != null)
            {
                var userRoleRequestDto = _mapper.Map<UserUserRoleRequestDto>(userRoleRequest);
                return Ok(userRoleRequestDto);
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAdminUserRoleRequests(
            string status = null,
            int page = 1,
            int pageSize = 10)
        {
            var adminUserRoleRequests = await _userRoleRequestRepository
                .GetAllAdminUserRoleRequestsAsync(status, page, pageSize);

            return Ok(adminUserRoleRequests);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{requestId}")]
        public async Task<IActionResult> UpdateUserRoleRequestStatus(Guid requestId, string status)
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
