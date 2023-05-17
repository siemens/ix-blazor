using Newtonsoft.Json;

namespace SiemensIXBlazor.Objects
{
    public class TreeNodeToggledEventResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("isExpaned")]
        public bool IsExpaned { get; set; }
    }
}
