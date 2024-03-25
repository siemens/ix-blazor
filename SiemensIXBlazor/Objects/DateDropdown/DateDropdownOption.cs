using Newtonsoft.Json;

namespace SiemensIXBlazor.Objects.DateDropdown;

public class
    DateDropdownOption
{
    [JsonProperty("id")] public string Id { get; set; } = null!;

    [JsonProperty("label")] public string Label { get; set; } = null!;

    [JsonProperty("from")] public string? From { get; set; }

    [JsonProperty("to")] public string? To { get; set; }
}