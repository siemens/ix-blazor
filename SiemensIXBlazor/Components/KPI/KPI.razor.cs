using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
    public partial class KPI
    {
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public string Orientation { get; set; } = "horizontal";
        [Parameter]
        public string State { get; set; } = "neutral";
        [Parameter]
        public string? Unit { get; set; }
        [Parameter]
        public string? Value { get; set; }
    }
}
