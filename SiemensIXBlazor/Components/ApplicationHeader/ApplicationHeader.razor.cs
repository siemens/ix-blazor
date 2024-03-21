using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
    public partial class ApplicationHeader
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string? Name { get; set; }
    }
}
