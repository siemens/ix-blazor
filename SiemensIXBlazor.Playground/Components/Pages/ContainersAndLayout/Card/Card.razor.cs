// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ContainersAndLayout.Card;

public partial class Card
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
    <Card Variant=""CardVariant.info"">
        <ix-icon name=""capacity""></ix-icon>
        <ix-typography bold>Number of components</ix-typography>
        <ix-typography>
            Item 1<br />
            Item 2<br />
            Item 3
        </ix-typography>
        <ix-typography format=""h1"">3</ix-typography>
    </Card>";
}
