using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;
using System.Text.Json;

namespace SiemensIXBlazor.Components
{
    public partial class Upload
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
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
        public EventCallback<List<IXFile>> FileChangedEvent { get; set; }

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
        public async void FileChanged(object[] files)
        {
            var ixFiles = ParseFileObject(files);
            await FileChangedEvent.InvokeAsync(ixFiles);
        }

        private static List<IXFile> ParseFileObject(object[] fileObjects)
        {
            List<IXFile> ixFiles = new();

            foreach (var fileObj in fileObjects)
            {
                var fileData = (JsonElement)fileObj;

                // Extract file properties and base64 data
                string fileName = fileData.GetProperty("name").GetString();
                long fileSize = fileData.GetProperty("size").GetInt64();
                string fileType = fileData.GetProperty("type").GetString();
                string base64Data = fileData.GetProperty("data").GetString();

                // Create a custom implementation of IBrowserFile
                IXFile ixFile = new(fileName, fileSize, fileType, base64Data);

                ixFiles.Add(ixFile);
            }
            
            return ixFiles;
        }
    }
}
