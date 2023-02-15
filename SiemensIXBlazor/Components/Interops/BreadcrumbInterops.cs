using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiemensIXBlazor.Components.Interops
{
    public class BreadcrumbInterops : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public BreadcrumbInterops(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/SiemensIXBlazor/js/interops/breadcrumbJsInterop.js").AsTask());
        }

        public async Task AddItemClickedEventListener(Breadcrumb breadcrumb, string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("listenItemClickedEvent", DotNetObjectReference.Create(breadcrumb), id);
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
