using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Objects.Pane
{
    public  class PaneBorderlessChangedEventResponse
    {
        [JsonPropertyName("slot")]
        public string Slot { get; set; }
        [JsonPropertyName("borderless")]
        public string Borderless { get; set; }
    }
}
