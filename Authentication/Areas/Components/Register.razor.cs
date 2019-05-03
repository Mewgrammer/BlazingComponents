using Cloud_In_A_Box.Authentication.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud_In_A_Box.Authentication.Areas.Components
{
    public class RegisterBase : ComponentBase
    {
        [CascadingParameter]
        public UserStateProvider UserState { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ErrorMessage { get; set; } = "";

        public event Action<bool, UserState> OnRegistrationResult;


        public async void RegisterUser()
        {
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords need to match";
                StateHasChanged();
                return;
            }
            ErrorMessage = "";
            await UserState.Register(new UserCredentials { Username = Username, Password = Password });
            if (UserState.CurrentUser == null)
            {
                ErrorMessage = "Registration failed";
                OnRegistrationResult?.Invoke(false, UserState.CurrentUser);
            }
            else
            {
                OnRegistrationResult?.Invoke(true, UserState.CurrentUser);
                UriHelper.NavigateTo("/login");
            }
            StateHasChanged();
        }
    }
}
