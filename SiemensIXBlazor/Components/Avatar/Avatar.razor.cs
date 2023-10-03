using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.Avatar
{
    public partial class Avatar
    {
        [Parameter]
        public string? Image { get; set; }
        [Parameter]
        public string? Initials { get; set; }
    }
}
