using Cloud_In_A_Box.Components.Areas.Authentication;
using Cloud_In_A_Box.Components.Areas.Authentication.Handlers;
using Cloud_In_A_Box.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud_In_A_Box.Services
{
    public class UserService : IUserService
    {
        public IEnumerable<User> Users { get; set; } = new List<User>();

        public UserService()
        {

        }

        public UserState Authenticate(UserCredentials credentials)
        {
            return new UserState { DisplayName = "Test User", Id = "Test-0", IsLoggedIn = false };
        }

        public Task<UserState> AuthenticateAsync(UserCredentials credentials)
        {
            return Task.FromResult(new UserState { DisplayName = "Test User", Id = "Test-0", IsLoggedIn = false });
        }
    }
}
