using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Button;

namespace SiemensIXBlazor.Components
{
    public partial class IconButton
    {
        [Parameter]
        public string? Color { get; set; }
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool Ghost { get; set; } = false;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public bool Loading { get; set; } = false;
        [Parameter]
        public bool Outline { get; set; } = false;
        [Parameter]
        public bool? Oval { get; set; }
        [Parameter]
        public IconButtonSize Size { get; set; } = IconButtonSize._24;
        [Parameter]
        public bool Selected { get; set; } = false;
        [Parameter]
        public string DataTooltip { get; set; } = string.Empty;
        [Parameter]
        public ButtonType Type { get; set; } = ButtonType.Button;
        [Parameter]
        public ButtonVariant Variant { get; set; } = ButtonVariant.primary;
        [Parameter]
        public EventCallback ClickEvent { get; set; }

        private void Clicked()
        {
            ClickEvent.InvokeAsync();
        }
    }
}
