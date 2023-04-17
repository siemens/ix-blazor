using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Tile;

namespace SiemensIXBlazor.Components
{
    public partial class Tile
    {
        [Parameter]
        public TileSize? Size { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
