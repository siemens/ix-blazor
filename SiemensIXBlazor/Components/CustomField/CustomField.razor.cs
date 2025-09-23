using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.CustomField
{
    public partial class CustomField
    {
        private ElementReference inputRef;
        private ElementReference buttonRef;
        private ElementReference fileRef;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool IsFileUpload { get; set; } = false;

        [Parameter]
        public string FileDisplayText { get; set; } = "No file chosen";

        [Parameter]
        public string? HelperText { get; set; }

        [Parameter]
        public string? InfoText { get; set; }

        [Parameter]
        public string? InvalidText { get; set; }

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public bool Required { get; set; } = false;

        [Parameter]
        public bool? ShowTextAsTooltip { get; set; }

        [Parameter]
        public string? ValidText { get; set; }

        [Parameter]
        public string? WarningText { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && IsFileUpload) // Sadece IsFileUpload true ise JS fonksiyonunu çağır
            {
                await JSRuntime.InvokeVoidAsync("customFieldHelpers.initFileUpload",
                    buttonRef, fileRef, inputRef);
            }
        }
    }
}