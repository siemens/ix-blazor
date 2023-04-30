using Newtonsoft.Json;

namespace SiemensIXBlazor.Objects
{
    public class FilterState
    {
        [JsonProperty("tokens")]
        public string[]? Tokens { get; set; }
        [JsonProperty("categories")]
        public FilterStateCategory[]? Categories { get; set; }
    }
}
