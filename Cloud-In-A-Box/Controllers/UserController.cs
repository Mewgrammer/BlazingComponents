using Cloud_In_A_Box.Components.Areas.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cloud_In_A_Box.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private static UserState LoggedOutState = new UserState { IsLoggedIn = false };

        [HttpGet("user")]
        public UserState GetUser()
        {
            return User.Identity.IsAuthenticated
                ? new UserState { IsLoggedIn = true, DisplayName = User.Identity.Name }
                : LoggedOutState;
        }

        [HttpGet("login")]
        public async Task<UserState> Login()
        {
            return User.Identity.IsAuthenticated
                ? new UserState { IsLoggedIn = true, DisplayName = User.Identity.Name }
                : LoggedOutState;
        }

        [HttpPut("logout")]
        public async Task<UserState> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LoggedOutState;
        }
    }
}
