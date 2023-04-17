using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.KPI;

namespace SiemensIXBlazor.Components
{
    public partial class KPI
    {
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public KpiOrientation Orientation { get; set; } = KpiOrientation.Horizontal;
        [Parameter]
        public KpiState State { get; set; } = KpiState.Neutral;
        [Parameter]
        public string? Unit { get; set; }
        [Parameter]
        public string? Value { get; set; }
    }
}
