using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums;

namespace SiemensIXBlazor.Components
{
    public partial class Card
    {
        [Parameter]
        public bool? Selected { get; set; }
        [Parameter]
        public CardVariant Variant { get; set; } = CardVariant.insight;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
