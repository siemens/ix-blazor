// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace SiemensIXBlazor.Playground.Components.Pages.SystemFeedbackAndStatus.MessageBar;

public partial class MessageBar
{
    private int activeTab = 0;
    public string ContentForBasics { get; private set; } = @"
        <SiemensIXBlazor.Components.MessageBar Id=""info-messagebar"" Type=""Enums.MessageBar.MessageBarType.Info"" Dismissible=""false"">
            Message Text
        </SiemensIXBlazor.Components.MessageBar>
        <SiemensIXBlazor.Components.MessageBar Id=""warning-messagebar"" Type=""""Enums.MessageBar.MessageBarType.Warning"" Dismissible=""false"">
            Message Text
        </SiemensIXBlazor.Components.MessageBar>
        <SiemensIXBlazor.Components.MessageBar Id=""danger-messagebar"" Type=""Enums.MessageBar.MessageBarType.Danger"" Dismissible=""false"">
            Message Text
        </SiemensIXBlazor.Components.MessageBar>";


    public string ContentForDismissible { get; private set; } = @"
        <div class=""message-bar"">
            @if (messageBarVisible)
            {
                <SiemensIXBlazor.Components.MessageBar Id=""info-messagebar"" Variant=""Enums.MessageBar.MessageBarType.info"" Dismissible=""false"" ClosedChangeEvent=""HandleCloseAnimationCompleted"">
                    Message text
                </SiemensIXBlazor.Components.MessageBar>
            }
            @if (!messageBarVisible)
            {
                <SiemensIXBlazor.Components.Button Id=""action-button2"" ClickEvent=""@HandleShowMessage"">
                    Show message bar
                </SiemensIXBlazor.Components.Button>
            }
        </div>";

    private bool messageBarVisible = true;


    private void HandleCloseAnimationCompleted()
    {
        messageBarVisible = false;
    }


    private void HandleShowMessage()
    {
        messageBarVisible = true;
    }
}

    
