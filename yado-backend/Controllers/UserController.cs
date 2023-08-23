using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yado_backend.Models;
using yado_backend.Models.Dtos;
using yado_backend.Repositories;

namespace yado_backend.Controllers

{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        protected ResponseAPI _responseAPI;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            this._responseAPI = new();
        }

        [Authorize(Roles = "3")]
        [HttpGet]
        [ResponseCache(CacheProfileName = "CacheProfile60sec")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();

            var userSummaryDtos = _mapper.Map<IEnumerable<UserSummaryDto>>(users);

            return Ok(userSummaryDtos);
        }

        [Authorize(Roles = "2, 3")]
        [HttpGet("{UUID}")]
        public async Task<IActionResult> GetUserDetails(string UUID)
        {
            var user = await _userRepository.GetUserDetails(UUID);
            if (user == null)
            {
                return NotFound();
            }

            var userDetailsDto = _mapper.Map<UserDetailsDto>(user);

            return Ok(userDetailsDto);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            bool isUnique = _userRepository.IsUniqueUser(userRegisterDto.Username, userRegisterDto.Email);

            if(!isUnique)
            {
                _responseAPI.StatusCode = HttpStatusCode.BadRequest;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add("The username or email already exists.");
                return BadRequest(_responseAPI);
            }

            var user = await _userRepository.Register(userRegisterDto
);

            if (user == null)
            {
                _responseAPI.StatusCode = HttpStatusCode.BadRequest;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add("Registration failed!");
                return BadRequest(_responseAPI);
            }

            _responseAPI.StatusCode = HttpStatusCode.OK;
            _responseAPI.IsSuccess = true;
            return Ok(_responseAPI);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginResponse = await _userRepository.Login(userLoginDto);

            if (loginResponse.User == null)
            {
                return Unauthorized();
            }

            return Ok(loginResponse);
        }

        [Authorize(Roles = "2, 3")]
        [HttpPut("{UUID}")]
        public async Task<IActionResult> UpdateUser(string UUID, [FromBody] UserDetailsDto updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _userRepository.UpdateUser(UUID, updatedUser);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }

        [Authorize]
        [HttpDelete("{UUID}")]
        public async Task<IActionResult> DeleteUser(string UUID)
        {
            var success = await _userRepository.DeleteUserByUUID(UUID);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
