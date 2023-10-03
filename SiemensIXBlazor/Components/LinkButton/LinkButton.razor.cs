using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.LinkButton;

namespace SiemensIXBlazor.Components
{
    public partial class LinkButton
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public LinkButtonTarget Target { get; set; } = LinkButtonTarget._self;
        [Parameter]
        public string? Url { get; set; }
    }
}
