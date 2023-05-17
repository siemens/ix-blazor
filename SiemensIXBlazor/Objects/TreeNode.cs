using Newtonsoft.Json;

namespace SiemensIXBlazor.Objects
{
    public class TreeNode
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("data")]
        public TreeData? Data { get; set; }
        [JsonProperty("hasChildren")]
        public bool HasChildren { get; set; }
        [JsonProperty("children")]
        public List<string>? Children { get; set; }
    }
}
