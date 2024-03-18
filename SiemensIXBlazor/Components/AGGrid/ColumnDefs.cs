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
        [JsonProperty("suppressSizeToFit")]
        public bool? SuppressSizeToFit { get; set; }
        [JsonProperty("suppressAutoSize")]
        public bool? SuppressAutoSize { get; set; }
        [JsonProperty("flex")]
        public int? Flex { get; set; }
        [JsonProperty("width")]
        public int? Width { get; set; }
        [JsonProperty("maxWidth")]
        public int? MaxWidth { get; set; }
        [JsonProperty("minWidth")]
        public int? MinWidth { get; set; }
        [JsonProperty("hide")]
        public bool? Hide { get; set; }
        [JsonProperty("lockPosition")]
        public string? LockPosition { get; set; }
        [JsonProperty("suppressMovable")]
        public bool? SuppressMovable {  get; set; }


    }
}
