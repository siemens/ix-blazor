// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using System.Text.Json;
using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components.Radio;

namespace SiemensIXBlazor.Tests;

public class RadioTests : TestContextBase
{
    [Fact]
    public void RendersOfficialPropertiesAndBooleanStates()
    {
        var cut = Render<Radio>(parameters => parameters
            .Add(p => p.Id, "radio")
            .Add(p => p.Checked, true)
            .Add(p => p.Disabled, true)
            .Add(p => p.Label, "Option")
            .Add(p => p.Name, "options")
            .Add(p => p.Required, true)
            .Add(p => p.Value, "option")
            .Add(p => p.CssClass, "custom-radio"));

        var element = cut.Find("ix-radio");
        Assert.Equal("radio", element.GetAttribute("id"));
        Assert.Equal("true", element.GetAttribute("checked"));
        Assert.Equal("true", element.GetAttribute("disabled"));
        Assert.Equal("Option", element.GetAttribute("label"));
        Assert.Equal("options", element.GetAttribute("name"));
        Assert.Equal("true", element.GetAttribute("required"));
        Assert.Equal("option", element.GetAttribute("value"));
        Assert.Equal("custom-radio", element.GetAttribute("class"));
    }

    [Fact]
    public void OmitsFalseBooleanAttributes()
    {
        var cut = Render<Radio>(parameters => parameters.Add(p => p.Id, "radio"));

        var element = cut.Find("ix-radio");
        Assert.False(element.HasAttribute("checked"));
        Assert.False(element.HasAttribute("disabled"));
        Assert.False(element.HasAttribute("required"));
    }

    [Fact]
    public async Task InvokesCheckedValueAndBlurCallbacks()
    {
        bool? checkedValue = null;
        string? value = null;
        var blurred = false;
        var cut = Render<Radio>(parameters => parameters
            .Add(p => p.Id, "radio")
            .Add(p => p.CheckedChangeEvent, EventCallback.Factory.Create<bool>(this, current => checkedValue = current))
            .Add(p => p.ValueChangeEvent, EventCallback.Factory.Create<string>(this, current => value = current))
            .Add(p => p.IxBlurEvent, EventCallback.Factory.Create(this, () => blurred = true)));

        await cut.InvokeAsync(() => cut.Instance.CheckedChange(JsonDocument.Parse("true").RootElement));
        await cut.InvokeAsync(() => cut.Instance.ValueChange(JsonDocument.Parse("\"selected\"").RootElement));
        await cut.InvokeAsync(() => cut.Instance.IxBlur());

        Assert.True(checkedValue);
        Assert.True(cut.Instance.Checked);
        Assert.Equal("selected", value);
        Assert.Equal("selected", cut.Instance.Value);
        Assert.True(blurred);
    }
}
