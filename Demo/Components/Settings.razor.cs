using BlazorEssentials.Demo.Models;
using BlazorEssentials.Demo.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BlazorEssentials.Demo.Components
{
    public class SettingsBase : ComponentBase
    {
        protected AppSettings ApplicationSettings { get; set; }

        [Inject]
        public SettingsService SettingsService { get; set; }

        protected override void OnInit()
        {
            ApplicationSettings = SettingsService.Settings;
        }

        public string GetAppSettingsAsJson()
        {
            return JsonConvert.SerializeObject(ApplicationSettings, Formatting.Indented);
        }

        public void ApplySettings()
        {
            StateHasChanged();
        }
    }
}
