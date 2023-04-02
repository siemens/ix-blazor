using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
    public partial class Spinner
    {
        [Parameter]
        public string Size { get; set; } = "medium";
        [Parameter]
        public string Variant { get; set; } = "secondary";
    }
}
