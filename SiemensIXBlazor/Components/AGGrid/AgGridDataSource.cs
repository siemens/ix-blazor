// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Components.AGGrid;

public interface IAgGridInfiniteDataSource<TData>
{
    ValueTask<AgGridDataBlock<TData>> GetRowsAsync(
        AgGridGetRowsRequest request,
        CancellationToken cancellationToken);
}

public sealed record AgGridGetRowsRequest(
    [property: JsonPropertyName("requestId")] string RequestId,
    [property: JsonPropertyName("startRow")] int StartRow,
    [property: JsonPropertyName("endRow")] int EndRow,
    [property: JsonPropertyName("sortModel")] JsonElement SortModel,
    [property: JsonPropertyName("filterModel")] JsonElement FilterModel);

public sealed record AgGridDataBlock<TData>(
    [property: JsonPropertyName("rows")] IReadOnlyList<TData> Rows,
    [property: JsonPropertyName("rowCount")] int? RowCount = null);

public sealed record AgGridTransaction<TData>(
    IReadOnlyList<TData>? Add = null,
    IReadOnlyList<TData>? Update = null,
    IReadOnlyList<TData>? Remove = null,
    int? AddIndex = null);

public sealed record AgGridTransactionResult<TData>(
    IReadOnlyList<TData> Add,
    IReadOnlyList<TData> Update,
    IReadOnlyList<TData> Remove);
