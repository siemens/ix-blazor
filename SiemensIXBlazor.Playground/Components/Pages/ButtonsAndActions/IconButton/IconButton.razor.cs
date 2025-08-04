// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ButtonsAndActions.IconButton;

public partial class IconButton
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <IconButton Icon=""info""></IconButton>
        <IconButton Icon=""info"" Variant=""Enums.Button.ButtonVariant.secondary""></IconButton>
        <IconButton Icon=""info"" Variant=""Enums.Button.ButtonVariant.danger""></IconButton>
        <IconButton Icon=""info"" Outline></IconButton>
        <IconButton Icon=""info"" Ghost></IconButton>
        
        <IconButton Icon=""info"" Oval=true></IconButton>
        <IconButton Icon=""info"" Variant=""Enums.Button.ButtonVariant.secondary"" Oval=true></IconButton>
        <IconButton Icon=""info"" Variant=""Enums.Button.ButtonVariant.danger"" Oval=true></IconButton>
        <IconButton Icon=""info"" Outline Oval=true></IconButton>
        <IconButton Icon=""info"" Ghost Oval=true></IconButton>

        <IconButton Icon=""info"" Size=""Enums.Button.IconButtonSize._12""></IconButton>
        <IconButton Icon=""info"" Size=""Enums.Button.IconButtonSize._16""></IconButton>
        <IconButton Icon=""info"" Size=""Enums.Button.IconButtonSize._24""></IconButton>";
}
