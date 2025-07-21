// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ContainersAndLayout.Flip;

public partial class Flip
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
    <FlipTile>
        <div slot=""header"">Flip header</div>
    
        <div slot=""footer"">
          <div>Predicted maintenance date</div>
          <div class=""d-flex align-items-center"">
            <ix-icon name=""info"" size=""16""></ix-icon>2021-06-22
          </div>
        </div>
    
        <FlipTileContent> Example 1 </FlipTileContent>
        <FlipTileContent> Example 2 </FlipTileContent>
    </FlipTile>";
}
