using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components.NavigationMenu
{
    public partial class NavigationMenuAvatarItem
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> ItemClickedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "itemClick", "ItemClicked");
            }
        }

        [JSInvokable]
        public async Task ItemClicked(MouseEventArgs args)
        {
            await ItemClickedEvent.InvokeAsync(args);
        }
    }
}
