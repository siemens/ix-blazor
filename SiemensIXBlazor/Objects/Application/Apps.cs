using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Objects.Application;

public class App
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Target { get; set; } = string.Empty;
    public string IconSrc { get; set; } = string.Empty;
}

