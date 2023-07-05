using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.ContentHeader;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class ContentHeader
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool HasBackButton { get; set; } = false;
        [Parameter]
        public string? HeaderSubTitle { get; set; }
        [Parameter]
        public string? HeaderTitle { get; set; }
        [Parameter]
        public ContentHeaderVariant Variant { get; set; } = ContentHeaderVariant.Primary;
        [Parameter]
        public EventCallback BackButtonClickedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "backButtonClick", "BackButtonClicked");
            }
        }

        [JSInvokable]
        public async void BackButtonClicked()
        {
            await BackButtonClickedEvent.InvokeAsync();
        }
    }
}
