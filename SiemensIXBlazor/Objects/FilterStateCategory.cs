using Newtonsoft.Json;

namespace SiemensIXBlazor.Objects
{
    public class FilterStateCategory
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("value")]
        public string? Value { get; set; }
        [JsonProperty("operator")]
        public string? Operator { get; set; }
    }
}
