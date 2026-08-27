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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Upload;
using SiemensIXBlazor.Objects;
using System.Reflection;
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

        [Fact]
        public async Task SetFilesToUploadForwardsFilesToInterop()
        {
            var cut = Render<Upload>(parameters => parameters.Add(p => p.Id, "upload"));
            var files = new[] { new { name = "file.txt" } };

            await cut.Instance.SetFilesToUploadAsync(files);
        }

        [Fact]
        public async Task DisposalRemovesUploadListenerAndDisposesModule()
        {
            var runtime = new Mock<IJSRuntime>();
            var module = new Mock<IJSObjectReference>();
            runtime
                .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
                .ReturnsAsync(module.Object);
            module
                .Setup(value => value.InvokeAsync<string?>("fileUploadEventHandler", It.IsAny<object[]?>()))
                .ReturnsAsync("upload-listener-1");
            Services.AddSingleton<IJSRuntime>(runtime.Object);

            var cut = Render<Upload>(parameters => parameters.Add(p => p.Id, "upload"));
            await cut.Instance.DisposeAsync();

            Assert.Contains(module.Invocations, invocation =>
                invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
                invocation.Arguments.Count == 2 &&
                (string)invocation.Arguments[0] == "removeFileUploadEventHandler" &&
                invocation.Arguments[1] is object[] arguments &&
                (string)arguments.Single() == "upload-listener-1");
            Assert.Contains(module.Invocations, invocation =>
                invocation.Method.Name == nameof(IJSObjectReference.DisposeAsync));
        }

        [Fact]
        public async Task NullUploadListenerIdIsHandledWithoutCleanupFailure()
        {
            var runtime = new Mock<IJSRuntime>();
            var module = new Mock<IJSObjectReference>();
            runtime
                .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
                .ReturnsAsync(module.Object);
            module
                .Setup(value => value.InvokeAsync<string?>("fileUploadEventHandler", It.IsAny<object[]?>()))
                .ReturnsAsync((string?)null);
            Services.AddSingleton<IJSRuntime>(runtime.Object);

            var cut = Render<Upload>(parameters => parameters.Add(p => p.Id, "upload"));
            await cut.Instance.DisposeAsync();

            Assert.DoesNotContain(module.Invocations, invocation =>
                invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
                invocation.Arguments.Count > 0 &&
                (string)invocation.Arguments[0] == "removeFileUploadEventHandler");
        }

        [Fact]
        public void UploadImportFailureIsSurfacedDuringInitialRegistration()
        {
            var runtime = new Mock<IJSRuntime>();
            runtime
                .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
                .ThrowsAsync(new JSException("upload interop import failed"));
            Services.AddSingleton<IJSRuntime>(runtime.Object);

            Assert.Throws<JSException>(() => Render<Upload>(parameters => parameters.Add(p => p.Id, "upload")));
        }

        [Fact]
        public async Task UploadInteropCanBeDisposedAfterListenerRegistrationFails()
        {
            var runtime = new Mock<IJSRuntime>();
            var module = new Mock<IJSObjectReference>();
            runtime
                .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
                .ReturnsAsync(module.Object);
            module
                .Setup(value => value.InvokeAsync<string?>("fileUploadEventHandler", It.IsAny<object[]?>()))
                .ThrowsAsync(new JSException("listener registration failed"));

            Type interopType = typeof(Upload).Assembly.GetType(
                "SiemensIXBlazor.Interops.FileUploadInterop", throwOnError: true)!;
            var interop = Assert.IsAssignableFrom<IAsyncDisposable>(
                Activator.CreateInstance(interopType, runtime.Object));
            MethodInfo addEventListener = interopType.GetMethod("AddEventListener")!;
            var registration = Assert.IsAssignableFrom<Task>(addEventListener.Invoke(interop,
                [new object(), "upload", "filesChanged", "FileChanged"]));

            await Assert.ThrowsAsync<JSException>(() => registration);
            await interop.DisposeAsync();

            Assert.Contains(module.Invocations, invocation =>
                invocation.Method.Name == nameof(IJSObjectReference.DisposeAsync));
        }
    }
}
