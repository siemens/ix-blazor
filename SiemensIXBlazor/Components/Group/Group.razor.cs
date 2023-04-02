using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Group
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public bool Collapsed { get; set; } = true;
        [Parameter]
        public bool ExpandOnHeaderClick { get; set; } = false;
        [Parameter]
        public string? Header { get; set; }
        [Parameter]
        public int? Index { get; set; }
        [Parameter]
        public bool? Selected { get; set; }
        [Parameter]
        public string? SubHeader { get; set; }
        [Parameter]
        public bool SuppressHeaderSelection { get; set; } = false;
        [Parameter]
        public EventCallback<bool> CollapsedChangedEvent { get; set; }
        [Parameter]
        public EventCallback<bool> SelectGroupEvent { get; set; }
        [Parameter]
        public EventCallback<int> SelectItemEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "collapsedChanged", "CollapsedChanged");
                await _interop.AddEventListener(this, Id, "selectGroup", "GroupSelected");
                await _interop.AddEventListener(this, Id, "selectItem", "ItemSelected");
            }
        }

        [JSInvokable]
        public async void CollapsedChanged(bool value)
        {
            await CollapsedChangedEvent.InvokeAsync(value);
        }

        [JSInvokable]
        public async void GroupSelected(bool value)
        {
            await SelectGroupEvent.InvokeAsync(value);
        }

        [JSInvokable]
        public async void ItemSelected(int index)
        {
            await SelectItemEvent.InvokeAsync(index);
        }
    }
}
