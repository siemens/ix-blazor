using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
    public partial class Tile
    {
        [Parameter]
        public string Size { get; set; } = "medium";
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
