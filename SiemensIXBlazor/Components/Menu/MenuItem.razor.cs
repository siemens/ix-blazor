// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Menu;

namespace SiemensIXBlazor.Components.Menu
{
  public partial class MenuItem : IXBaseComponent
  {
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool Active { get; set; } = false;
    [Parameter]
    public bool Disabled { get; set; } = false;
    [Parameter]
    public bool Home { get; set; } = false;
    [Parameter]
    public bool Bottom { get; set; } = false;
    [Parameter]
    public string? Icon { get; set; }
    [Parameter]
    public int? Notifications { get; set; }
    [Parameter]
    public string? Label { get; set; }
    [Parameter]
    public string? Slot { get; set; }
    [Parameter]
    public string? TooltipText { get; set; }
    [Parameter]
    public string? Href { get; set; }
    [Parameter]
    public MenuItemTarget Target { get; set; } = MenuItemTarget._self;
    [Parameter]
    public string? Rel { get; set; }
  }
}
