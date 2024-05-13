// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Spinner;

namespace SiemensIXBlazor.Components
{
    public partial class Spinner
    {
        [Parameter]
        public string Size { get; set; } = "medium";
        [Parameter]
        public SpinnerVariant Variant { get; set; } = SpinnerVariant.secondary;
    }
}
