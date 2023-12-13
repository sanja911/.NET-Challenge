using System.Security.Cryptography;
using System.Text;
using DotnetWebApiWithEFCodeFirst.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projects.Models;
using Projects.Services;

namespace Auth.Controllers
{
    [Route("api/auth")]
    [ApiController]

    public class AuthController : Controller
    {
        public AuthService service;

        public AuthController(AuthService _service)
        {
            service = _service;
        }

        [HttpPost("register")]
        public ActionResult<User> Create([FromBody] User user)
        {
            var existingUsers = service.GetUsers(user.Email);
            if (existingUsers.Count > 0)
            {
                return Problem(
                    title: "This Email Already Exists",
                    detail: "This email is already exists and you can login using this email",
                    statusCode: StatusCodes.Status400BadRequest
                    );
            }
            var userData = service.Create(user);
            return Json(userData);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] User user)
        {
            var token = service.Authenticate(user.Email, user.Password);
            if (token == null)
                return Unauthorized();

            return Ok(new { token, user });
        }
    }
}