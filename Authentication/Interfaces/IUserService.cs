﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorEssentials.Authentication.Models;

namespace BlazorEssentials.Authentication.Interfaces
{
    public interface IUserService
    {
        UserState Register(UserCredentials credentials);
        Task<UserState> RegisterAsync(UserCredentials credentials);

        UserState Authenticate(UserCredentials credentials);
        Task<UserState> AuthenticateAsync(UserCredentials credentials);

    }
}
