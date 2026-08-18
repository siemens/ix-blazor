// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Upload;
using SiemensIXBlazor.Objects;
using System.Text.Json;
using Xunit;

namespace SiemensIXBlazor.Tests
{
    public class UploadTests : TestContextBase
    {
        [Fact]
        public void UploadRendersCorrectly()
        {
            // Arrange
            var cut = Render<Upload>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.Accept, "image/*")
                .Add(p => p.Disabled, true)
                .Add(p => p.I18nUploadDisabled, "File upload currently not possible.")
                .Add(p => p.I18nUploadFile, "Upload file…")
                .Add(p => p.LoadingText, "Checking files…")
                .Add(p => p.Multiline, true)
                .Add(p => p.Multiple, true)
                .Add(p => p.DirectoryUpload, true)
                .Add(p => p.State, UploadFileState.UPLOAD_FAILED)
                .Add(p => p.SelectFileText, "+ Drag files here or…")
                .Add(p => p.UploadFailedText, "Upload failed. Please try again.")
                .Add(p => p.UploadSuccessText, "Upload successful")
            );

            // Assert
            cut.MarkupMatches("<ix-upload id=\"testId\" accept=\"image/*\" disabled='true' i18n-upload-disabled=\"File upload currently not possible.\" i18n-upload-file=\"Upload file…\" loading-text=\"Checking files…\" multiline='true' multiple='true' directory-upload='true' state=\"UPLOAD_FAILED\" select-file-text=\"+ Drag files here or…\" upload-failed-text=\"Upload failed. Please try again.\" upload-success-text=\"Upload successful\"></ix-upload>");
        }

        [Fact]
        public void DirectoryUploadUsesOfficialDefaultState()
        {
            var cut = Render<Upload>(parameters => parameters
                .Add(p => p.Id, "folder-upload")
                .Add(p => p.DirectoryUpload, true));

            cut.MarkupMatches("<ix-upload id=\"folder-upload\" directory-upload='true' state=\"SELECT_FILE\" i18n-upload-disabled=\"File upload currently not possible.\" upload-failed-text=\"Upload failed. Please try again.\" upload-success-text=\"Upload successful\"></ix-upload>");
        }

        [Fact]
        public async Task FileChangedEventWorks()
        {
            // Arrange
            IXFile? changedFile = null;
            var cut = Render<Upload>(parameters => parameters
                .Add(p => p.Id, "upload")
                .Add(p => p.FileChangedEvent, EventCallback.Factory.Create<List<IXFile>>(this, newValue => { changedFile = newValue.Single(); }))
            );

            // Simulate the file change event
            var files = new[]
            {
                JsonSerializer.SerializeToElement(new
                {
                    name = "file1.txt",
                    size = 1234L,
                    type = "text/plain",
                    data = "base64EncodedData"
                })
            };

            await cut.Instance.FileChanged(files);

            // Assert
            Assert.NotNull(changedFile);
            Assert.Equal("file1.txt", changedFile!.Name);
            Assert.Equal(1234L, changedFile.Size);
            Assert.Equal("text/plain", changedFile.Type);
            Assert.Equal("base64EncodedData", changedFile.Base64Data);
        }
    }
}
