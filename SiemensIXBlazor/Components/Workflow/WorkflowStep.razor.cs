using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Workflow;

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
        public WorkflowPosition Position { get; set; } = WorkflowPosition.Undefined;
        [Parameter]
        public bool Selected { get; set; } = false;
        [Parameter]
        public WorkflowStatus Status { get; set; } = WorkflowStatus.Open;
        [Parameter]
        public bool Vertical { get; set; } = false;
    }
}
