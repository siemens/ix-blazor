// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
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
using Newtonsoft.Json;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Tests;

public class ToastTests : TestContextBase
{
    [Fact]
    public void ToastContainer_RendersOfficialDefaults()
    {
        var cut = Render<ToastContainer>(parameters => parameters
            .Add(p => p.Id, "toast-container"));

        var container = cut.Find("ix-toast-container");
        Assert.Equal("toast-container", container.Id);
        Assert.Equal("bottom-right", container.GetAttribute("position"));
    }

    [Fact]
    public void ToastContainer_RendersTopRightPositionAndAttributes()
    {
        var cut = Render<ToastContainer>(parameters => parameters
            .Add(p => p.Id, "toast-container")
            .Add(p => p.Position, ToastPosition.TopRight)
            .AddUnmatched("role", "region")
            .Add(p => p.Class, "custom-container")
            .Add(p => p.Style, "inset: 1rem;"));

        var container = cut.Find("ix-toast-container");
        Assert.Equal("top-right", container.GetAttribute("position"));
        Assert.Equal("region", container.GetAttribute("role"));
        Assert.Equal("custom-container", container.GetAttribute("class"));
        Assert.Equal("inset: 1rem;", container.GetAttribute("style"));
    }

    [Fact]
    public void Toast_RendersOfficialPropertiesAndSlots()
    {
        var cut = Render<Toast>(parameters => parameters
            .Add(p => p.Id, "toast")
            .Add(p => p.Type, ToastType.Success)
            .Add(p => p.ToastTitle, "Saved")
            .Add(p => p.AutoCloseDelay, 3000)
            .Add(p => p.PreventAutoClose, true)
            .Add(p => p.Icon, "save")
            .Add(p => p.IconColor, "color-success")
            .Add(p => p.HideIcon, true)
            .Add(p => p.AriaLabelCloseIconButton, "Close notification")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "The message")))
            .Add(p => p.ActionContent, (RenderFragment)(builder => builder.AddContent(0, "Undo"))));

        var toast = cut.Find("ix-toast");
        Assert.Equal("toast", toast.Id);
        Assert.Equal("success", toast.GetAttribute("type"));
        Assert.Equal("Saved", toast.GetAttribute("toast-title"));
        Assert.Equal("3000", toast.GetAttribute("auto-close-delay"));
        Assert.NotNull(toast.GetAttribute("prevent-auto-close"));
        Assert.Equal("save", toast.GetAttribute("icon"));
        Assert.Equal("color-success", toast.GetAttribute("icon-color"));
        Assert.NotNull(toast.GetAttribute("hide-icon"));
        Assert.Equal("Close notification", toast.GetAttribute("aria-label-close-icon-button"));
        Assert.Contains("The message", toast.InnerHtml);
        Assert.Contains("Undo", cut.Find("[slot='action']").InnerHtml);
    }

    [Fact]
    public void Toast_OmitsFalseBooleanProperties()
    {
        var cut = Render<Toast>(parameters => parameters.Add(p => p.Id, "toast"));

        var toast = cut.Find("ix-toast");
        Assert.Null(toast.GetAttribute("prevent-auto-close"));
        Assert.Null(toast.GetAttribute("hide-icon"));
    }

    [Fact]
    public async Task Toast_CloseToastInvokesCallback()
    {
        var closed = false;
        var cut = Render<Toast>(parameters => parameters
            .Add(p => p.Id, "toast")
            .Add(p => p.CloseToastEvent, EventCallback.Factory.Create(this, () => closed = true)));

        await cut.Instance.CloseToast();

        Assert.True(closed);
    }

    [Fact]
    public async Task Toast_ExposesPauseResumeAndIsPausedMethods()
    {
        var (jsRuntime, module) = AddJsModule();
        module.Setup(m => m.InvokeAsync<bool>("isToastPaused", It.IsAny<object[]>()))
            .Returns(new ValueTask<bool>(true));

        var cut = Render<Toast>(parameters => parameters.Add(p => p.Id, "toast"));

        await cut.Instance.PauseAsync();
        await cut.Instance.ResumeAsync();
        var isPaused = await cut.Instance.IsPausedAsync();

        Assert.True(isPaused);
        module.Verify(m => m.InvokeAsync<bool>("isToastPaused", It.Is<object[]>(args => args.SequenceEqual(new object[] { "toast" }))), Times.Once);
        _ = jsRuntime;
    }

    [Fact]
    public async Task ToastContainer_ShowToastReturnsLifecycleHandle()
    {
        var (_, module) = AddJsModule();
        module.Setup(m => m.InvokeAsync<string>("showToast", It.IsAny<object[]>()))
            .Returns(new ValueTask<string>("toast-1"));
        module.Setup(m => m.InvokeAsync<bool>("isPaused", It.IsAny<object[]>()))
            .Returns(new ValueTask<bool>(true));

        var cut = Render<ToastContainer>(parameters => parameters
            .Add(p => p.Id, "toast-container"));
        var result = await cut.Instance.ShowToast(new ToastConfig
        {
            Title = "Saved",
            Message = "The changes were saved.",
            Action = "<button>Undo</button>",
            Type = ToastType.Success,
            AutoClose = false,
            AutoCloseDelay = 3000,
            Icon = "save",
            IconColor = "color-success",
            HideIcon = false
        });

        await result.PauseAsync();
        await result.ResumeAsync();
        Assert.True(await result.IsPausedAsync());
        await result.CloseAsync("undone");

        module.Verify(m => m.InvokeAsync<string>("showToast", It.Is<object[]>(args =>
            args.Length == 3 && args[1] != null && args[1].ToString() == "toast-container" &&
            args[2] != null && Convert.ToString(args[2])!.Contains("\"type\":\"success\""))), Times.Once);
        module.Verify(m => m.InvokeAsync<bool>("isPaused", It.Is<object[]>(args => args.SequenceEqual(new object[] { "toast-1" }))), Times.Once);
    }

    [Fact]
    public async Task ToastContainer_OnCloseForwardsResultAndRemovesHandle()
    {
        var (_, module) = AddJsModule();
        module.Setup(m => m.InvokeAsync<string>("showToast", It.IsAny<object[]>()))
            .Returns(new ValueTask<string>("toast-1"));

        var cut = Render<ToastContainer>(parameters => parameters
            .Add(p => p.Id, "toast-container"));
        var result = await cut.Instance.ShowToast(new ToastConfig { Message = "Message" });
        JsonElement? received = null;
        result.OnClose += (_, value) => received = value;

        using var document = JsonDocument.Parse("\"closed\"");
        await cut.Instance.ToastClosed("toast-1", document.RootElement);

        Assert.Equal("closed", received?.GetString());
    }

    [Fact]
    public async Task ToastContainer_RejectsNullConfiguration()
    {
        var cut = Render<ToastContainer>(parameters => parameters
            .Add(p => p.Id, "toast-container"));

        await Assert.ThrowsAsync<ArgumentNullException>(() => cut.Instance.ShowToast(null!));
    }

    [Fact]
    public async Task ToastContainer_DisposesOnlyItsOwnToastHandles()
    {
        var (_, module) = AddJsModule();
        module.Setup(m => m.InvokeAsync<string>("showToast", It.IsAny<object[]>()))
            .Returns(new ValueTask<string>("toast-1"));

        var cut = Render<ToastContainer>(parameters => parameters
            .Add(p => p.Id, "toast-container"));
        await cut.Instance.ShowToast(new ToastConfig { Message = "Message" });

        await cut.Instance.DisposeAsync();

        module.Verify(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>(
            "dispose", It.Is<object[]>(args => args.Length == 1 && args[0].ToString() == "toast-container")), Times.Once);
    }

    [Fact]
    public void ToastConfig_SerializesOfficialPropertyNamesAndEnumValues()
    {
        var json = JsonConvert.SerializeObject(new ToastConfig
        {
            Title = "Saved",
            Message = "Done",
            Type = ToastType.Warning,
            AutoClose = true,
            AutoCloseDelay = 1000,
            HideIcon = true
        });

        Assert.Contains("\"title\":\"Saved\"", json);
        Assert.Contains("\"message\":\"Done\"", json);
        Assert.Contains("\"type\":\"warning\"", json);
        Assert.Contains("\"autoClose\":true", json);
        Assert.DoesNotContain("preventAutoClose", json);
        Assert.DoesNotContain("messageHtml", json);
        Assert.DoesNotContain("position", json);
    }

    private (Mock<IJSRuntime> JsRuntime, Mock<IJSObjectReference> Module) AddJsModule()
    {
        var jsRuntime = new Mock<IJSRuntime>();
        var module = new Mock<IJSObjectReference>();
        jsRuntime.Setup(j => j.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]>()))
            .Returns(new ValueTask<IJSObjectReference>(module.Object));
        Services.AddSingleton(jsRuntime.Object);
        return (jsRuntime, module);
    }
}
