﻿using Microsoft.AspNetCore.Components.Routing;

namespace BlazingComponents.Lib.Models
{
    public class NavLinkItem
    {
        public string DisplayName { get; set; } = "NavLink";
        public string Href { get; set; } = "";
        public string Icon { get; set; } = "fas fa-link"; // https://fontawesome.com/icons?d=gallery
        public NavLinkMatch Match { get; set; } = NavLinkMatch.All;
    }
}
