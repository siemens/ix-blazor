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
using SiemensIXBlazor.Components.Modal;

namespace SiemensIXBlazor.Tests.Modal;

public class LoadingServiceTests
{
    [Fact]
    public async Task ShowAndUpdateLoadingModalForwardsLifecycleOperations()
    {
        var runtime = new Mock<IJSRuntime>();
        var reference = new Mock<IJSObjectReference>();
        runtime
            .Setup(value => value.InvokeAsync<IJSObjectReference>(
                "siemensIXInterop.modal.showLoading", It.IsAny<object[]?>()))
            .ReturnsAsync(reference.Object);
        var service = new LoadingService(runtime.Object);

        var context = await service.ShowModalLoadingAsync(new ModalLoadingOptions
        {
            Message = "Loading",
            Centered = true,
        });
        await context.UpdateAsync("Still loading");
        await context.FinishAsync();
        await context.FinishAsync("Complete");
        await context.FinishAsync("Complete", 250);
        await context.DisposeAsync();

        Assert.Contains(runtime.Invocations, invocation =>
            invocation.Method.Name == nameof(IJSRuntime.InvokeAsync) &&
            invocation.Arguments.Count == 2 &&
            (string)invocation.Arguments[0] == "siemensIXInterop.modal.showLoading");
        Assert.Equal(1, CountInvocations(reference, "update"));
        Assert.Equal(3, CountInvocations(reference, "finish"));
        Assert.Equal(1, reference.Invocations.Count(invocation => invocation.Method.Name == nameof(IJSObjectReference.DisposeAsync)));
    }

    [Fact]
    public async Task ShowModalLoadingRejectsNullOptions()
    {
        var service = new LoadingService(new Mock<IJSRuntime>().Object);

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            service.ShowModalLoadingAsync(null!));
    }

    private static int CountInvocations(Mock<IJSObjectReference> reference, string methodName) =>
        reference.Invocations.Count(invocation =>
            invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
            invocation.Arguments.Count == 2 &&
            (string)invocation.Arguments[0] == methodName);
}
