// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.CardAccordion;

namespace SiemensIXBlazor.Components
{
    public partial class CardAccordion
    {
        [Parameter]
        public string? AriaLabelExpandButton { get; set; }

        [Parameter]
        public bool Collapse { get; set; } = false;

        [Parameter]
        public CardAccordionVariant Variant { get; set; } = CardAccordionVariant.outline;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
