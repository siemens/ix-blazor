using Microsoft.JSInterop;
using Newtonsoft.Json;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Components
{
    public partial class Toast
    {
        public async void ShowToast(ToastConfig config)
        {
            await JSRuntime.InvokeVoidAsync("showMessage", JsonConvert.SerializeObject(config));
        }
    }
}
