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
    public async Task MultipleComponentInstancesDisposeOnlyTheirOwnListeners()
    {
        var runtime = new Mock<IJSRuntime>();
        var module = new Mock<IJSObjectReference>();
        runtime
            .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
            .ReturnsAsync(module.Object);
        module
            .SetupSequence(value => value.InvokeAsync<string>("listenEvent", It.IsAny<object[]?>()))
            .ReturnsAsync("listener-1")
            .ReturnsAsync("listener-2");

        var firstComponent = new TestInteropComponent();
        var secondComponent = new TestInteropComponent();
        var firstInterop = new BaseInterop(runtime.Object);
        var secondInterop = new BaseInterop(runtime.Object);

        await firstInterop.AddEventListener(firstComponent, "first", "change", "Changed");
        await secondInterop.AddEventListener(secondComponent, "second", "change", "Changed");

        await firstComponent.DisposeAsync();

        var removalsAfterFirstDisposal = GetRemovedListenerIds(module);
        Assert.Equal(["listener-1"], removalsAfterFirstDisposal);

        await secondComponent.DisposeAsync();

        Assert.Equal(["listener-1", "listener-2"], GetRemovedListenerIds(module));
    }

    [Fact]
    public async Task RepeatedComponentMountsAndUnmountsRemoveEachListenerOnce()
    {
        var runtime = new Mock<IJSRuntime>();
        var module = new Mock<IJSObjectReference>();
        runtime
            .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
            .ReturnsAsync(module.Object);
        module
            .SetupSequence(value => value.InvokeAsync<string>("listenEvent", It.IsAny<object[]?>()))
            .ReturnsAsync("listener-1")
            .ReturnsAsync("listener-2")
            .ReturnsAsync("listener-3");

        for (var index = 1; index <= 3; index++)
        {
            var component = new TestInteropComponent();
            var interop = new BaseInterop(runtime.Object);

            await interop.AddEventListener(component, $"element-{index}", "change", "Changed");
            await component.DisposeAsync();
            await component.DisposeAsync();
        }

        Assert.Equal(["listener-1", "listener-2", "listener-3"], GetRemovedListenerIds(module));
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

    [Fact]
    public async Task ListenerRegistrationFailureDoesNotLeakTheInteropReference()
    {
        var runtime = new Mock<IJSRuntime>();
        var module = new Mock<IJSObjectReference>();
        runtime
            .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
            .ReturnsAsync(module.Object);
        module
            .Setup(value => value.InvokeAsync<string>("listenEvent", It.IsAny<object[]?>()))
            .ThrowsAsync(new JSException("listener registration failed"));

        var interop = new BaseInterop(runtime.Object);

        await Assert.ThrowsAsync<JSException>(() =>
            interop.AddEventListener(new object(), "element", "change", "Changed"));
        await interop.DisposeAsync();

        Assert.DoesNotContain(module.Invocations, invocation =>
            invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
            invocation.Arguments.Count > 0 &&
            (string)invocation.Arguments[0] == "removeEventListener");
    }

    [Fact]
    public async Task DisposalContinuesWhenJavaScriptListenerRemovalFails()
    {
        var runtime = new Mock<IJSRuntime>();
        var module = new Mock<IJSObjectReference>();
        runtime
            .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
            .ReturnsAsync(module.Object);
        module
            .Setup(value => value.InvokeAsync<string>("listenEvent", It.IsAny<object[]?>()))
            .ReturnsAsync("listener-1");
        module
            .Setup(value => value.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>(
                "removeEventListener", It.IsAny<object[]?>()))
            .ThrowsAsync(new JSException("listener removal failed"));

        var interop = new BaseInterop(runtime.Object);
        await interop.AddEventListener(new object(), "element", "change", "Changed");

        await interop.DisposeAsync();
        await interop.DisposeAsync();

        Assert.Contains(module.Invocations, invocation =>
            invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
            invocation.Arguments.Count == 2 &&
            (string)invocation.Arguments[0] == "removeEventListener");
    }

    [Fact]
    public async Task DisposalIgnoresAnImportFailureAfterRegistrationWasStarted()
    {
        var runtime = new Mock<IJSRuntime>();
        runtime
            .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
            .ThrowsAsync(new JSException("module import failed"));

        var interop = new BaseInterop(runtime.Object);

        await Assert.ThrowsAsync<JSException>(() =>
            interop.AddEventListener(new object(), "element", "change", "Changed"));
        await interop.DisposeAsync();
    }

    [Fact]
    public async Task DisposalContinuesWhenModuleDisposalFails()
    {
        var runtime = new Mock<IJSRuntime>();
        var module = new Mock<IJSObjectReference>();
        runtime
            .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
            .ReturnsAsync(module.Object);
        module
            .Setup(value => value.InvokeAsync<string>("listenEvent", It.IsAny<object[]?>()))
            .ReturnsAsync("listener-1");
        module
            .Setup(value => value.DisposeAsync())
            .Returns(new ValueTask(Task.FromException(new JSException("module disposal failed"))));

        var interop = new BaseInterop(runtime.Object);
        await interop.AddEventListener(new object(), "element", "change", "Changed");

        await interop.DisposeAsync();
    }

    [Fact]
    public async Task RegistrationCompletingDuringDisposalIsRemovedImmediately()
    {
        var runtime = new Mock<IJSRuntime>();
        var module = new Mock<IJSObjectReference>();
        var listenerIdSource = new TaskCompletionSource<string>();
        runtime
            .Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
            .ReturnsAsync(module.Object);
        module
            .Setup(value => value.InvokeAsync<string>("listenEvent", It.IsAny<object[]?>()))
            .Returns(() => new ValueTask<string>(listenerIdSource.Task));

        var interop = new BaseInterop(runtime.Object);
        Task registration = interop.AddEventListener(new object(), "element", "change", "Changed");
        await Task.Delay(10);
        Task disposal = interop.DisposeAsync().AsTask();
        listenerIdSource.SetResult("race-listener");

        await Task.WhenAll(registration, disposal);

        Assert.Contains(module.Invocations, invocation =>
            invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
            invocation.Arguments.Count == 2 &&
            (string)invocation.Arguments[0] == "removeEventListener" &&
            invocation.Arguments[1] is object[] arguments &&
            (string)arguments.Single() == "race-listener");
    }

    private sealed class TestInteropComponent : IXBaseComponent
    {
        public void Register(IAsyncDisposable resource) => RegisterDisposable(resource);
    }

    private static List<string> GetRemovedListenerIds(Mock<IJSObjectReference> module)
    {
        return module.Invocations
            .Where(invocation =>
                invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
                invocation.Arguments.Count == 2 &&
                (string)invocation.Arguments[0] == "removeEventListener" &&
                invocation.Arguments[1] is object[] arguments &&
                arguments.Length == 1)
            .Select(invocation => (string)((object[])invocation.Arguments[1])[0])
            .ToList();
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
