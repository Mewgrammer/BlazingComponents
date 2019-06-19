using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazingComponents.Authentication.Models;

namespace BlazingComponents.Authentication.Interfaces
{
    public interface IUserService
    {
        Task<UserState> RegisterAsync(UserCredentials credentials);
        Task<UserState> LoginAsync(UserCredentials credentials);

    }
}
