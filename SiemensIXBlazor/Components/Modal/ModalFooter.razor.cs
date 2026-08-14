// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.Modal
{
    /// <summary>
    /// Blazor wrapper for ix-modal-footer web component
    /// Provides footer section for modal dialogs, typically containing action buttons
    /// </summary>
    public partial class ModalFooter : IXBaseComponent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

    }
}
