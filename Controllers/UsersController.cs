using DailyAuto.Models;
using DailyAuto.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyAuto.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <returns>Returns User</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the user is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> CreateUser(User user)
        {
            var model = _usersService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = model.Id }, model);
        }

        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> GetUserById(long id)
        {
            var model = _usersService.GetUserById(id);

            if (model is null)
                return NotFound();

            return Ok(model);
        }

        /// <summary>
        /// Updates user info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateUser(long id, User user)
        {
            var existingUser = _usersService.GetUserById(id);

            if (existingUser is null)
                return NotFound();

            var model = _usersService.UpdateUser(id, user);
            return NoContent();
        }


        /*private static UserDTO ModelToDTO(User user) =>
            new UserDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };*/
    }

    public class CreateUserRequest { }
}
