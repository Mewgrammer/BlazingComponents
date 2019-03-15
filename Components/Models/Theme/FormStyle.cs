using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud_In_A_Box.Components.Models.Theme
{
    public class FormClass
    {
        public string CheckboxWrapper { get; set; } = "";
        public string Checkbox { get; set; } = "filled-in";
        public string CheckboxLabel { get; set; } = "";

        public string SwitchWrapper { get; set; } = "switch";
        public string Switch { get; set; } = "";
        public string SwitchLever { get; set; } = "lever";

        public string InputWrapper { get; set; } = "input-field";
        public string Input { get; set; } = "form-control validate";
        public string Select { get; set; } = "form-control";
        public string Option { get; set; } = "";
        public string Slider { get; set; } = "form-control";
        public string Radio { get; set; } = "form-control";
        public string Field { get; set; } = "form-group";
    }
}
