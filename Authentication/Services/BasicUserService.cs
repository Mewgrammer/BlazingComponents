using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingComponents.Authentication.Interfaces;
using BlazingComponents.Authentication.Models;

namespace BlazingComponents.Authentication.Services
{
    public class BasicUserService : IUserService
    {
        public IList<User> Users { get; set; } = new List<User>();


        public UserState Register(UserCredentials credentials)
        {
            if (Users.Any(u => u.Username == credentials.Username)) return null;
            var securePassword = BCrypt.Net.BCrypt.HashPassword(credentials.Password);
            var newUser = new User { Username = credentials.Username, Id = Guid.NewGuid().ToString(), Password = securePassword, Role = EUserRole.Guest };
            Users.Add(newUser);
            return newUser.GetUserState();
        }

        public Task<UserState> RegisterAsync(UserCredentials credentials)
        {
            if (Users.Any(u => u.Username == credentials.Username)) return null;
            var securePassword = BCrypt.Net.BCrypt.HashPassword(credentials.Password);
            var newUser = new User { Username = credentials.Username, Id = Guid.NewGuid().ToString(), Password = securePassword, Role = EUserRole.Guest };
            Users.Add(newUser);
            return Task.FromResult(newUser.GetUserState());
        }

        public UserState Authenticate(UserCredentials credentials)
        {
            return Users.SingleOrDefault(u => u.Username == credentials.Username && BCrypt.Net.BCrypt.Verify(credentials.Password, u.Password)).GetUserState();
        }

        public Task<UserState> AuthenticateAsync(UserCredentials credentials)
        {
            return Task.FromResult(Users.SingleOrDefault(u => u.Username == credentials.Username && BCrypt.Net.BCrypt.Verify(credentials.Password, u.Password)).GetUserState());
        }
    }
}
