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
using Microsoft.JSInterop;
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
            Assert.Equal("TT", picker.GetAttribute("format"));
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

            // Assert
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
                .Add(p => p.Time, "15:30")
                .Add(p => p.Class, "my-class")
                .Add(p => p.Style, "color:red;")
                .Add(p => p.Format, "HH:mm")
            );

            // Assert
            cut.MarkupMatches(
                @"<ix-time-picker data-test=""my-value"" id=""tp1""
                    corners=""left""
                    format=""HH:mm""
                    hour-interval=""1""
                    millisecond-interval=""1""
                    minute-interval=""1""
                    second-interval=""1""
                    time=""15:30""
                    i18n-hour-column-header=""hr""
                    i18n-minute-column-header=""min""
                    i18n-second-column-header=""sec""
                    i18n-millisecond-column-header=""ms""
                    style=""display: block; width: 20rem; color:red;""
                    class=""my-class"">
                </ix-time-picker>"
            );
        }

        [Fact]
        public async Task TimeSelectAndTimeChange_JsInvokable_InvokeEventCallbacks()
        {
            // Arrange
            var cut = RenderComponent<TimePicker>(parameters => parameters
                .Add(p => p.Id, "tp-callback")
                .Add(p => p.TimeSelectEvent,
                    EventCallback.Factory.Create<string>(this, d => TimeSelectedValue = d))
                .Add(p => p.TimeChangeEvent,
                    EventCallback.Factory.Create<string>(this, d => TimeChangedValue = d))
            );

            // Act
            await cut.InvokeAsync(() => cut.Instance.TimeSelected("15:30"));
            await cut.InvokeAsync(() => cut.Instance.TimeChanged("15:31"));

            // Assert
            Assert.Equal("15:30", TimeSelectedValue);
            Assert.Equal("15:31", TimeChangedValue);
        }

        private string? TimeSelectedValue { get; set; }
        private string? TimeChangedValue { get; set; }
    }
}
