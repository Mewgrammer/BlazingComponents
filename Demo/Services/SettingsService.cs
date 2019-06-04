using BlazorEssentials.Demo.Models;

namespace BlazorEssentials.Demo.Services
{
    public class SettingsService
    {
        public AppSettings Settings { get; set; } = new AppSettings();

        public SettingsService()
        {

        }
    }
}
