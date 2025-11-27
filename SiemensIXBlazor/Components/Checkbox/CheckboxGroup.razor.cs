using SiemensIXBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.Checkbox
{
    public partial class CheckboxGroup
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? InfoText { get; set; }

        [Parameter]
        public string? WarningText { get; set; }

        [Parameter]
        public string? InvalidText { get; set; }

        [Parameter]
        public string? ValidText { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}