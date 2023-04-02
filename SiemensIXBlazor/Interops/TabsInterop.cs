using Microsoft.JSInterop;

namespace SiemensIXBlazor.Interops
{
    internal class TabsInterop
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public TabsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/SiemensIXBlazor/js/interops/tabsInterop.js").AsTask());
        }

        public async Task InitialComponent(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("initalTable", id);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
