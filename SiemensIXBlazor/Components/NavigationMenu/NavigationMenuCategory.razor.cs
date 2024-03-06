using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.NavigationMenu
{
    public partial class NavigationMenuCategory: IXBaseComponent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public int? Notifications { get; set; }
    }
}
