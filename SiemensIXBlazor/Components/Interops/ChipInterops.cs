using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiemensIXBlazor.Components.Interops
{
    public class ChipInterops : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ChipInterops(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/SiemensIXBlazor/js/interops/chipJsInterop.js").AsTask());
        }

        public async Task AddClosedEventListener(Chip chip, string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("listenClosedEvent", DotNetObjectReference.Create(chip), id);
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
