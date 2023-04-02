using Microsoft.JSInterop;

namespace SiemensIXBlazor.Components
{
    public partial class Toast
    {
        public async void ShowToast(string message, string messageSeverity)
        {
            await JSRuntime.InvokeVoidAsync("showMessage", message, messageSeverity);
        }
    }
}
