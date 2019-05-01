using Cloud_In_A_Box.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_In_A_Box.Authentication.Interfaces
{
    public interface IUserService
    {
        UserState Register(UserCredentials credentials);
        Task<UserState> RegisterAsync(UserCredentials credentials);

        UserState Authenticate(UserCredentials credentials);
        Task<UserState> AuthenticateAsync(UserCredentials credentials);

    }
}
