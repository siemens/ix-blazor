// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;
using SiemensIXBlazor.Enums.Upload;
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
        public string? I18nUploadFile { get; set; }
        [Parameter]
        public bool DirectoryUpload { get; set; } = false;
        [Parameter]
        public UploadFileState State { get; set; } = UploadFileState.SELECT_FILE;
        [Parameter]
        public string? LoadingText { get; set; }
        [Parameter]
        public bool Multiline { get; set; } = false;
        [Parameter]
        public bool Multiple { get; set; } = false;
        [Parameter]
        public string? SelectFileText { get; set; }
        [Parameter]
        public string UploadFailedText { get; set; } = "Upload failed. Please try again.";
        [Parameter]
        public string UploadSuccessText { get; set; } = "Upload successful";
        [Parameter]
        public EventCallback<List<IXFile>> FileChangedEvent { get; set; }

        private FileUploadInterop? _fileUploadInterop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _fileUploadInterop ??= new(JSRuntime);
                RegisterDisposable(_fileUploadInterop);

                await _fileUploadInterop.AddEventListener(this, Id, "filesChanged", "FileChanged");
            }
        }

        [JSInvokable]
        public async Task FileChanged(JsonElement[] files)
        {
            var ixFiles = ParseFileObject(files);
            await FileChangedEvent.InvokeAsync(ixFiles);
        }

        public async Task SetFilesToUploadAsync(object files)
        {
            if (_fileUploadInterop is null)
            {
                _fileUploadInterop = new(JSRuntime);
                RegisterDisposable(_fileUploadInterop);
            }

            await _fileUploadInterop.SetFilesToUpload(Id, files);
        }

        private static List<IXFile> ParseFileObject(IEnumerable<JsonElement> fileObjects)
        {
            List<IXFile> ixFiles = new();

            foreach (var fileObj in fileObjects)
            {
                var fileData = fileObj;

                // Extract file properties and base64 data
                string fileName = fileData.GetProperty("name").GetString() ?? string.Empty;
                long fileSize = fileData.GetProperty("size").GetInt64();
                string fileType = fileData.GetProperty("type").GetString() ?? string.Empty;
                string base64Data = fileData.GetProperty("data").GetString() ?? string.Empty;

                // Create a custom implementation of IBrowserFile
                IXFile ixFile = new(fileName, fileSize, fileType, base64Data);

                ixFiles.Add(ixFile);
            }
            
            return ixFiles;
        }

    }
}
