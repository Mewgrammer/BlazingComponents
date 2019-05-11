using Cloud_In_A_Box.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloud_In_A_Box.Authentication.Handlers;
using Cloud_In_A_Box.Authentication.Models;
using Cloud_In_A_Box.Authentication.Interfaces;

namespace Cloud_In_A_Box.Services
{
    public class UserService : IUserService
    {
        public IList<User> Users { get; set; } = new List<User>();


        public UserState Register(UserCredentials credentials)
        {
            var newUser = new User { Username = credentials.Username, Id = Guid.NewGuid().ToString(), Password = credentials.Password, Role = EUserRole.Guest };
            Users.Add(newUser);
            return newUser.GetUserState();
        }

        public Task<UserState> RegisterAsync(UserCredentials credentials)
        {
            var newUser = new User { Username = credentials.Username, Id = Guid.NewGuid().ToString(), Password = credentials.Password, Role = EUserRole.Guest };
            Users.Add(newUser);
            return Task.FromResult(newUser.GetUserState());
        }

        public UserState Authenticate(UserCredentials credentials)
        {
            return Users.SingleOrDefault(u => u.Username == credentials.Username && u.Password == credentials.Password).GetUserState();
        }

        public Task<UserState> AuthenticateAsync(UserCredentials credentials)
        {
            return Task.FromResult(Users.SingleOrDefault(u => u.Username == credentials.Username && u.Password == credentials.Password).GetUserState());
        }
    }
}
