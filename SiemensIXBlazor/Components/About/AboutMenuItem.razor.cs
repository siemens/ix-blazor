using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.About
{
    public partial class AboutMenuItem
    {
        /// <summary>
        /// About Item label. Default value is null.
        /// </summary>
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
