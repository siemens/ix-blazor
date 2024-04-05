using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;
using SiemensIXBlazor.Interops;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.Pane;
using SiemensIXBlazor.Objects.Pane;
using System.Text.Json;
using Newtonsoft.Json;


namespace SiemensIXBlazor.Components
{
    public partial class Pane
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public bool Borderless { get; set; } = false;
        [Parameter]
        public PaneComposition Composition { get; set; } = PaneComposition.top;
        [Parameter]
        public bool Expanded { get; set; } = false;
        [Parameter]
        public string? Heading { get; set; }
        [Parameter]
        public bool HideOnCollapse { get; set; } = false;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string Size { get; set; } = "240px";
        [Parameter]
        public PaneVariant Variant { get; set; } = PaneVariant.inline;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public EventCallback<PaneExpandedChangedEventResponse> ExpandedChangedEvent { get; set; }
        [Parameter]
        public EventCallback<PaneBorderlessChangedEventResponse> BorderlessChangedEvent { get; set; }
        [Parameter]
        public EventCallback<PaneVariantChangedEventResponse> VariantChangedEvent { get; set; }


        private BaseInterop _interop;

        protected  override  async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "expandedChanged", "ExpandChanged");
                await _interop.AddEventListener(this, Id, "variantChanged", "VariantChanged");
                await _interop.AddEventListener(this, Id, "borderlessChanged", "BorderlessChanged");
            }
        }

        [JSInvokable]
        public async void ExpandChanged(JsonElement data)
        {
            var jsonData = data.GetRawText();

            var paneExpandChangedEventResponse = JsonConvert.DeserializeObject<PaneExpandedChangedEventResponse>(jsonData);

            await ExpandedChangedEvent.InvokeAsync(paneExpandChangedEventResponse);
        }

        [JSInvokable]
        public async void BorderlessChanged(JsonElement data)
        {
            var jsonData = data.GetRawText();

            var paneBorderlessChangedEventResponse = JsonConvert.DeserializeObject<PaneBorderlessChangedEventResponse>(jsonData);

            await BorderlessChangedEvent.InvokeAsync(paneBorderlessChangedEventResponse);
        }

        [JSInvokable]
        public async void VariantChanged(JsonElement data)
        {
            var jsonData = data.GetRawText();

            var paneVariantChangedEventResponse = JsonConvert.DeserializeObject<PaneVariantChangedEventResponse>(jsonData);

            await VariantChangedEvent.InvokeAsync(paneVariantChangedEventResponse);
        }
    }
}
