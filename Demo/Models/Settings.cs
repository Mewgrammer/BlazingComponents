namespace BlazorEssentials.Demo.Models
{
    public enum SettingsEnum
    {
        Option1 = 1,
        Option2 = 2,
        Option3 = 3,
        Option4 = 4,
        Option5 = 5,
        Option6 = 6,

    }

    public class AppSettings
    {
        public string TextOption1 { get; set; } = "";
        public string TextOption2 { get; set; } = "";
        public string TextOption3 { get; set; } = "";

        public int NumberOption1 { get; set; } = 0;
        public int NumberOption2 { get; set; } = 0;
        public int NumberOption3 { get; set; } = 0;

        public bool BoolOption1 { get; set; }
        public bool BoolOption2 { get; set; }
        public bool BoolOption3 { get; set; }

        public SettingsEnum EnumOption1 { get; set; } = SettingsEnum.Option1;
        public SettingsEnum EnumOption2 { get; set; } = SettingsEnum.Option2;
        public SettingsEnum EnumOption3 { get; set; } = SettingsEnum.Option3;
    }
}
