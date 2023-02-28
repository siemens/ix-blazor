using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class SplitButton
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool Ghost { get; set; } = false;
        [Parameter]
        public string Icon { get; set; } = string.Empty;
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public bool Outline { get; set; } = false;
        [Parameter]
        public string Placement { get; set; } = "bottom-start";
        [Parameter]
        public string SplitIcon { get; set; } = "context-menu";
        [Parameter]
        public string Variant { get; set; } = "Primary";
        [Parameter]
        public EventCallback ButtonClickedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "buttonClick", "ButtonClicked");
            }
        }

        [JSInvokable]
        public async void ButtonClicked()
        {
            await ButtonClickedEvent.InvokeAsync();
        }
    }
}
