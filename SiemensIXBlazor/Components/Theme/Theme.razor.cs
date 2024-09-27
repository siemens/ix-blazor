// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.JSInterop;

namespace SiemensIXBlazor.Components
{
    public partial class Theme
    {
        public async Task SetTheme(string theme)
        {
            await JSRuntime.InvokeVoidAsync("siemensIXInterop.setTheme", theme);
        }

        public async Task ToggleTheme()
        {
            await JSRuntime.InvokeVoidAsync("siemensIXInterop.toggleTheme");
        }

        public async Task ToggleSystemTheme(bool useSystemTheme)
        {
            await JSRuntime.InvokeVoidAsync("siemensIXInterop.toggleSystemTheme", useSystemTheme);
        }
    }
}
