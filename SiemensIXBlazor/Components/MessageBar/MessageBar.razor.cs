using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.MessageBar;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class MessageBar
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Dismissible { get; set; } = true;
        [Parameter]
        public MessageBarType? Type { get; set; }
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
