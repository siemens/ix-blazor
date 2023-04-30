using Newtonsoft.Json;

namespace SiemensIXBlazor.Objects
{
    public class Category
    {
        [JsonProperty("label")]
        public string? Label { get; set; }
        [JsonProperty("options")]
        public string[]? Options { get; set; }
    }
}
