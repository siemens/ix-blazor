using Newtonsoft.Json;

namespace SiemensIXBlazor.Components.AGGrid
{
    public class ColumnDefs
    {
        [JsonProperty("field")]
        public string? Field { get; set; }
        [JsonProperty("headerName")]
        public string? HeaderName { get; set; }
        [JsonProperty("resizable")]
        public bool? Resizable { get; set; }
        [JsonProperty("checkboxSelection")]
        public bool? CheckboxSelection { get; set; }
        [JsonProperty("sortable")]
        public bool? Sortable { get; set; }
        [JsonProperty("filter")]
        public bool? Filter { get; set; }
    }
}
