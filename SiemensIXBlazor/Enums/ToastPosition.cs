// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Runtime.Serialization;

namespace SiemensIXBlazor.Enums
{
    public enum ToastPosition
    {
        [EnumMember(Value = "bottom-right")]
        BottomRight,

        [EnumMember(Value = "top-right")]
        TopRight
    }
}
