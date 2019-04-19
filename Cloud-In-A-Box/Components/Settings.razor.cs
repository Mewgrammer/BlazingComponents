using System.Threading.Tasks;
using Cloud_In_A_Box.Models;
using Cloud_In_A_Box.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Cloud_In_A_Box.Components
{
    public class SettingsBase : ComponentBase
    {
        protected AppSettings ApplicationSettings { get; set; }

        [Inject]
        public SettingsService SettingsService { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        protected override async Task OnInitAsync()
        {
            ApplicationSettings = SettingsService.Settings;
            await JsRuntime.InvokeAsync<string>("initSelects", "");
        }

        public string GetAppSettingsAsJson()
        {
            return JsonConvert.SerializeObject(ApplicationSettings, Formatting.Indented);
        }
    }
}
