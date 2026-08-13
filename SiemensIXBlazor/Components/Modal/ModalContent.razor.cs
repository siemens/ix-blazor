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
    /// Blazor wrapper for ix-modal-content web component
    /// Provides content section for modal dialogs
    /// </summary>
    public partial class ModalContent : IXBaseComponent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

    }
}
