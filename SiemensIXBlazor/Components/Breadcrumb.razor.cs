using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;
using SiemensIXBlazor.Elements;

namespace SiemensIXBlazor.Components
{
    public partial class Breadcrumb
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<string> ItemClicked { get; set; }
        [Parameter]
        public bool Ghost { get; set; } = false;
        [Parameter]
        public string[] NextItems { get; set; } = Array.Empty<string>();
        [Parameter]
        public int VisibleItemCount { get; set; } = 9;

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "itemClick", "BreadcrumbItemClicked");
            }
        }

        [JSInvokable]
        public async Task BreadcrumbItemClicked(string label)
        {
            await ItemClicked.InvokeAsync(label);
        }
    }
}
