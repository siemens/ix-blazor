using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace SiemensIXBlazor.Components.AGGrid
{
	public partial class AGGrid
	{
		[Parameter, EditorRequired]
		public string Id { get; set; } = string.Empty;

		public async Task<IJSObjectReference?> CreateGrid(GridOptions options)
		{
			if (Id == string.Empty)
			{
				return null;
			}

			return await JSRuntime.InvokeAsync<IJSObjectReference?>("agGridInterop.createGrid", Id, JsonConvert.SerializeObject(options));
		}
	}
}
