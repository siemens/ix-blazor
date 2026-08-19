// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

namespace SiemensIXBlazor.Components.AGGrid;

public sealed class AgGridOptions
{
    public AgGridSelectionOptions? RowSelection { get; set; }
    public bool? Pagination { get; set; }
    public int? PaginationPageSize { get; set; }
    public bool? PaginationAutoPageSize { get; set; }
    public object? PaginationPageSizeSelector { get; set; }
    public bool? PaginateChildRows { get; set; }
    public bool? SuppressPaginationPanel { get; set; }
    public bool? Loading { get; set; }
    /// <summary>
    /// Enables alternating row backgrounds in the official iX AG Grid theme.
    /// </summary>
    public bool StripedRows { get; set; }
    public bool? AnimateRows { get; set; }
    public bool? RowDragManaged { get; set; }
    public bool? SuppressContextMenu { get; set; }
    public bool? PreventDefaultOnContextMenu { get; set; }
    public bool? AllowContextMenuWithControlKey { get; set; }
    public bool? SuppressMenuHide { get; set; }
    public bool? EnableBrowserTooltips { get; set; }
    public int? TooltipSwitchShowDelay { get; set; }
    public int? TooltipHideDelay { get; set; }
    public bool? TooltipMouseTrack { get; set; }
    public bool? TooltipInteraction { get; set; }
    public bool? SuppressCellFocus { get; set; }
    public bool? SuppressRowHoverHighlight { get; set; }
    public bool? ColumnHoverHighlight { get; set; }
    public bool? SuppressRowTransform { get; set; }
    public bool? SuppressRowVirtualisation { get; set; }
    public bool? SuppressColumnVirtualisation { get; set; }
    public bool? EnableCellTextSelection { get; set; }
    public bool? EnsureDomOrder { get; set; }
    public bool? EnableRtl { get; set; }
    public bool? MaintainColumnOrder { get; set; }
    public bool? SuppressMovableColumns { get; set; }
    public bool? SuppressColumnMoveAnimation { get; set; }
    public bool? SuppressAutoSize { get; set; }
    public int? AutoSizePadding { get; set; }
    public bool? SkipHeaderOnAutoSize { get; set; }
    public bool? AnimateColumnResizing { get; set; }
    public bool? ReadOnlyEdit { get; set; }
    public bool? SingleClickEdit { get; set; }
    public bool? SuppressClickEdit { get; set; }
    public bool? StopEditingWhenCellsLoseFocus { get; set; }
    public bool? EnterNavigatesVertically { get; set; }
    public bool? EnterNavigatesVerticallyAfterEdit { get; set; }
    public bool? EnableCellEditingOnBackspace { get; set; }
    public bool? UndoRedoCellEditing { get; set; }
    public int? UndoRedoCellEditingLimit { get; set; }
    public bool? CacheQuickFilter { get; set; }
    public bool? IncludeHiddenColumnsInQuickFilter { get; set; }
    public bool? CopyHeadersToClipboard { get; set; }
    public bool? CopyGroupHeadersToClipboard { get; set; }
    public string? ClipboardDelimiter { get; set; }
    public bool? SuppressCopyRowsToClipboard { get; set; }
    public bool? SuppressClipboardPaste { get; set; }
    public bool? SuppressCopySingleCellRanges { get; set; }
    public bool? SuppressLastEmptyLineOnPaste { get; set; }
    public int? RowHeight { get; set; }
    public int? HeaderHeight { get; set; }
    public int? GroupHeaderHeight { get; set; }
    public int? FloatingFiltersHeight { get; set; }
    public int? PivotHeaderHeight { get; set; }
    public int? PivotGroupHeaderHeight { get; set; }
    public int? TabIndex { get; set; }
    public int? RowBuffer { get; set; }
    public bool? SuppressFocusAfterRefresh { get; set; }
    public bool? SuppressChangeDetection { get; set; }
    public AgGridAutoSizeStrategy? AutoSizeStrategy { get; set; }
    public AgGridColumnDefinition? DefaultColumnDefinition { get; set; }
    public int? TooltipShowDelay { get; set; }
    public string? DomLayout { get; set; }
    public string? QuickFilterText { get; set; }
    public string? OverlayLoadingTemplate { get; set; }
    public string? OverlayNoRowsTemplate { get; set; }
    public Dictionary<string, string>? LocaleText { get; set; }
    public Dictionary<string, object?> AdditionalOptions { get; } = [];

    internal Dictionary<string, object?> ToDictionary()
    {
        Dictionary<string, object?> values = [];
        AgGridSerialization.Add(values, "rowSelection", RowSelection?.ToDictionary());
        AgGridSerialization.Add(values, "pagination", Pagination);
        AgGridSerialization.Add(values, "paginationPageSize", PaginationPageSize);
        AgGridSerialization.Add(values, "paginationAutoPageSize", PaginationAutoPageSize);
        AgGridSerialization.Add(values, "paginationPageSizeSelector", PaginationPageSizeSelector);
        AgGridSerialization.Add(values, "paginateChildRows", PaginateChildRows);
        AgGridSerialization.Add(values, "suppressPaginationPanel", SuppressPaginationPanel);
        AgGridSerialization.Add(values, "loading", Loading);
        AgGridSerialization.Add(values, "animateRows", AnimateRows);
        AgGridSerialization.Add(values, "rowDragManaged", RowDragManaged);
        AgGridSerialization.Add(values, "suppressContextMenu", SuppressContextMenu);
        AgGridSerialization.Add(values, "preventDefaultOnContextMenu", PreventDefaultOnContextMenu);
        AgGridSerialization.Add(values, "allowContextMenuWithControlKey", AllowContextMenuWithControlKey);
        AgGridSerialization.Add(values, "suppressMenuHide", SuppressMenuHide);
        AgGridSerialization.Add(values, "enableBrowserTooltips", EnableBrowserTooltips);
        AgGridSerialization.Add(values, "tooltipSwitchShowDelay", TooltipSwitchShowDelay);
        AgGridSerialization.Add(values, "tooltipHideDelay", TooltipHideDelay);
        AgGridSerialization.Add(values, "tooltipMouseTrack", TooltipMouseTrack);
        AgGridSerialization.Add(values, "tooltipInteraction", TooltipInteraction);
        AgGridSerialization.Add(values, "suppressCellFocus", SuppressCellFocus);
        AgGridSerialization.Add(values, "suppressRowHoverHighlight", SuppressRowHoverHighlight);
        AgGridSerialization.Add(values, "columnHoverHighlight", ColumnHoverHighlight);
        AgGridSerialization.Add(values, "suppressRowTransform", SuppressRowTransform);
        AgGridSerialization.Add(values, "suppressRowVirtualisation", SuppressRowVirtualisation);
        AgGridSerialization.Add(values, "suppressColumnVirtualisation", SuppressColumnVirtualisation);
        AgGridSerialization.Add(values, "enableCellTextSelection", EnableCellTextSelection);
        AgGridSerialization.Add(values, "ensureDomOrder", EnsureDomOrder);
        AgGridSerialization.Add(values, "enableRtl", EnableRtl);
        AgGridSerialization.Add(values, "maintainColumnOrder", MaintainColumnOrder);
        AgGridSerialization.Add(values, "suppressMovableColumns", SuppressMovableColumns);
        AgGridSerialization.Add(values, "suppressColumnMoveAnimation", SuppressColumnMoveAnimation);
        AgGridSerialization.Add(values, "suppressAutoSize", SuppressAutoSize);
        AgGridSerialization.Add(values, "autoSizePadding", AutoSizePadding);
        AgGridSerialization.Add(values, "skipHeaderOnAutoSize", SkipHeaderOnAutoSize);
        AgGridSerialization.Add(values, "animateColumnResizing", AnimateColumnResizing);
        AgGridSerialization.Add(values, "readOnlyEdit", ReadOnlyEdit);
        AgGridSerialization.Add(values, "singleClickEdit", SingleClickEdit);
        AgGridSerialization.Add(values, "suppressClickEdit", SuppressClickEdit);
        AgGridSerialization.Add(values, "stopEditingWhenCellsLoseFocus", StopEditingWhenCellsLoseFocus);
        AgGridSerialization.Add(values, "enterNavigatesVertically", EnterNavigatesVertically);
        AgGridSerialization.Add(values, "enterNavigatesVerticallyAfterEdit", EnterNavigatesVerticallyAfterEdit);
        AgGridSerialization.Add(values, "enableCellEditingOnBackspace", EnableCellEditingOnBackspace);
        AgGridSerialization.Add(values, "undoRedoCellEditing", UndoRedoCellEditing);
        AgGridSerialization.Add(values, "undoRedoCellEditingLimit", UndoRedoCellEditingLimit);
        AgGridSerialization.Add(values, "cacheQuickFilter", CacheQuickFilter);
        AgGridSerialization.Add(values, "includeHiddenColumnsInQuickFilter", IncludeHiddenColumnsInQuickFilter);
        AgGridSerialization.Add(values, "copyHeadersToClipboard", CopyHeadersToClipboard);
        AgGridSerialization.Add(values, "copyGroupHeadersToClipboard", CopyGroupHeadersToClipboard);
        AgGridSerialization.Add(values, "clipboardDelimiter", ClipboardDelimiter);
        AgGridSerialization.Add(values, "suppressCopyRowsToClipboard", SuppressCopyRowsToClipboard);
        AgGridSerialization.Add(values, "suppressClipboardPaste", SuppressClipboardPaste);
        AgGridSerialization.Add(values, "suppressCopySingleCellRanges", SuppressCopySingleCellRanges);
        AgGridSerialization.Add(values, "suppressLastEmptyLineOnPaste", SuppressLastEmptyLineOnPaste);
        AgGridSerialization.Add(values, "rowHeight", RowHeight);
        AgGridSerialization.Add(values, "headerHeight", HeaderHeight);
        AgGridSerialization.Add(values, "groupHeaderHeight", GroupHeaderHeight);
        AgGridSerialization.Add(values, "floatingFiltersHeight", FloatingFiltersHeight);
        AgGridSerialization.Add(values, "pivotHeaderHeight", PivotHeaderHeight);
        AgGridSerialization.Add(values, "pivotGroupHeaderHeight", PivotGroupHeaderHeight);
        AgGridSerialization.Add(values, "tabIndex", TabIndex);
        AgGridSerialization.Add(values, "rowBuffer", RowBuffer);
        AgGridSerialization.Add(values, "suppressFocusAfterRefresh", SuppressFocusAfterRefresh);
        AgGridSerialization.Add(values, "suppressChangeDetection", SuppressChangeDetection);
        AgGridSerialization.Add(values, "autoSizeStrategy", AutoSizeStrategy);
        AgGridSerialization.Add(
            values,
            "defaultColDef",
            DefaultColumnDefinition is null
                ? null
                : ((IAgGridColumnDefinition)DefaultColumnDefinition).ToDictionary());
        AgGridSerialization.Add(values, "tooltipShowDelay", TooltipShowDelay);
        AgGridSerialization.Add(values, "domLayout", DomLayout);
        AgGridSerialization.Add(values, "quickFilterText", QuickFilterText);
        AgGridSerialization.Add(values, "overlayLoadingTemplate", OverlayLoadingTemplate);
        AgGridSerialization.Add(values, "overlayNoRowsTemplate", OverlayNoRowsTemplate);
        AgGridSerialization.Add(values, "localeText", LocaleText);
        AgGridSerialization.MergeAdditional(values, AdditionalOptions, nameof(AgGridOptions));
        return values;
    }
}

public sealed class AgGridSelectionOptions
{
    public string Mode { get; set; } = "multiRow";
    public bool? Checkboxes { get; set; }
    public bool? HeaderCheckbox { get; set; }
    public bool? EnableClickSelection { get; set; }
    public string? SelectAll { get; set; }

    internal Dictionary<string, object?> ToDictionary()
    {
        Dictionary<string, object?> values = new() { ["mode"] = Mode };
        AgGridSerialization.Add(values, "checkboxes", Checkboxes);
        AgGridSerialization.Add(values, "headerCheckbox", HeaderCheckbox);
        AgGridSerialization.Add(values, "enableClickSelection", EnableClickSelection);
        AgGridSerialization.Add(values, "selectAll", SelectAll);
        return values;
    }
}

internal static class AgGridSerialization
{
    internal static void Add(Dictionary<string, object?> target, string name, object? value)
    {
        if (value is not null)
        {
            target[name] = value;
        }
    }

    internal static void MergeAdditional(
        Dictionary<string, object?> target,
        IReadOnlyDictionary<string, object?> additional,
        string owner)
    {
        foreach ((string name, object? value) in additional)
        {
            if (!target.TryAdd(name, value))
            {
                throw new InvalidOperationException(
                    $"{owner}.{nameof(AgGridOptions.AdditionalOptions)} contains '{name}', which is already configured by a typed property.");
            }
        }
    }
}
