// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;

namespace SiemensIXBlazor.Components.AGGrid;

public readonly record struct AgGridEventName(string Value)
{
    public static implicit operator AgGridEventName(string value) => new(value);
    public override string ToString() => Value;
}

public static class AgGridEventNames
{
    public static readonly AgGridEventName CellClicked = new("cellClicked");
    public static readonly AgGridEventName CellDoubleClicked = new("cellDoubleClicked");
    public static readonly AgGridEventName CellValueChanged = new("cellValueChanged");
    public static readonly AgGridEventName RowClicked = new("rowClicked");
    public static readonly AgGridEventName RowDoubleClicked = new("rowDoubleClicked");
    public static readonly AgGridEventName RowSelected = new("rowSelected");
    public static readonly AgGridEventName SelectionChanged = new("selectionChanged");
    public static readonly AgGridEventName FilterChanged = new("filterChanged");
    public static readonly AgGridEventName SortChanged = new("sortChanged");
    public static readonly AgGridEventName PaginationChanged = new("paginationChanged");
    public static readonly AgGridEventName FirstDataRendered = new("firstDataRendered");
    public static readonly AgGridEventName RowDataUpdated = new("rowDataUpdated");
    public static readonly AgGridEventName GridPreDestroyed = new("gridPreDestroyed");
    public static readonly AgGridEventName ColumnEverythingChanged = new("columnEverythingChanged");
    public static readonly AgGridEventName ColumnMoved = new("columnMoved");
    public static readonly AgGridEventName ColumnVisible = new("columnVisible");
    public static readonly AgGridEventName ColumnPinned = new("columnPinned");
    public static readonly AgGridEventName ColumnResized = new("columnResized");
    public static readonly AgGridEventName DisplayedColumnsChanged = new("displayedColumnsChanged");
    public static readonly AgGridEventName VirtualColumnsChanged = new("virtualColumnsChanged");
    public static readonly AgGridEventName AsyncTransactionsFlushed = new("asyncTransactionsFlushed");
    public static readonly AgGridEventName ModelUpdated = new("modelUpdated");
    public static readonly AgGridEventName CellContextMenu = new("cellContextMenu");
    public static readonly AgGridEventName CellFocused = new("cellFocused");
    public static readonly AgGridEventName RowValueChanged = new("rowValueChanged");
    public static readonly AgGridEventName CellEditingStarted = new("cellEditingStarted");
    public static readonly AgGridEventName CellEditingStopped = new("cellEditingStopped");
    public static readonly AgGridEventName GridSizeChanged = new("gridSizeChanged");
    public static readonly AgGridEventName ViewportChanged = new("viewportChanged");
    public static readonly AgGridEventName BodyScroll = new("bodyScroll");
    public static readonly AgGridEventName BodyScrollEnd = new("bodyScrollEnd");
    public static readonly AgGridEventName StateUpdated = new("stateUpdated");
    public static readonly AgGridEventName PasteStart = new("pasteStart");
    public static readonly AgGridEventName PasteEnd = new("pasteEnd");
    public static readonly AgGridEventName NewColumnsLoaded = new("newColumnsLoaded");
    public static readonly AgGridEventName GridColumnsChanged = new("gridColumnsChanged");
    public static readonly AgGridEventName ColumnGroupOpened = new("columnGroupOpened");
    public static readonly AgGridEventName ColumnHeaderMouseOver = new("columnHeaderMouseOver");
    public static readonly AgGridEventName ColumnHeaderMouseLeave = new("columnHeaderMouseLeave");
    public static readonly AgGridEventName ColumnHeaderClicked = new("columnHeaderClicked");
    public static readonly AgGridEventName ColumnHeaderContextMenu = new("columnHeaderContextMenu");
    public static readonly AgGridEventName CellMouseDown = new("cellMouseDown");
    public static readonly AgGridEventName HeaderFocused = new("headerFocused");
    public static readonly AgGridEventName CellKeyDown = new("cellKeyDown");
    public static readonly AgGridEventName CellMouseOver = new("cellMouseOver");
    public static readonly AgGridEventName CellMouseOut = new("cellMouseOut");
    public static readonly AgGridEventName FilterModified = new("filterModified");
    public static readonly AgGridEventName FilterUiChanged = new("filterUiChanged");
    public static readonly AgGridEventName FilterOpened = new("filterOpened");
    public static readonly AgGridEventName FloatingFilterUiChanged = new("floatingFilterUiChanged");
    public static readonly AgGridEventName TooltipShow = new("tooltipShow");
    public static readonly AgGridEventName TooltipHide = new("tooltipHide");
    public static readonly AgGridEventName VirtualRowRemoved = new("virtualRowRemoved");
    public static readonly AgGridEventName DragStarted = new("dragStarted");
    public static readonly AgGridEventName DragStopped = new("dragStopped");
    public static readonly AgGridEventName DragCancelled = new("dragCancelled");
    public static readonly AgGridEventName RowEditingStarted = new("rowEditingStarted");
    public static readonly AgGridEventName RowEditingStopped = new("rowEditingStopped");
    public static readonly AgGridEventName ColumnMenuVisibleChanged = new("columnMenuVisibleChanged");
    public static readonly AgGridEventName ContextMenuVisibleChanged = new("contextMenuVisibleChanged");
    public static readonly AgGridEventName RowDragEnter = new("rowDragEnter");
    public static readonly AgGridEventName RowDragMove = new("rowDragMove");
    public static readonly AgGridEventName RowDragLeave = new("rowDragLeave");
    public static readonly AgGridEventName RowDragEnd = new("rowDragEnd");
    public static readonly AgGridEventName RowDragCancel = new("rowDragCancel");
}

public sealed record AgGridEvent<TData>(
    AgGridEventName Name,
    long Sequence,
    int? RowIndex,
    string? ColumnId,
    TData? Data,
    JsonElement? Value,
    JsonElement Payload);

public sealed record AgGridReadyEvent<TData>(AgGridApi<TData> Api);

public sealed record AgGridInitializationError(string InstanceId, Exception Exception);
