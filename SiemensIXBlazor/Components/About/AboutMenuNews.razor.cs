using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components.About
{
    public partial class AboutMenuNews
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? AboutItemLabel { get; set; }
        [Parameter]
        public bool Expanded { get; set; } = false;
        [Parameter]
        public string I18NShowMore { get; set; } = "Show more";
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public int OffsetBottom { get; set; } = 0;
        [Parameter]
        public bool Show { get; set; } = false;
        [Parameter]
        public EventCallback ClosePopoverEvent { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> ShowMoreEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "closePopover", "ClosePopover");
                await _interop.AddEventListener(this, Id, "showMore", "ShowMore");
            }
        }

        [JSInvokable]
        public async Task ClosePopover()
        {
            await ClosePopoverEvent.InvokeAsync();
        }

        [JSInvokable]
        public async Task ShowMore(MouseEventArgs args)
        {
            await ShowMoreEvent.InvokeAsync(args);
        }
    }
}
