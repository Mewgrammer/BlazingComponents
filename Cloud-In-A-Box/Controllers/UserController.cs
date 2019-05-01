using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cloud_In_A_Box.Authentication.Handlers;
using Cloud_In_A_Box.Authentication.Models;
using Cloud_In_A_Box.Authentication.Interfaces;

namespace Cloud_In_A_Box.Controllers
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
