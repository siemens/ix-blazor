// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using SiemensIXBlazor.Components.Radio;
using SiemensIXBlazor.Enums.Radio;

namespace SiemensIXBlazor.Tests;

public class RadioGroupTests : TestContextBase
{
    [Fact]
    public void RendersPublicPropertiesAndChildContent()
    {
        var cut = Render<RadioGroup>(parameters => parameters
            .Add(p => p.Id, "radio-group")
            .Add(p => p.Label, "Storage options")
            .Add(p => p.HelperText, "Choose one")
            .Add(p => p.InfoText, "Info")
            .Add(p => p.WarningText, "Warning")
            .Add(p => p.ValidText, "Valid")
            .Add(p => p.InvalidText, "Invalid")
            .Add(p => p.ShowTextAsTooltip, true)
            .Add(p => p.Value, "512")
            .Add(p => p.Direction, RadioGroupDirection.Row)
            .AddChildContent("Options"));

        cut.MarkupMatches("<ix-radio-group id=\"radio-group\" label=\"Storage options\" helper-text=\"Choose one\" info-text=\"Info\" warning-text=\"Warning\" valid-text=\"Valid\" invalid-text=\"Invalid\" show-text-as-tooltip value=\"512\" direction=\"row\">Options</ix-radio-group>");
    }

    [Fact]
    public void DirectionDefaultsToColumn()
    {
        var cut = Render<RadioGroup>(parameters => parameters
            .Add(p => p.Id, "radio-group")
        );

        Assert.Equal(RadioGroupDirection.Column, cut.Instance.Direction);
        Assert.Equal("column", cut.Find("ix-radio-group").GetAttribute("direction"));
    }

    [Fact]
    public async Task ValueChangeEventUpdatesValueAndInvokesCallback()
    {
        string? received = null;
        var cut = Render<RadioGroup>(parameters => parameters
            .Add(p => p.Id, "radio-group")
            .Add(p => p.ValueChangeEvent, EventCallback.Factory.Create<string>(this, value => received = value)));

        await cut.Instance.ValueChange(JsonDocument.Parse("\"option-2\"").RootElement);

        Assert.Equal("option-2", cut.Instance.Value);
        Assert.Equal("option-2", received);
    }
}
