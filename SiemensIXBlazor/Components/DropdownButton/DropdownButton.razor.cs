using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Button;

namespace SiemensIXBlazor.Components
{
    public partial class DropdownButton
    {
        [Parameter]
        public bool Active { get; set; } = false;
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool Ghost { get; set; } = false;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string Label { get; set; } = string.Empty;
        [Parameter]
        public bool Outline { get; set; } = false;
        [Parameter]
        public ButtonVariant Variant { get; set; } = ButtonVariant.Primary;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        
    }
}
