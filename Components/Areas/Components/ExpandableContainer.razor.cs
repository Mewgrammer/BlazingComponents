using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace BlazingComponents.Lib.Areas.Components
{
    public class ExpandableContainerBase : ComponentBase
    {
        [Parameter]
        public string AnimateExpandClass { get; set; } = "animated slideInLeft"; //https://daneden.github.io/animate.css/

        [Parameter]
        public string AnimateCollapseClass { get; set; } = "animated slideInRight"; //https://daneden.github.io/animate.css/

        [Parameter]
        public bool Expanded { get; set; } = true;

        [Parameter]
        public RenderFragment CollapsedContent { get; set; }

        [Parameter]
        public RenderFragment ExpandedContent { get; set; }


        public void Toggle()
        {
            Expanded = !Expanded;
        }

        public void Expand()
        {
            Expanded = true;
        }

        public void Collapse()
        {
            Expanded = false;
        }
    }
}
