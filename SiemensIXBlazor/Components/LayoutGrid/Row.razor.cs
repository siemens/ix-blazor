using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.LayoutGrid
{
    public partial class Row
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
