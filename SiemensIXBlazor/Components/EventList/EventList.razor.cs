using Microsoft.AspNetCore.Components;

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
    }
}
