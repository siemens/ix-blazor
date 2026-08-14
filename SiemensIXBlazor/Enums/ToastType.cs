// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Runtime.Serialization;

namespace SiemensIXBlazor.Enums
{
    public enum ToastType
    {
        [EnumMember(Value = "info")]
        Info,

        [EnumMember(Value = "success")]
        Success,

        [EnumMember(Value = "warning")]
        Warning,

        [EnumMember(Value = "error")]
        Error
    }
}
