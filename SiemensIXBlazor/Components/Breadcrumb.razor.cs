using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;
using SiemensIXBlazor.Elements;

namespace SiemensIXBlazor.Components
{
    public partial class Breadcrumb
    {
        [Parameter]
        public IEnumerable<BreadcrumbElement>? Elements { get; set; }
        [Parameter]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<string> ItemClicked { get; set; }
        [Parameter]
        public bool Ghost { get; set; } = false;
        [Parameter]
        public string[] NextItems { get; set; } = Array.Empty<string>();
        [Parameter]
        public int VisibleItemCount { get; set; } = 9;

        private BreadcrumbInterops _breadcrumbInterops;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _breadcrumbInterops = new(JSRuntime);

                await _breadcrumbInterops.AddItemClickedEventListener(this, Id);
            }
        }

        [JSInvokable]
        public async Task BreadcrumbItemClicked(string label)
        {
            await ItemClicked.InvokeAsync(label);
        }
    }
}
