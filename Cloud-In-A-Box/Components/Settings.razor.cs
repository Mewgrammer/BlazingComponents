using System.Threading.Tasks;
using Cloud_In_A_Box.Models;
using Cloud_In_A_Box.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace Cloud_In_A_Box.Components
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
