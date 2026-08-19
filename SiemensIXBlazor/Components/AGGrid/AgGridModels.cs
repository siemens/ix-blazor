// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Components.AGGrid;

public sealed record AgGridRowNode<TData>(
    [property: JsonPropertyName("id")] string? Id,
    [property: JsonPropertyName("rowIndex")] int? RowIndex,
    [property: JsonPropertyName("rowPinned")] string? RowPinned,
    [property: JsonPropertyName("data")] TData? Data,
    [property: JsonPropertyName("selected")] bool? Selected,
    [property: JsonPropertyName("expanded")] bool? Expanded);

public sealed record AgGridCellPosition(
    [property: JsonPropertyName("rowIndex")] int RowIndex,
    [property: JsonPropertyName("columnId")] string ColumnId,
    [property: JsonPropertyName("rowPinned")] string? RowPinned = null);

public sealed record AgGridVerticalPixelRange(
    [property: JsonPropertyName("top")] int Top,
    [property: JsonPropertyName("bottom")] int Bottom);

public sealed record AgGridHorizontalPixelRange(
    [property: JsonPropertyName("left")] int Left,
    [property: JsonPropertyName("right")] int Right);

public sealed record AgGridColumnState(
    [property: JsonPropertyName("colId")] string ColId,
    [property: JsonPropertyName("hide")] bool? Hide = null,
    [property: JsonPropertyName("width")] int? Width = null,
    [property: JsonPropertyName("flex")] int? Flex = null,
    [property: JsonPropertyName("sort")] string? Sort = null,
    [property: JsonPropertyName("sortType")] string? SortType = null,
    [property: JsonPropertyName("sortIndex")] int? SortIndex = null,
    [property: JsonPropertyName("aggFunc")] object? AggFunc = null,
    [property: JsonPropertyName("pivot")] bool? Pivot = null,
    [property: JsonPropertyName("pivotIndex")] int? PivotIndex = null,
    [property: JsonPropertyName("pinned")] object? Pinned = null,
    [property: JsonPropertyName("rowGroup")] bool? RowGroup = null,
    [property: JsonPropertyName("rowGroupIndex")] int? RowGroupIndex = null);

public sealed record AgGridColumnStateDefaults(
    [property: JsonPropertyName("hide")] bool? Hide = null,
    [property: JsonPropertyName("width")] int? Width = null,
    [property: JsonPropertyName("flex")] int? Flex = null,
    [property: JsonPropertyName("sort")] string? Sort = null,
    [property: JsonPropertyName("sortType")] string? SortType = null,
    [property: JsonPropertyName("sortIndex")] int? SortIndex = null,
    [property: JsonPropertyName("aggFunc")] object? AggFunc = null,
    [property: JsonPropertyName("pivot")] bool? Pivot = null,
    [property: JsonPropertyName("pivotIndex")] int? PivotIndex = null,
    [property: JsonPropertyName("pinned")] object? Pinned = null,
    [property: JsonPropertyName("rowGroup")] bool? RowGroup = null,
    [property: JsonPropertyName("rowGroupIndex")] int? RowGroupIndex = null);

public sealed record AgGridApplyColumnStateParameters(
    [property: JsonPropertyName("state")] IReadOnlyList<AgGridColumnState>? State = null,
    [property: JsonPropertyName("applyOrder")] bool? ApplyOrder = null,
    [property: JsonPropertyName("defaultState")] AgGridColumnStateDefaults? DefaultState = null);

public sealed record AgGridStartEditingCellParameters(
    [property: JsonPropertyName("rowIndex")] int RowIndex,
    [property: JsonPropertyName("colKey")] string ColumnKey,
    [property: JsonPropertyName("rowPinned")] string? RowPinned = null,
    [property: JsonPropertyName("key")] string? Key = null);

public sealed record AgGridColumnWidthLimit(
    [property: JsonPropertyName("colId")] string ColId,
    [property: JsonPropertyName("minWidth")] int? MinWidth = null,
    [property: JsonPropertyName("maxWidth")] int? MaxWidth = null);

public sealed record AgGridAutoSizeStrategy(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("defaultMinWidth")] int? DefaultMinWidth = null,
    [property: JsonPropertyName("defaultMaxWidth")] int? DefaultMaxWidth = null,
    [property: JsonPropertyName("width")] int? Width = null,
    [property: JsonPropertyName("skipHeader")] bool? SkipHeader = null,
    [property: JsonPropertyName("colIds")] IReadOnlyList<string>? ColumnIds = null,
    [property: JsonPropertyName("columnLimits")] IReadOnlyList<AgGridColumnWidthLimit>? ColumnLimits = null,
    [property: JsonPropertyName("scaleUpToFitGridWidth")] bool? ScaleUpToFitGridWidth = null);
