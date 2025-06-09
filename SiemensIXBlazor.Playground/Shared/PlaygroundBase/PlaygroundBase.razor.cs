using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Playground.Shared.PlaygroundBase
{
    public partial class PlaygroundBase
    {
        private int activeTab = 0;

        [Parameter]
        [EditorRequired]
        public string Id { get; set; }

        [Parameter]
        [EditorRequired]
        public string ComponentType { get; set; }

        [Parameter]
        [EditorRequired]
        public string TabId { get; set; }

        [Parameter]
        [EditorRequired]
        public RenderFragment ComponentContent { get; set; }

        [Parameter]
        [EditorRequired]
        public string CodeContent { get; set; }

        [Parameter]
        public string UsageTabTitle { get; set; } = "Usage";

        [Parameter]
        public string CodeTabTitle { get; set; } = "Code";

        [Parameter]
        public string UsageTabIcon { get; set; } = "document";

        [Parameter]
        public string CodeTabIcon { get; set; } = "code";
    }
}
