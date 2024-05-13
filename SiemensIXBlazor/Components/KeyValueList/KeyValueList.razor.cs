// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
	public partial class KeyValueList
	{
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// Optional striped key value list style. Default value is null.
        /// </summary>
        [Parameter]
        public bool? Striped { get; set; }
    }
}

