using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using BlazingComponents.Authentication.Models;

namespace BlazingComponents.Authentication.Areas.Components
{
    public class LoginBase : ComponentBase
    {

        [CascadingParameter]
        public UserStateProvider UserState { get; set; }

        public string Username { get; set; } = "";
        public string Password { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        public event Action<bool, UserState> OnLoginResult;

        protected override void OnInit()
        {
            Username = UserState?.CurrentUser?.DisplayName;
        }

        public async void SignIn()
        {
            ErrorMessage = "";
            await UserState.SignIn(new UserCredentials { Username = Username, Password = Password });
            if (!UserState.IsLoggedIn)
            {
                ErrorMessage = "Login failed";
                OnLoginResult?.Invoke(false, UserState.CurrentUser);
            }
            else
            {
                OnLoginResult?.Invoke(true, UserState.CurrentUser);
            }
            StateHasChanged();
        }
    }
}
