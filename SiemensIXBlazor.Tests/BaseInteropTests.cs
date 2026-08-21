// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Microsoft.JSInterop;
using Moq;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Tests;

public class BaseInteropTests
{
    [Fact]
    public async Task ListenerCanOmitNonSerializableEventDetails()
    {
        var runtime = new Mock<IJSRuntime>();
        var module = new Mock<IJSObjectReference>();
        runtime
            .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
            .ReturnsAsync(module.Object);
        module
            .Setup(value => value.InvokeAsync<string>("listenEvent", It.IsAny<object[]?>()))
            .ReturnsAsync("listener-1");

        var interop = new BaseInterop(runtime.Object);
        await interop.AddEventListener(new object(), "element", "blur", "Blurred", includeDetail: false);

        Assert.Contains(module.Invocations, invocation =>
            invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
            invocation.Arguments.Count == 2 &&
            (string)invocation.Arguments[0] == "listenEvent" &&
            invocation.Arguments[1] is object[] arguments &&
            arguments.Length == 5 &&
            arguments[4] is false);
    }

    [Fact]
    public async Task DisposeRemovesRegisteredEventListeners()
    {
        var runtime = new Mock<IJSRuntime>();
        var module = new Mock<IJSObjectReference>();
        runtime
            .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
            .ReturnsAsync(module.Object);
        module
            .Setup(value => value.InvokeAsync<string>("listenEvent", It.IsAny<object[]?>()))
            .ReturnsAsync("listener-1");

        var interop = new BaseInterop(runtime.Object);
        await interop.AddEventListener(new object(), "element", "change", "Changed");
        await interop.DisposeAsync();

        Assert.Contains(module.Invocations, invocation =>
            invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
            invocation.Arguments.Count == 2 &&
            (string)invocation.Arguments[0] == "removeEventListener" &&
            invocation.Arguments[1] is object[] arguments &&
            arguments.Length == 1 &&
            (string)arguments[0] == "listener-1");
    }

    [Fact]
    public async Task BaseComponentDisposesRegisteredInterop()
    {
        var runtime = new Mock<IJSRuntime>();
        var module = new Mock<IJSObjectReference>();
        runtime
            .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
            .ReturnsAsync(module.Object);
        module
            .Setup(value => value.InvokeAsync<string>("listenEvent", It.IsAny<object[]?>()))
            .ReturnsAsync("listener-1");

        var component = new TestInteropComponent();
        var interop = new BaseInterop(runtime.Object);

        await interop.AddEventListener(component, "element", "change", "Changed");
        await component.DisposeAsync();

        Assert.Contains(module.Invocations, invocation =>
            invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
            invocation.Arguments.Count == 2 &&
            (string)invocation.Arguments[0] == "removeEventListener" &&
            invocation.Arguments[1] is object[] arguments &&
            arguments.Length == 1 &&
            (string)arguments[0] == "listener-1");
    }

    [Fact]
    public async Task BaseComponentDisposesRegisteredCustomResource()
    {
        var component = new TestInteropComponent();
        var resource = new TestDisposable();

        component.Register(resource);
        await component.DisposeAsync();

        Assert.True(resource.IsDisposed);
    }

    [Fact]
    public async Task ResourceRegisteredAfterComponentDisposalIsDisposed()
    {
        var component = new TestInteropComponent();
        var resource = new TestDisposable();

        await component.DisposeAsync();
        component.Register(resource);

        Assert.True(resource.IsDisposed);
    }

    [Fact]
    public async Task ListenerRegistrationAfterDisposalIsRejected()
    {
        var runtime = new Mock<IJSRuntime>();
        var interop = new BaseInterop(runtime.Object);

        await interop.DisposeAsync();

        await Assert.ThrowsAsync<ObjectDisposedException>(() =>
            interop.AddEventListener(new object(), "element", "change", "Changed"));
    }

    private sealed class TestInteropComponent : IXBaseComponent
    {
        public void Register(IAsyncDisposable resource) => RegisterDisposable(resource);
    }

    private sealed class TestDisposable : IAsyncDisposable
    {
        public bool IsDisposed { get; private set; }

        public ValueTask DisposeAsync()
        {
            IsDisposed = true;
            return ValueTask.CompletedTask;
        }
    }
}
