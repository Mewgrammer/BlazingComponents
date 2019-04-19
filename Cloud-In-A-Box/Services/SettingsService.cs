using Cloud_In_A_Box.Components.Models;
using Cloud_In_A_Box.Models;

namespace Cloud_In_A_Box.Services
{
    public class SettingsService
    {
        public AppSettings Settings { get; set; } = new AppSettings();

        public SettingsService()
        {

        }
    }
}
