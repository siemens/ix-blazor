using Newtonsoft.Json;

namespace SiemensIXBlazor.Components.AGGrid
{
    public class GridOptions
    {
        [JsonProperty("columnDefs")]
        public List<ColumnDefs>? ColumnDefs { get; set; }
        [JsonProperty("rowData")]
        public List<Dictionary<string, dynamic>>? RowData { get; set; }
        [JsonProperty("rowSelection")]
        public string? RowSelection { get; set; }
        [JsonProperty("suppressCellFocus")]
        public bool? SuppressCellFocus { get; set; }
        [JsonProperty("checkboxSelection")]
        public bool? CheckboxSelection { get; set; }
        [JsonProperty("overlayLoadingTemplate")]
        public string? OverlayLoadingTemplate { get; set; }
        [JsonProperty("overlayNoRowsTemplate")]
        public string? OverlayNoRowsTemplate { get; set; }
    }
}
