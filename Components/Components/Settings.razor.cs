using Cloud_In_A_Box.Components.Models;
using Cloud_In_A_Box.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud_In_A_Box.Components
{
    public class SettingsBase : ComponentBase
    {
        protected AppSettings AppSettings { get; set; }

        [Inject]
        public SettingsService SettingsService { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        protected async override Task OnInitAsync()
        {
            AppSettings = SettingsService.Settings;
            await JsRuntime.InvokeAsync<string>("initSelects", "");
        }

        public string GetAppSettingsAsJson()
        {
            return JsonConvert.SerializeObject(AppSettings, Formatting.Indented);
        }
    }
}
