using Microsoft.JSInterop;
using SiemensIXBlazor.Playground.Interops;

namespace SiemensIXBlazor.Playground.Services
{
    public class CodeHighlightService : ICodeHighlightService, IAsyncDisposable
    {
        private readonly PrismInterop _prismInterop;

        public CodeHighlightService(IJSRuntime jsRuntime)
        {
            _prismInterop = new PrismInterop(jsRuntime);
        }

        public async ValueTask HighlightAll()
        {
            await _prismInterop.HighlightAllAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _prismInterop.DisposeAsync();
        }
    }
}
