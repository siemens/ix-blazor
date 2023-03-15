using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
    public partial class WorkflowStep
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Clickable { get; set; } = false;
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public string? Position { get; set; }
        [Parameter]
        public bool Selected { get; set; } = false;
        [Parameter]
        public string Status { get; set; } = "open";
        [Parameter]
        public bool Vertical { get; set; } = false;
    }
}
