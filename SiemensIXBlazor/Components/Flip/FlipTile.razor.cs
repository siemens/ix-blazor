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
        public dynamic Height { get; set; } = 15.125;
        [Parameter]
        public dynamic Width { get; set; } = 16;
    }
}
