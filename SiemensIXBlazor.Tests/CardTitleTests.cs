// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Tests;

public class CardTitleTests : TestContextBase
{
    [Fact]
    public void RendersDefaultAndTitleActionsSlots()
    {
        var cut = Render<CardTitle>(parameters => parameters
            .Add(p => p.ChildContent, builder => builder.AddContent(0, "Title"))
            .Add(p => p.TitleActions, builder => builder.AddContent(0, "Actions")));

        Assert.Contains("Title", cut.Markup);
        Assert.Contains("slot=\"title-actions\"", cut.Markup);
        Assert.Contains("Actions", cut.Markup);
    }
}
