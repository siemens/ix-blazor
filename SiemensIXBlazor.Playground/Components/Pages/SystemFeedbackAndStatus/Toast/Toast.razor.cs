// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Playground.Components.Pages.SystemFeedbackAndStatus.Toast;

public partial class Toast
{
    private int activeTab = 0;

    private string message;

    private SiemensIXBlazor.Components.Toast toast;

    public string CustomToastMessage()
    {
        return "This message is from template";
    }

    public async Task HandleShowToast()
    {
        ToastConfig config = new ToastConfig()
        {
            Message="My toast message!",
            Type = "info"
        };
        await toast.ShowToast(config);
    }
    public async Task HandleShowToastCustomMessage()
    {
        ToastConfig config = new ToastConfig()
        {
            Title = "Toast Headline",
            Message = CustomToastMessage(),
            Type = "success"
        };
        await toast.ShowToast(config);
    }
    public async Task HandleShowToastActionButton()
    {
        ToastConfig config = new ToastConfig()
        {
            Title = "Toast Headline",
            Message = CustomToastMessage(),
            Type = "info"
        };
        await toast.ShowToast(config);
    }
    public async Task HandleShowToastPosition()
    {
        ToastConfig config = new ToastConfig()
        {
            Message = "My toast message!",
            Type = "info"
        };
        await toast.ShowToast(config);
    }
}
