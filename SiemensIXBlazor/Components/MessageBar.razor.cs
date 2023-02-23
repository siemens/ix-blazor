using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class MessageBar
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Dismissible { get; set; } = true;
        [Parameter]
        public string Type { get; set; } = "info";
        [Parameter]
        public EventCallback ClosedChangeEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "closedChange", "ClosedChange");
            }
        }

        [JSInvokable]
        public async void ClosedChange()
        {
            await ClosedChangeEvent.InvokeAsync();
        } 
    }
}
