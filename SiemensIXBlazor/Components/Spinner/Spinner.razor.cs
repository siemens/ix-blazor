using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Spinner;

namespace SiemensIXBlazor.Components
{
    public partial class Spinner
    {
        [Parameter]
        public string Size { get; set; } = "medium";
        [Parameter]
        public SpinnerVariant? Variant { get; set; }
    }
}
