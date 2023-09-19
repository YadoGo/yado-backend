using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using XSystem.Security.Cryptography;
using yado_backend.Data;
using yado_backend.Models;
using yado_backend.Models.Dtos;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace yado_backend.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _dbContext;
        private readonly string secretKey;


        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            secretKey = Environment.GetEnvironmentVariable("SecretKey");
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetUserDetails(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<User> GetUserByUsername(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }


        public bool IsUniqueUser(string username, string email)
        {
            var existingUserWithUsername = _dbContext.Users.FirstOrDefault(u => u.Username == username);
            var existingUserWithEmail = _dbContext.Users.FirstOrDefault(u => u.Email == email);

            if (existingUserWithUsername == null && existingUserWithEmail == null)
            {
                return true;
            }
            return false;
        }

        public async Task<User> Register(UserRegisterDto userRegisterDto)
        {
            var passwordEncrypt = EncryptPasswordWithMD5(userRegisterDto.Password);

            User user = new()
            { 
                Id = Guid.NewGuid(),
                Username = userRegisterDto.Username,
                Email = userRegisterDto.Email,
                Password = passwordEncrypt,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            user.Password = passwordEncrypt;

            UserRole userRole = new()
            {
                UserId = user.Id,
                RoleId = 1
            };

            _dbContext.UserRoles.Add(userRole);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserLoginReponseDto> Login(UserLoginDto userLoginDto)
        {
            var passwordEncrypt = EncryptPasswordWithMD5(userLoginDto.Password);

            var user = _dbContext.Users.FirstOrDefault(
                user => user.Email.ToLower() == userLoginDto.Email.ToLower()
                && user.Password == passwordEncrypt
                );

            if (user == null)
            {
                return new UserLoginReponseDto()
                {
                    Token = "",
                    User = null
                };
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim("Id", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var userRoles = await GetUserRoles(user.Id);

            foreach (var role in userRoles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);

            UserLoginReponseDto userLoginReponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };

            return userLoginReponseDto;
        }

        public async Task<bool> UpdateUser(Guid id, UserDetailsDto updatedUserDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                user.Username = updatedUserDto.Username;
                user.FirstName = updatedUserDto.FirstName;
                user.LastName = updatedUserDto.LastName;

                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<bool> DeleteUserById(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<bool> ChangePassword(Guid userId, UserChangePasswordDto changePasswordDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            if (!VerifyPassword(user, changePasswordDto.CurrentPassword))
            {
                return false;
            }

            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
            {
                return false;
            }

            user.Password = EncryptPasswordWithMD5(changePasswordDto.NewPassword);

            await _dbContext.SaveChangesAsync();

            return true;
        }


        public static string EncryptPasswordWithMD5(string password)
        {
            MD5CryptoServiceProvider x = new();
            
            byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
            data = x.ComputeHash(data);

            string resp = "";
            for(int i = 0; i < data.Length; i++)
            {
                resp += data[i].ToString("x2").ToLower();
            }

            return resp;
        }

        public async Task<List<string>> GetUserRoles(Guid userId)
        {
            var userRoles = await _dbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.Name)
                .ToListAsync();

            return userRoles;
        }

        private static bool VerifyPassword(User user, string currentPassword)
        {
            return user.Password == EncryptPasswordWithMD5(currentPassword);
        }

    }
}

