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
using SiemensIXBlazor.Components.DateTimeInput;
using SiemensIXBlazor.Enums.Input;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Tests;

public class DateTimeInputTest : TestContextBase
{
    [Fact]
    public void OfficialPropertiesAndSlotsRender()
    {
        var cut = RenderComponent<DateTimeInput>(parameters => parameters
            .Add(p => p.Id, "datetime-input")
            .Add(p => p.Format, "yyyy/LL/dd HH:mm")
            .Add(p => p.TextAlignment, InputTextAlignment.End)
            .Add(p => p.StartSlot, (RenderFragment)(builder => builder.AddContent(0, "start")))
            .Add(p => p.EndSlot, (RenderFragment)(builder => builder.AddContent(0, "end"))));

        Assert.Contains("format=\"yyyy/LL/dd HH:mm\"", cut.Markup);
        Assert.Contains("text-alignment=\"end\"", cut.Markup);
        Assert.Contains("slot=\"start\"", cut.Markup);
        Assert.Contains("slot=\"end\"", cut.Markup);
    }

    [Fact]
    public async Task ValueAndValidityEventsAreTyped()
    {
        string? value = null;
        DateTimeInputValidityState? validity = null;
        var cut = RenderComponent<DateTimeInput>(parameters => parameters
            .Add(p => p.Id, "datetime-input")
            .Add(p => p.ValueChangeEvent, EventCallback.Factory.Create<string?>(this, item => value = item))
            .Add(p => p.ValidityStateChangeEvent, EventCallback.Factory.Create<DateTimeInputValidityState>(this, item => validity = item)));

        await cut.InvokeAsync(() => cut.Instance.ValueChange(JsonSerializer.SerializeToElement("2026/01/01 12:00:00")));
        await cut.InvokeAsync(() => cut.Instance.ValidityStateChange(JsonSerializer.SerializeToElement(new { patternMismatch = true, invalidReason = "bad" })));

        Assert.Equal("2026/01/01 12:00:00", value);
        Assert.True(validity!.PatternMismatch);
        Assert.Equal("bad", validity.InvalidReason);
    }
}
