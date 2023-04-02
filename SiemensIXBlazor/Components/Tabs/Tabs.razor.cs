using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Tabs
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string Layout { get; set; } = "auto";
        [Parameter]
        public string Placement { get; set; } = "bottom";
        [Parameter]
        public bool Rounded { get; set; } = false;
        [Parameter]
        public int Selected { get; set; } = 0;
        [Parameter]
        public bool Small { get; set; } = false;

        TabsInterop _tabsInterop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _tabsInterop = new(JSRuntime);

                await _tabsInterop.InitialComponent(Id);
            }
        }
    }
}
