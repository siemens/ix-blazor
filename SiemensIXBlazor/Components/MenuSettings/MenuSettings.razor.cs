using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components.MenuSettings
{
    public partial class MenuSettings
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? ActiveTabLabel { get; set; }
        [Parameter]
        public string Label { get; set; } = "Settings";
        [Parameter]
        public bool Show { get; set; } = false;
        [Parameter]
        public EventCallback<MouseEventArgs> ClosedEvent { get; set; }

        private BaseInterop _interop;
        private Lazy<Task<IJSObjectReference>>? moduleTask;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "close", "Closed");

                moduleTask = new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/SiemensIXBlazor/js/interops/settingsMenuInterop.js").AsTask());
            }
        }

        [JSInvokable]
        public async Task Closed(MouseEventArgs args)
        {
            await ClosedEvent.InvokeAsync(args);
        }

        public async Task ToggleSettings(bool status)
        {
            var module = await moduleTask.Value;
            if (module != null)
            {
                await module.InvokeVoidAsync("toggleSettings", Id, status);
            };
        }
    }
}
