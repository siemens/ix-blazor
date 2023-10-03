using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Dropdown
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? Anchor { get; set; }
        [Parameter]
        public dynamic CloseBehavior { get; set; } = "both";
        [Parameter]
        public string? Header { get; set; }
        [Parameter]
        public string Placement { get; set; } = "bottom-start";
        [Parameter]
        public string PositioningStrategy { get; set; } = "fixed";
        [Parameter]
        public bool Show { get; set; } = false;
        [Parameter]
        public bool SuppressAutomaticPlacement { get; set; } = false;
        [Parameter]
        public string? Trigger { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public EventCallback<bool> ShowChangedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "showChanged", "ShowChanged");
            }
        }

        [JSInvokable]
        public async void ShowChanged(bool value)
        {
            await ShowChangedEvent.InvokeAsync(value);
        }
    }
}
