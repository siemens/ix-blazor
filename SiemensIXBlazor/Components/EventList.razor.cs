using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiemensIXBlazor.Components
{
    public partial class EventList
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Animated { get; set; } = true;
        [Parameter]
        public bool? Chevron { get; set; }
        [Parameter]
        public bool Compact { get; set; } = false;
        [Parameter]
        public string ItemHeight { get; set; } = "S";
        [Parameter]
        public string Class { get; set; } = string.Empty;
    }
}
