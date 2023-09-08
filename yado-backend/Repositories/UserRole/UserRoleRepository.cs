using Microsoft.EntityFrameworkCore;
using yado_backend.Data;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;

        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUserRoleAsync(Guid userId, int roleId)
        {
            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserRoleAsync(Guid userId, int roleId)
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Role>> GetRolesForUserAsync(Guid userId)
        {
            var roles = await _context.Roles
                .Where(r => r.UserRoles.Any(ur => ur.UserId == userId))
                .ToListAsync();

            return roles;
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(int roleId, int page, int pageSize)
        {
            var usersQuery = _context.UserRoles
                .Where(ur => ur.RoleId == roleId)
                .Select(ur => ur.User);

            var totalUsers = await usersQuery.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            if (page < 1)
                page = 1;
            else if (page > totalPages)
                page = totalPages;

            var users = await usersQuery
                .OrderBy(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return users;
        }
    }
}

