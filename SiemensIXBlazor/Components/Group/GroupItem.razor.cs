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
    public partial class GroupItem
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? AriaLabelIcon { get; set; }  
        [Parameter]
        public bool Focusable { get; set; } = true;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public int? Index { get; set; }
        [Parameter]
        public string? SecondaryText { get; set; }
        [Parameter]
        public bool? Selected { get; set; }
        [Parameter]
        public bool SuppressSelection { get; set; } = false;
        [Parameter]
        public string? Text { get; set; }
        [Parameter]
        public EventCallback<string> SelectedChangeEvent { get; set; }

        public async void ItemClicked()
        {
           await SelectedChangeEvent.InvokeAsync(Id);
        }

    }
}
