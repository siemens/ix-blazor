// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ButtonsAndActions.Button;

public partial class Button
{
    private int activeTab = 0;

    public string ContentForPrimary { get; private set; } = @"
        <Button>Test Button</Button>
        <Button Disabled>Test Button</Button>";
    public string ContentForPrimaryOutline { get; private set; } = @"
        <Button Outline>Test Button</Button>
        <Button Outline Disabled>Test Button</Button>";
    public string ContentForPrimaryGhost { get; private set; } = @"
        <Button Ghost>Test Button</Button>
        <Button Ghost Disabled>Test Button</Button>";
    public string ContentForSecondary { get; private set; } = @"
        <Button Variant=""Enums.Button.ButtonVariant.secondary"">Test Button</Button>
        <Button Disabled Variant=""Enums.Button.ButtonVariant.secondary"">Test Button</Button>";
    public string ContentForSecondaryOutline { get; private set; } = @"
        <Button Outline Variant=""Enums.Button.ButtonVariant.secondary"">Test Button</Button>
        <Button Outline Disabled Variant=""Enums.Button.ButtonVariant.secondary"">Test Button</Button>";
    public string ContentForSecondaryGhost { get; private set; } = @"
        <Button Ghost Variant=""Enums.Button.ButtonVariant.secondary"">Test Button</Button>
        <Button Ghost Disabled Variant=""Enums.Button.ButtonVariant.secondary"">Test Button</Button>";
    public string ContentForDanger{ get; private set; } = @"
        <Button Variant=""Enums.Button.ButtonVariant.danger"">Test Button</Button>
        <Button Disabled Variant=""Enums.Button.ButtonVariant.danger"">Test Button</Button>";

    public string ContentForDangerOutline { get; private set; } = @"
        <Button Outline Variant=""Enums.Button.ButtonVariant.danger"">Test Button</Button>
        <Button Outline Disabled Variant=""Enums.Button.ButtonVariant.danger"">Test Button</Button>";

    public string ContentForDangerGhost { get; private set; } = @"
        <Button Ghost Variant=""Enums.Button.ButtonVariant.danger"">Test Button</Button>
        <Button Ghost Disabled Variant=""Enums.Button.ButtonVariant.danger"">Test Button</Button>";
    public string ContentForTextAndIconButton { get; private set; } = @"
        <Button Icon=""Star"">Test Button</Button>
        <Button Icon=""Star"" Variant=""Enums.Button.ButtonVariant.secondary"">Test Button</Button>
        <Button Outline Icon=""Star"">Test Button</Button>
        <Button Ghost Icon=""Star"">Test Button</Button>";
        
    public string ContentForButtonGroup { get; private set; } = @"";
    public string ContentForLoadingButton { get; private set; } = @"
        <Button Outline Loading>Test Button</Button>
        <Button Outline Loading Icon=""Star"">Test Button</Button>
        <Button Outline Loading Icon=""Star""></Button>
        <Button Outline Loading Variant = ""Enums.Button.ButtonVariant.secondary"" Icon=""Star"">Test Button</Button>
        <Button Outline Loading Variant = ""Enums.Button.ButtonVariant.secondary"">Test Button</Button>
        <Button Outline Loading Variant = ""Enums.Button.ButtonVariant.secondary""></Button>";
        

}
