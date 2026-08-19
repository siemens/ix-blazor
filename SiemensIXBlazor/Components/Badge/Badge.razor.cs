// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Badge;

namespace SiemensIXBlazor.Components;

public partial class Badge
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public bool AlignLeft { get; set; }

    [Parameter]
    public string? AriaLabelIcon { get; set; }

    [Parameter]
    public string? Background { get; set; }

    [Parameter]
    public string? BadgeColor { get; set; }

    [Parameter]
    public bool Border { get; set; }

    [Parameter]
    public bool EnableAnimation { get; set; }

    [Parameter]
    public string? Icon { get; set; }

    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public int OffsetX { get; set; }

    [Parameter]
    public int OffsetY { get; set; }

    [Parameter]
    public bool Outline { get; set; }

    [Parameter]
    public BadgePosition Position { get; set; } = BadgePosition.TopAfter;

    [Parameter]
    public object? TooltipText { get; set; } = false;

    [Parameter]
    public BadgeType Type { get; set; } = BadgeType.Counter;

    [Parameter]
    public BadgeVariant Variant { get; set; } = BadgeVariant.Primary;

    private string? TooltipTextAttribute => TooltipText switch
    {
        bool value => value ? string.Empty : null,
        null => null,
        _ => TooltipText.ToString(),
    };

    private string TypeAttribute => Type switch
    {
        BadgeType.Counter => "counter",
        BadgeType.Dot => "dot",
        BadgeType.Label => "label",
        BadgeType.StatusIcon => "status-icon",
        _ => throw new ArgumentOutOfRangeException(),
    };

    private string PositionAttribute => Position switch
    {
        BadgePosition.TopAfter => "top-after",
        BadgePosition.BottomAfter => "bottom-after",
        _ => throw new ArgumentOutOfRangeException(),
    };

    private string VariantAttribute => Variant switch
    {
        BadgeVariant.Alarm => "alarm",
        BadgeVariant.Critical => "critical",
        BadgeVariant.Custom => "custom",
        BadgeVariant.Error => "error",
        BadgeVariant.Info => "info",
        BadgeVariant.Neutral => "neutral",
        BadgeVariant.Primary => "primary",
        BadgeVariant.Success => "success",
        BadgeVariant.Warning => "warning",
        _ => throw new ArgumentOutOfRangeException(),
    };
}
