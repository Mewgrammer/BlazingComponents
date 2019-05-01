using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Cloud_In_A_Box.Components.Areas.Authentication
{

    public class UserStateProviderBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public UserState CurrentUser { get; private set; }

        public bool IsLoggedIn => CurrentUser?.IsLoggedIn ?? false;

        protected override async Task OnInitAsync()
        {
            var url = HttpClient.BaseAddress.AbsoluteUri + "user";
            CurrentUser = await HttpClient.GetJsonAsync<UserState>(url);
        }

        public async Task SignIn(UserCredentials credentials)
        {
            var url = HttpClient.BaseAddress.AbsoluteUri + "login";
            var newUserState = await Authenticator.BasicAuth(url, credentials);
            CurrentUser = newUserState;
            StateHasChanged();
        }

        public async Task SignOut()
        {
            // Transition to "loading" state synchronously, then asynchronously update
            CurrentUser = null;
            StateHasChanged();
            var url = HttpClient.BaseAddress.AbsoluteUri + "logout";
            CurrentUser = await HttpClient.PutJsonAsync<UserState>(url, null);
            StateHasChanged();
        }
    }
}
