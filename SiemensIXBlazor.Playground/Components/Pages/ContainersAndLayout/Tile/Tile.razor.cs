// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ContainersAndLayout.Tile;

public partial class Tile
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
    <Tile Size=""TileSize.Medium"" Class=""mr-1"">
        <div slot=""header"">Tile header</div>
        <div class=""text-l"">92.8 °C</div>
    </Tile>";
}
