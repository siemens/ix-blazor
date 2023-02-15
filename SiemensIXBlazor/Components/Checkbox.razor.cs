using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
    public partial class Checkbox
    {
        [Parameter]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string Label { get; set; } = string.Empty;
        [Parameter]
        public bool Checked { get; set; } = false;
        [Parameter]
        public EventCallback<bool> OnChangeEvent { get; set; }

        public bool Value { get; set; }

        private void OnChange()
        {
            Checked = Value;
            OnChangeEvent.InvokeAsync(Value);
        }
    }
}
