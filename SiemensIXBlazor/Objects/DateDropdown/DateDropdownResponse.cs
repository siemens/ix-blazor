using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Objects.DateDropdown;

public class DateDropdownResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("from")]
    public string? From { get; set; }

    [JsonPropertyName("to")]
    public string? To { get; set; }
}