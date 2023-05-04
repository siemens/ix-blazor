using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.MenuSettings
{
    public partial class MenuSettingsItem
    {
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
