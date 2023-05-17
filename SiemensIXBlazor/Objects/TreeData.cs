using Newtonsoft.Json;

namespace SiemensIXBlazor.Objects
{
    public class TreeData
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
