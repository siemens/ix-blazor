using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
    public partial class BreadcrumbItem
    {
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string? Label { get; set; }
    }
}
