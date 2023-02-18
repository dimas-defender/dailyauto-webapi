using DailyAuto.Models;
using DailyAuto.Services.Implementation;
using DailyAuto.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace DailyAuto.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;
        private TokenService _tokenService;
        public UsersController(IUsersService usersService, TokenService tokenService)
        {
            _usersService = usersService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// /// <param name="request">RegistrationRequest</param>
        /// <returns>Returns UserDTO</returns>
        /*/// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the user is null</response>*/
        [HttpPost]
        [Route("api/v1/auth/signup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<UserDTO> Register(RegistrationRequest request)
        {
            var existUser = _usersService.GetUserByLogin(request.Login);

            if (existUser != null)
                return Conflict();

            var user = new User(0, request.Login, GetHash(request.Password), request.License);

            var model = _usersService.CreateUser(user);
            var dto = ModelToDTO(model);

            return CreatedAtAction(nameof(GetUserById), new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Signs in a user
        /// </summary>
        /// <param name="request">AuthRequest</param>
        /// <returns>Returns AuthResponse</returns>
        [HttpPost]
        [Route("api/v1/auth/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthResponse> Authenticate(AuthRequest request)
        {
            var user = _usersService.GetUserByLogin(request.Login);

            if (user is null)
                return BadRequest();

            if (GetHash(request.Password) != user.Password)
                return BadRequest();

            var accessToken = _tokenService.CreateToken(user);

            return Ok(new AuthResponse
            {
                Login = user.Login,
                Token = accessToken
            });
        }

        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns UserDTO</returns>
        [HttpGet, Authorize]
        [Route("api/v1/users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<UserDTO> GetUserById(long id)
        {
            if (id < 1)
                return BadRequest();

            var userId = long.Parse(User.Claims.First(i => i.Type == "user_id").Value);

            if (userId != id)
                return Forbid();

            var model = _usersService.GetUserById(id);

            return Ok(ModelToDTO(model));
        }

        /// <summary>
        /// Updates user info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request">UpdateUserRequest</param>
        /// <returns>Returns NoContent</returns>
        [HttpPatch, Authorize]
        [Route("api/v1/users/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult UpdateUser(long id, UpdateUserRequest request)
        {
            if (id < 1)
                return BadRequest();

            var userId = long.Parse(User.Claims.First(i => i.Type == "user_id").Value);

            if (userId != id)
                return Forbid();

            var user = _usersService.GetUserById(id);

            if (request.HasProperty(nameof(user.Password)))
            {
                if (string.IsNullOrWhiteSpace(request.Password))
                    return BadRequest();

                user.Password = GetHash(request.Password);
            }

            if (request.HasProperty(nameof(user.License)))
            {
                if (string.IsNullOrWhiteSpace(request.License))
                    return BadRequest();

                user.License = request.License;
            }

            _usersService.UpdateUser(id, user);
            return NoContent();
        }
        private static UserDTO ModelToDTO(User user) =>
            new UserDTO
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                License = user.License
            };

        private static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }
    }
    public class UserDTO
    {
        public long Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? License { get; set; }
    }

    public class RegistrationRequest
    {
        [Required]
        public string Login { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string License { get; set; } = null!;
    }

    public class AuthRequest
    {
        [Required]
        public string Login { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }

    public class AuthResponse
    {
        public string Login { get; set; } = null!;
        public string Token { get; set; } = null!;
    }

    public class UpdateUserRequest
    {
        private readonly HashSet<string> _properties = new HashSet<string>();
        public bool HasProperty(string propertyName) => _properties.Contains(propertyName);
        protected void SetHasProperty(string propertyName) => _properties.Add(propertyName);

        private string? _password;
        private string? _license;

        public string? Password
        {
            get => _password;
            set { _password = value; SetHasProperty(nameof(Password)); }
        }
        public string? License
        {
            get => _license;
            set { _license = value; SetHasProperty(nameof(License)); }
        }
    }
}
