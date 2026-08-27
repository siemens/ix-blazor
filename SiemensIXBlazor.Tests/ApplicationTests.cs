// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
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
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Objects.Application;
using System.Reflection;

namespace SiemensIXBlazor.Tests
{
    public class ApplicationTests : TestContextBase
    {
        [Fact]
        public void ApplicationRendersWithoutCrashing()
        {
            // Arrange
            var cut = Render<Application>(parameters => {
                parameters.Add(p => p.Id, "testId");
                parameters.Add(p => p.ForceBreakpoint, Enums.ForceBreakpoint.lg);
                parameters.Add(p => p.Theme, "testTheme");
                parameters.Add(p => p.ColorSchema, Enums.ColorSchema.Dark);
            });

            // Assert
            cut.MarkupMatches("<ix-application id='testId' force-breakpoint='lg' theme='testTheme' color-schema='dark'></ix-application>");
        }

        [Fact]
        public async Task AppliesBreakpointAndApplicationConfigChangesAfterRender()
        {
            var runtime = new Mock<IJSRuntime>();
            var module = new Mock<IJSObjectReference>();
            runtime.Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
                .ReturnsAsync(module.Object);
            Services.AddSingleton(runtime.Object);

            var cut = Render<Application>(parameters => parameters
                .Add(p => p.Id, "application")
                .Add(p => p.Breakpoints, new[] { "sm", "md" }));

            cut.Render(parameters => parameters
                .Add(p => p.Breakpoints, new[] { "xs", "lg" })
                .Add(p => p.AppSwitchConfig, new AppSwitchConfig { CurrentAppId = "next" }));
            await Task.Delay(25);

            Assert.Contains(module.Invocations, invocation =>
                invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
                invocation.Arguments.Count == 2 &&
                (string)invocation.Arguments[0] == "setBreakpoints");
            Assert.Contains(module.Invocations, invocation =>
                invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
                invocation.Arguments.Count == 2 &&
                (string)invocation.Arguments[0] == "setApplicationConfig");
        }

        [Fact]
        public async Task IgnoresDisconnectedBreakpointUpdates()
        {
            var runtime = new Mock<IJSRuntime>();
            var module = new Mock<IJSObjectReference>();
            runtime.Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
                .ReturnsAsync(module.Object);
            module.SetupSequence(value => value.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>(
                    "setBreakpoints", It.IsAny<object[]?>()))
                .Returns(new ValueTask<Microsoft.JSInterop.Infrastructure.IJSVoidResult>(default(Microsoft.JSInterop.Infrastructure.IJSVoidResult)!))
                .ThrowsAsync(new JSDisconnectedException("disconnected"));
            Services.AddSingleton(runtime.Object);

            var cut = Render<Application>(parameters => parameters.Add(p => p.Id, "application"));
            cut.Render(parameters => parameters.Add(p => p.Breakpoints, new[] { "xs" }));
            await Task.Delay(25);
        }

        [Fact]
        public async Task RapidApplicationConfigUpdatesLeaveTheLatestConfigApplied()
        {
            var runtime = new Mock<IJSRuntime>();
            var module = new Mock<IJSObjectReference>();
            runtime.Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
                .ReturnsAsync(module.Object);
            Services.AddSingleton(runtime.Object);

            var cut = Render<Application>(parameters => parameters.Add(p => p.Id, "application"));
            cut.Render(parameters => parameters.Add(p => p.AppSwitchConfig,
                new AppSwitchConfig { CurrentAppId = "first" }));
            cut.Render(parameters => parameters.Add(p => p.AppSwitchConfig,
                new AppSwitchConfig { CurrentAppId = "latest" }));
            await Task.Delay(25);

            var configCalls = module.Invocations
                .Where(invocation => invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync)
                    && invocation.Arguments.Count == 2
                    && (string)invocation.Arguments[0] == "setApplicationConfig")
                .ToList();

            Assert.NotEmpty(configCalls);
            var arguments = Assert.IsType<object[]>(configCalls[^1].Arguments[1]);
            Assert.Contains("latest", Assert.IsType<string>(arguments[1]));
        }

        [Fact]
        public async Task IgnoresAStaleApplicationConfigVersion()
        {
            var runtime = new Mock<IJSRuntime>();
            var module = new Mock<IJSObjectReference>();
            runtime.Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
                .ReturnsAsync(module.Object);
            Services.AddSingleton(runtime.Object);

            var cut = Render<Application>(parameters => parameters.Add(p => p.Id, "application"));
            cut.Render(parameters => parameters.Add(p => p.AppSwitchConfig,
                new AppSwitchConfig { CurrentAppId = "current" }));
            await Task.Delay(25);
            module.Invocations.Clear();

            var applyMethod = typeof(Application).GetMethod(
                "ApplyApplicationConfigAsync", BindingFlags.Instance | BindingFlags.NonPublic)!;
            var staleTask = Assert.IsAssignableFrom<Task>(applyMethod.Invoke(cut.Instance,
                [0, new AppSwitchConfig { CurrentAppId = "stale" }]));
            await staleTask;

            Assert.DoesNotContain(module.Invocations, invocation =>
                invocation.Method.Name == nameof(IJSObjectReference.InvokeAsync) &&
                invocation.Arguments.Count > 0 &&
                (string)invocation.Arguments[0] == "setApplicationConfig");
        }

        [Fact]
        public void IgnoresDisconnectedApplicationConfigUpdates()
        {
            var runtime = new Mock<IJSRuntime>();
            var module = new Mock<IJSObjectReference>();
            runtime.Setup(value => value.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
                .ReturnsAsync(module.Object);
            module.Setup(value => value.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>(
                    "setApplicationConfig", It.IsAny<object[]?>()))
                .ThrowsAsync(new JSDisconnectedException("disconnected"));
            Services.AddSingleton(runtime.Object);

            Render<Application>(parameters => parameters
                .Add(p => p.Id, "application")
                .Add(p => p.AppSwitchConfig, new AppSwitchConfig { CurrentAppId = "app" }));
        }

        [Fact]
        public void ColorSchemaDefaultsToSystem()
        {
            var cut = Render<Application>((Action<Bunit.ComponentParameterCollectionBuilder<Application>>)(_ => { }));

            Assert.Equal(Enums.ColorSchema.System, cut.Instance.ColorSchema);
            Assert.Contains("color-schema=\"system\"", cut.Markup);
        }

        [Fact]
        public void AppSwitchConfig_SetsValueAndCallsInitialParameter()
        {
            // Arrange
            var config = new AppSwitchConfig
            {
                CurrentAppId = "app-1"
            };
            var cut = Render<Application>(parameters => parameters
                .Add(p => p.AppSwitchConfig, config));

            // Assert
            Assert.Equal(config, cut.Instance.AppSwitchConfig);
        }
        
    }
}
