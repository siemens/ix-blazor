using Microsoft.JSInterop;

namespace SiemensIXBlazor.Components
{
    public class BlindInterops : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public BlindInterops(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/SiemensIXBlazor/js/interops/blindJsInterop.js").AsTask());
        }

        public async Task AddCollapsedChangedEventListener(Blind blind, string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("listenCollapsedEvent", DotNetObjectReference.Create(blind), id);
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
