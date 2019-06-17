using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazingComponents.Authentication.Models;

namespace BlazingComponents.Authentication.Interfaces
{
    public interface IUserService
    {
        UserState Register(UserCredentials credentials);
        Task<UserState> RegisterAsync(UserCredentials credentials);

        UserState Authenticate(UserCredentials credentials);
        Task<UserState> AuthenticateAsync(UserCredentials credentials);

    }
}
