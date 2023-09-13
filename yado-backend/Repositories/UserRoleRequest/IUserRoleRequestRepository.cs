using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Repositories
{
    public interface IUserRoleRequestRepository
    {
        Task<UserRoleRequest> CreateAsync(UserUserRoleRequestDto userRoleRequestDto);
        Task<UserRoleRequest> GetUserUserRoleRequestAsync(Guid userId);
        Task<List<AdminUserRoleRequestDto>> GetAllAdminUserRoleRequestsAsync(string status = null, int page = 1, int pageSize = 10);
        Task<bool> UpdateUserRoleRequestStatusAsync(Guid requestId, string status);
        Task DeleteExpiredCancelledUserRoleRequestsAsync();
    }
}
