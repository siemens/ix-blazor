using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Objects.Pane
{
    public  class PaneVariantChangedEventResponse
    {
        [JsonPropertyName("slot")]
        public string Slot { get; set; }
        [JsonPropertyName("variant")]
        public string Variant { get; set; }
    }
}
