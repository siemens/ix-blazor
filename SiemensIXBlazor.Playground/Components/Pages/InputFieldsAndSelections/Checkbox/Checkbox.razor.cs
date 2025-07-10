// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.InputFieldsAndSelections.Checkbox;

public partial class Checkbox
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
    <div style=""margin-bottom: 1rem"">
      <input type=""checkbox"" id=""checkbox_01"" />
      <label for=""checkbox_01"">Simple checkbox</label>
    </div>
    
    <div>
      <input type=""checkbox"" id=""checkbox_02"" disabled />
      <label for=""checkbox_02"">Disabled checkbox</label>
    </div>";
}
