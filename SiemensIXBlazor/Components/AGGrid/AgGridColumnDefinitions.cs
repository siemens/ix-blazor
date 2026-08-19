// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

namespace SiemensIXBlazor.Components.AGGrid;

public interface IAgGridColumnDefinition
{
    internal Dictionary<string, object?> ToDictionary();
}

public sealed class AgGridColumnDefinition : IAgGridColumnDefinition
{
    public string? Field { get; set; }
    public string? ColId { get; set; }
    public string? HeaderName { get; set; }
    public string? HeaderTooltip { get; set; }
    public object? Type { get; set; }
    public object? Sort { get; set; }
    public int? SortIndex { get; set; }
    public object? InitialSort { get; set; }
    public int? InitialSortIndex { get; set; }
    public object? InitialPinned { get; set; }
    public int? InitialWidth { get; set; }
    public int? InitialFlex { get; set; }
    public bool? UnSortIcon { get; set; }
    public bool? Sortable { get; set; }
    public object? Filter { get; set; }
    public bool? FloatingFilter { get; set; }
    public bool? Editable { get; set; }
    public bool? Resizable { get; set; }
    public bool? Hide { get; set; }
    public object? Pinned { get; set; }
    public object? LockPosition { get; set; }
    public int? Width { get; set; }
    public int? MinWidth { get; set; }
    public int? MaxWidth { get; set; }
    public int? Flex { get; set; }
    public bool? CheckboxSelection { get; set; }
    public bool? HeaderCheckboxSelection { get; set; }
    public bool? HeaderCheckboxSelectionFilteredOnly { get; set; }
    public bool? HeaderCheckboxSelectionCurrentPageOnly { get; set; }
    public bool? ShowDisabledCheckboxes { get; set; }
    public bool? RowDrag { get; set; }
    public string? TooltipField { get; set; }
    public string? CellRenderer { get; set; }
    public object? CellRendererParams { get; set; }
    public string? CellEditor { get; set; }
    public object? CellEditorParams { get; set; }
    public bool? SingleClickEdit { get; set; }
    public bool? CellEditorPopup { get; set; }
    public bool? UseValueFormatterForExport { get; set; }
    public bool? UseValueParserForImport { get; set; }
    public bool? AutoHeight { get; set; }
    public bool? WrapText { get; set; }
    public bool? EnableCellChangeFlash { get; set; }
    public bool? SuppressSizeToFit { get; set; }
    public bool? SuppressAutoSize { get; set; }
    public bool? InitialHide { get; set; }
    public bool? LockVisible { get; set; }
    public bool? SuppressMovable { get; set; }
    public bool? LockPinned { get; set; }
    public bool? WrapHeaderText { get; set; }
    public bool? AutoHeaderHeight { get; set; }
    public bool? SuppressHeaderContextMenu { get; set; }
    public bool? SuppressHeaderMenuButton { get; set; }
    public bool? SuppressHeaderFilterButton { get; set; }
    public string? DefaultAggFunc { get; set; }
    public IReadOnlyList<string>? AllowedAggFuncs { get; set; }
    public Dictionary<string, object?> AdditionalOptions { get; } = [];

    Dictionary<string, object?> IAgGridColumnDefinition.ToDictionary()
    {
        Dictionary<string, object?> values = [];
        AgGridSerialization.Add(values, "field", Field);
        AgGridSerialization.Add(values, "colId", ColId);
        AgGridSerialization.Add(values, "headerName", HeaderName);
        AgGridSerialization.Add(values, "headerTooltip", HeaderTooltip);
        AgGridSerialization.Add(values, "type", Type);
        AgGridSerialization.Add(values, "sort", Sort);
        AgGridSerialization.Add(values, "sortIndex", SortIndex);
        AgGridSerialization.Add(values, "initialSort", InitialSort);
        AgGridSerialization.Add(values, "initialSortIndex", InitialSortIndex);
        AgGridSerialization.Add(values, "initialPinned", InitialPinned);
        AgGridSerialization.Add(values, "initialWidth", InitialWidth);
        AgGridSerialization.Add(values, "initialFlex", InitialFlex);
        AgGridSerialization.Add(values, "unSortIcon", UnSortIcon);
        AgGridSerialization.Add(values, "sortable", Sortable);
        AgGridSerialization.Add(values, "filter", Filter);
        AgGridSerialization.Add(values, "floatingFilter", FloatingFilter);
        AgGridSerialization.Add(values, "editable", Editable);
        AgGridSerialization.Add(values, "resizable", Resizable);
        AgGridSerialization.Add(values, "hide", Hide);
        AgGridSerialization.Add(values, "pinned", Pinned);
        AgGridSerialization.Add(values, "lockPosition", LockPosition);
        AgGridSerialization.Add(values, "width", Width);
        AgGridSerialization.Add(values, "minWidth", MinWidth);
        AgGridSerialization.Add(values, "maxWidth", MaxWidth);
        AgGridSerialization.Add(values, "flex", Flex);
        AgGridSerialization.Add(values, "checkboxSelection", CheckboxSelection);
        AgGridSerialization.Add(values, "headerCheckboxSelection", HeaderCheckboxSelection);
        AgGridSerialization.Add(values, "headerCheckboxSelectionFilteredOnly", HeaderCheckboxSelectionFilteredOnly);
        AgGridSerialization.Add(values, "headerCheckboxSelectionCurrentPageOnly", HeaderCheckboxSelectionCurrentPageOnly);
        AgGridSerialization.Add(values, "showDisabledCheckboxes", ShowDisabledCheckboxes);
        AgGridSerialization.Add(values, "rowDrag", RowDrag);
        AgGridSerialization.Add(values, "tooltipField", TooltipField);
        AgGridSerialization.Add(values, "cellRenderer", CellRenderer);
        AgGridSerialization.Add(values, "cellRendererParams", CellRendererParams);
        AgGridSerialization.Add(values, "cellEditor", CellEditor);
        AgGridSerialization.Add(values, "cellEditorParams", CellEditorParams);
        AgGridSerialization.Add(values, "singleClickEdit", SingleClickEdit);
        AgGridSerialization.Add(values, "cellEditorPopup", CellEditorPopup);
        AgGridSerialization.Add(values, "useValueFormatterForExport", UseValueFormatterForExport);
        AgGridSerialization.Add(values, "useValueParserForImport", UseValueParserForImport);
        AgGridSerialization.Add(values, "autoHeight", AutoHeight);
        AgGridSerialization.Add(values, "wrapText", WrapText);
        AgGridSerialization.Add(values, "enableCellChangeFlash", EnableCellChangeFlash);
        AgGridSerialization.Add(values, "suppressSizeToFit", SuppressSizeToFit);
        AgGridSerialization.Add(values, "suppressAutoSize", SuppressAutoSize);
        AgGridSerialization.Add(values, "initialHide", InitialHide);
        AgGridSerialization.Add(values, "lockVisible", LockVisible);
        AgGridSerialization.Add(values, "suppressMovable", SuppressMovable);
        AgGridSerialization.Add(values, "lockPinned", LockPinned);
        AgGridSerialization.Add(values, "wrapHeaderText", WrapHeaderText);
        AgGridSerialization.Add(values, "autoHeaderHeight", AutoHeaderHeight);
        AgGridSerialization.Add(values, "suppressHeaderContextMenu", SuppressHeaderContextMenu);
        AgGridSerialization.Add(values, "suppressHeaderMenuButton", SuppressHeaderMenuButton);
        AgGridSerialization.Add(values, "suppressHeaderFilterButton", SuppressHeaderFilterButton);
        AgGridSerialization.Add(values, "defaultAggFunc", DefaultAggFunc);
        AgGridSerialization.Add(values, "allowedAggFuncs", AllowedAggFuncs);
        AgGridSerialization.MergeAdditional(values, AdditionalOptions, nameof(AgGridColumnDefinition));
        return values;
    }
}

public sealed class AgGridColumnGroupDefinition : IAgGridColumnDefinition
{
    public string? GroupId { get; set; }
    public string? HeaderName { get; set; }
    public bool? OpenByDefault { get; set; }
    public bool? MarryChildren { get; set; }
    public bool? SuppressStickyLabel { get; set; }
    public IReadOnlyList<IAgGridColumnDefinition> Children { get; set; } = [];
    public Dictionary<string, object?> AdditionalOptions { get; } = [];

    Dictionary<string, object?> IAgGridColumnDefinition.ToDictionary()
    {
        Dictionary<string, object?> values = [];
        AgGridSerialization.Add(values, "groupId", GroupId);
        AgGridSerialization.Add(values, "headerName", HeaderName);
        AgGridSerialization.Add(values, "openByDefault", OpenByDefault);
        AgGridSerialization.Add(values, "marryChildren", MarryChildren);
        AgGridSerialization.Add(values, "suppressStickyLabel", SuppressStickyLabel);
        values["children"] = Children.Select(child => child.ToDictionary()).ToArray();
        AgGridSerialization.MergeAdditional(values, AdditionalOptions, nameof(AgGridColumnGroupDefinition));
        return values;
    }
}
