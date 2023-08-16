using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using yado_backend.Data;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

     
        public async Task<bool> DeleteUserByUUID(string UUID)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UUID == UUID);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetUserDetails(string UUID)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UUID == UUID);
        }

        public async Task<bool> InsertUser(User user)
        {
            _dbContext.Users.Add(user);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;

        }
    }
}

