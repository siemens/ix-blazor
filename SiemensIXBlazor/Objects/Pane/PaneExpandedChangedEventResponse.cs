using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Objects.Pane
{
    public  class PaneExpandedChangedEventResponse
    {
        [JsonPropertyName("slot")]
        public string Slot { get; set; }
        [JsonPropertyName("expanded")]
        public string Expanded { get; set; }
    }
}
