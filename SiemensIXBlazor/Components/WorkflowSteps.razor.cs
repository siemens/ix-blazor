using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class WorkflowSteps
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Clickable { get; set; } = false;
        [Parameter]
        public bool Linear { get; set; } = false;
        [Parameter]
        public int SelectedIndex { get; set; } = 0;
        [Parameter]
        public bool Vertical { get; set; } = false;
        [Parameter]
        public EventCallback<int> StepSelectedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "stepSelected", "StepSelected");
            }
        }

        [JSInvokable]
        public async void StepSelected(int index)
        {
            SelectedIndex = index;
            await StepSelectedEvent.InvokeAsync(index);
            await InvokeAsync(StateHasChanged);
        }
    }
}
