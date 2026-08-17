// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.JSInterop;
using System.Text.Json;

namespace SiemensIXBlazor.Components.AGGrid;

public sealed class AgGridApi<TData>
{
    private readonly IJSObjectReference _controller;
    private bool _disposed;

    internal AgGridApi(IJSObjectReference controller)
    {
        _controller = controller;
    }

    public ValueTask<TData[]?> GetSelectedRowsAsync() =>
        InvokeControllerAsync<TData[]?>("getSelectedRows");

    public ValueTask<IReadOnlyList<AgGridRowNode<TData>>?> GetSelectedNodesAsync() =>
        InvokeControllerAsync<IReadOnlyList<AgGridRowNode<TData>>?>("getSelectedNodes");

    public ValueTask<int> GetDisplayedRowCountAsync() =>
        InvokeControllerAsync<int>("getDisplayedRowCount");

    public ValueTask<AgGridRowNode<TData>?> GetDisplayedRowAtIndexAsync(int index) =>
        InvokeControllerAsync<AgGridRowNode<TData>?>("getDisplayedRowAtIndex", index);

    public ValueTask<AgGridRowNode<TData>?> GetRowNodeAsync(string id) =>
        InvokeControllerAsync<AgGridRowNode<TData>?>("getRowNode", id);

    public ValueTask<IReadOnlyList<AgGridRowNode<TData>>?> GetRenderedNodesAsync() =>
        InvokeControllerAsync<IReadOnlyList<AgGridRowNode<TData>>?>("getRenderedNodes");

    public ValueTask<int> GetFirstDisplayedRowIndexAsync() =>
        InvokeAsync<int>("getFirstDisplayedRowIndex");

    public ValueTask<int> GetLastDisplayedRowIndexAsync() =>
        InvokeAsync<int>("getLastDisplayedRowIndex");

    public ValueTask<string?> GetGridIdAsync() => InvokeAsync<string?>("getGridId");
    public ValueTask<TResult?> GetGridOptionAsync<TResult>(string key) =>
        InvokeAsync<TResult?>("getGridOption", key);
    public ValueTask<AgGridVerticalPixelRange?> GetVerticalPixelRangeAsync() =>
        InvokeAsync<AgGridVerticalPixelRange?>("getVerticalPixelRange");
    public ValueTask<AgGridHorizontalPixelRange?> GetHorizontalPixelRangeAsync() =>
        InvokeAsync<AgGridHorizontalPixelRange?>("getHorizontalPixelRange");
    public ValueTask<int> GetPinnedTopRowCountAsync() => InvokeAsync<int>("getPinnedTopRowCount");
    public ValueTask<int> GetPinnedBottomRowCountAsync() => InvokeAsync<int>("getPinnedBottomRowCount");
    public ValueTask<JsonElement> GetColumnDefinitionsAsync() => InvokeAsync<JsonElement>("getColumnDefs");
    public ValueTask<JsonElement> GetEditingCellsAsync() => InvokeAsync<JsonElement>("getEditingCells");
    public ValueTask<JsonElement> GetSizesForCurrentThemeAsync() =>
        InvokeAsync<JsonElement>("getSizesForCurrentTheme");

    public ValueTask<AgGridCellPosition?> GetFocusedCellAsync() =>
        InvokeControllerAsync<AgGridCellPosition?>("getFocusedCell");

    public ValueTask<AgGridTransactionResult<TData>?> RemoveSelectedRowsAsync() =>
        InvokeControllerAsync<AgGridTransactionResult<TData>?>("removeSelectedRows");

    public ValueTask SetRowDataAsync(IReadOnlyList<TData>? rows) =>
        InvokeControllerVoidAsync("setRowData", rows ?? []);

    public ValueTask SetColumnDefinitionsAsync(IReadOnlyList<IAgGridColumnDefinition> columns) =>
        InvokeControllerVoidAsync(
            "setColumnDefinitions",
            columns.Select(column => column.ToDictionary()).ToArray());

    public ValueTask<AgGridTransactionResult<TData>?> ApplyTransactionAsync(AgGridTransaction<TData> transaction) =>
        InvokeControllerAsync<AgGridTransactionResult<TData>?>("applyTransaction", transaction);

    public ValueTask ApplyTransactionDeferredAsync(AgGridTransaction<TData> transaction) =>
        InvokeVoidAsync("applyTransactionAsync", transaction);

    public ValueTask FlushAsyncTransactionsAsync() => InvokeVoidAsync("flushAsyncTransactions");

    public ValueTask SelectAllAsync(string? mode = null) =>
        mode is null ? InvokeVoidAsync("selectAll") : InvokeVoidAsync("selectAll", mode);

    public ValueTask DeselectAllAsync(string? mode = null) =>
        mode is null ? InvokeVoidAsync("deselectAll") : InvokeVoidAsync("deselectAll", mode);

    public ValueTask SelectAllFilteredAsync() => InvokeVoidAsync("selectAllFiltered");
    public ValueTask DeselectAllFilteredAsync() => InvokeVoidAsync("deselectAllFiltered");
    public ValueTask SelectAllOnCurrentPageAsync() => InvokeVoidAsync("selectAllOnCurrentPage");
    public ValueTask DeselectAllOnCurrentPageAsync() => InvokeVoidAsync("deselectAllOnCurrentPage");

    public ValueTask<JsonElement> GetFilterModelAsync() => InvokeAsync<JsonElement>("getFilterModel");
    public ValueTask SetFilterModelAsync(object? model) => InvokeVoidAsync("setFilterModel", model);
    public ValueTask<JsonElement> GetColumnStateAsync() => InvokeAsync<JsonElement>("getColumnState");
    public ValueTask<IReadOnlyList<AgGridColumnState>?> GetTypedColumnStateAsync() =>
        InvokeAsync<IReadOnlyList<AgGridColumnState>?>("getColumnState");
    public ValueTask<bool> ApplyColumnStateAsync(object state) => InvokeAsync<bool>("applyColumnState", state);
    public ValueTask<bool> ApplyColumnStateAsync(AgGridApplyColumnStateParameters state) =>
        InvokeAsync<bool>("applyColumnState", state);
    public ValueTask<JsonElement> GetStateAsync() => InvokeAsync<JsonElement>("getState");
    public ValueTask UpdateGridOptionsAsync(IReadOnlyDictionary<string, object?> options) =>
        InvokeControllerVoidAsync("updateOptions", options);
    public ValueTask UpdateGridOptionsAsync(AgGridOptions options) =>
        InvokeControllerVoidAsync("updateOptions", options.ToDictionary());
    public ValueTask SetGridOptionAsync(string key, object? value) =>
        InvokeVoidAsync("setGridOption", key, value);
    public ValueTask SizeColumnsToFitAsync() => InvokeVoidAsync("sizeColumnsToFit");
    public ValueTask AutoSizeAllColumnsAsync(bool skipHeader = false) =>
        InvokeVoidAsync("autoSizeAllColumns", skipHeader);
    public ValueTask AutoSizeColumnsAsync(IReadOnlyList<string> columnKeys, bool skipHeader = false) =>
        InvokeVoidAsync("autoSizeColumns", columnKeys, skipHeader);
    public ValueTask SetColumnsVisibleAsync(IReadOnlyList<string> columnKeys, bool visible) =>
        InvokeVoidAsync("setColumnsVisible", columnKeys, visible);
    public ValueTask SetColumnsPinnedAsync(IReadOnlyList<string> columnKeys, object? pinned) =>
        InvokeVoidAsync("setColumnsPinned", columnKeys, pinned);
    public ValueTask EnsureIndexVisibleAsync(int index, string? position = null) =>
        position is null
            ? InvokeVoidAsync("ensureIndexVisible", index)
            : InvokeVoidAsync("ensureIndexVisible", index, position);
    public ValueTask<bool> EnsureNodeVisibleAsync(string rowId, string? position = null) =>
        position is null
            ? InvokeControllerAsync<bool>("ensureNodeVisible", rowId)
            : InvokeControllerAsync<bool>("ensureNodeVisible", rowId, position);
    public ValueTask EnsureColumnVisibleAsync(string columnKey, string? position = null) =>
        position is null
            ? InvokeVoidAsync("ensureColumnVisible", columnKey)
            : InvokeVoidAsync("ensureColumnVisible", columnKey, position);
    public ValueTask SetRowNodeExpandedAsync(
        string rowId,
        bool expanded,
        bool? expandParents = null,
        bool? forceSync = null) =>
        InvokeControllerVoidAsync("setRowNodeExpanded", rowId, expanded, expandParents, forceSync);
    public ValueTask SetFocusedCellAsync(AgGridCellPosition cell) =>
        cell.RowPinned is null
            ? InvokeVoidAsync("setFocusedCell", cell.RowIndex, cell.ColumnId)
            : InvokeVoidAsync("setFocusedCell", cell.RowIndex, cell.ColumnId, cell.RowPinned);
    public ValueTask SetFocusedHeaderAsync(string columnKey, bool floatingFilter = false) =>
        InvokeVoidAsync("setFocusedHeader", columnKey, floatingFilter);
    public ValueTask StartEditingCellAsync(AgGridStartEditingCellParameters parameters) =>
        InvokeVoidAsync("startEditingCell", parameters);
    public ValueTask StopEditingAsync(bool cancel = false) => InvokeVoidAsync("stopEditing", cancel);
    public ValueTask RefreshClientSideRowModelAsync(string? step = null) =>
        step is null
            ? InvokeVoidAsync("refreshClientSideRowModel")
            : InvokeVoidAsync("refreshClientSideRowModel", step);
    public ValueTask ResetRowHeightsAsync() => InvokeVoidAsync("resetRowHeights");
    public ValueTask OnRowHeightChangedAsync() => InvokeVoidAsync("onRowHeightChanged");
    public ValueTask PaginationGoToPageAsync(int page) => InvokeVoidAsync("paginationGoToPage", page);
    public ValueTask<int> PaginationGetCurrentPageAsync() => InvokeAsync<int>("paginationGetCurrentPage");
    public ValueTask<bool> PaginationIsLastPageFoundAsync() => InvokeAsync<bool>("paginationIsLastPageFound");
    public ValueTask<int> PaginationGetPageSizeAsync() => InvokeAsync<int>("paginationGetPageSize");
    public ValueTask<int> PaginationGetTotalPagesAsync() => InvokeAsync<int>("paginationGetTotalPages");
    public ValueTask<int> PaginationGetRowCountAsync() => InvokeAsync<int>("paginationGetRowCount");
    public ValueTask PaginationGoToNextPageAsync() => InvokeVoidAsync("paginationGoToNextPage");
    public ValueTask PaginationGoToPreviousPageAsync() => InvokeVoidAsync("paginationGoToPreviousPage");
    public ValueTask PaginationGoToFirstPageAsync() => InvokeVoidAsync("paginationGoToFirstPage");
    public ValueTask PaginationGoToLastPageAsync() => InvokeVoidAsync("paginationGoToLastPage");
    public ValueTask PaginationSetPageSizeAsync(int pageSize) => SetGridOptionAsync("paginationPageSize", pageSize);
    public ValueTask<string?> GetQuickFilterAsync() => InvokeAsync<string?>("getQuickFilter");
    public ValueTask ResetQuickFilterAsync() => InvokeVoidAsync("resetQuickFilter");
    public ValueTask SetStateAsync(object state, IReadOnlyList<string>? propertiesToIgnore = null) =>
        propertiesToIgnore is null
            ? InvokeVoidAsync("setState", state)
            : InvokeVoidAsync("setState", state, propertiesToIgnore);
    public ValueTask<string?> GetDataAsCsvAsync(object? parameters = null) =>
        parameters is null ? InvokeAsync<string?>("getDataAsCsv") : InvokeAsync<string?>("getDataAsCsv", parameters);
    public ValueTask RefreshHeaderAsync() => InvokeVoidAsync("refreshHeader");
    public ValueTask FlashCellsAsync(object? parameters = null) =>
        parameters is null ? InvokeVoidAsync("flashCells") : InvokeVoidAsync("flashCells", parameters);
    public ValueTask ResetColumnStateAsync() => InvokeVoidAsync("resetColumnState");
    public ValueTask ResetColumnGroupStateAsync() => InvokeVoidAsync("resetColumnGroupState");
    public ValueTask<JsonElement> GetColumnGroupStateAsync() => InvokeAsync<JsonElement>("getColumnGroupState");
    public ValueTask<IReadOnlyList<string>?> GetAllDisplayedColumnIdsAsync() =>
        InvokeControllerAsync<IReadOnlyList<string>?>("getAllDisplayedColumnIds");
    public ValueTask SetColumnWidthsAsync(object widths) => InvokeVoidAsync("setColumnWidths", widths);
    public ValueTask SetGridAriaPropertyAsync(string property, string? value) =>
        InvokeVoidAsync("setGridAriaProperty", property, value);
    public ValueTask UndoCellEditingAsync() => InvokeVoidAsync("undoCellEditing");
    public ValueTask RedoCellEditingAsync() => InvokeVoidAsync("redoCellEditing");
    public ValueTask<int> GetCurrentUndoSizeAsync() => InvokeAsync<int>("getCurrentUndoSize");
    public ValueTask<int> GetCurrentRedoSizeAsync() => InvokeAsync<int>("getCurrentRedoSize");
    public ValueTask<bool?> IsLastRowIndexKnownAsync() => InvokeAsync<bool?>("isLastRowIndexKnown");
    public ValueTask<JsonElement> GetCacheBlockStateAsync() => InvokeAsync<JsonElement>("getCacheBlockState");
    public ValueTask RefreshInfiniteCacheAsync() => InvokeVoidAsync("refreshInfiniteCache");
    public ValueTask PurgeInfiniteCacheAsync() => InvokeVoidAsync("purgeInfiniteCache");
    public ValueTask SetRowCountAsync(int rowCount, bool? maxRowFound = null) =>
        maxRowFound is null
            ? InvokeVoidAsync("setRowCount", rowCount)
            : InvokeVoidAsync("setRowCount", rowCount, maxRowFound);
    public ValueTask ExportDataAsCsvAsync(object? parameters = null) =>
        parameters is null ? InvokeVoidAsync("exportDataAsCsv") : InvokeVoidAsync("exportDataAsCsv", parameters);
    public ValueTask SetLoadingAsync(bool loading) => InvokeControllerVoidAsync("setLoading", loading);
    public ValueTask ShowLoadingOverlayAsync() => SetLoadingAsync(true);
    public ValueTask ShowNoRowsOverlayAsync() => InvokeVoidAsync("showNoRowsOverlay");
    public ValueTask HideOverlayAsync() => InvokeControllerVoidAsync("hideOverlay");
    public ValueTask RefreshCellsAsync(object? parameters = null) =>
        parameters is null ? InvokeVoidAsync("refreshCells") : InvokeVoidAsync("refreshCells", parameters);
    public ValueTask RedrawRowsAsync(object? parameters = null) =>
        parameters is null ? InvokeVoidAsync("redrawRows") : InvokeVoidAsync("redrawRows", parameters);

    public ValueTask<TResult> InvokeAsync<TResult>(string method, params object?[] arguments)
    {
        ThrowIfDisposed();
        return _controller.InvokeAsync<TResult>("invoke", method, arguments);
    }

    public ValueTask InvokeVoidAsync(string method, params object?[] arguments)
    {
        ThrowIfDisposed();
        return _controller.InvokeVoidAsync("invoke", method, arguments);
    }

    internal void MarkDisposed() => _disposed = true;

    private ValueTask<TResult> InvokeControllerAsync<TResult>(string method, params object?[] arguments)
    {
        ThrowIfDisposed();
        return _controller.InvokeAsync<TResult>(method, arguments);
    }

    private ValueTask InvokeControllerVoidAsync(string method, params object?[] arguments)
    {
        ThrowIfDisposed();
        return _controller.InvokeVoidAsync(method, arguments);
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
