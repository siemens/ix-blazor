using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
	public partial class EmptyState
	{
        [Parameter, EditorRequired]
        public string Id { get; set; }
        /// <summary>
        /// Optional empty state action
        /// </summary>
        [Parameter]
		public string? Action { get; set; }
        /// <summary>
        /// Empty state header
        /// </summary>
        [Parameter]
        public string? Header { get; set; }
        /// <summary>
        /// Optional empty state icon
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }
        /// <summary>
        /// Optional empty state layout - one of 'large', 'compact' or 'compactBreak'
        /// </summary>
        [Parameter]
        public string Layout { get; set; } = "large";
        /// <summary>
        /// Optional empty state sub header
        /// </summary>
        [Parameter]
        public string? SubHeader { get; set; }
        /// <summary>
        /// Empty state action click event
        /// </summary>
        [Parameter]
        public EventCallback ActionClickedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "actionClick", "ActionClicked");
            }
        }

        [JSInvokable]
        public async void ActionClicked()
        {
            await ActionClickedEvent.InvokeAsync();
        }

    }
}

