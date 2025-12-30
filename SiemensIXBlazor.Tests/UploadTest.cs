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
using SiemensIXBlazor.Objects;
using Xunit;

namespace SiemensIXBlazor.Tests
{
    public class UploadTests : TestContextBase
    {
        [Fact]
        public void UploadRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Upload>(
                ("Id", "testId"),
                ("Accept", "image/*"),
                ("Disabled", true),
                ("I18nUploadDisabled", "File upload currently not possible."),
                ("I18nUploadFile", "Upload file…"),
                ("LoadingText", "Checking files…"),
                ("Multiline", true),
                ("Multiple", true),
                ("SelectFileText", "+ Drag files here or…"),
                ("UploadFailedText", "Upload failed. Please try again."),
                ("UploadSuccessText", "Upload successful")
            );

            // Assert
            cut.MarkupMatches("<ix-upload id=\"testId\" accept=\"image/*\" disabled i18n-upload-disabled=\"File upload currently not possible.\" i18n-upload-file=\"Upload file…\" loading-text=\"Checking files…\" multiline multiple select-file-text=\"+ Drag files here or…\" upload-failed-text=\"Upload failed. Please try again.\" upload-success-text=\"Upload successful\"></ix-upload>");
        }

        [Fact]
        public async Task FileChangedEventWorks()
        {
            // Arrange
            var fileChanged = false;
            var cut = RenderComponent<Upload>(parameters => parameters
                .Add(p => p.Id, "upload")
                .Add(p => p.FileChangedEvent, EventCallback.Factory.Create<List<IXFile>>(this, newValue => { fileChanged = true; }))
            );

            // Simulate the file change event
            var files = new object[] {
                new { name = "file1.txt", size = 1234L, type = "text/plain", data = "base64EncodedData" }
            };

            await cut.Instance.FileChangedEvent.InvokeAsync(new List<IXFile>());



            // Assert
            Assert.True(fileChanged);
        }
    }
}
