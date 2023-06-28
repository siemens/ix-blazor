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
        /// <summary>
        /// Subtitle of the about news. Default value is null.
        /// </summary>
        [Parameter]
        public string? AboutItemLabel { get; set; }
        [Parameter]
        public bool Expanded { get; set; } = false;
        [Parameter]
        public string I18NShowMore { get; set; } = "Show more";
        /// <summary>
        /// Title of the about news. Default value is null.
        /// </summary>
        [Parameter]
        public string? Label { get; set; }
        /// <summary>
        /// Bottom offset. Default value is: 0
        /// </summary>
        [Parameter]
        public int OffsetBottom { get; set; } = 0;
        /// <summary>
        /// Show about news. Default value is: false
        /// </summary>
        [Parameter]
        public bool Show { get; set; } = false;
        /// <summary>
        /// Popover closed event.
        /// </summary>
        [Parameter]
        public EventCallback ClosePopoverEvent { get; set; }
        /// <summary>
        /// Show more button is pressed event. Return value is: MouseEventArgs
        /// </summary>
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
