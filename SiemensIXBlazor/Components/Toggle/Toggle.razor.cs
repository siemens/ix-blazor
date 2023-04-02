using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Toggle
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public bool Checked { get; set; } = false;
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool HideText { get; set; } = false;
        [Parameter]
        public bool Indeterminate { get; set; } = false;
        [Parameter]
        public string TextIndeterminate { get; set; } = "Mixed";
        [Parameter]
        public string TextOff { get; set; } = "Off";
        [Parameter]
        public string TextOn { get; set; } = "On";
        [Parameter]
        public EventCallback<bool> CheckedChangeEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "checkedChange", "CheckedChannged");
            }
        }

        [JSInvokable]
        public async void CheckedChannged(bool value)
        {
            await CheckedChangeEvent.InvokeAsync(value);
        }
    }
}
