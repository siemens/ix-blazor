using Microsoft.JSInterop;

namespace SiemensIXBlazor.Interops
{
    public class BaseInterop
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public BaseInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/SiemensIXBlazor/js/interops/baseJsInterop.js").AsTask());
        }

        public async Task AddEventListener(object classObject, string id, string eventName, string callbackFunctionName)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("listenEvent", DotNetObjectReference.Create(classObject), id, eventName, callbackFunctionName);
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
