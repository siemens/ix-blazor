using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.Button;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components.ToggleButton
{
    public partial class ToggleButton
    {
        [Parameter, EditorRequired]
        public string Id { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
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
        public bool Pressed { get; set; } = false;
        [Parameter]
        public ButtonVariant Variant { get; set; } = ButtonVariant.secondary;
        [Parameter]
        public EventCallback<bool> PressedChangeEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "pressedChange", "PressedChange");
            }
        }

        [JSInvokable]
        public async void PressedChange(bool value)
        {
            await PressedChangeEvent.InvokeAsync(value);
        }
    }
}
