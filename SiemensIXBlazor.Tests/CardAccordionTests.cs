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
using SiemensIXBlazor.Enums.CardAccordion;

namespace SiemensIXBlazor.Tests;

public class CardAccordionTests : TestContextBase
{
    [Fact]
    public void RendersOfficialPropertiesAndContent()
    {
        var cut = Render<CardAccordion>(parameters => parameters
            .Add(p => p.AriaLabelExpandButton, "Expand card")
            .Add(p => p.Collapse, true)
            .Add(p => p.Variant, CardAccordionVariant.success)
            .Add(p => p.ChildContent, builder => builder.AddContent(0, "Accordion content")));

        cut.MarkupMatches("<ix-card-accordion slot=\"card-accordion\" aria-label-expand-button=\"Expand card\" collapse=\"\" variant=\"success\">Accordion content</ix-card-accordion>");
    }

    [Fact]
    public void CollapseAndVariantHaveOfficialDefaults()
    {
        var cut = Render<CardAccordion>((Action<Bunit.ComponentParameterCollectionBuilder<CardAccordion>>)(_ => { }));

        Assert.False(cut.Instance.Collapse);
        Assert.Equal(CardAccordionVariant.outline, cut.Instance.Variant);
        Assert.DoesNotContain("collapse", cut.Markup);
        Assert.Contains("variant=\"outline\"", cut.Markup);
        Assert.Contains("slot=\"card-accordion\"", cut.Markup);
    }
}
