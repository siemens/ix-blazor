// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.JSInterop;
using Newtonsoft.Json;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Components
{
    public partial class Toast
    {
        public async void ShowToast(ToastConfig config)
        {
            await JSRuntime.InvokeVoidAsync("showMessage", JsonConvert.SerializeObject(config));
        }
    }
}
