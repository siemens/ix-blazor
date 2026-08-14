// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using SiemensIXBlazor.Components.Modal;
using SiemensIXBlazor.Enums.Modal;
using SiemensIXBlazor.Tests;
using Xunit;

namespace SiemensIXBlazor.Tests.Modal;

public class ModalServiceTest : TestContextBase
{
    [Fact]
    public async Task ModalHost_ShouldRenderOfficialModalConfiguration()
    {
        Services.AddSingleton<ModalService>(services =>
            new ModalService(services.GetRequiredService<IJSRuntime>()));
        var service = Services.GetRequiredService<ModalService>();
        var host = RenderComponent<ModalHost>();

        var config = new ModalConfig
        {
            Content = builder => builder.AddContent(0, "Modal content"),
            Animation = false,
            Backdrop = false,
            CloseOnBackdropClick = true,
            Centered = true,
            IsNonBlocking = true,
            Size = ModalSize.full_width,
        };

        await service.OpenAsync<string>(config);
        host.Render();

        var modal = host.Find("ix-modal");
        Assert.Equal("full-width", modal.GetAttribute("size"));
        Assert.Equal("true", modal.GetAttribute("disable-animation"));
        Assert.Equal("true", modal.GetAttribute("hide-backdrop"));
        Assert.Equal("true", modal.GetAttribute("close-on-backdrop-click"));
        Assert.Equal("true", modal.GetAttribute("centered"));
        Assert.Equal("true", modal.GetAttribute("is-non-blocking"));
        Assert.Contains("Modal content", modal.InnerHtml);
    }

    [Fact]
    public async Task ModalService_ShouldPreserveDismissReasonAndAllowCancellation()
    {
        Services.AddSingleton<ModalService>(services =>
            new ModalService(services.GetRequiredService<IJSRuntime>()));
        var service = Services.GetRequiredService<ModalService>();
        var host = RenderComponent<ModalHost>();
        var wasCalled = false;
        var config = new ModalConfig
        {
            Content = builder => builder.AddContent(0, "Modal content"),
            BeforeDismiss = reason =>
            {
                wasCalled = reason.HasValue;
                return Task.FromResult(false);
            },
        };

        var instance = await service.OpenAsync<string>(config);
        host.Render();
        var allowed = await host.Instance.BeforeDismiss(JsonDocument.Parse("\"blocked\"").RootElement);

        Assert.False(allowed);
        Assert.True(wasCalled);
        Assert.False(instance.Dismissed.IsCompleted);
    }
}
