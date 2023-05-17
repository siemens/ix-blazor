using Newtonsoft.Json;

namespace SiemensIXBlazor.Objects
{
    public class TreeContextNode
    {
        [JsonProperty("isExpanded")]
        public bool IsExpanded { get; set; }
        [JsonProperty("isSelected")]
        public bool IsSelected { get; set; }
    }
}
