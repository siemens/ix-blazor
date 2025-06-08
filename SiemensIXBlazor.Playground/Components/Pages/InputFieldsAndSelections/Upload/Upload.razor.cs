// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.InputFieldsAndSelections.Upload;

public partial class Upload
{
    private int activeTab = 0;
    
     public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.Upload Id=""upload-basic"">
        </SiemensIXBlazor.Components.Upload>
    ";
}
