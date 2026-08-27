// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using System.Text.Json;
using SiemensIXBlazor.Components.AGGrid;

namespace SiemensIXBlazor.Tests.AGGrid;

public sealed class AgGridModelTests
{
    [Fact]
    public void PublicModelsPreserveTypedValuesAndWireNames()
    {
        var row = new AgGridRowNode<string>("row-1", 2, "top", "Motor", true, false);
        var cell = new AgGridCellPosition(2, "status", "top");
        var vertical = new AgGridVerticalPixelRange(10, 100);
        var horizontal = new AgGridHorizontalPixelRange(20, 200);
        var state = new AgGridColumnState(
            "status", false, 120, 2, "asc", "alphanumeric", 0, "sum", true, 1,
            "left", true, 2);
        var defaults = new AgGridColumnStateDefaults(
            false, 100, 1, "desc", "numeric", 1, "avg", false, 0,
            "right", false, 0);
        var apply = new AgGridApplyColumnStateParameters([state], true, defaults);
        var edit = new AgGridStartEditingCellParameters(2, "status", "top", "M");
        var limit = new AgGridColumnWidthLimit("status", 80, 240);
        var strategy = new AgGridAutoSizeStrategy(
            "fitGridWidth", 80, 300, 120, true, ["status"], [limit], true);

        Assert.Equal("Motor", row.Data);
        Assert.True(row.Selected);
        Assert.False(row.Expanded);
        Assert.Equal("status", cell.ColumnId);
        Assert.Equal(100, vertical.Bottom);
        Assert.Equal(200, horizontal.Right);
        Assert.Equal("asc", state.Sort);
        Assert.Equal("avg", defaults.AggFunc);
        Assert.True(apply.ApplyOrder);
        Assert.Equal("M", edit.Key);
        Assert.Equal(240, limit.MaxWidth);
        Assert.Equal(["status"], strategy.ColumnIds);

        var json = JsonSerializer.Serialize(new { row, cell, state, strategy });
        Assert.Contains("\"rowIndex\":2", json);
        Assert.Contains("\"columnId\":\"status\"", json);
        Assert.Contains("\"colId\":\"status\"", json);
        Assert.Contains("\"columnLimits\"", json);
    }

    [Fact]
    public void InfiniteDataSourceAndTransactionModelsPreserveRowsAndRequests()
    {
        using var sortModel = JsonDocument.Parse("[{\"colId\":\"status\",\"sort\":\"asc\"}]");
        using var filterModel = JsonDocument.Parse("{\"status\":{\"filterType\":\"text\"}}");
        var request = new AgGridGetRowsRequest("request-1", 10, 20, sortModel.RootElement.Clone(), filterModel.RootElement.Clone());
        var block = new AgGridDataBlock<string>(["row-1", "row-2"], 100);
        var transaction = new AgGridTransaction<string>(["new"], ["changed"], ["removed"], 3);
        var result = new AgGridTransactionResult<string>(["added"], ["updated"], ["deleted"]);

        Assert.Equal("request-1", request.RequestId);
        Assert.Equal(10, request.StartRow);
        Assert.Equal(20, request.EndRow);
        Assert.Equal(["row-1", "row-2"], block.Rows);
        Assert.Equal(100, block.RowCount);
        Assert.Equal(["new"], transaction.Add);
        Assert.Equal(["changed"], transaction.Update);
        Assert.Equal(["removed"], transaction.Remove);
        Assert.Equal(3, transaction.AddIndex);
        Assert.Equal(["deleted"], result.Remove);
    }

    [Fact]
    public void OptionalGridModelsSupportDefaultAndNullableCombinations()
    {
        var transaction = new AgGridTransaction<string>();
        var block = new AgGridDataBlock<string>([]);
        var node = new AgGridRowNode<string>(null, null, null, null, null, null);
        var state = new AgGridColumnState("status");
        var strategy = new AgGridAutoSizeStrategy("fitGridWidth");

        Assert.Null(transaction.Add);
        Assert.Null(transaction.Update);
        Assert.Null(transaction.Remove);
        Assert.Null(transaction.AddIndex);
        Assert.Null(block.RowCount);
        Assert.Null(node.Id);
        Assert.Null(node.Data);
        Assert.Null(state.Hide);
        Assert.Null(state.Pinned);
        Assert.Null(strategy.ColumnIds);

        var json = JsonSerializer.Serialize(new { transaction, block, node, state, strategy });
        Assert.Contains("\"rowCount\":null", json);
        Assert.Contains("\"colId\":\"status\"", json);
    }
}
