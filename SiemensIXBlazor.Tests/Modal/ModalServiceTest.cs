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
        var host = Render<ModalHost>((Action<Bunit.ComponentParameterCollectionBuilder<ModalHost>>)(_ => { }));

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
    public async Task ModalHost_ShouldKeepAnimationAndBackdropEnabledByDefault()
    {
        Services.AddSingleton<ModalService>(services =>
            new ModalService(services.GetRequiredService<IJSRuntime>()));
        var service = Services.GetRequiredService<ModalService>();
        var host = Render<ModalHost>((Action<Bunit.ComponentParameterCollectionBuilder<ModalHost>>)(_ => { }));

        await service.OpenAsync<string>(new ModalConfig
        {
            Content = builder => builder.AddContent(0, "Modal content"),
        });
        host.Render();

        var modal = host.Find("ix-modal");
        Assert.False(modal.HasAttribute("disable-animation"));
        Assert.False(modal.HasAttribute("hide-backdrop"));
    }

    [Fact]
    public async Task ModalService_ShouldPreserveDismissReasonAndAllowCancellation()
    {
        Services.AddSingleton<ModalService>(services =>
            new ModalService(services.GetRequiredService<IJSRuntime>()));
        var service = Services.GetRequiredService<ModalService>();
        var host = Render<ModalHost>((Action<Bunit.ComponentParameterCollectionBuilder<ModalHost>>)(_ => { }));
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

    [Fact]
    public async Task ModalHost_CompletesCloseAndDismissWithTypedReasons()
    {
        Services.AddSingleton<ModalService>(services =>
            new ModalService(services.GetRequiredService<IJSRuntime>()));
        var service = Services.GetRequiredService<ModalService>();
        var host = Render<ModalHost>((Action<Bunit.ComponentParameterCollectionBuilder<ModalHost>>)(_ => { }));

        var closed = await service.OpenAsync<string>(new ModalConfig
        {
            Content = builder => builder.AddContent(0, "Close content"),
        });
        host.Render();
        await host.Instance.DialogClose(JsonDocument.Parse("\"saved\"").RootElement);

        Assert.Equal("saved", await closed.Closed);
        host.Render();

        var dismissed = await service.OpenAsync<int>(new ModalConfig
        {
            Content = builder => builder.AddContent(0, "Dismiss content"),
        });
        host.Render();
        await host.Instance.DialogDismiss(JsonDocument.Parse("42").RootElement);

        Assert.Equal(42, await dismissed.Dismissed);
    }

    [Fact]
    public async Task ModalInstance_ForwardsCloseAndDismissCallsToJsRuntime()
    {
        var jsRuntime = new Mock<IJSRuntime>();
        Services.AddSingleton<IJSRuntime>(jsRuntime.Object);
        var service = new ModalService(jsRuntime.Object);
        var instance = await service.OpenAsync<string>(new ModalConfig());

        await instance.CloseAsync("close-reason");
        await instance.DismissAsync("dismiss-reason");

        Assert.Contains(jsRuntime.Invocations, invocation =>
            invocation.Method.Name == nameof(IJSRuntime.InvokeAsync) &&
            invocation.Arguments.Count == 2 &&
            (string)invocation.Arguments[0] == "siemensIXInterop.modal.close");
        Assert.Contains(jsRuntime.Invocations, invocation =>
            invocation.Method.Name == nameof(IJSRuntime.InvokeAsync) &&
            invocation.Arguments.Count == 2 &&
            (string)invocation.Arguments[0] == "siemensIXInterop.modal.dismiss");
    }

    [Fact]
    public async Task ModalService_RejectsOpeningASecondModalAndRejectsStaleInstances()
    {
        Services.AddSingleton<ModalService>(services =>
            new ModalService(services.GetRequiredService<IJSRuntime>()));
        var service = Services.GetRequiredService<ModalService>();
        var host = Render<ModalHost>((Action<Bunit.ComponentParameterCollectionBuilder<ModalHost>>)(_ => { }));
        var first = await service.OpenAsync<string>(new ModalConfig());

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.OpenAsync<string>(new ModalConfig()));

        host.Render();
        await host.Instance.DialogDismiss(null);

        await Assert.ThrowsAsync<InvalidOperationException>(() => first.CloseAsync("stale"));
    }
}
