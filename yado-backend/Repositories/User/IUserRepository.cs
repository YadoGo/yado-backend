using System;
using yado_backend.Models;

namespace yado_backend.Repositories
{
	public interface IUserRepository
	{
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserDetails(string UUID);

        Task<bool> InsertUser(User user);

        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUserByUUID(string UUID);
    }
}

