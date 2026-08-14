// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components.Modal;
using SiemensIXBlazor.Tests;
using Xunit;

namespace SiemensIXBlazor.Tests.Modal;

public class ModalHeaderTest : TestContextBase
{
    [Fact]
    public void ModalHeader_ShouldRenderOfficialProperties()
    {
        var component = RenderComponent<ModalHeader>(parameters => parameters
            .Add(p => p.HideClose, true)
            .Add(p => p.Icon, "info")
            .Add(p => p.AriaLabelIcon, "Information")
            .Add(p => p.AriaLabelCloseIconButton, "Close dialog")
            .Add(p => p.IconColor, "color-info")
            .AddChildContent("Modal title"));

        var element = component.Find("ix-modal-header");
        Assert.Equal("true", element.GetAttribute("hide-close"));
        Assert.Equal("info", element.GetAttribute("icon"));
        Assert.Equal("Information", element.GetAttribute("aria-label-icon"));
        Assert.Equal("Close dialog", element.GetAttribute("aria-label-close-icon-button"));
        Assert.Equal("color-info", element.GetAttribute("icon-color"));
        Assert.Contains("Modal title", element.InnerHtml);
    }

    [Fact]
    public void ModalHeader_ShouldUseOfficialCloseLabelByDefault()
    {
        var component = RenderComponent<ModalHeader>();

        Assert.Equal("Close modal", component.Instance.AriaLabelCloseIconButton);
    }
}
