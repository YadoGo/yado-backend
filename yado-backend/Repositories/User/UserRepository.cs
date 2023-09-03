using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using XSystem.Security.Cryptography;
using yado_backend.Data;
using yado_backend.Models;
using yado_backend.Models.Dtos;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace yado_backend.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _dbContext;
        private string secretKey;


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
            var roleId = userRegisterDto.RoleId;

            var role = await _dbContext.Roles.FindAsync(roleId);

            User user = new()
            { 
                Id = Guid.NewGuid(),
                Username = userRegisterDto.Username,
                Email = userRegisterDto.Email,
                Password = passwordEncrypt,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Role = role
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            user.Password = passwordEncrypt;
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
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

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
    }
}

