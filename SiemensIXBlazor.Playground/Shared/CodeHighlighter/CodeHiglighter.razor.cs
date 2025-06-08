
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Playground.Services;

namespace SiemensIXBlazor.Playground.Shared.CodeHighlighter
{
    public partial class CodeHiglighter : ComponentBase
    {

        [Parameter]
        public RenderFragment ComponentPreview { get; set; }

        [Parameter]
        public string CodeContent { get; set; }

        [Inject]
        private ICodeHighlightService CodeHighlightService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await CodeHighlightService.HighlightAll();
            }
        }
    }
}
