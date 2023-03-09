using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public string? Unit { get; set; }
        [Parameter]
        public string? Value { get; set; }
    }
}
