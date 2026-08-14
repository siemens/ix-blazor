// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Modal;

namespace SiemensIXBlazor.Components.Modal;

/// <summary>
/// Configuration used to open a modal through <see cref="ModalService"/>.
/// </summary>
public class ModalConfig
{
    public RenderFragment Content { get; set; } = _ => { };
    public bool Animation { get; set; } = true;
    public string? AriaDescribedby { get; set; }
    public string? AriaLabelledby { get; set; }
    public bool Backdrop { get; set; } = true;
    public bool CloseOnBackdropClick { get; set; }
    public Func<JsonElement?, Task<bool>>? BeforeDismiss { get; set; }
    public bool Centered { get; set; }
    public bool IsNonBlocking { get; set; }
    public ModalSize Size { get; set; } = ModalSize.size360;
}
