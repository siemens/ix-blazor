using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Elements;

namespace SiemensIXBlazor.Components
{
    public partial class DropdownButton
    {
        [Parameter]
        public string Id { get; set; } = string.Empty;
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
        public string Variant { get; set; } = "Primary";
        [Parameter]
        public IEnumerable<DropdownItemElement>? DropdownItemElements { get; set; }
        [Parameter]
        public EventCallback<string> OnClickEvent { get; set; }

        private async void Clicked(string label)
        {
            await OnClickEvent.InvokeAsync(label);
        }
    }
}
