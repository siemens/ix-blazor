// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using Newtonsoft.Json;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Tests
{
    public class ToastTests : TestContextBase
    {
        [Fact]
        public void ComponentRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<Toast>();

            // Assert
            cut.MarkupMatches("<ix-toast-container></ix-toast-container>");
        }

        [Fact]
        public void UserAttributesAreAppliedCorrectly()
        {
            // Arrange
            var attributes = new
            {
                role = "alert",
                tabindex = "0",
                id = "toast-container-1"
            };

            // Act
            var cut = RenderComponent<Toast>(parameters => parameters
                .AddUnmatched("role", attributes.role)
                .AddUnmatched("tabindex", attributes.tabindex)
                .AddUnmatched("id", attributes.id));

            // Assert
            cut.MarkupMatches($"<ix-toast-container role=\"{attributes.role}\" tabindex=\"{attributes.tabindex}\" id=\"{attributes.id}\"></ix-toast-container>");
        }

        [Fact]
        public void StyleIsAppliedCorrectly()
        {
            // Arrange
            var style = "position: fixed; top: 10px; right: 10px;";

            // Act
            var cut = RenderComponent<Toast>(parameters => parameters.Add(p => p.Style, style));

            // Assert
            cut.MarkupMatches($"<ix-toast-container style=\"{style}\"></ix-toast-container>");
        }

        [Fact]
        public void ClassIsAppliedCorrectly()
        {
            // Arrange
            var className = "custom-toast-container";

            // Act
            var cut = RenderComponent<Toast>(parameters => parameters.Add(p => p.Class, className));

            // Assert
            cut.MarkupMatches($"<ix-toast-container class=\"{className}\"></ix-toast-container>");
        }

        [Fact]
        public async Task ShowToast_WithValidConfig_InvokesJSCorrectly()
        {
            // Arrange
            var jsRuntimeMock = new Mock<IJSRuntime>();
            Services.AddSingleton(jsRuntimeMock.Object);

            var toastConfig = new ToastConfig
            {
                Message = "Test message",
                Title = "Test title",
                Type = "success",
                Icon = "info",
                IconColor = "#00FF00",
                PreventAutoClose = true,
                AutoCloseDelay = 3000
            };

            string expectedJson = JsonConvert.SerializeObject(toastConfig);

            // Setup the expected JS call
            jsRuntimeMock
                .Setup(js => js.InvokeAsync<object>(
                    It.Is<string>(s => s == "siemensIXInterop.showMessage"),
                    It.Is<object[]>(args => args.Length == 1 && args[0].ToString() == expectedJson)))
                .ReturnsAsync(new ValueTask<object>());

            var cut = RenderComponent<Toast>();

            // Act
            await cut.InvokeAsync(() => cut.Instance.ShowToast(toastConfig));

            // Assert
            jsRuntimeMock.Verify(
                js => js.InvokeAsync<object>(
                    It.Is<string>(s => s == "siemensIXInterop.showMessage"),
                    It.Is<object[]>(args => args.Length == 1 && args[0].ToString() == expectedJson)),
                Times.Once);
        }

        [Fact]
        public async Task ShowToast_WithNullConfig_ThrowsArgumentNullException()
        {
            // Arrange
            var jsRuntimeMock = new Mock<IJSRuntime>();
            Services.AddSingleton(jsRuntimeMock.Object);
            var cut = RenderComponent<Toast>();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await cut.InvokeAsync(() => cut.Instance.ShowToast(null)));
        }

        [Fact]
        public async Task ShowToast_WithDefaultValues_UsesCorrectDefaults()
        {
            // Arrange
            var jsRuntimeMock = new Mock<IJSRuntime>();
            Services.AddSingleton(jsRuntimeMock.Object);

            var toastConfig = new ToastConfig
            {
                Message = "Test message"
            };

            jsRuntimeMock
                .Setup(js => js.InvokeAsync<object>(
                    It.Is<string>(s => s == "siemensIXInterop.showMessage"),
                    It.Is<object[]>(args => VerifyToastConfigDefaults(args[0].ToString(), "Test message"))))
                .ReturnsAsync(new ValueTask<object>());

            var cut = RenderComponent<Toast>();

            // Act
            await cut.InvokeAsync(() => cut.Instance.ShowToast(toastConfig));

            // Assert
            jsRuntimeMock.Verify(
                js => js.InvokeAsync<object>(
                    It.Is<string>(s => s == "siemensIXInterop.showMessage"),
                    It.IsAny<object[]>()),
                Times.Once);
        }

        [Fact]
        public async Task ShowToast_WhenJSRuntimeThrowsException_PropagatesException()
        {
            // Arrange
            var jsRuntimeMock = new Mock<IJSRuntime>();
            Services.AddSingleton(jsRuntimeMock.Object);

            var toastConfig = new ToastConfig { Message = "Test message" };
            var expectedException = new JSException("Test JS exception");

            jsRuntimeMock
                .Setup(js => js.InvokeAsync<object>(
                    It.Is<string>(s => s == "siemensIXInterop.showMessage"),
                    It.IsAny<object[]>()))
                .ThrowsAsync(expectedException);

            var cut = RenderComponent<Toast>();

            // Act & Assert
            var actualException = await Assert.ThrowsAsync<JSException>(async () =>
                await cut.InvokeAsync(() => cut.Instance.ShowToast(toastConfig)));

            Assert.Same(expectedException, actualException);
        }

        [Theory]
        [InlineData("info")]
        [InlineData("success")]
        [InlineData("warning")]
        [InlineData("error")]
        public async Task ShowToast_WithDifferentTypes_InvokesJSCorrectly(string toastType)
        {
            // Arrange
            var jsRuntimeMock = new Mock<IJSRuntime>();
            Services.AddSingleton(jsRuntimeMock.Object);

            var toastConfig = new ToastConfig
            {
                Message = "Test message",
                Type = toastType
            };

            jsRuntimeMock
                .Setup(js => js.InvokeAsync<object>(
                    It.Is<string>(s => s == "siemensIXInterop.showMessage"),
                    It.Is<object[]>(args => JsonConvert.DeserializeObject<ToastConfig>(args[0].ToString()).Type == toastType)))
                .ReturnsAsync(new ValueTask<object>());

            var cut = RenderComponent<Toast>();

            // Act
            await cut.InvokeAsync(() => cut.Instance.ShowToast(toastConfig));

            // Assert
            jsRuntimeMock.Verify(
                js => js.InvokeAsync<object>(
                    It.Is<string>(s => s == "siemensIXInterop.showMessage"),
                    It.Is<object[]>(args => JsonConvert.DeserializeObject<ToastConfig>(args[0].ToString()).Type == toastType)),
                Times.Once);
        }

        [Fact]
        public async Task ShowToast_WithAction_PassesThroughInterop()
        {
            var jsRuntimeMock = new Mock<IJSRuntime>();
            Services.AddSingleton(jsRuntimeMock.Object);

            var toastConfig = new ToastConfig
            {
                Title = "Toast headline",
                MessageHtml = "<div>Message from template</div>",
                Action = "<ix-button variant=\"tertiary\" icon=\"undo\">Undo</ix-button>"
            };

            string expectedJson = JsonConvert.SerializeObject(toastConfig);

            jsRuntimeMock
                .Setup(js => js.InvokeAsync<object>(
                    It.Is<string>(s => s == "siemensIXInterop.showMessage"),
                    It.Is<object[]>(args => args.Length == 1 && args[0].ToString() == expectedJson)))
                .ReturnsAsync(new ValueTask<object>());

            var cut = RenderComponent<Toast>();

            await cut.InvokeAsync(() => cut.Instance.ShowToast(toastConfig));

            jsRuntimeMock.Verify(
                js => js.InvokeAsync<object>(
                    It.Is<string>(s => s == "siemensIXInterop.showMessage"),
                    It.Is<object[]>(args => args.Length == 1 && args[0].ToString() == expectedJson)),
                Times.Once);
        }

        [Theory]
        [InlineData(ToastPosition.BottomRight)]
        [InlineData(ToastPosition.TopRight)]
        public async Task ShowToast_WithPosition_PassesThroughInterop(ToastPosition position)
        {
            // Arrange
            var jsRuntimeMock = new Mock<IJSRuntime>();
            Services.AddSingleton(jsRuntimeMock.Object);

            var toastConfig = new ToastConfig
            {
                Title = "Positioned Toast",
                Message = "This toast has a position",
                Position = position
            };

            string expectedJson = JsonConvert.SerializeObject(toastConfig);

            jsRuntimeMock
                .Setup(js => js.InvokeAsync<object>(
                    It.Is<string>(s => s == "siemensIXInterop.showMessage"),
                    It.Is<object[]>(args => args.Length == 1 && args[0].ToString() == expectedJson)))
                .ReturnsAsync(new ValueTask<object>());

            var cut = RenderComponent<Toast>();

            // Act
            await cut.InvokeAsync(() => cut.Instance.ShowToast(toastConfig));

            // Assert
            jsRuntimeMock.Verify(
                js => js.InvokeAsync<object>(
                    It.Is<string>(s => s == "siemensIXInterop.showMessage"),
                    It.Is<object[]>(args => JsonConvert.DeserializeObject<ToastConfig>(args[0].ToString()).Position == position)),
                Times.Once);
        }

        #region Helper Methods

        private bool VerifyToastConfigDefaults(string json, string expectedMessage)
        {
            var config = JsonConvert.DeserializeObject<ToastConfig>(json);
            if (config == null)
                return false;

            return config.Message == expectedMessage &&
                   config.Type == "info" &&
                   config.PreventAutoClose == true &&
                   config.AutoCloseDelay == 5000;
        }

        #endregion
    }
}
