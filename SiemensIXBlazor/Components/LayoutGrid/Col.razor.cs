using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.LayoutGrid
{
    public partial class Col
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string? Size { get; set; }
        [Parameter]
        public string? SizeLg { get; set; }
        [Parameter]
        public string? SizeMd { get; set; }
        [Parameter]
        public string? SizeSm { get; set; }
    }
}
