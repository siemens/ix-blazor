using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiemensIXBlazor.Components
{
    public partial class FlipTile
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string? State { get; set; }
        [Parameter]
        public string Class { get; set; } = string.Empty;
    }
}
