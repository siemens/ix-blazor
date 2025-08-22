// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.DataDisplay.KeyValue;

public partial class KeyValue
{
    private int activeTab = 0;
    public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.KeyValue Id=""basic-key-value"" Label=""Label"" Value=""Value""></SiemensIXBlazor.Components.KeyValue>";

    public string ContentForCustomValue { get; private set; } = @"
        <SiemensIXBlazor.Components.KeyValue Id=""custom-key-value"" Label=""Label"">
            <input class=""ix-form-control""
                   placeholder=""Enter text here""
                   type=""text""
                   slot=""custom-value"" />
        </SiemensIXBlazor.Components.KeyValue>";

    public string ContentForWithIcon { get; private set; } = @"
        <SiemensIXBlazor.Components.KeyValue Icon=""location"" Label=""Label"" Value=""Value"">
        </SiemensIXBlazor.Components.KeyValue>";

    public string ContentForLabelOnLeft { get; private set; } = @"
        <SiemensIXBlazor.Components.KeyValue Id=""left-key-value"" Label=""Label"" LabelPosition=""left"" Value=""Value"">
        </SiemensIXBlazor.Components.KeyValue>";
}
