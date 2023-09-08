using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IUserRoleRepository
    {
        Task AddUserRoleAsync(Guid userId, int roleId);
        Task RemoveUserRoleAsync(Guid userId, int roleId);
        Task<IEnumerable<Role>> GetRolesForUserAsync(Guid userId);
        Task<IEnumerable<User>> GetUsersByRoleAsync(int roleId, int page, int pageSize);
    }
}

