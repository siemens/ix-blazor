// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using SiemensIXBlazor.Components.AGGrid;
using System.Text.Json;

namespace SiemensIXBlazor.Tests.AGGrid;

public sealed class AGGridTests : TestContextBase
{
    private readonly Mock<IJSRuntime> _runtime = new();
    private readonly Mock<IJSObjectReference> _module = new();
    private readonly Mock<IJSObjectReference> _controller = new();

    public AGGridTests()
    {
        _runtime
            .Setup(runtime => runtime.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]?>()))
            .ReturnsAsync(_module.Object);
        _module
            .Setup(module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()))
            .ReturnsAsync(_controller.Object);
        _controller
            .Setup(controller => controller.InvokeAsync<JsonElement>("invoke", It.IsAny<object[]?>()))
            .ReturnsAsync(() => JsonDocument.Parse("{}").RootElement.Clone());
        Services.AddSingleton(_runtime.Object);
    }

    [Fact]
    public void InitializesAutomaticallyWithRenderedElement()
    {
        Dictionary<string, object?>? capturedOptions = null;
        _module
            .Setup(module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()))
            .Callback<string, object[]?>((_, arguments) =>
                capturedOptions = Assert.IsType<Dictionary<string, object?>>(arguments![1]))
            .ReturnsAsync(_controller.Object);

        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid();

        Assert.True(cut.Instance.IsReady);
        Assert.NotNull(cut.Instance.Api);
        Assert.NotNull(capturedOptions);
        Assert.Equal(2, Assert.IsAssignableFrom<Array>(capturedOptions["rowData"]).Length);
        Assert.Single(Assert.IsAssignableFrom<Array>(capturedOptions["columnDefs"]));
        _runtime.Verify(
            runtime => runtime.InvokeAsync<IJSObjectReference>(
                "import",
                It.Is<object[]?>(arguments =>
                    (string)arguments![0] == "./_content/Siemens.IX.Blazor/js/siemens-ix/aggrid/ag-grid.bundle.js")),
            Times.Once);
    }

    [Fact]
    public async Task ReturnsTypedSelectedRows()
    {
        TestRow[] selected = [new("Motor", 1)];
        _controller
            .Setup(controller => controller.InvokeAsync<TestRow[]?>("getSelectedRows", It.IsAny<object[]?>()))
            .ReturnsAsync(selected);

        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid();

        TestRow[] result = await cut.Instance.GetSelectedRowsAsync();

        Assert.Equal(selected, result);
    }

    [Fact]
    public async Task DispatchesTypedAndGenericEvents()
    {
        AgGridEvent<TestRow>? genericEvent = null;
        AgGridEvent<TestRow>? typedEvent = null;
        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid(parameters => parameters
            .Add(component => component.EventReceived, EventCallback.Factory.Create<AgGridEvent<TestRow>>(
                this,
                value => genericEvent = value))
            .Add(component => component.CellClicked, EventCallback.Factory.Create<AgGridEvent<TestRow>>(
                this,
                value => typedEvent = value)));

        using JsonDocument document = JsonDocument.Parse(
            """{"rowIndex":1,"column":{"colId":"name"},"value":"Pump","data":{"name":"Pump","value":2}}""");
        await cut.Instance.DispatchEventAsync("cellClicked", document.RootElement);

        Assert.NotNull(genericEvent);
        Assert.NotNull(typedEvent);
        Assert.Equal("name", typedEvent.ColumnId);
        Assert.Equal(1, typedEvent.RowIndex);
        Assert.Equal(new TestRow("Pump", 2), typedEvent.Data);
        Assert.Equal(1, typedEvent.Sequence);
    }

    [Fact]
    public async Task DispatchesAdditionalTypedEvents()
    {
        AgGridEvent<TestRow>? rowEvent = null;
        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid(parameters => parameters
            .Add(component => component.RowDoubleClicked, EventCallback.Factory.Create<AgGridEvent<TestRow>>(
                this,
                value => rowEvent = value)));

        using JsonDocument document = JsonDocument.Parse(
            """{"rowIndex":0,"data":{"name":"Pump","value":2}}""");
        await cut.Instance.DispatchEventAsync("rowDoubleClicked", document.RootElement);

        Assert.NotNull(rowEvent);
        Assert.Equal(0, rowEvent.RowIndex);
        Assert.Equal(new TestRow("Pump", 2), rowEvent.Data);
    }

    [Fact]
    public void RejectsRowDataTogetherWithInfiniteDatasource()
    {
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            RenderGrid(parameters => parameters.Add(
                component => component.InfiniteDataSource,
                new TestDataSource())));

        Assert.Contains("cannot be configured together", exception.ToString());
    }

    [Fact]
    public async Task BridgesInfiniteDatasourceRequests()
    {
        TestDataSource datasource = new();
        using IRenderedComponent<AGGrid<TestRow>> cut = Render<AGGrid<TestRow>>(parameters => parameters
            .Add(component => component.ColumnDefinitions, Columns)
            .Add(component => component.InfiniteDataSource, datasource));
        using JsonDocument sort = JsonDocument.Parse("[]");
        using JsonDocument filter = JsonDocument.Parse("{}");
        AgGridGetRowsRequest request = new("request-1", 0, 100, sort.RootElement, filter.RootElement);

        AgGridDataBlock<TestRow> result = await cut.Instance.GetInfiniteRowsAsync(request);

        Assert.Single(result.Rows);
        Assert.Equal(1, result.RowCount);
        Assert.Equal(request, datasource.LastRequest);
    }

    [Fact]
    public void ReplacesChangedRowDataWithoutRecreatingGrid()
    {
        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid();
        TestRow[] replacement = [new("Replacement", 10)];

        cut.Render(parameters => parameters
            .Add(component => component.RowData, replacement)
            .Add(component => component.ColumnDefinitions, Columns));

        Assert.Single(_controller.Invocations, invocation =>
            invocation.Arguments.Count > 1 &&
            (string)invocation.Arguments[0] == "setRowData" &&
            ReferenceEquals(((object[]?)invocation.Arguments[1])![0], replacement));
        _module.Verify(
            module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()),
            Times.Once);
    }

    [Fact]
    public void RecreatesGridWhenExtensionModuleChanges()
    {
        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid();

        cut.Render(parameters => parameters
            .Add(component => component.RowData, Rows)
            .Add(component => component.ColumnDefinitions, Columns)
            .Add(component => component.JavaScriptModule, "./grid-extension.js"));

        Assert.Single(_controller.Invocations, invocation =>
            invocation.Arguments.Count > 0 &&
            (string)invocation.Arguments[0] == "destroy");
        _module.Verify(
            module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()),
            Times.Exactly(2));
    }

    [Fact]
    public void DuplicateTypedAndAdditionalOptionIsRejected()
    {
        AgGridOptions options = new() { Pagination = true };
        options.AdditionalOptions["pagination"] = false;

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            RenderGrid(parameters => parameters.Add(component => component.Options, options)));

        Assert.Contains("already configured", exception.ToString());
    }

    [Fact]
    public void RecreatesWithGridStateWhenOptionsChange()
    {
        using JsonDocument stateDocument = JsonDocument.Parse("{\"filter\":{\"name\":{\"filter\":\"Pump\"}}}");
        JsonElement state = stateDocument.RootElement.Clone();
        _controller
            .Setup(controller => controller.InvokeAsync<JsonElement>("invoke", It.IsAny<object[]?>()))
            .ReturnsAsync(state);
        List<Dictionary<string, object?>> capturedOptions = [];
        _module
            .Setup(module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()))
            .Callback<string, object[]?>((_, arguments) =>
                capturedOptions.Add(Assert.IsType<Dictionary<string, object?>>(arguments![1])))
            .ReturnsAsync(_controller.Object);

        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid();
        cut.Render(parameters => parameters
            .Add(component => component.RowData, Rows)
            .Add(component => component.ColumnDefinitions, Columns)
            .Add(component => component.Options, new AgGridOptions { Pagination = true }));

        Assert.Equal(2, capturedOptions.Count);
        JsonElement restored = Assert.IsType<JsonElement>(capturedOptions[1]["initialState"]);
        Assert.Equal("Pump", restored.GetProperty("filter").GetProperty("name").GetProperty("filter").GetString());
    }

    [Fact]
    public void RejectsRawOptionsOwnedByTheComponent()
    {
        AgGridOptions options = new();
        options.AdditionalOptions["columnDefs"] = Array.Empty<object>();

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            RenderGrid(parameters => parameters.Add(component => component.Options, options)));

        Assert.Contains("component owns that option", exception.ToString());
    }

    [Fact]
    public async Task CancelsOutstandingDatasourceRequestsWhenReloaded()
    {
        BlockingDataSource datasource = new();
        using IRenderedComponent<AGGrid<TestRow>> cut = Render<AGGrid<TestRow>>(parameters => parameters
            .Add(component => component.ColumnDefinitions, Columns)
            .Add(component => component.InfiniteDataSource, datasource));
        using JsonDocument sort = JsonDocument.Parse("[]");
        using JsonDocument filter = JsonDocument.Parse("{}");
        AgGridGetRowsRequest request = new("blocking-request", 0, 100, sort.RootElement, filter.RootElement);

        Task<AgGridDataBlock<TestRow>> pending = cut.Instance.GetInfiniteRowsAsync(request);
        await datasource.Started.Task;
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => cut.Instance.GetInfiniteRowsAsync(request));
        await cut.Instance.ReloadAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => pending);
        Assert.True(datasource.RequestCancellation.IsCancellationRequested);
    }

    [Fact]
    public void SerializesTypedOptionsColumnsGroupsAndRawExtensions()
    {
        AgGridOptions options = new()
        {
            RowSelection = new()
            {
                Mode = "singleRow",
                Checkboxes = true,
                HeaderCheckbox = false,
                EnableClickSelection = true,
                SelectAll = "filtered",
            },
            Pagination = true,
            PaginationPageSize = 25,
            PaginationAutoPageSize = false,
            PaginateChildRows = true,
            SuppressPaginationPanel = true,
            Loading = true,
            AnimateRows = true,
            RowDragManaged = true,
            SuppressContextMenu = true,
            EnableBrowserTooltips = true,
            TooltipInteraction = true,
            SuppressCellFocus = true,
            EnableRtl = true,
            MaintainColumnOrder = true,
            SuppressMovableColumns = true,
            ReadOnlyEdit = true,
            SingleClickEdit = true,
            SuppressClickEdit = true,
            StopEditingWhenCellsLoseFocus = true,
            UndoRedoCellEditing = true,
            UndoRedoCellEditingLimit = 20,
            CacheQuickFilter = true,
            IncludeHiddenColumnsInQuickFilter = true,
            CopyHeadersToClipboard = true,
            ClipboardDelimiter = ";",
            RowHeight = 41,
            HeaderHeight = 43,
            TooltipShowDelay = 100,
            DomLayout = "autoHeight",
            QuickFilterText = "pump",
            OverlayLoadingTemplate = "loading",
            OverlayNoRowsTemplate = "empty",
            AutoSizeStrategy = new AgGridAutoSizeStrategy("fitGridWidth", DefaultMinWidth: 80),
            DefaultColumnDefinition = new AgGridColumnDefinition { Resizable = true },
        };
        AgGridColumnDefinition child = new()
        {
            Field = "name",
            ColId = "equipment-name",
            HeaderName = "Name",
            HeaderTooltip = "Equipment name",
            Type = "numericColumn",
            Sort = "asc",
            SortIndex = 0,
            Sortable = true,
            Filter = "agTextColumnFilter",
            FloatingFilter = true,
            Editable = true,
            Resizable = true,
            Hide = false,
            Pinned = "left",
            Width = 150,
            MinWidth = 100,
            MaxWidth = 200,
            Flex = 1,
            CheckboxSelection = true,
            HeaderCheckboxSelection = true,
            HeaderCheckboxSelectionFilteredOnly = true,
            RowDrag = true,
            TooltipField = "name",
            CellRenderer = "statusRenderer",
            CellRendererParams = new Dictionary<string, object?> { ["prefix"] = "Status: " },
            CellEditor = "agTextCellEditor",
            CellEditorParams = new { MaxLength = 40 },
            AutoHeight = true,
            WrapText = true,
            InitialHide = false,
            SuppressMovable = true,
        };
        IAgGridColumnDefinition[] columns =
        [
            new AgGridColumnGroupDefinition
            {
                GroupId = "equipment",
                HeaderName = "Equipment",
                OpenByDefault = true,
                MarryChildren = true,
                Children = [child],
            },
        ];
        Dictionary<string, object?>? capturedOptions = null;
        _module
            .Setup(module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()))
            .Callback<string, object[]?>((_, arguments) =>
                capturedOptions = Assert.IsType<Dictionary<string, object?>>(arguments![1]))
            .ReturnsAsync(_controller.Object);

        using IRenderedComponent<AGGrid<TestRow>> cut = Render<AGGrid<TestRow>>(parameters => parameters
            .Add(component => component.RowData, Rows)
            .Add(component => component.ColumnDefinitions, columns)
            .Add(component => component.Options, options));

        JsonElement serialized = JsonSerializer.SerializeToElement(capturedOptions);
        string[] expectedOptionNames =
        [
            "rowSelection", "pagination", "paginationPageSize", "paginationAutoPageSize", "paginateChildRows",
            "suppressPaginationPanel", "animateRows", "rowDragManaged", "suppressContextMenu",
            "enableBrowserTooltips", "tooltipInteraction", "suppressCellFocus", "enableRtl",
            "maintainColumnOrder", "suppressMovableColumns", "readOnlyEdit", "singleClickEdit", "suppressClickEdit",
            "stopEditingWhenCellsLoseFocus", "undoRedoCellEditing", "undoRedoCellEditingLimit",
            "cacheQuickFilter", "includeHiddenColumnsInQuickFilter", "copyHeadersToClipboard", "clipboardDelimiter",
            "rowHeight", "headerHeight", "tooltipShowDelay", "domLayout",
            "quickFilterText", "overlayLoadingTemplate", "overlayNoRowsTemplate",
            "autoSizeStrategy", "defaultColDef", "columnDefs", "rowData",
        ];
        Assert.All(expectedOptionNames, name => Assert.True(serialized.TryGetProperty(name, out _), name));
        Assert.Equal("singleRow", serialized.GetProperty("rowSelection").GetProperty("mode").GetString());
        Assert.All(
            new[] { "checkboxes", "headerCheckbox", "enableClickSelection", "selectAll" },
            name => Assert.True(serialized.GetProperty("rowSelection").TryGetProperty(name, out _), name));
        Assert.True(serialized.GetProperty("undoRedoCellEditing").GetBoolean());
        Assert.Equal(25, serialized.GetProperty("paginationPageSize").GetInt32());
        Assert.True(serialized.GetProperty("loading").GetBoolean());
        Assert.Equal("autoHeight", serialized.GetProperty("domLayout").GetString());
        JsonElement group = serialized.GetProperty("columnDefs")[0];
        Assert.Equal("equipment", group.GetProperty("groupId").GetString());
        JsonElement serializedChild = group.GetProperty("children")[0];
        string[] expectedColumnNames =
        [
            "field", "colId", "headerName", "headerTooltip", "type", "sort", "sortIndex", "sortable", "filter", "floatingFilter", "editable",
            "resizable", "hide", "pinned", "width", "minWidth", "maxWidth", "flex", "checkboxSelection",
            "headerCheckboxSelection", "headerCheckboxSelectionFilteredOnly", "rowDrag", "tooltipField", "cellRenderer", "cellRendererParams",
            "cellEditor", "cellEditorParams", "autoHeight", "wrapText", "initialHide", "suppressMovable",
        ];
        Assert.All(expectedColumnNames, name => Assert.True(serializedChild.TryGetProperty(name, out _), name));
        Assert.Equal("equipment-name", serializedChild.GetProperty("colId").GetString());
        Assert.Equal("statusRenderer", serializedChild.GetProperty("cellRenderer").GetString());
        Assert.Equal("Status: ", serializedChild.GetProperty("cellRendererParams").GetProperty("prefix").GetString());
        Assert.True(serializedChild.GetProperty("wrapText").GetBoolean());
    }

    [Fact]
    public async Task ForwardsEveryTypedApiOperationToTheGridController()
    {
        using JsonDocument jsonDocument = JsonDocument.Parse("{}");
        JsonElement json = jsonDocument.RootElement.Clone();
        _controller.Setup(controller => controller.InvokeAsync<JsonElement>("invoke", It.IsAny<object[]?>())).ReturnsAsync(json);
        _controller.Setup(controller => controller.InvokeAsync<bool>("invoke", It.IsAny<object[]?>())).ReturnsAsync(true);
        _controller.Setup(controller => controller.InvokeAsync<int>("invoke", It.IsAny<object[]?>())).ReturnsAsync(2);
        _controller
            .Setup(controller => controller.InvokeAsync<AgGridTransactionResult<TestRow>?>(
                "applyTransaction",
                It.IsAny<object[]?>()))
            .ReturnsAsync(new AgGridTransactionResult<TestRow>([new("Valve", 3)], [], []));
        _controller
            .Setup(controller => controller.InvokeAsync<AgGridTransactionResult<TestRow>?>(
                "removeSelectedRows",
                It.IsAny<object[]?>()))
            .ReturnsAsync(new AgGridTransactionResult<TestRow>([], [], [new("Pump", 2)]));
        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid();
        AgGridApi<TestRow> api = cut.Instance.Api!;
        _controller.Invocations.Clear();

        await api.SetRowDataAsync(Rows);
        await api.SetColumnDefinitionsAsync(Columns);
        AgGridTransactionResult<TestRow>? transaction =
            await api.ApplyTransactionAsync(new(Add: [new("Valve", 3)]));
        Assert.Equal("Valve", Assert.Single(transaction!.Add).Name);
        await api.RemoveSelectedRowsAsync();
        await api.ApplyTransactionDeferredAsync(new(Update: [Rows[0]]));
        await api.FlushAsyncTransactionsAsync();
        await api.SelectAllAsync("filtered");
        await api.DeselectAllAsync();
        await api.GetFilterModelAsync();
        await api.SetFilterModelAsync(new { name = "Pump" });
        await api.GetColumnStateAsync();
        await api.ApplyColumnStateAsync(new { state = Array.Empty<object>() });
        await api.GetStateAsync();
        await api.SizeColumnsToFitAsync();
        await api.AutoSizeAllColumnsAsync(true);
        await api.EnsureIndexVisibleAsync(10, "middle");
        await api.PaginationGoToPageAsync(2);
        await api.PaginationGetCurrentPageAsync();
        await api.ExportDataAsCsvAsync(new { fileName = "equipment.csv" });
        await api.ShowLoadingOverlayAsync();
        await api.SetLoadingAsync(false);
        await api.ShowNoRowsOverlayAsync();
        await api.HideOverlayAsync();
        await api.RefreshCellsAsync(new { force = true });
        await api.RedrawRowsAsync();
        await api.InvokeAsync<JsonElement>("getDisplayedRowAtIndex", 0);
        await api.InvokeVoidAsync("resetQuickFilter");

        string[] directCalls = _controller.Invocations
            .Select(invocation => (string)invocation.Arguments[0])
            .Where(identifier => identifier != "invoke")
            .ToArray();
        Assert.Equal(
            [
                "setRowData", "setColumnDefinitions", "applyTransaction", "removeSelectedRows",
                "setLoading", "setLoading", "hideOverlay",
            ],
            directCalls);
        string[] invokedMethods = _controller.Invocations
            .Where(invocation => (string)invocation.Arguments[0] == "invoke")
            .Select(invocation => (string)((object[]?)invocation.Arguments[1])![0])
            .ToArray();
        Assert.Equal(
        [
            "applyTransactionAsync", "flushAsyncTransactions", "selectAll", "deselectAll",
            "getFilterModel", "setFilterModel", "getColumnState", "applyColumnState", "getState",
            "sizeColumnsToFit", "autoSizeAllColumns", "ensureIndexVisible", "paginationGoToPage",
            "paginationGetCurrentPage", "exportDataAsCsv", "showNoRowsOverlay", "refreshCells", "redrawRows",
            "getDisplayedRowAtIndex", "resetQuickFilter",
        ], invokedMethods);
    }

    [Fact]
    public async Task ExposesTypedNavigationStateAndConfigurationOperations()
    {
        _controller
            .Setup(controller => controller.InvokeAsync<IReadOnlyList<AgGridColumnState>?>(
                "invoke",
                It.IsAny<object[]?>()))
            .ReturnsAsync([new("name", Sort: "asc")]);

        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid();
        AgGridApi<TestRow> api = cut.Instance.Api!;
        _controller.Invocations.Clear();

        await api.GetSelectedNodesAsync();
        await api.GetDisplayedRowCountAsync();
        await api.GetDisplayedRowAtIndexAsync(0);
        await api.GetRowNodeAsync("0");
        await api.GetFocusedCellAsync();
        await api.GetTypedColumnStateAsync();
        await api.ApplyColumnStateAsync(new(
            State: [new("name", Hide: false)],
            ApplyOrder: true));
        await api.UpdateGridOptionsAsync(new Dictionary<string, object?>
        {
            ["quickFilterText"] = "Pump",
        });
        await api.UpdateGridOptionsAsync(new AgGridOptions { QuickFilterText = "Pump" });
        await api.SetGridOptionAsync("quickFilterText", "Pump");
        await api.AutoSizeColumnsAsync(["name"], skipHeader: true);
        await api.SetColumnsVisibleAsync(["name"], visible: true);
        await api.SetColumnsPinnedAsync(["name"], "left");
        await api.EnsureNodeVisibleAsync("0", "middle");
        await api.SetFocusedCellAsync(new(0, "name"));
        await api.StartEditingCellAsync(new(0, "name"));
        await api.StopEditingAsync(cancel: true);
        await api.RefreshClientSideRowModelAsync("sort");

        string[] directCalls = _controller.Invocations
            .Select(invocation => (string)invocation.Arguments[0])
            .Where(identifier => identifier != "invoke")
            .ToArray();
        Assert.Equal(
        [
            "getSelectedNodes", "getDisplayedRowCount", "getDisplayedRowAtIndex", "getRowNode",
            "getFocusedCell", "updateOptions", "updateOptions", "ensureNodeVisible",
        ], directCalls);
    }

    [Fact]
    public async Task ExposesCommunityApiOperationsWithoutRequiringRawInvoke()
    {
        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid();
        AgGridApi<TestRow> api = cut.Instance.Api!;
        _controller.Invocations.Clear();

        await api.GetRenderedNodesAsync();
        await api.GetFirstDisplayedRowIndexAsync();
        await api.GetLastDisplayedRowIndexAsync();
        await api.GetGridIdAsync();
        await api.GetGridOptionAsync<bool>("pagination");
        await api.GetVerticalPixelRangeAsync();
        await api.GetHorizontalPixelRangeAsync();
        await api.GetPinnedTopRowCountAsync();
        await api.GetPinnedBottomRowCountAsync();
        await api.GetColumnDefinitionsAsync();
        await api.GetEditingCellsAsync();
        await api.GetSizesForCurrentThemeAsync();
        await api.SelectAllFilteredAsync();
        await api.DeselectAllFilteredAsync();
        await api.SelectAllOnCurrentPageAsync();
        await api.DeselectAllOnCurrentPageAsync();
        await api.EnsureColumnVisibleAsync("name", "middle");
        await api.SetRowNodeExpandedAsync("0", true, expandParents: true, forceSync: true);
        await api.SetFocusedHeaderAsync("name", floatingFilter: true);
        await api.ResetRowHeightsAsync();
        await api.OnRowHeightChangedAsync();
        await api.PaginationIsLastPageFoundAsync();
        await api.PaginationGetPageSizeAsync();
        await api.PaginationGetTotalPagesAsync();
        await api.PaginationGetRowCountAsync();
        await api.PaginationGoToNextPageAsync();
        await api.PaginationGoToPreviousPageAsync();
        await api.PaginationGoToFirstPageAsync();
        await api.PaginationGoToLastPageAsync();
        await api.PaginationSetPageSizeAsync(25);
        await api.GetQuickFilterAsync();
        await api.ResetQuickFilterAsync();
        await api.SetStateAsync(new { filter = new { name = "Pump" } }, ["pagination"]);
        await api.GetDataAsCsvAsync();
        await api.RefreshHeaderAsync();
        await api.FlashCellsAsync(new { columns = new[] { "name" } });
        await api.ResetColumnStateAsync();
        await api.ResetColumnGroupStateAsync();
        await api.GetColumnGroupStateAsync();
        await api.GetAllDisplayedColumnIdsAsync();
        await api.SetColumnWidthsAsync(new[] { new { key = "name", width = 180 } });
        await api.SetGridAriaPropertyAsync("label", "Equipment");
        await api.UndoCellEditingAsync();
        await api.RedoCellEditingAsync();
        await api.GetCurrentUndoSizeAsync();
        await api.GetCurrentRedoSizeAsync();
        await api.IsLastRowIndexKnownAsync();
        await api.GetCacheBlockStateAsync();
        await api.RefreshInfiniteCacheAsync();
        await api.PurgeInfiniteCacheAsync();
        await api.SetRowCountAsync(100, maxRowFound: true);

        string[] invokedMethods = _controller.Invocations
            .Where(invocation => (string)invocation.Arguments[0] == "invoke")
            .Select(invocation => (string)((object[]?)invocation.Arguments[1])![0])
            .ToArray();

        Assert.Equal(
        [
            "getFirstDisplayedRowIndex", "getLastDisplayedRowIndex", "getGridId", "getGridOption",
            "getVerticalPixelRange", "getHorizontalPixelRange", "getPinnedTopRowCount", "getPinnedBottomRowCount",
            "getColumnDefs", "getEditingCells", "getSizesForCurrentTheme",
            "selectAllFiltered", "deselectAllFiltered", "selectAllOnCurrentPage", "deselectAllOnCurrentPage",
            "ensureColumnVisible", "setFocusedHeader", "resetRowHeights", "onRowHeightChanged",
            "paginationIsLastPageFound", "paginationGetPageSize", "paginationGetTotalPages", "paginationGetRowCount",
            "paginationGoToNextPage", "paginationGoToPreviousPage", "paginationGoToFirstPage", "paginationGoToLastPage",
            "setGridOption", "getQuickFilter", "resetQuickFilter", "setState", "getDataAsCsv", "refreshHeader",
            "flashCells", "resetColumnState", "resetColumnGroupState", "getColumnGroupState", "setColumnWidths",
            "setGridAriaProperty",
            "undoCellEditing", "redoCellEditing", "getCurrentUndoSize", "getCurrentRedoSize",
            "isLastRowIndexKnown", "getCacheBlockState", "refreshInfiniteCache", "purgeInfiniteCache", "setRowCount",
        ], invokedMethods);

        string[] directMethods = _controller.Invocations
            .Where(invocation => (string)invocation.Arguments[0] != "invoke")
            .Select(invocation => (string)invocation.Arguments[0])
            .ToArray();
        Assert.Equal(["getRenderedNodes", "setRowNodeExpanded", "getAllDisplayedColumnIds"], directMethods);
    }

    [Fact]
    public void SerializesAdditionalCommunityOptions()
    {
        AgGridOptions options = new()
        {
            PaginationPageSizeSelector = new[] { 5, 10, 25 },
            SuppressRowHoverHighlight = true,
            ColumnHoverHighlight = true,
            SuppressRowTransform = true,
            SuppressRowVirtualisation = true,
            SuppressColumnVirtualisation = true,
            EnableCellTextSelection = true,
            EnsureDomOrder = true,
            SuppressCopySingleCellRanges = true,
            SuppressLastEmptyLineOnPaste = true,
            LocaleText = new() { ["noRowsToShow"] = "Nothing here" },
        };

        Dictionary<string, object?>? capturedOptions = null;
        _module
            .Setup(module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()))
            .Callback<string, object[]?>((_, arguments) =>
                capturedOptions = Assert.IsType<Dictionary<string, object?>>(arguments![1]))
            .ReturnsAsync(_controller.Object);

        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid(parameters => parameters
            .Add(component => component.Options, options));

        JsonElement serialized = JsonSerializer.SerializeToElement(capturedOptions);
        Assert.Equal([5, 10, 25], serialized.GetProperty("paginationPageSizeSelector").EnumerateArray()
            .Select(element => element.GetInt32()).ToArray());
        Assert.True(serialized.GetProperty("suppressRowHoverHighlight").GetBoolean());
        Assert.True(serialized.GetProperty("enableCellTextSelection").GetBoolean());
        Assert.Equal("Nothing here", serialized.GetProperty("localeText").GetProperty("noRowsToShow").GetString());
    }

    [Fact]
    public void ReplacesChangedColumnsWithoutRecreatingGrid()
    {
        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid();
        IAgGridColumnDefinition[] replacement =
        [
            new AgGridColumnDefinition { Field = "value", HeaderName = "Value" },
        ];

        cut.Render(parameters => parameters
            .Add(component => component.RowData, Rows)
            .Add(component => component.ColumnDefinitions, replacement));

        Assert.Single(_controller.Invocations, invocation =>
            invocation.Arguments.Count > 0 && (string)invocation.Arguments[0] == "setColumnDefinitions");
        _module.Verify(
            module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()),
            Times.Once);
    }

    [Fact]
    public async Task ReportsInitializationFailureOnceAndReloadsExplicitly()
    {
        _module
            .SetupSequence(module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()))
            .ThrowsAsync(new JSException("initialization failed"))
            .ReturnsAsync(_controller.Object);
        List<AgGridInitializationError> errors = [];
        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid(parameters => parameters.Add(
            component => component.InitializationFailed,
            EventCallback.Factory.Create<AgGridInitializationError>(this, errors.Add)));

        Assert.False(cut.Instance.IsReady);
        Assert.Single(errors);
        cut.Render();
        _module.Verify(
            module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()),
            Times.Once);

        await cut.Instance.ReloadAsync();

        Assert.True(cut.Instance.IsReady);
        _module.Verify(
            module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()),
            Times.Exactly(2));
    }

    [Theory]
    [InlineData("datasource")]
    [InlineData("rowModelType")]
    public void RejectsRawInfiniteOptionsOwnedByTheComponent(string optionName)
    {
        AgGridOptions options = new();
        options.AdditionalOptions[optionName] = new object();

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            Render<AGGrid<TestRow>>(parameters => parameters
                .Add(component => component.ColumnDefinitions, Columns)
                .Add(component => component.InfiniteDataSource, new TestDataSource())
                .Add(component => component.Options, options)));

        Assert.Contains("component owns that option", exception.ToString());
    }

    [Fact]
    public async Task SubscribesAllTypedAndExplicitEventsWithoutDuplicates()
    {
        object? capturedSettings = null;
        _module
            .Setup(module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()))
            .Callback<string, object[]?>((_, arguments) => capturedSettings = arguments![3])
            .ReturnsAsync(_controller.Object);
        List<string> received = [];
        EventCallback<AgGridEvent<TestRow>> callback(string name) =>
            EventCallback.Factory.Create<AgGridEvent<TestRow>>(this, _ => received.Add(name));
        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid(parameters => parameters
            .Add(component => component.EventSubscriptions,
                new AgGridEventName[] { "modelUpdated", AgGridEventNames.CellClicked })
            .Add(component => component.CellClicked, callback("cell"))
            .Add(component => component.CellValueChanged, callback("value"))
            .Add(component => component.RowClicked, callback("row"))
            .Add(component => component.SelectionChanged, callback("selection"))
            .Add(component => component.FilterChanged, callback("filter"))
            .Add(component => component.SortChanged, callback("sort"))
            .Add(component => component.PaginationChanged, callback("pagination"))
            .Add(component => component.ModelUpdated, callback("model")));

        JsonElement settings = JsonSerializer.SerializeToElement(capturedSettings);
        string[] subscriptions = settings.GetProperty("eventSubscriptions")
            .EnumerateArray()
            .Select(element => element.GetString()!)
            .ToArray();
        Assert.Equal(
        [
            "cellClicked", "cellValueChanged", "filterChanged", "modelUpdated", "paginationChanged",
            "rowClicked", "selectionChanged", "sortChanged",
        ], subscriptions);

        using JsonDocument payload = JsonDocument.Parse("{}");
        foreach (string eventName in new[]
        {
            "cellClicked", "cellValueChanged", "rowClicked", "selectionChanged",
            "filterChanged", "sortChanged", "paginationChanged", "modelUpdated",
        })
        {
            await cut.Instance.DispatchEventAsync(eventName, payload.RootElement);
        }
        Assert.Equal(["cell", "value", "row", "selection", "filter", "sort", "pagination", "model"], received);
    }

    [Fact]
    public async Task RaisesGridReadyAndDisposesControllerModuleAndApi()
    {
        AgGridReadyEvent<TestRow>? ready = null;
        IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid(parameters => parameters.Add(
            component => component.GridReady,
            EventCallback.Factory.Create<AgGridReadyEvent<TestRow>>(this, value => ready = value)));
        AgGridApi<TestRow> api = cut.Instance.Api!;

        Assert.Same(api, ready?.Api);
        await cut.Instance.DisposeAsync();
        cut.Dispose();

        Assert.Single(_controller.Invocations, invocation =>
            invocation.Arguments.Count > 0 && (string)invocation.Arguments[0] == "destroy");
        _controller.Verify(controller => controller.DisposeAsync(), Times.Once);
        _module.Verify(module => module.DisposeAsync(), Times.Once);
        Assert.Throws<ObjectDisposedException>(() => api.SizeColumnsToFitAsync());
    }

    [Fact]
    public async Task RefreshesRowsThroughTheComponentApi()
    {
        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid();
        _controller.Invocations.Clear();

        await cut.Instance.RefreshDataAsync();

        Assert.Single(_controller.Invocations, invocation =>
            invocation.Arguments.Count > 0 && (string)invocation.Arguments[0] == "setRowData");
    }

    [Fact]
    public async Task RejectsInfiniteRequestsWithoutDatasourceAndAfterDisposal()
    {
        using JsonDocument sort = JsonDocument.Parse("[]");
        using JsonDocument filter = JsonDocument.Parse("{}");
        AgGridGetRowsRequest request = new("request", 0, 10, sort.RootElement, filter.RootElement);
        using IRenderedComponent<AGGrid<TestRow>> withoutDatasource = RenderGrid();
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => withoutDatasource.Instance.GetInfiniteRowsAsync(request));

        using IRenderedComponent<AGGrid<TestRow>> disposed = Render<AGGrid<TestRow>>(parameters => parameters
            .Add(component => component.ColumnDefinitions, Columns)
            .Add(component => component.InfiniteDataSource, new TestDataSource()));
        await disposed.Instance.DisposeAsync();

        await Assert.ThrowsAsync<ObjectDisposedException>(
            () => disposed.Instance.GetInfiniteRowsAsync(request));
    }

    [Fact]
    public void RecreatesWhenEventSubscriptionsChange()
    {
        using IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid();

        cut.Render(parameters => parameters
            .Add(component => component.RowData, Rows)
            .Add(component => component.ColumnDefinitions, Columns)
            .Add(component => component.EventSubscriptions, new AgGridEventName[] { "modelUpdated" }));

        _module.Verify(
            module => module.InvokeAsync<IJSObjectReference>("createAgGrid", It.IsAny<object[]?>()),
            Times.Exactly(2));
        Assert.Single(_controller.Invocations, invocation =>
            invocation.Arguments.Count > 0 && (string)invocation.Arguments[0] == "destroy");
    }

    [Fact]
    public async Task ReadsDirectColumnIdsAndIgnoresEventsAfterDisposal()
    {
        AgGridEvent<TestRow>? received = null;
        IRenderedComponent<AGGrid<TestRow>> cut = RenderGrid(parameters => parameters.Add(
            component => component.EventReceived,
            EventCallback.Factory.Create<AgGridEvent<TestRow>>(this, value => received = value)));
        using JsonDocument payload = JsonDocument.Parse("{\"columnId\":\"status\"}");

        await cut.Instance.DispatchEventAsync("cellFocused", payload.RootElement);
        Assert.Equal("status", received?.ColumnId);

        received = null;
        await cut.Instance.DisposeAsync();
        await cut.Instance.DispatchEventAsync("cellFocused", payload.RootElement);
        Assert.Null(received);
        cut.Dispose();
    }

    private IRenderedComponent<AGGrid<TestRow>> RenderGrid(
        Action<ComponentParameterCollectionBuilder<AGGrid<TestRow>>>? configure = null)
    {
        return Render<AGGrid<TestRow>>(parameters =>
        {
            parameters
                .Add(component => component.Id, "test-grid")
                .Add(component => component.Style, "height: 20rem")
                .Add(component => component.RowData, Rows)
                .Add(component => component.ColumnDefinitions, Columns);
            configure?.Invoke(parameters);
        });
    }

    private static readonly TestRow[] Rows =
    [
        new("Motor", 1),
        new("Pump", 2),
    ];

    private static readonly IAgGridColumnDefinition[] Columns =
    [
        new AgGridColumnDefinition { Field = "name", HeaderName = "Name", Sortable = true },
    ];

    public sealed record TestRow(string Name, int Value);

    private sealed class TestDataSource : IAgGridInfiniteDataSource<TestRow>
    {
        public AgGridGetRowsRequest? LastRequest { get; private set; }

        public ValueTask<AgGridDataBlock<TestRow>> GetRowsAsync(
            AgGridGetRowsRequest request,
            CancellationToken cancellationToken)
        {
            LastRequest = request;
            return ValueTask.FromResult(
                new AgGridDataBlock<TestRow>([new("Infinite", 1)], 1));
        }
    }

    private sealed class BlockingDataSource : IAgGridInfiniteDataSource<TestRow>
    {
        public TaskCompletionSource Started { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public CancellationToken RequestCancellation { get; private set; }

        public async ValueTask<AgGridDataBlock<TestRow>> GetRowsAsync(
            AgGridGetRowsRequest request,
            CancellationToken cancellationToken)
        {
            RequestCancellation = cancellationToken;
            Started.SetResult();
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            return new([], null);
        }
    }
}
