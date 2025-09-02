// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ApplicationFrame.Avatar;

public partial class Avatar
{
    private int activeTab = 0;
    public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.Avatar.Avatar Id=""basic-avatar""></SiemensIXBlazor.Components.Avatar.Avatar>     ";

    public string ContentForInitials { get; private set; } = @"
        <SiemensIXBlazor.Components.Avatar.Avatar Id=""initials-avatar"" Initials=""JD""></SiemensIXBlazor.Components.Avatar.Avatar>     ";

    public string ContentForImage { get; private set; } = @"
        <SiemensIXBlazor.Components.Avatar.Avatar Id=""image-avatar"" Image=""https://www.gravatar.com/avatar/00000000000000000000000000000000""></SiemensIXBlazor.Components.Avatar.Avatar>     ";
}
