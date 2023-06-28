using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components.About
{
    public partial class AboutMenu
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Active tab. Default value is null.
        /// </summary>
        [Parameter]
        public string? ActiveTabLabel { get; set; }
        /// <summary>
        /// Label of first tab. Default value is: 'About & legal information' 
        /// </summary>
        [Parameter]
        public string Label { get; set; } = "About & legal information";
        /// <summary>
        /// Internal. Default value is: false
        /// </summary>
        [Parameter]
        public bool Show { get; set; } = false;
        /// <summary>
        /// About and Legal closed event. Return value is: MouseEventArgs
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> ClosedEvent { get; set; }
        
        private BaseInterop _interop;
        private Lazy<Task<IJSObjectReference>>? moduleTask;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "close", "Closed");

                moduleTask = new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/SiemensIXBlazor/js/interops/aboutMenuInterop.js").AsTask());
            }
        }

        [JSInvokable]
        public async Task Closed(MouseEventArgs args)
        {
            await ClosedEvent.InvokeAsync(args);
        }

        public async Task ToggleAbout(bool status)
        {
            var module = await moduleTask.Value;
            if (module != null)
            {
                await module.InvokeVoidAsync("toggleAbout", Id, status);
            };
        }

    }
}
