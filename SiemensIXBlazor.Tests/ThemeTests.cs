// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Tests;

public class ThemeTests : TestContextBase
{
    [Fact]
    public async Task SetTheme_InvokesJsRuntimeWithCorrectParameters()
    {
        // Arrange
        var jsRuntimeMock = new Mock<IJSRuntime>();
        Services.AddSingleton<IJSRuntime>(jsRuntimeMock.Object);
        var themeName = "dark";

        // Act
        var cut = RenderComponent<Theme>();
        await cut.Instance.SetTheme(themeName);

        // Assert
        jsRuntimeMock.Verify(js => js.InvokeAsync<object>(
            "siemensIXInterop.setTheme",
            It.Is<object[]>(args =>
                args.Length == 1 &&
                args[0] is string &&
                (string)args[0] == themeName
            )
        ), Times.Once());

        jsRuntimeMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ToggleTheme_InvokesJsRuntimeWithCorrectParameters()
    {
        // Arrange
        var jsRuntimeMock = new Mock<IJSRuntime>();
        Services.AddSingleton<IJSRuntime>(jsRuntimeMock.Object);

        // Act
        var cut = RenderComponent<Theme>();
        await cut.Instance.ToggleTheme();

        // Assert
        jsRuntimeMock.Verify(js => js.InvokeAsync<object>(
            "siemensIXInterop.toggleTheme",
            It.Is<object[]>(args => args.Length == 0)
        ), Times.Once());

        jsRuntimeMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ToggleSystemTheme_InvokesJsRuntimeWithCorrectParameters()
    {
        // Arrange
        var jsRuntimeMock = new Mock<IJSRuntime>();
        Services.AddSingleton<IJSRuntime>(jsRuntimeMock.Object);
        var useSystemTheme = true;

        // Act
        var cut = RenderComponent<Theme>();
        await cut.Instance.ToggleSystemTheme(useSystemTheme);

        // Assert
        jsRuntimeMock.Verify(js => js.InvokeAsync<object>(
            "siemensIXInterop.toggleSystemTheme",
            It.Is<object[]>(args =>
                args.Length == 1 &&
                args[0] is bool &&
                (bool)args[0] == useSystemTheme
            )
        ), Times.Once());

        jsRuntimeMock.VerifyNoOtherCalls();
    }
}
