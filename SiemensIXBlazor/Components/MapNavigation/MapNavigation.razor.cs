using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components.MapNavigation
{
    public partial class MapNavigation
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? ApplicationName { get; set; }
        [Parameter]
        public bool HideContextMenu { get; set; } = true;
        [Parameter]
        public string? NavigationTitle { get; set; }
        [Parameter]
        public EventCallback ContextMenuClickEvent { get; set; }
        [Parameter]
        public EventCallback<bool> NavigationToggledEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "contextMenuClick", "ContextMenuClicked");
                await _interop.AddEventListener(this, Id, "navigationToggled", "CollapsedChanged");
            }
        }

        [JSInvokable]
        public async Task ContextMenuClicked()
        {
            await ContextMenuClickEvent.InvokeAsync();
        }

        [JSInvokable]
        public async Task NavigationToggled(bool status)
        {
            await NavigationToggledEvent.InvokeAsync(status);
        }
    }
}
