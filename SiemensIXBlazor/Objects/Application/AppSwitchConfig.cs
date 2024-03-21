using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Objects.Application;

public class AppSwitchConfig
{
    public string CurrentAppId { get; set; } = string.Empty;
    public List<App> Apps { get; set; } = [];
    public string? I18nAppSwitch { get; set; }
    public string? I18nLoadingApps { get; set; }

}