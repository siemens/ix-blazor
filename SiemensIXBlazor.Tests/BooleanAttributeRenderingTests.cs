// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Components.Checkbox;
using SiemensIXBlazor.Components.TabItem;

namespace SiemensIXBlazor.Tests;

public class BooleanAttributeRenderingTests : TestContextBase
{
    [Fact]
    public void BooleanAttributesOmitFalseAndRenderTrue()
    {
        var buttonFalse = Render<Button>(parameters => parameters
            .Add(p => p.Disabled, false));
        var buttonTrue = Render<Button>(parameters => parameters
            .Add(p => p.Disabled, true));

        Assert.Null(buttonFalse.Find("ix-button").GetAttribute("disabled"));
        Assert.Equal("true", buttonTrue.Find("ix-button").GetAttribute("disabled"));

        var checkboxFalse = Render<Checkbox>(parameters => parameters
            .Add(p => p.Label, "Option")
            .Add(p => p.Checked, false));
        var checkboxTrue = Render<Checkbox>(parameters => parameters
            .Add(p => p.Label, "Option")
            .Add(p => p.Checked, true));

        Assert.Null(checkboxFalse.Find("ix-checkbox").GetAttribute("checked"));
        Assert.Equal("true", checkboxTrue.Find("ix-checkbox").GetAttribute("checked"));

        var selectItemFalse = Render<SelectItem>(parameters => parameters
            .Add(p => p.Id, "option-false")
            .Add(p => p.Label, "Option")
            .Add(p => p.Value, "option")
            .Add(p => p.Selected, false));
        var selectItemTrue = Render<SelectItem>(parameters => parameters
            .Add(p => p.Id, "option-true")
            .Add(p => p.Label, "Option")
            .Add(p => p.Value, "option")
            .Add(p => p.Selected, true));

        Assert.Null(selectItemFalse.Find("ix-select-item").GetAttribute("selected"));
        Assert.Equal("true", selectItemTrue.Find("ix-select-item").GetAttribute("selected"));

        var tabItemFalse = Render<TabItem>(parameters => parameters
            .Add(p => p.TabKey, "option")
            .Add(p => p.Selected, false));
        var tabItemTrue = Render<TabItem>(parameters => parameters
            .Add(p => p.TabKey, "option")
            .Add(p => p.Selected, true));

        Assert.Null(tabItemFalse.Find("ix-tab-item").GetAttribute("selected"));
        Assert.Equal("true", tabItemTrue.Find("ix-tab-item").GetAttribute("selected"));

        var eventListFalse = Render<SiemensIXBlazor.Components.EventList>(parameters => parameters
            .Add(p => p.Animated, false));
        var eventListTrue = Render<SiemensIXBlazor.Components.EventList>(parameters => parameters
            .Add(p => p.Animated, true));

        Assert.Null(eventListFalse.Find("ix-event-list").GetAttribute("animated"));
        Assert.Equal("true", eventListTrue.Find("ix-event-list").GetAttribute("animated"));

        var keyValueListFalse = Render<KeyValueList>(parameters => parameters
            .Add(p => p.Striped, false));
        var keyValueListTrue = Render<KeyValueList>(parameters => parameters
            .Add(p => p.Striped, true));

        Assert.Null(keyValueListFalse.Find("ix-key-value-list").GetAttribute("striped"));
        Assert.Equal("true", keyValueListTrue.Find("ix-key-value-list").GetAttribute("striped"));
    }
}
