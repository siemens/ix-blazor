// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

namespace SiemensIXBlazor.Objects
{
    public sealed class ValidityState
    {
        public bool BadInput { get; set; }
        public bool CustomError { get; set; }
        public bool PatternMismatch { get; set; }
        public bool RangeOverflow { get; set; }
        public bool RangeUnderflow { get; set; }
        public bool StepMismatch { get; set; }
        public bool TooLong { get; set; }
        public bool TooShort { get; set; }
        public bool TypeMismatch { get; set; }
        public bool Valid { get; set; }
        public bool ValueMissing { get; set; }
    }
}
