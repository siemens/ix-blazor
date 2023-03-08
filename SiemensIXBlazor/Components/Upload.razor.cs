using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Upload
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string? Accept { get; set; }
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public string I18nUploadDisabled { get; set; } = "File upload currently not possible.";
        [Parameter]
        public string I18nUploadFile { get; set; } = "Upload file…";
        [Parameter]
        public string LoadingText { get; set; } = "Checking files…";
        [Parameter]
        public bool Multiline { get; set; } = false;
        [Parameter]
        public bool Multiple { get; set; } = false;
        [Parameter]
        public string SelectFileText { get; set; } = "+ Drag files here or…";
        [Parameter]
        public string UploadFailedText { get; set; } = "Upload failed. Please try again.";
        [Parameter]
        public string UploadSuccessText { get; set; } = "Upload successful";
        [Parameter]
        public EventCallback<byte[]> FileChangedEvent { get; set; }

        FileUploadInterop _fileUploadInterop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _fileUploadInterop = new(JSRuntime);

                await _fileUploadInterop.AddEventListener(this, Id, "filesChanged", "FileChanged");
            }
        }

        [JSInvokable]
        public async void FileChanged(byte[] data)
        {
           await FileChangedEvent.InvokeAsync(data);
        }
    }
}
