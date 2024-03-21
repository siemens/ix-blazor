using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components.NavigationMenu
{
    public partial class NavigationMenuAvatar
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? Bottom { get; set; }
        [Parameter]
        public string I18NLogout { get; set; } = "Logout";
        [Parameter]
        public string? Image { get; set; }
        [Parameter]
        public string? Initials { get; set; }
        [Parameter]
        public bool ShowLogoutButton { get; set; } = true;
        [Parameter]
        public string? Top { get; set; }
        [Parameter]
        public EventCallback LogoutClickedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "logoutClick", "LogoutClicked");
            }
        }

        [JSInvokable]
        public async Task LogoutClicked()
        {
            await LogoutClickedEvent.InvokeAsync();
        }
    }
}
