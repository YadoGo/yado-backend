using System;
using yado_backend.Models;
using yado_backend.Models.Dtos;
namespace yado_backend.Repositories
{
	public interface IUserRepository
	{
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserDetails(string UUID);

        bool IsUniqueUser(string user, string email);

        Task<UserLoginReponseDto> Login(UserLoginDto userLoginDto);

        Task<User> Register(UserRegisterDto userRegisterDto);

        Task<bool> UpdateUser(string UUID, UserDetailsDto updatedUser);

        Task<bool> DeleteUserByUUID(string UUID);
    }
}

