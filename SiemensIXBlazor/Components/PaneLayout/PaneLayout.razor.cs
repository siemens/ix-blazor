using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Pane;

namespace SiemensIXBlazor.Components
{
    public partial class PaneLayout
    {
        [Parameter]
        public bool Borderless { get; set; } = false;
        [Parameter]
        public string Layout { get; set; } = "full-vertical";
        [Parameter]
        public PaneVariant Variant { get; set; } = PaneVariant.inline;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

    }
}
