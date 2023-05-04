using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.About
{
    public partial class AboutMenuItem
    {
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
