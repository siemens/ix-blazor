using Microsoft.AspNetCore.Components;

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
        public bool Outline { get; set; } = false;
        [Parameter]
        public bool? Oval { get; set; }
        [Parameter]
        public bool Selected { get; set; } = false;
        [Parameter]
        public string Size { get; set; } = "24";
        [Parameter]
        public string DataTooltip { get; set; } = string.Empty;
        [Parameter]
        public string Type { get; set; } = "button";
        [Parameter]
        public string Variant { get; set; } = "Primary";
        [Parameter]
        public EventCallback ClickEvent { get; set; }

        private void Clicked()
        {
            ClickEvent.InvokeAsync();
        }
    }
}
