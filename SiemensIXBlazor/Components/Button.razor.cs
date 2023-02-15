using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Elements;

namespace SiemensIXBlazor.Components
{
    public partial class Button
    {
        [Parameter]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string Label { get; set; } = string.Empty;
        [Parameter]
        public string Variant { get; set; } = "Primary";
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool Ghost { get; set; } = false;
        [Parameter]
        public bool Outline { get; set; } = false;
        [Parameter]
        public bool Selected { get; set; } = false;
        [Parameter]
        public string Type { get; set; } = "button";
        [Parameter]
        public ButtonIconElement? ButtonIcon { get; set; }
        [Parameter]
        public EventCallback ClickEvent { get; set; }

        private void Clicked()
        {
            ClickEvent.InvokeAsync();
        }
    }
}
