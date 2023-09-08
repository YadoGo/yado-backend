using Microsoft.EntityFrameworkCore;
using yado_backend.Data;
using yado_backend.Enums;
using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Repositories
{
    public class UserRoleRequestRepository : IUserRoleRequestRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRoleRequestRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserRoleRequest> CreateAsync(UserUserRoleRequestDto userRoleRequestDto)
        {
            var userRoleRequest = new UserRoleRequest
            {
                UserId = userRoleRequestDto.UserId,
                RequestedRoleId = userRoleRequestDto.RequestedRoleId,
                Message = userRoleRequestDto.Message
            };

            _dbContext.UserRoleRequests.Add(userRoleRequest);
            await _dbContext.SaveChangesAsync();

            return userRoleRequest;
        }

        public async Task<UserRoleRequest> GetUserUserRoleRequestAsync(Guid userId)
        {
            return await _dbContext.UserRoleRequests
                .FirstOrDefaultAsync(ur => ur.UserId == userId);
        }

        public async Task<List<AdminUserRoleRequestDto>> GetAllAdminUserRoleRequestsAsync(UserRoleRequestStatus? status = null, int page = 1, int pageSize = 10)
        {
            var query = _dbContext.UserRoleRequests.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(ur => ur.Status == status);
            }

            query = query.OrderByDescending(ur => ur.RequestedAt);

            var skip = (page - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            var adminUserRoleRequests = await query
                .Select(ur => new AdminUserRoleRequestDto
                {
                    Id = ur.Id,
                    UserId = ur.UserId,
                    Username = ur.User.Username,
                    RequestedRoleId = ur.RequestedRoleId,
                    RequestedRoleName = ur.RequestedRole.Name,
                    RequestedAt = ur.RequestedAt,
                    Status = ur.Status,
                    ApprovedByUserId = ur.ApprovedByUserId,
                    ApprovedByUsername = ur.ApprovedByUser != null ? ur.ApprovedByUser.Username : null,
                    Message = ur.Message
                })
                .ToListAsync();

            return adminUserRoleRequests;
        }

        public async Task<bool> UpdateUserRoleRequestStatusAsync(Guid requestId, UserRoleRequestStatus status)
        {
            var userRoleRequest = await _dbContext.UserRoleRequests.FindAsync(requestId);

            if (userRoleRequest == null)
            {
                return false;
            }

            userRoleRequest.Status = status;
            userRoleRequest.LastStatusUpdate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task DeleteExpiredCancelledUserRoleRequestsAsync()
        {
            var deadlineDate = DateTime.UtcNow.AddDays(-7);

            var cancelledRequests = await _dbContext.UserRoleRequests
                .Where(r => r.Status == UserRoleRequestStatus.Cancelled && r.LastStatusUpdate <= deadlineDate)
                .ToListAsync();

            foreach (var request in cancelledRequests)
            {
                _dbContext.UserRoleRequests.Remove(request);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
