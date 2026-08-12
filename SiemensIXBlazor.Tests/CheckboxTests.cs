// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components.Checkbox;
using SiemensIXBlazor.Enums.Checkbox;

namespace SiemensIXBlazor.Tests;

public class CheckboxTests : TestContextBase
{
    [Fact]
    public void CheckboxRendersOfficialProperties()
    {
        var cut = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Id, "checkbox")
            .Add(p => p.Label, "Accept terms")
            .Add(p => p.Name, "terms")
            .Add(p => p.Value, "accepted")
            .Add(p => p.Checked, true)
            .Add(p => p.Indeterminate, true)
            .Add(p => p.Required, true));

        cut.MarkupMatches("<ix-checkbox id=\"checkbox\" checked indeterminate label=\"Accept terms\" name=\"terms\" required value=\"accepted\"></ix-checkbox>");
    }

    [Fact]
    public void CheckboxGroupRendersValidationTextAndDirection()
    {
        var cut = RenderComponent<CheckboxGroup>(parameters => parameters
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

        cut.MarkupMatches("<ix-checkbox-group id=\"checkbox-group\" label=\"Options\" info-text=\"Info\" warning-text=\"Warning\" invalid-text=\"Invalid\" valid-text=\"Valid\" helper-text=\"Choose any\" direction=\"row\" show-text-as-tooltip>Options</ix-checkbox-group>");
    }

    [Fact]
    public void CheckboxGroupDirectionDefaultsToColumn()
    {
        var cut = RenderComponent<CheckboxGroup>(("Id", "checkbox-group"));

        Assert.Equal(CheckboxGroupDirection.Column, cut.Instance.Direction);
        Assert.Equal("column", cut.Find("ix-checkbox-group").GetAttribute("direction"));
    }
}
