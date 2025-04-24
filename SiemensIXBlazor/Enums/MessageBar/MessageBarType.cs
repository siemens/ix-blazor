// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

namespace SiemensIXBlazor.Enums.MessageBar
{
    public enum MessageBarType
    {
        Alarm,
        [Obsolete("Type `danger` will be removed in future versions. Use `alarm` instead.")]
        Danger,
        Critical,
        Warning,
        Success,
        Info,
        Neutral,
        Primary
    }
}
