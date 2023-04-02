using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
    public partial class DropdownItem
    {
        [Parameter]
        public string Label { get; set; } = string.Empty;
        [Parameter]
        public string Value { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<string> OnClickEvent { get; set; }

        private async void Clicked(string label)
        {
            await OnClickEvent.InvokeAsync(label);
        }
    }
}
