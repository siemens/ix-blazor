// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums;

namespace SiemensIXBlazor.Components
{
    public partial class Card
    {
        [Parameter]
        public bool? Selected { get; set; }
        [Parameter]
        public CardVariant Variant { get; set; } = CardVariant.outline;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
