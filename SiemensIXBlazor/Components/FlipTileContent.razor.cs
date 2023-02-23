using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
    public partial class FlipTileContent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
