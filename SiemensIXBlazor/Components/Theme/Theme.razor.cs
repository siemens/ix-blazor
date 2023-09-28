using Microsoft.JSInterop;

namespace SiemensIXBlazor.Components
{
    public partial class Theme
    {
        public async Task SetTheme(string theme)
        {
            Console.WriteLine(theme);
            await JSRuntime.InvokeVoidAsync("setTheme", theme);
        }

        public async Task ToggleTheme()
        {
            await JSRuntime.InvokeVoidAsync("toggleTheme");
        }

        public async Task ToggleSystemTheme(bool useSystemTheme)
        {
            await JSRuntime.InvokeVoidAsync("toggleSystemTheme", useSystemTheme);
        }
    }
}
