using EmployeeInformation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static Model.Enums;

namespace EmployeeInformation.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// User service.
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService) => _userService = userService;

        /// <summary>
        /// Authenticates user.
        /// </summary>
        /// <param name="Username">Registered username.</param>
        /// <param name="Password">Registered password.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate(string Username, [DataType(DataType.Password)]  string Password)
        {
            var user = _userService.Authenticate(Username, Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }

        /// <summary>
        /// Registers user.
        /// </summary>
        /// <param name="Username">Required username.</param>
        /// <param name="Password">Required password.</param>
        /// <param name="Role">User role.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult CreateUser(string Username, [DataType(DataType.Password)] string Password, Role Role)
        {
            if (string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password)
                && string.IsNullOrWhiteSpace(Role.ToString()))
                return BadRequest(ModelState);

            if (!_userService.Register(Username, Password, Role))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the user {Username}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return Ok($"User created. {Username}");
        }
    }
}
