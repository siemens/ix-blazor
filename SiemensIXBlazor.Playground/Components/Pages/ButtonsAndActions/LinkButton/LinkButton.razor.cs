// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ButtonsAndActions.LinkButton;

public partial class LinkButton
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <LinkButton Url=""https://ix.siemens.io/"">Link text</LinkButton>";
    public string ContentForDisabled { get; private set; } = @"
        <LinkButton Url=""https://ix.siemens.io/"" Disabled>Link text</LinkButton>";
}
