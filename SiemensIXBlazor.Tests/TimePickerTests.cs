// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
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
using SiemensIXBlazor.Enums.DatePicker;

namespace SiemensIXBlazor.Tests
{
    public class TimePickerTests : TestContextBase
    {
        [Fact]
        public void ComponentRendersWithDefaultParameters()
        {
            // Arrange & Act
            var cut = RenderComponent<TimePicker>(parameters => parameters
                .Add(p => p.Id, "tp-default")
            );
            var picker = cut.Find("ix-time-picker");

            // Assert required attributes
            Assert.Equal("tp-default", picker.GetAttribute("id"));
            Assert.Equal("rounded", picker.GetAttribute("corners"));
            Assert.Equal("yyyy/MM/dd", picker.GetAttribute("format"));
            Assert.Null(picker.GetAttribute("show-hour"));
            Assert.Null(picker.GetAttribute("show-minutes"));
            Assert.Null(picker.GetAttribute("show-seconds"));
            Assert.Equal("Done", picker.GetAttribute("text-select-time"));
            // Default Time is dynamic; just assert it's non-empty
            Assert.False(string.IsNullOrWhiteSpace(picker.GetAttribute("time")));
        }

        [Theory]
        [InlineData(DatePickerCorners.Left, "left")]
        [InlineData(DatePickerCorners.Right, "right")]
        [InlineData(DatePickerCorners.Rounded, "rounded")]
        public void CornersRenderCorrectly(DatePickerCorners corners, string expected)
        {
            // Arrange & Act
            var cut = RenderComponent<TimePicker>(parameters => parameters
                .Add(p => p.Id, "tp-corners")
                .Add(p => p.Corners, corners)
            );
            var picker = cut.Find("ix-time-picker");

            // Assert corners attribute matches enum string
            Assert.Equal(expected, picker.GetAttribute("corners"));
        }

        [Fact]
        public void ComponentRendersWithAllParametersSetCorrectly()
        {
            // Arrange & Act
            var cut = RenderComponent<TimePicker>(parameters => parameters
                .AddUnmatched("data-test", "my-value")
                .Add(p => p.Id, "tp1")
                .Add(p => p.Corners, DatePickerCorners.Left)
                .Add(p => p.ShowHour, true)
                .Add(p => p.ShowMinutes, true)
                .Add(p => p.ShowSeconds, true)
                .Add(p => p.TextSelectTime, "SelectTime")
                .Add(p => p.Time, "2025-04-24T15:30:00")
                .Add(p => p.Class, "my-class")
                .Add(p => p.Style, "color:red;")
                .Add(p=>p.Format,"MM-dd")
            );

            // Assert full markup
            cut.MarkupMatches(
                @"<ix-time-picker data-test=""my-value"" id=""tp1""
                    corners=""left""
                    format=""MM-dd""
                    hour-interval=""1""
                    millisecond-interval=""1""
                    minute-interval=""1""  
                    second-interval=""1""
                    show-hour
                    show-minutes
                    show-seconds
                    text-select-time=""SelectTime""
                    time=""2025-04-24T15:30:00""
                    style=""display: block; width: 20rem; color:red;""
                    class=""my-class"">
                </ix-time-picker>"
            );
        }

        [Fact]
        public void OnAfterRender_InvokesJsRuntimeImportForModule()
        {
            // Arrange
            var jsRuntimeMock = Mock.Get(Services.GetRequiredService<IJSRuntime>());

            // Act: first render triggers OnAfterRenderAsync(true)
            RenderComponent<TimePicker>(parameters => parameters.Add(p => p.Id, "tp-js"));

            // Assert: import called once to load the JS module
            jsRuntimeMock.Verify(
                js => js.InvokeAsync<IJSObjectReference>(
                    "import", It.IsAny<object[]>()),
                Times.Once);
        }

        [Fact]
        public async Task DoneAndTimeChanged_JsInvokable_InvokeEventCallbacks()
        {
            // Arrange
            var cut = RenderComponent<TimePicker>(parameters => parameters
                .Add(p => p.Id, "tp-callback")
                .Add(p => p.DoneEvent, EventCallback.Factory.Create<string>(this, (string d) => DoneValue = d))
                .Add(p => p.TimeChangeEvent, EventCallback.Factory.Create<string>(this, (string d) => TimeChangedValue = d))
            );

            // Act: invoke the two JSInvokable methods
            await cut.InvokeAsync(() => cut.Instance.Done("2025/04/24"));
            await cut.InvokeAsync(() => cut.Instance.TimeChanged("2025/04/25"));

            // Assert that our handlers were called
            Assert.Equal("2025/04/24", DoneValue);
            Assert.Equal("2025/04/25", TimeChangedValue);
        }

        private string? DoneValue { get; set; }
        private string? TimeChangedValue { get; set; }
    }
}
