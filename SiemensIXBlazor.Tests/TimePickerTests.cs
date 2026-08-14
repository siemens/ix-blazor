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
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.TimePicker;

namespace SiemensIXBlazor.Tests;

public class TimePickerTests : TestContextBase
{
    [Fact]
    public void OfficialDefaultsAreRendered()
    {
        var cut = Render<TimePicker>(parameters => parameters.Add(p => p.Id, "time-picker"));
        var picker = cut.Find("ix-time-picker");

        Assert.Equal("rounded", picker.GetAttribute("corners"));
        Assert.Equal("TT", picker.GetAttribute("format"));
        Assert.Equal("100", picker.GetAttribute("millisecond-interval"));
        Assert.Null(picker.GetAttribute("time"));
        Assert.DoesNotContain("width:", picker.GetAttribute("style") ?? string.Empty);
    }

    [Theory]
    [InlineData(TimePickerCorners.Left, "left")]
    [InlineData(TimePickerCorners.Right, "right")]
    [InlineData(TimePickerCorners.Rounded, "rounded")]
    [InlineData(TimePickerCorners.Straight, "straight")]
    public void CornersRenderCorrectly(TimePickerCorners corners, string expected)
    {
        var cut = Render<TimePicker>(parameters => parameters
            .Add(p => p.Id, "time-picker")
            .Add(p => p.Corners, corners));

        Assert.Equal(expected, cut.Find("ix-time-picker").GetAttribute("corners"));
    }

    [Fact]
    public async Task TimeEventsInvokeCallbacks()
    {
        string? selected = null;
        string? changed = null;
        var cut = Render<TimePicker>(parameters => parameters
            .Add(p => p.Id, "time-picker")
            .Add(p => p.TimeSelectEvent, EventCallback.Factory.Create<string>(this, value => selected = value))
            .Add(p => p.TimeChangeEvent, EventCallback.Factory.Create<string>(this, value => changed = value)));

        await cut.Instance.TimeSelected("15:30");
        await cut.Instance.TimeChanged("15:31");

        Assert.Equal("15:30", selected);
        Assert.Equal("15:31", changed);
    }
}
