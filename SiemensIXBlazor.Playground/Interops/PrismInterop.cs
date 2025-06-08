using Microsoft.JSInterop;

namespace SiemensIXBlazor.Playground.Interops
{
    public class PrismInterop : IAsyncDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference? _module;

        public PrismInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task HighlightAllAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("Prism.highlightAll");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error highlighting code: {ex.Message}");
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_module is not null)
            {
                await _module.DisposeAsync();
            }
        }
    }
}
