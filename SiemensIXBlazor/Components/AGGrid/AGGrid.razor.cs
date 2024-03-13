using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace SiemensIXBlazor.Components.AGGrid
{
    public partial class AGGrid
    {
        [Parameter]
        public EventCallback OnCellClicked { get; set; }

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        private DotNetObjectReference<AGGrid>? dotNetHelper;

        public async Task<IJSObjectReference?> CreateGrid(GridOptions options)
        {
            if (Id == string.Empty)
            {
                return null;
            }

            dotNetHelper = DotNetObjectReference.Create(this);

            return await JSRuntime.InvokeAsync<IJSObjectReference?>("agGridInterop.createGrid", dotNetHelper, Id, JsonConvert.SerializeObject(options));
        }

        public async Task<object?> GetSelectedRows(IJSObjectReference gird)
        {
            return await JSRuntime.InvokeAsync<Object>("agGridInterop.getSelectedRows", gird);
            
        }

        [JSInvokable]
        public async Task OnCellClickedCallback()
        {
            await OnCellClicked.InvokeAsync();
        }
    }
}
