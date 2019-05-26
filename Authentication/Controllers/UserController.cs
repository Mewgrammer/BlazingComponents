using System.Threading.Tasks;
using BlazorEssentials.Authentication.Interfaces;
using BlazorEssentials.Authentication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEssentials.Authentication.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private static UserState LoggedOutState = new UserState { IsLoggedIn = false };

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("user")]
        public UserState GetUser()
        {
            return User.Identity.IsAuthenticated
                ? new UserState { IsLoggedIn = true, DisplayName = User.Identity.Name }
                : LoggedOutState;
        }

        [HttpGet("login")]
        public async Task<ActionResult<UserState>> Login()
        {
            return User.Identity.IsAuthenticated
                ? new UserState { IsLoggedIn = true, DisplayName = User.Identity.Name }
                : LoggedOutState;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserState>> Register(UserCredentials userCredentials)
        {
            var user =  await _userService.RegisterAsync(userCredentials);
            if(user == null)
            {
                return Conflict(user);
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPut("logout")]
        public async Task<ActionResult<UserState>> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LoggedOutState;
        }
    }
}
