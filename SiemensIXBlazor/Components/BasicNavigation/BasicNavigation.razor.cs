using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.BasicNavigation
{
    public partial class BasicNavigation
    {
        [Parameter]
        public string? ApplicationName { get; set; }
        [Parameter]
        public bool HideHeader { get; set; } = false;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
