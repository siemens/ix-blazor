// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ButtonsAndActions.ToggleButton;

public partial class ToggleButton
{
    private int activeTab = 0;
    public string ContentForPrimary { get; private set; } = @"
        <ToggleButton Variant=""Enums.Button.ButtonVariant.primary"">Normal</ToggleButton>
        <ToggleButton Pressed=""true"" Variant=""Enums.Button.ButtonVariant.primary"">Pressed</ToggleButton>
        <ToggleButton Disabled Variant=""Enums.Button.ButtonVariant.primary"">Pressed</ToggleButton>
        <ToggleButton Disabled Loading Variant=""Enums.Button.ButtonVariant.primary"">Pressed</ToggleButton>";
    public string ContentForPrimaryOutline { get; private set; } = @"
        <ToggleButton Outline Variant=""Enums.Button.ButtonVariant.primary"">Normal</ToggleButton>
        <ToggleButton Outline Pressed=""true"" Variant=""Enums.Button.ButtonVariant.primary"">Pressed</ToggleButton>
        <ToggleButton Outline Disabled Variant=""Enums.Button.ButtonVariant.primary"">Pressed</ToggleButton>
        <ToggleButton Outline Disabled Loading Variant=""Enums.Button.ButtonVariant.primary"">Pressed</ToggleButton>";
    public string ContentForPrimaryGhost { get; private set; } = @"
        <ToggleButton Ghost Variant=""Enums.Button.ButtonVariant.primary"">Normal</ToggleButton>
        <ToggleButton Ghost Pressed=""true"" Variant=""Enums.Button.ButtonVariant.primary"">Pressed</ToggleButton>
        <ToggleButton Ghost Disabled Variant=""Enums.Button.ButtonVariant.primary"">Pressed</ToggleButton>
        <ToggleButton Ghost Disabled Loading Variant=""Enums.Button.ButtonVariant.primary"">Pressed</ToggleButton>";

    public string ContentForSecondary { get; private set; } = @"
        <ToggleButton Variant=""Enums.Button.ButtonVariant.secondary"">Normal</ToggleButton>
        <ToggleButton Pressed=""true"" Variant=""Enums.Button.ButtonVariant.secondary"">Pressed</ToggleButton>
        <ToggleButton Disabled Variant=""Enums.Button.ButtonVariant.secondary"">Pressed</ToggleButton>
        <ToggleButton Disabled Loading Variant=""Enums.Button.ButtonVariant.secondary"">Pressed</ToggleButton>";
    public string ContentForSecondaryOutline { get; private set; } = @"
        <ToggleButton Outline Variant=""Enums.Button.ButtonVariant.secondary"">Normal</ToggleButton>
        <ToggleButton Outline Pressed=""true"" Variant=""Enums.Button.ButtonVariant.secondary"">Pressed</ToggleButton>
        <ToggleButton Outline Disabled Variant=""Enums.Button.ButtonVariant.secondary"">Pressed</ToggleButton>
        <ToggleButton Outline Disabled Loading Variant=""Enums.Button.ButtonVariant.secondary"">Pressed</ToggleButton>";
    public string ContentForSecondaryGhost { get; private set; } = @"
        <ToggleButton Ghost Variant=""Enums.Button.ButtonVariant.secondary"">Normal</ToggleButton>
        <ToggleButton Ghost Pressed=""true"" Variant=""Enums.Button.ButtonVariant.secondary"">Pressed</ToggleButton>
        <ToggleButton Ghost Disabled Variant=""Enums.Button.ButtonVariant.secondary"">Pressed</ToggleButton>
        <ToggleButton Ghost Disabled Loading Variant=""Enums.Button.ButtonVariant.secondary"">Pressed</ToggleButton>";
    public string ContentForIconToggleButtonSecondaryOutline { get; private set; } = @"
        <IconToggleButton Outline=""true"" Icon=""checkboxes""></IconToggleButton>
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Pressed=""true""></IconToggleButton>
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Disabled=""true""></IconToggleButton>
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Disabled=""true"" Loading=""true""></IconToggleButton>
        
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Size=""16""></IconToggleButton>
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Pressed=""true"" Size=""16""></IconToggleButton>
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Disabled=""true"" Size=""16""></IconToggleButton>
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Disabled=""true"" Loading=""true"" Size=""16""></IconToggleButton>

        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Size=""12""></IconToggleButton>
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Pressed=""true"" Size=""12""></IconToggleButton>
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Disabled=""true"" Size=""12""></IconToggleButton>
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Disabled=""true"" Loading=""true"" Size=""12""></IconToggleButton>";

    public string ContentForIconToggleButtonSecondaryGhost { get; private set; } = @"
        <IconToggleButton Ghost=""true"" Icon=""checkboxes""></IconToggleButton>
        <IconToggleButton Ghost=""true"" Icon=""checkboxes"" Pressed=""true""></IconToggleButton>
        <IconToggleButton Ghost=""true"" Icon=""checkboxes"" Disabled=""true""></IconToggleButton>
        <IconToggleButton Ghost=""true"" Icon=""checkboxes"" Disabled=""true"" Loading=""true""></IconToggleButton>";
    public string ContentForIconToggleButtonSecondary { get; private set; } = @"
        <IconToggleButton Icon=""checkboxes""></IconToggleButton>
        <IconToggleButton Icon=""checkboxes"" Pressed=""true""></IconToggleButton>
        <IconToggleButton Icon=""checkboxes"" Disabled=""true""></IconToggleButton>
        <IconToggleButton Icon=""checkboxes"" Disabled=""true"" Loading=""true""></IconToggleButton>";

    public string ContentForIconToggleButtonPrimaryOutline { get; private set; } = @"
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Variant=""Enums.Button.ButtonVariant.primary""></IconToggleButton>
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Pressed=""true"" Variant=""Enums.Button.ButtonVariant.primary""></IconToggleButton>
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Disabled=""true"" Variant=""Enums.Button.ButtonVariant.primary""></IconToggleButton>
        <IconToggleButton Outline=""true"" Icon=""checkboxes"" Disabled=""true"" Loading=""true"" Variant=""Enums.Button.ButtonVariant.primary""></IconToggleButton>";
    public string ContentForIconToggleButtonPrimaryGhost { get; private set; } = @"
        <IconToggleButton Ghost=""true"" Icon=""checkboxes"" Variant=""Enums.Button.ButtonVariant.primary""></IconToggleButton>
        <IconToggleButton Ghost=""true"" Icon=""checkboxes"" Pressed=""true"" Variant=""Enums.Button.ButtonVariant.primary""></IconToggleButton>
        <IconToggleButton Ghost=""true"" Icon=""checkboxes"" Disabled=""true"" Variant=""Enums.Button.ButtonVariant.primary""></IconToggleButton>
        <IconToggleButton Ghost=""true"" Icon=""checkboxes"" Disabled=""true"" Loading=""true"" Variant=""Enums.Button.ButtonVariant.primary""></IconToggleButton>";

    public string ContentFor { get; private set; } = @"
        < ToggleButton>Normal</ToggleButton>
        <ToggleButton Id=""toggle-btn-1"" Pressed=""true"">Pressed</ToggleButton>
        
        <IconToggleButton Outline=""true"" Icon=""checkboxes""></IconToggleButton>
        <IconToggleButton
            Outline=""true""
            Icon=""checkboxes""
            Pressed=""true""
        ></IconToggleButton>";
}
