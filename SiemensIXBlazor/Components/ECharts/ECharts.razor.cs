using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace SiemensIXBlazor.Components.ECharts
{
    public partial class ECharts
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        public async void InitialChart(dynamic options)
        {
            string serializedOptions = JsonConvert.SerializeObject(options);

            await JSRuntime.InvokeVoidAsync("initializeChart", Id, serializedOptions);
        }
    }
}
