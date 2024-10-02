using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components.MapNavigationOverlay
{
    public partial class MapNavigationOverlay
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? Name { get; set; }
        [Parameter]
        public string? Icon { get; set; }

        [Parameter]
        public string? IconColor { get; set; }

        [Parameter]
        public EventCallback CloseButtonClickedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "closeClick", "CloseButtonClickedEvent");
            }
        }


        [JSInvokable]
        public async Task CloseClick()
        {
            await CloseButtonClickedEvent.InvokeAsync();
        }
    }
}
