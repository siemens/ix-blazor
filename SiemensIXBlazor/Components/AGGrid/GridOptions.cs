// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Newtonsoft.Json;

namespace SiemensIXBlazor.Components.AGGrid
{
    public class GridOptions
    {
        [JsonProperty("columnDefs")]
        public List<ColumnDefs>? ColumnDefs { get; set; }
        [JsonProperty("rowData")]
        public List<Dictionary<string, dynamic>>? RowData { get; set; }
        [JsonProperty("rowSelection")]
        public string? RowSelection { get; set; }
        [JsonProperty("suppressCellFocus")]
        public bool? SuppressCellFocus { get; set; }
        [JsonProperty("checkboxSelection")]
        public bool? CheckboxSelection { get; set; }
        [JsonProperty("overlayLoadingTemplate")]
        public string? OverlayLoadingTemplate { get; set; }
        [JsonProperty("overlayNoRowsTemplate")]
        public string? OverlayNoRowsTemplate { get; set; }
    }
}
