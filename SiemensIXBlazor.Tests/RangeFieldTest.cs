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
using SiemensIXBlazor.Components.RangeField;
using SiemensIXBlazor.Enums.RangeField;

namespace SiemensIXBlazor.Tests;

public class RangeFieldTest : TestContextBase
{
    [Theory]
    [InlineData(RangeFieldType.TimeRange, "time-range")]
    [InlineData(RangeFieldType.DateRange, "date-range")]
    [InlineData(RangeFieldType.DateTimeRange, "datetime-range")]
    public void TypeUsesOfficialAttributeValue(RangeFieldType type, string expected)
    {
        var cut = RenderComponent<RangeField>(parameters => parameters
            .Add(p => p.Type, type)
            .Add(p => p.HideArrow, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "range inputs"))));

        Assert.Contains($"type=\"{expected}\"", cut.Markup);
        Assert.Contains("hide-arrow", cut.Markup);
        Assert.Contains("range inputs", cut.Markup);
    }
}
