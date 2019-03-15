using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud_In_A_Box.Components.Models.Theme
{
    public class ThemeInfo
    {
        public BackgroundStyle Background { get; set; } = new BackgroundStyle();
        public ButtonClass Button { get; set; } = new ButtonClass();
        public TableClass Table { get; set; } = new TableClass();
        public FormClass Form { get; set; } = new FormClass();
    }
}
