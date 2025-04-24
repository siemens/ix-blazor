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
        public async Task ShowToast(ToastConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config), "Toast configuration cannot be null");
            }

            try
            {
                await JSRuntime.InvokeAsync<object>("siemensIXInterop.showMessage", JsonConvert.SerializeObject(config));
            }
            catch (JSException jsException)
            {
                throw;
            }
        }
    }
}
