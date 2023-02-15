using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Blind
    {
        [Parameter]
        public string Label { get; set; } = string.Empty;
        [Parameter]
        public string Content { get; set; } = string.Empty;
        [Parameter]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public bool Collapsed { get; set; } = false;
        [Parameter]
        public EventCallback<bool> CollapsedChangedEvent { get; set; }

        private BlindInterops _blindInterops;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _blindInterops = new(JSRuntime);

                await _blindInterops.AddCollapsedChangedEventListener(this, Id);
            }
        }

        [JSInvokable]
        public async Task CollapsedChanged(bool value)
        {
            await CollapsedChangedEvent.InvokeAsync(value);
        }
    }
}
