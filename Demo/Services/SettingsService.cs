using BlazingComponents.Demo.Models;

namespace BlazingComponents.Demo.Services
{
    public class SettingsService
    {
        public AppSettings Settings { get; set; } = new AppSettings();

        public SettingsService()
        {

        }
    }
}
