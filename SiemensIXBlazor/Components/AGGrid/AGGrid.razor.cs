using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace SiemensIXBlazor.Components.AGGrid
{
    public partial class AGGrid
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        public async Task CreateGrid(GridOptions options)
        {
            if (Id == string.Empty)
            {
                return;
            }

            await JSRuntime.InvokeVoidAsync("agGridInterop.createGrid", Id, JsonConvert.SerializeObject(options));
        }
    }
}
