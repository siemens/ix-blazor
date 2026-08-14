// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

namespace SiemensIXBlazor.Objects;

public class DateInputValidityState
{
    public bool PatternMismatch { get; set; }
    public bool ValueMissing { get; set; }
    public string? InvalidReason { get; set; }
}
