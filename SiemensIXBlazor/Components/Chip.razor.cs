using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiemensIXBlazor.Components
{
    public partial class Chip
    {
        [Parameter]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string Label { get; set; } = string.Empty;
        [Parameter]
        public bool Active { get; set; } = true;
        [Parameter]
        public string? Background { get; set; }
        [Parameter]
        public bool Closable { get; set; } = false;
        [Parameter]
        public string? Color { get; set; }
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public bool Outline { get; set; } = false;
        [Parameter]
        public string Variant { get; set; } = "primary";
        [Parameter]
        public EventCallback ClosedEvent { get; set; }

        private ChipInterops _chipInterops;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _chipInterops = new(JSRuntime);

                await _chipInterops.AddClosedEventListener(this, Id);
            }
        }

        [JSInvokable]
        public async void Closed()
        {
            await ClosedEvent.InvokeAsync();
        }
    }
}
