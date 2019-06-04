using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BlazorEssentials.Authentication.Models;
using Newtonsoft.Json;

namespace BlazorEssentials.Authentication.Helpers
{
    public static class Authenticator
    {
        public static async Task<UserState> BasicAuth(string url, UserCredentials credentials)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{credentials.Username}:{credentials.Password}");
            UserState userState = null;
            using(var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                userState = JsonConvert.DeserializeObject<UserState>(result);
            }
            return userState;
        }
    }
}
