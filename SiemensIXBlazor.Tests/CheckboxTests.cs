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
using SiemensIXBlazor.Components.Checkbox;
using SiemensIXBlazor.Enums.Checkbox;
using System.Text.Json;

namespace SiemensIXBlazor.Tests;

public class CheckboxTests : TestContextBase
{
    [Fact]
    public void CheckboxRendersOfficialProperties()
    {
        var cut = Render<Checkbox>(parameters => parameters
            .Add(p => p.Id, "checkbox")
            .Add(p => p.Label, "Accept terms")
            .Add(p => p.Name, "terms")
            .Add(p => p.Value, "accepted")
            .Add(p => p.Checked, true)
            .Add(p => p.Indeterminate, true)
            .Add(p => p.Required, true));

        cut.MarkupMatches("<ix-checkbox id=\"checkbox\" checked='true' indeterminate='true' label=\"Accept terms\" name=\"terms\" required='true' value=\"accepted\"></ix-checkbox>");
    }

    [Fact]
    public void CheckboxGroupRendersValidationTextAndDirection()
    {
        var cut = Render<CheckboxGroup>(parameters => parameters
            .Add(p => p.Id, "checkbox-group")
            .Add(p => p.Label, "Options")
            .Add(p => p.HelperText, "Choose any")
            .Add(p => p.InfoText, "Info")
            .Add(p => p.WarningText, "Warning")
            .Add(p => p.ValidText, "Valid")
            .Add(p => p.InvalidText, "Invalid")
            .Add(p => p.ShowTextAsTooltip, true)
            .Add(p => p.Direction, CheckboxGroupDirection.Row)
            .AddChildContent("Options"));

        cut.MarkupMatches("<ix-checkbox-group id=\"checkbox-group\" label=\"Options\" info-text=\"Info\" warning-text=\"Warning\" invalid-text=\"Invalid\" valid-text=\"Valid\" helper-text=\"Choose any\" direction=\"row\" show-text-as-tooltip='true'>Options</ix-checkbox-group>");
    }

    [Fact]
    public void CheckboxGroupDirectionDefaultsToColumn()
    {
        var cut = Render<CheckboxGroup>(parameters => parameters
            .Add(p => p.Id, "checkbox-group")
        );

        Assert.Equal(CheckboxGroupDirection.Column, cut.Instance.Direction);
        Assert.Equal("column", cut.Find("ix-checkbox-group").GetAttribute("direction"));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CheckedChangedUpdatesTheValueAndCallback(bool value)
    {
        bool? callbackValue = null;
        var cut = Render<Checkbox>(parameters => parameters
            .Add(p => p.Id, "checkbox")
            .Add(p => p.CheckedChangeEvent, EventCallback.Factory.Create<bool>(this, changed => callbackValue = changed)));

        await cut.Instance.CheckedChanged(JsonSerializer.SerializeToElement(value));

        Assert.Equal(value, cut.Instance.Checked);
        Assert.Equal(value, callbackValue);
    }

    [Fact]
    public void CheckboxFalseStatesAreOmittedFromTheMarkup()
    {
        var cut = Render<Checkbox>(parameters => parameters
            .Add(p => p.Id, "checkbox")
            .Add(p => p.Checked, false)
            .Add(p => p.Indeterminate, false));

        Assert.DoesNotContain(" checked=", cut.Markup);
        Assert.DoesNotContain(" indeterminate=", cut.Markup);
    }

    [Fact]
    public void CheckboxParameterUpdatesAreAppliedAfterInitialRender()
    {
        var cut = Render<Checkbox>(parameters => parameters
            .Add(p => p.Id, "checkbox")
            .Add(p => p.Checked, true)
            .Add(p => p.Indeterminate, true));

        cut.Render(parameters => parameters
            .Add(p => p.Checked, false)
            .Add(p => p.Indeterminate, false));

        Assert.False(cut.Instance.Checked);
        Assert.False(cut.Instance.Indeterminate);
        Assert.DoesNotContain(" checked=", cut.Markup);
        Assert.DoesNotContain(" indeterminate=", cut.Markup);
    }

    [Fact]
    public async Task ValueChangedUsesTheOfficialDefaultForNullValues()
    {
        string? callbackValue = null;
        var cut = Render<Checkbox>(parameters => parameters
            .Add(p => p.Id, "checkbox")
            .Add(p => p.ValueChangedEvent, EventCallback.Factory.Create<string>(this, value => callbackValue = value)));

        await cut.Instance.ValueChanged(JsonDocument.Parse("null").RootElement);

        Assert.Equal("on", cut.Instance.Value);
        Assert.Equal("on", callbackValue);
    }
}
