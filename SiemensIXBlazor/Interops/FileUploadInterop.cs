using Microsoft.JSInterop;

namespace SiemensIXBlazor.Interops
{
    internal class FileUploadInterop
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public FileUploadInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/SiemensIXBlazor/js/interops/fileUploadInterop.js").AsTask());
        }

        public async Task AddEventListener(object classObject, string id, string eventName, string callbackFunctionName)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("fileUploadEventHandler", DotNetObjectReference.Create(classObject), id, eventName, callbackFunctionName);
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
