using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.LayoutGrid
{
    public partial class LayoutGrid
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public int Columns { get; set; } = 12;
        [Parameter]
        public string Gap { get; set; } = "24";
        [Parameter]
        public bool NoMargin { get; set; } = false;
    }
}
