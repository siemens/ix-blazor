using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.NavigationMenu
{
    public partial class NavigationMenuItem
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool? Active { get; set; }
        [Parameter]
        public bool? Disabled { get; set; }
        [Parameter]
        public bool Home { get; set; } = false;
        [Parameter]
        public int? Notifications { get; set; }
        [Parameter]
        public string TabIcon { get; set; } = "document";
    }
}
