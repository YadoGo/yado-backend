using System;
using yado_backend.Models;
using yado_backend.Models.Dtos;
namespace yado_backend.Repositories
{
	public interface IUserRepository
	{
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserDetails(Guid id);
        Task<User> GetUserByUsername(string username);
        bool IsUniqueUser(string user, string email);
        Task<UserLoginReponseDto> Login(UserLoginDto userLoginDto);
        Task<User> Register(UserRegisterDto userRegisterDto);
        Task<bool> UpdateUser(Guid id, UserDetailsDto updatedUser);
        Task<bool> DeleteUserById(Guid id);
        Task<bool> ChangePassword(Guid userId, UserChangePasswordDto changePasswordDto);
    }
}

