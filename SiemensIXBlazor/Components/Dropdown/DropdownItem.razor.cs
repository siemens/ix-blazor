// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
    public partial class DropdownItem
    {
        [Parameter]
        public string Label { get; set; } = string.Empty;
        [Parameter]
        public string Value { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<string> OnClickEvent { get; set; }

        private async void Clicked(string label)
        {
            await OnClickEvent.InvokeAsync(label);
        }
    }
}
