// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.DataDisplay.KeyValueList;

public partial class KeyValueList
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.KeyValueList Id=""basic-key-value-list"">
            <SiemensIXBlazor.Components.KeyValue id=""basic-key-value-1"" Label=""Label"" LabelPosition=""left"" Value=""Value""></SiemensIXBlazor.Components.KeyValue>
            <SiemensIXBlazor.Components.KeyValue id=""basic-key-value-2"" Label=""Label"" LabelPosition=""left"" Value=""Value""></SiemensIXBlazor.Components.KeyValue>
            <SiemensIXBlazor.Components.KeyValue id=""basic-key-value-3"" Label=""Label"" LabelPosition=""left"" Value=""Value""></SiemensIXBlazor.Components.KeyValue>
        </SiemensIXBlazor.Components.KeyValueList>";

    public string ContentForCustomValue { get; private set; } = @"
        <SiemensIXBlazor.Components.KeyValueList Id=""custom-key-value-list"">
            <SiemensIXBlazor.Components.KeyValue Id=""custom-key-value-1"" Label=""Label"" LabelPosition=""left"">
                <input class=""ix-form-control""
                       placeholder=""Enter text here""
                       type=""text""
                       slot=""custom-value"" />
            </SiemensIXBlazor.Components.KeyValue>
            <SiemensIXBlazor.Components.KeyValue Id=""custom-key-value-2"" Label=""Label"" LabelPosition=""left"">
                <input class=""ix-form-control""
                       placeholder=""Enter text here""
                       type=""text""
                       slot=""custom-value"" />
            </SiemensIXBlazor.Components.KeyValue>
            <SiemensIXBlazor.Components.KeyValue Id=""custom-key-value-3"" Label=""Label"" LabelPosition=""left"">
                <input class=""ix-form-control""
                       placeholder=""Enter text here""
                       type=""text""
                       slot=""custom-value"" />
            </SiemensIXBlazor.Components.KeyValue>
        </SiemensIXBlazor.Components.KeyValueList>";

    public string ContentForWithIcon { get; private set; } = @"
        <SiemensIXBlazor.Components.KeyValueList Id=""with-icon-key-value-list"">
            <SiemensIXBlazor.Components.KeyValue Id=""with-icon-key-value-1"" Icon=""location"" Label=""Label"" LabelPosition=""left"" Value=""Value"">
            </SiemensIXBlazor.Components.KeyValue>
            <SiemensIXBlazor.Components.KeyValue Id=""with-icon-key-value-2"" Icon=""location"" Label=""Label"" LabelPosition=""left"" Value=""Value"">
            </SiemensIXBlazor.Components.KeyValue>
            <SiemensIXBlazor.Components.KeyValue Id=""with-icon-key-value-3"" Icon=""location"" Label=""Label"" LabelPosition=""left"" Value=""Value"">
            </SiemensIXBlazor.Components.KeyValue>
        </SiemensIXBlazor.Components.KeyValueList>";

    public string ContentForStriped { get; private set; } = @"
        <SiemensIXBlazor.Components.KeyValueList Id=""striped-key-value-list"" Striped=""true"">
            <SiemensIXBlazor.Components.KeyValue Id=""striped-key-value-1"" Label=""Label"" LabelPosition=""left"" Value=""Value"">
            </SiemensIXBlazor.Components.KeyValue>
            <SiemensIXBlazor.Components.KeyValue Id=""striped-key-value-2"" Label=""Label"" LabelPosition=""left"" Value=""Value"">
            </SiemensIXBlazor.Components.KeyValue>
            <SiemensIXBlazor.Components.KeyValue Id=""striped-key-value-3"" Label=""Label"" LabelPosition=""left"" Value=""Value"">
            </SiemensIXBlazor.Components.KeyValue>
        </SiemensIXBlazor.Components.KeyValueList>";
}
