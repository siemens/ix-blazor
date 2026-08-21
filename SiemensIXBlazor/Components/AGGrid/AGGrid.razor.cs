// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Concurrent;
using System.Text.Json;

namespace SiemensIXBlazor.Components.AGGrid;

public partial class AGGrid<TData> : IAsyncDisposable
{
    private const string ModulePath =
        "./_content/Siemens.IX.Blazor/js/siemens-ix/aggrid/ag-grid.bundle.js";

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true,
    };

    private readonly SemaphoreSlim _lifecycleGate = new(1, 1);
    private readonly ConcurrentDictionary<string, CancellationTokenSource> _dataSourceRequests = new();
    private readonly string _instanceId = $"ag-grid-{Guid.NewGuid():N}";
    private ElementReference _element;
    private IJSObjectReference? _module;
    private IJSObjectReference? _controller;
    private DotNetObjectReference<AGGrid<TData>>? _dotNetReference;
    private string? _optionsSignature;
    private string? _columnsSignature;
    private string? _eventsSignature;
    private object? _rowDataReference;
    private object? _dataSourceReference;
    private string? _javaScriptModuleReference;
    private bool _stripedRows;
    private long _eventSequence;
    private bool _isReady;
    private bool _lifecycleFaulted;
    private bool _disposed;

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    [Parameter]
    public string? Id { get; set; }

    [Parameter]
    public IReadOnlyList<TData>? RowData { get; set; }

    [Parameter, EditorRequired]
    public IReadOnlyList<IAgGridColumnDefinition> ColumnDefinitions { get; set; } = [];

    [Parameter]
    public AgGridOptions Options { get; set; } = new();

    [Parameter]
    public IAgGridInfiniteDataSource<TData>? InfiniteDataSource { get; set; }

    [Parameter]
    public string? JavaScriptModule { get; set; }

    [Parameter]
    public IReadOnlyCollection<AgGridEventName> EventSubscriptions { get; set; } = [];

    [Parameter]
    public EventCallback<AgGridReadyEvent<TData>> GridReady { get; set; }

    [Parameter]
    public EventCallback<AgGridInitializationError> InitializationFailed { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> EventReceived { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> CellClicked { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> CellValueChanged { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> CellDoubleClicked { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> RowClicked { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> RowDoubleClicked { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> RowSelected { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> SelectionChanged { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> FilterChanged { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> SortChanged { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> PaginationChanged { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> FirstDataRendered { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> RowDataUpdated { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> GridPreDestroyed { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> ColumnEverythingChanged { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> ColumnMoved { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> ColumnVisible { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> ColumnPinned { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> ColumnResized { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> DisplayedColumnsChanged { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> VirtualColumnsChanged { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> AsyncTransactionsFlushed { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> ModelUpdated { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> CellContextMenu { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> CellFocused { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> RowValueChanged { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> CellEditingStarted { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> CellEditingStopped { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> GridSizeChanged { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> ViewportChanged { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> BodyScroll { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> BodyScrollEnd { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> StateUpdated { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> PasteStart { get; set; }

    [Parameter]
    public EventCallback<AgGridEvent<TData>> PasteEnd { get; set; }

    public AgGridApi<TData>? Api { get; private set; }
    public bool IsReady => _isReady;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_disposed || _lifecycleFaulted)
        {
            return;
        }

        await _lifecycleGate.WaitAsync();
        try
        {
            if (_controller is null)
            {
                await InitializeAsync();
            }
            else
            {
                await SynchronizeParametersAsync();
            }
        }
        catch (Exception exception)
        {
            _isReady = false;
            _lifecycleFaulted = true;
            if (InitializationFailed.HasDelegate)
            {
                await InitializationFailed.InvokeAsync(new(_instanceId, exception));
            }
            else
            {
                throw;
            }
        }
        finally
        {
            _lifecycleGate.Release();
        }
    }

    public async Task ReloadAsync()
    {
        ThrowIfDisposed();
        await _lifecycleGate.WaitAsync();
        try
        {
            _lifecycleFaulted = false;
            await DestroyGridAsync();
            await InitializeAsync();
        }
        finally
        {
            _lifecycleGate.Release();
        }
        await InvokeAsync(StateHasChanged);
    }

    public async Task<TData[]> GetSelectedRowsAsync()
    {
        AgGridApi<TData> api = EnsureApi();
        return await api.GetSelectedRowsAsync() ?? [];
    }

    public async Task RefreshDataAsync()
    {
        AgGridApi<TData> api = EnsureApi();
        await api.SetRowDataAsync(RowData);
        _rowDataReference = RowData;
    }

    [JSInvokable]
    public async Task DispatchEventAsync(string eventName, JsonElement payload)
    {
        if (_disposed)
        {
            return;
        }

        TData? data = default;
        if (payload.TryGetProperty("data", out JsonElement dataElement) &&
            dataElement.ValueKind is not JsonValueKind.Null and not JsonValueKind.Undefined)
        {
            data = dataElement.Deserialize<TData>(SerializerOptions);
        }

        int? rowIndex = payload.TryGetProperty("rowIndex", out JsonElement rowIndexElement) &&
                        rowIndexElement.TryGetInt32(out int parsedRowIndex)
            ? parsedRowIndex
            : null;

        string? columnId = null;
        if (payload.TryGetProperty("column", out JsonElement columnElement) &&
            columnElement.TryGetProperty("colId", out JsonElement columnIdElement))
        {
            columnId = columnIdElement.GetString();
        }
        else if (payload.TryGetProperty("columnId", out JsonElement directColumnId) ||
                 payload.TryGetProperty("colId", out directColumnId))
        {
            columnId = directColumnId.GetString();
        }

        JsonElement? value = payload.TryGetProperty("value", out JsonElement valueElement)
            ? valueElement.Clone()
            : null;

        AgGridEvent<TData> gridEvent = new(
            new(eventName),
            Interlocked.Increment(ref _eventSequence),
            rowIndex,
            columnId,
            data,
            value,
            payload.Clone());

        await EventReceived.InvokeAsync(gridEvent);
        await InvokeTypedEventAsync(eventName, gridEvent);
    }

    [JSInvokable]
    public async Task<AgGridDataBlock<TData>> GetInfiniteRowsAsync(AgGridGetRowsRequest request)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(AGGrid<TData>));
        }
        if (InfiniteDataSource is null)
        {
            throw new InvalidOperationException("No infinite datasource is configured.");
        }

        CancellationTokenSource cancellation = new();
        if (!_dataSourceRequests.TryAdd(request.RequestId, cancellation))
        {
            cancellation.Dispose();
            throw new InvalidOperationException($"Duplicate datasource request '{request.RequestId}'.");
        }

        try
        {
            return await InfiniteDataSource.GetRowsAsync(request, cancellation.Token);
        }
        finally
        {
            if (_dataSourceRequests.TryRemove(request.RequestId, out CancellationTokenSource? completed))
            {
                completed.Dispose();
            }
        }
    }

    public override async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }
        _disposed = true;

        CancelDataSourceRequests();

        await _lifecycleGate.WaitAsync();
        try
        {
            await DestroyGridAsync();
            if (_module is not null)
            {
                try
                {
                    await _module.DisposeAsync();
                }
                catch (Exception exception) when (IsDisposedJavaScriptException(exception))
                {
                }
                _module = null;
            }
        }
        finally
        {
            _dotNetReference?.Dispose();
            _dotNetReference = null;
            _lifecycleGate.Release();
            _lifecycleGate.Dispose();
        }
    }

    private async Task InitializeAsync(JsonElement? restoredState = null)
    {
        ValidateParameters();
        Dictionary<string, object?> options = BuildOptions();
        if (restoredState is not null && !options.ContainsKey("initialState"))
        {
            options["initialState"] = restoredState.Value;
        }
        string[] eventNames = BuildEventSubscriptions();

        _module ??= await JSRuntime.InvokeAsync<IJSObjectReference>("import", ModulePath);
        _dotNetReference ??= DotNetObjectReference.Create(this);

        _controller = await _module.InvokeAsync<IJSObjectReference>(
            "createAgGrid",
            _element,
            options,
            _dotNetReference,
            new
            {
                instanceId = _instanceId,
                javaScriptModule = JavaScriptModule,
                stripedRows = Options.StripedRows,
                hasInfiniteDataSource = InfiniteDataSource is not null,
                eventSubscriptions = eventNames,
            });

        Api = new(_controller);
        _lifecycleFaulted = false;
        CaptureParameterState(eventNames);
        _isReady = true;
        await GridReady.InvokeAsync(new(Api));
        await InvokeAsync(StateHasChanged);
    }

    private async Task SynchronizeParametersAsync()
    {
        ValidateParameters();
        Dictionary<string, object?> options = BuildOptions(includeRowsAndColumns: false);
        string optionsSignature = JsonSerializer.Serialize(options, SerializerOptions);
        string columnsSignature = JsonSerializer.Serialize(BuildColumnDefinitions(), SerializerOptions);
        string[] eventNames = BuildEventSubscriptions();
        string eventsSignature = string.Join('|', eventNames);

        if (!ReferenceEquals(_dataSourceReference, InfiniteDataSource) ||
            !string.Equals(_javaScriptModuleReference, JavaScriptModule, StringComparison.Ordinal) ||
            _stripedRows != Options.StripedRows ||
            !string.Equals(_eventsSignature, eventsSignature, StringComparison.Ordinal))
        {
            await RecreatePreservingStateAsync();
            return;
        }

        if (!string.Equals(_optionsSignature, optionsSignature, StringComparison.Ordinal))
        {
            await RecreatePreservingStateAsync();
            return;
        }

        if (!string.Equals(_columnsSignature, columnsSignature, StringComparison.Ordinal))
        {
            await Api!.SetColumnDefinitionsAsync(ColumnDefinitions);
            _columnsSignature = columnsSignature;
        }

        if (!ReferenceEquals(_rowDataReference, RowData) && InfiniteDataSource is null)
        {
            await Api!.SetRowDataAsync(RowData);
            _rowDataReference = RowData;
        }
    }

    private Dictionary<string, object?> BuildOptions(bool includeRowsAndColumns = true)
    {
        Dictionary<string, object?> options = Options.ToDictionary();
        if (includeRowsAndColumns)
        {
            options["columnDefs"] = BuildColumnDefinitions();
            if (InfiniteDataSource is null)
            {
                options["rowData"] = RowData ?? [];
            }
        }
        return options;
    }

    private Dictionary<string, object?>[] BuildColumnDefinitions() =>
        ColumnDefinitions.Select(column => column.ToDictionary()).ToArray();

    private string[] BuildEventSubscriptions()
    {
        HashSet<string> eventNames = EventSubscriptions.Select(name => name.Value).ToHashSet(StringComparer.Ordinal);
        AddEventIfHandled(eventNames, CellClicked, AgGridEventNames.CellClicked);
        AddEventIfHandled(eventNames, CellValueChanged, AgGridEventNames.CellValueChanged);
        AddEventIfHandled(eventNames, CellDoubleClicked, AgGridEventNames.CellDoubleClicked);
        AddEventIfHandled(eventNames, RowClicked, AgGridEventNames.RowClicked);
        AddEventIfHandled(eventNames, RowDoubleClicked, AgGridEventNames.RowDoubleClicked);
        AddEventIfHandled(eventNames, RowSelected, AgGridEventNames.RowSelected);
        AddEventIfHandled(eventNames, SelectionChanged, AgGridEventNames.SelectionChanged);
        AddEventIfHandled(eventNames, FilterChanged, AgGridEventNames.FilterChanged);
        AddEventIfHandled(eventNames, SortChanged, AgGridEventNames.SortChanged);
        AddEventIfHandled(eventNames, PaginationChanged, AgGridEventNames.PaginationChanged);
        AddEventIfHandled(eventNames, FirstDataRendered, AgGridEventNames.FirstDataRendered);
        AddEventIfHandled(eventNames, RowDataUpdated, AgGridEventNames.RowDataUpdated);
        AddEventIfHandled(eventNames, GridPreDestroyed, AgGridEventNames.GridPreDestroyed);
        AddEventIfHandled(eventNames, ColumnEverythingChanged, AgGridEventNames.ColumnEverythingChanged);
        AddEventIfHandled(eventNames, ColumnMoved, AgGridEventNames.ColumnMoved);
        AddEventIfHandled(eventNames, ColumnVisible, AgGridEventNames.ColumnVisible);
        AddEventIfHandled(eventNames, ColumnPinned, AgGridEventNames.ColumnPinned);
        AddEventIfHandled(eventNames, ColumnResized, AgGridEventNames.ColumnResized);
        AddEventIfHandled(eventNames, DisplayedColumnsChanged, AgGridEventNames.DisplayedColumnsChanged);
        AddEventIfHandled(eventNames, VirtualColumnsChanged, AgGridEventNames.VirtualColumnsChanged);
        AddEventIfHandled(eventNames, AsyncTransactionsFlushed, AgGridEventNames.AsyncTransactionsFlushed);
        AddEventIfHandled(eventNames, ModelUpdated, AgGridEventNames.ModelUpdated);
        AddEventIfHandled(eventNames, CellContextMenu, AgGridEventNames.CellContextMenu);
        AddEventIfHandled(eventNames, CellFocused, AgGridEventNames.CellFocused);
        AddEventIfHandled(eventNames, RowValueChanged, AgGridEventNames.RowValueChanged);
        AddEventIfHandled(eventNames, CellEditingStarted, AgGridEventNames.CellEditingStarted);
        AddEventIfHandled(eventNames, CellEditingStopped, AgGridEventNames.CellEditingStopped);
        AddEventIfHandled(eventNames, GridSizeChanged, AgGridEventNames.GridSizeChanged);
        AddEventIfHandled(eventNames, ViewportChanged, AgGridEventNames.ViewportChanged);
        AddEventIfHandled(eventNames, BodyScroll, AgGridEventNames.BodyScroll);
        AddEventIfHandled(eventNames, BodyScrollEnd, AgGridEventNames.BodyScrollEnd);
        AddEventIfHandled(eventNames, StateUpdated, AgGridEventNames.StateUpdated);
        AddEventIfHandled(eventNames, PasteStart, AgGridEventNames.PasteStart);
        AddEventIfHandled(eventNames, PasteEnd, AgGridEventNames.PasteEnd);
        return eventNames.Order(StringComparer.Ordinal).ToArray();
    }

    private static void AddEventIfHandled(
        HashSet<string> names,
        EventCallback<AgGridEvent<TData>> callback,
        AgGridEventName eventName)
    {
        if (callback.HasDelegate)
        {
            names.Add(eventName.Value);
        }
    }

    private async Task InvokeTypedEventAsync(string eventName, AgGridEvent<TData> gridEvent)
    {
        if (eventName == AgGridEventNames.CellClicked.Value) await CellClicked.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.CellValueChanged.Value) await CellValueChanged.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.CellDoubleClicked.Value) await CellDoubleClicked.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.RowClicked.Value) await RowClicked.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.RowDoubleClicked.Value) await RowDoubleClicked.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.RowSelected.Value) await RowSelected.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.SelectionChanged.Value) await SelectionChanged.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.FilterChanged.Value) await FilterChanged.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.SortChanged.Value) await SortChanged.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.PaginationChanged.Value) await PaginationChanged.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.FirstDataRendered.Value) await FirstDataRendered.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.RowDataUpdated.Value) await RowDataUpdated.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.GridPreDestroyed.Value) await GridPreDestroyed.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.ColumnEverythingChanged.Value) await ColumnEverythingChanged.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.ColumnMoved.Value) await ColumnMoved.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.ColumnVisible.Value) await ColumnVisible.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.ColumnPinned.Value) await ColumnPinned.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.ColumnResized.Value) await ColumnResized.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.DisplayedColumnsChanged.Value) await DisplayedColumnsChanged.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.VirtualColumnsChanged.Value) await VirtualColumnsChanged.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.AsyncTransactionsFlushed.Value) await AsyncTransactionsFlushed.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.ModelUpdated.Value) await ModelUpdated.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.CellContextMenu.Value) await CellContextMenu.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.CellFocused.Value) await CellFocused.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.RowValueChanged.Value) await RowValueChanged.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.CellEditingStarted.Value) await CellEditingStarted.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.CellEditingStopped.Value) await CellEditingStopped.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.GridSizeChanged.Value) await GridSizeChanged.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.ViewportChanged.Value) await ViewportChanged.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.BodyScroll.Value) await BodyScroll.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.BodyScrollEnd.Value) await BodyScrollEnd.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.StateUpdated.Value) await StateUpdated.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.PasteStart.Value) await PasteStart.InvokeAsync(gridEvent);
        else if (eventName == AgGridEventNames.PasteEnd.Value) await PasteEnd.InvokeAsync(gridEvent);
    }

    private void CaptureParameterState(string[] eventNames)
    {
        Dictionary<string, object?> optionsWithoutRows = Options.ToDictionary();
        _optionsSignature = JsonSerializer.Serialize(optionsWithoutRows, SerializerOptions);
        _columnsSignature = JsonSerializer.Serialize(BuildColumnDefinitions(), SerializerOptions);
        _eventsSignature = string.Join('|', eventNames);
        _rowDataReference = RowData;
        _dataSourceReference = InfiniteDataSource;
        _javaScriptModuleReference = JavaScriptModule;
        _stripedRows = Options.StripedRows;
    }

    private void ValidateParameters()
    {
        ArgumentNullException.ThrowIfNull(Options);
        ArgumentNullException.ThrowIfNull(ColumnDefinitions);
        if (InfiniteDataSource is not null && RowData is not null)
        {
            throw new InvalidOperationException(
                $"{nameof(RowData)} and {nameof(InfiniteDataSource)} cannot be configured together.");
        }

        RejectComponentOwnedOption("columnDefs");
        RejectComponentOwnedOption("rowData");
        if (InfiniteDataSource is not null)
        {
            RejectComponentOwnedOption("datasource");
            RejectComponentOwnedOption("rowModelType");
        }
    }

    private void RejectComponentOwnedOption(string name)
    {
        if (Options.AdditionalOptions.ContainsKey(name))
        {
            throw new InvalidOperationException(
                $"{nameof(AgGridOptions)}.{nameof(AgGridOptions.AdditionalOptions)} cannot configure '{name}' because the component owns that option.");
        }
    }

    private AgGridApi<TData> EnsureApi()
    {
        ThrowIfDisposed();
        return Api ?? throw new InvalidOperationException("AG Grid is not ready yet.");
    }

    private async Task DestroyGridAsync()
    {
        _isReady = false;
        CancelDataSourceRequests();
        Api?.MarkDisposed();
        Api = null;

        if (_controller is not null)
        {
            try
            {
                await _controller.InvokeVoidAsync("destroy");
                await _controller.DisposeAsync();
            }
            catch (Exception exception) when (_disposed && IsDisposedJavaScriptException(exception))
            {
            }
            finally
            {
                _controller = null;
            }
        }
    }

    private async Task RecreatePreservingStateAsync()
    {
        JsonElement? state = null;
        if (Api is not null)
        {
            state = await Api.GetStateAsync();
        }
        await DestroyGridAsync();
        await InitializeAsync(state);
    }

    private void CancelDataSourceRequests()
    {
        foreach (CancellationTokenSource request in _dataSourceRequests.Values)
        {
            try
            {
                request.Cancel();
            }
            catch (ObjectDisposedException)
            {
                // A completing datasource request won the disposal race.
            }
        }
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    private static bool IsDisposedJavaScriptException(Exception exception) =>
        exception is JSDisconnectedException or JSException ||
        exception is InvalidOperationException invalidOperation &&
        invalidOperation.Message.Contains("JS object instance", StringComparison.OrdinalIgnoreCase);
}
