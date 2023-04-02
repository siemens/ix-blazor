using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
    public class IXBaseComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? UserAttributes { get; set; }
        [Parameter]
        public string? Class { get; set; }
        [Parameter]
        public string? Style { get; set; }
    }
}
