// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

import assert from "node:assert/strict";
import test from "node:test";
import { Window } from "happy-dom";
import { __testing, createAgGrid } from "../src/ag-grid-interop.js";

const {
  AgGridController,
  communityModuleNames,
  createCellRendererComponent,
  createInfiniteDataSource,
  projectTransactionResult,
  registerCellRenderer,
  toSerializable,
} = __testing;

test("wrapper registers the complete AG Grid Community feature module set", () => {
  assert.deepEqual(communityModuleNames, [
    "AlignedGrids",
    "BigIntFilter",
    "CellApi",
    "CellSpan",
    "CellStyle",
    "CheckboxEditor",
    "ClientSideRowModel",
    "ClientSideRowModelApi",
    "ColumnApi",
    "ColumnAutoSize",
    "ColumnHover",
    "CsvExport",
    "CustomEditor",
    "CustomFilter",
    "DateEditor",
    "DateFilter",
    "DragAndDrop",
    "EventApi",
    "ExternalFilter",
    "GridState",
    "HighlightChanges",
    "InfiniteRowModel",
    "LargeTextEditor",
    "Locale",
    "NumberEditor",
    "NumberFilter",
    "Pagination",
    "PinnedRow",
    "QuickFilter",
    "RenderApi",
    "RowApi",
    "RowAutoHeight",
    "RowDrag",
    "RowSelection",
    "RowStyle",
    "ScrollApi",
    "SelectEditor",
    "TextEditor",
    "TextFilter",
    "Tooltip",
    "UndoRedoEdit",
    "Validation",
    "ValueCache",
  ]);
});

test("toSerializable projects supported values and removes cycles", () => {
  const repeated = { id: 7 };
  const source = {
    name: "row",
    value: 42n,
    created: new Date("2026-01-02T03:04:05Z"),
    callback() {},
    api: { shouldNotLeak: true },
    first: repeated,
    second: repeated,
    selectedNodes: [{ data: repeated }],
  };
  source.self = source;

  assert.deepEqual(toSerializable(source), {
    name: "row",
    value: "42",
    created: "2026-01-02T03:04:05.000Z",
    first: { id: 7 },
  });
});

test("cell renderer adapter implements the AG Grid lifecycle", () => {
  const browser = new Window({ url: "https://localhost/" });
  installBrowserGlobals(browser);
  const calls = [];
  const Renderer = createCellRendererComponent({
    create(params) {
      calls.push(["create", params.value]);
      const element = document.createElement("span");
      element.textContent = `${params.prefix}${params.value}`;
      return element;
    },
    refresh(element, params) {
      calls.push(["refresh", params.value]);
      element.textContent = `${params.prefix}${params.value}`;
      return true;
    },
    destroy(element, params) {
      calls.push(["destroy", element.textContent, params.value]);
    },
  });
  const renderer = new Renderer();

  assert.throws(() => renderer.getGui(), /not been initialized/);
  renderer.init({ value: "Motor", prefix: "Status: " });
  const gui = renderer.getGui();
  assert.equal(gui.textContent, "Status: Motor");
  assert.equal(renderer.refresh({ value: "Pump", prefix: "Status: " }), true);
  assert.equal(gui.textContent, "Status: Pump");
  renderer.destroy();
  renderer.destroy();
  assert.equal(renderer.refresh({ value: "Valve", prefix: "Status: " }), false);
  assert.deepEqual(calls, [
    ["create", "Motor"],
    ["refresh", "Pump"],
    ["destroy", "Status: Pump", "Pump"],
  ]);

  browser.close();
});

test("cell renderer adapter returns false when no refresh implementation exists", () => {
  const browser = new Window({ url: "https://localhost/" });
  installBrowserGlobals(browser);
  const Renderer = createCellRendererComponent({
    create() {
      return document.createElement("span");
    },
  });
  const renderer = new Renderer();
  renderer.init({ value: "Motor" });
  assert.equal(renderer.refresh({ value: "Pump" }), false);
  browser.close();
});

test("cell renderer adapter validates renderer contracts", () => {
  assert.throws(
    () => createCellRendererComponent(),
    /requires a create function/
  );
  assert.throws(
    () => registerCellRenderer({}, "statusRenderer", {}),
    /requires a renderer component/
  );
  assert.throws(
    () => registerCellRenderer({}, "", class Renderer {}),
    /requires a renderer name/
  );

  const browser = new Window({ url: "https://localhost/" });
  installBrowserGlobals(browser);
  const Renderer = createCellRendererComponent({
    create() {
      return document.createTextNode("not an element");
    },
  });
  assert.throws(
    () => new Renderer().init({}),
    /must return an HTMLElement/
  );
  browser.close();
});

test("registerCellRenderer adds a named component without mutating options", () => {
  const existing = class ExistingRenderer {};
  const renderer = class StatusRenderer {};
  const options = {
    rowData: [{ name: "Motor" }],
    components: { existing },
  };

  const configured = registerCellRenderer(options, "statusRenderer", renderer);

  assert.notEqual(configured, options);
  assert.equal(configured.rowData, options.rowData);
  assert.deepEqual(configured.components, { existing, statusRenderer: renderer });
  assert.deepEqual(options.components, { existing });
});

test("controller forwards subscribed events without replacing grid handlers", async () => {
  const listeners = new Map();
  const calls = [];
  const api = createApi(listeners);
  const dotNetReference = {
    async invokeMethodAsync(...arguments_) {
      calls.push(arguments_);
    },
  };
  const controller = new AgGridController(api, dotNetReference, "grid-1", null);
  controller.addEventSubscriptions(["cellClicked", "cellClicked"]);

  await listeners.get("cellClicked")({
    rowIndex: 3,
    data: { id: 7 },
    api,
  });

  assert.equal(listeners.size, 1);
  assert.deepEqual(calls, [
    ["DispatchEventAsync", "cellClicked", { rowIndex: 3, data: { id: 7 } }],
  ]);
});

test("controller invokes the API, returns selected rows, and destroys once", async () => {
  const listeners = new Map();
  const api = createApi(listeners);
  let extensionDisposed = 0;
  const controller = new AgGridController(api, {}, "grid-2", {
    async disposeAgGrid() {
      extensionDisposed += 1;
    },
  });
  controller.addEventSubscriptions(["selectionChanged"]);

  controller.updateOptions({ pagination: true });
  controller.setLoading(true);
  assert.equal(api.loading, true);
  controller.hideOverlay();
  assert.equal(api.loading, false);
  assert.equal(api.hideOverlayCount, 1);
  controller.setRowData([{ id: 2 }]);
  controller.setColumnDefinitions([{ field: "id" }]);
  assert.deepEqual(controller.getSelectedRows(), [{ id: 1 }]);
  assert.deepEqual(controller.removeSelectedRows(), {
    add: [],
    update: [],
    remove: [{ id: 1 }],
  });
  assert.deepEqual(controller.applyTransaction({ add: [{ id: 3 }] }), {
    add: [{ id: 3 }],
    update: [],
    remove: [],
  });
  assert.deepEqual(await controller.invoke("echo", [{ id: 2 }]), { id: 2 });
  await assert.rejects(() => controller.invoke("missing", []), /does not exist/);
  assert.deepEqual(api.updatedOptions, { pagination: true });
  assert.deepEqual(api.rowData, [{ id: 2 }]);
  assert.deepEqual(api.columnDefs, [{ field: "id" }]);

  await controller.destroy();
  await controller.destroy();

  assert.equal(api.destroyCount, 1);
  assert.equal(extensionDisposed, 1);
  assert.equal(listeners.size, 0);
  assert.throws(() => controller.getSelectedRows(), /already been destroyed/);
});

test("transaction results contain row data rather than AG Grid RowNodes", () => {
  const internal = { circular: null };
  internal.circular = internal;
  assert.deepEqual(projectTransactionResult({
    add: [{ data: { id: 1 }, internal }],
    update: [{ data: { id: 2 }, internal }],
    remove: [{ data: { id: 3 }, internal }],
  }), {
    add: [{ id: 1 }],
    update: [{ id: 2 }],
    remove: [{ id: 3 }],
  });
});

test("controller projects row, focus, and visibility APIs for Blazor", () => {
  const listeners = new Map();
  const api = createApi(listeners);
  const controller = new AgGridController(api, {}, "grid-projection-api", null);

  assert.deepEqual(controller.getSelectedNodes(), [{
    id: "1",
    rowIndex: 0,
    rowPinned: null,
    data: { id: 1, name: "Motor" },
    selected: true,
    expanded: false,
  }]);
  assert.equal(controller.getDisplayedRowCount(), 1);
  assert.deepEqual(controller.getDisplayedRowAtIndex(0), {
    id: "1",
    rowIndex: 0,
    rowPinned: null,
    data: { id: 1, name: "Motor" },
    selected: true,
    expanded: false,
  });
  assert.deepEqual(controller.getRowNode("1"), {
    id: "1",
    rowIndex: 0,
    rowPinned: null,
    data: { id: 1, name: "Motor" },
    selected: true,
    expanded: false,
  });
  assert.deepEqual(controller.getFocusedCell(), {
    rowIndex: 0,
    columnId: "name",
    rowPinned: null,
  });
  assert.equal(controller.ensureNodeVisible("1", "middle"), true);
  assert.equal(controller.ensureNodeVisible("missing", "middle"), false);
  assert.deepEqual(api.visibleNode, { id: "1", position: "middle" });
});

test("controller projects rendered columns and row expansion helpers", () => {
  const listeners = new Map();
  const api = createApi(listeners);
  const controller = new AgGridController(api, {}, "grid-community-helpers", null);

  assert.deepEqual(controller.getRenderedNodes(), [
    {
      id: "1",
      rowIndex: 0,
      rowPinned: null,
      data: { id: 1, name: "Motor" },
      selected: true,
      expanded: false,
    },
  ]);
  assert.deepEqual(controller.getAllDisplayedColumnIds(), ["name", "status"]);
  assert.equal(controller.setRowNodeExpanded("1", true, true, true), true);
  assert.equal(controller.setRowNodeExpanded("missing", true), false);
  assert.deepEqual(api.expandedNode, {
    id: "1",
    expanded: true,
    expandParents: true,
    forceSync: true,
  });
});

test("extension event projection is used and receives the resolved column id", async () => {
  const listeners = new Map();
  const calls = [];
  const api = createApi(listeners);
  const controller = new AgGridController(
    api,
    { async invokeMethodAsync(...arguments_) { calls.push(arguments_); } },
    "grid-projection",
    {
      projectAgGridEvent(name, event) {
        return { projected: name, data: event.data };
      },
    }
  );
  controller.addEventSubscriptions(["cellClicked"]);

  await listeners.get("cellClicked")({
    data: { id: 7 },
    column: { getColId: () => "equipment" },
  });

  assert.deepEqual(calls, [[
    "DispatchEventAsync",
    "cellClicked",
    { projected: "cellClicked", data: { id: 7 }, columnId: "equipment" },
  ]]);
  await controller.destroy();
});

test("createAgGrid renders and destroys a real Community grid", async () => {
  const browser = new Window({ url: "https://localhost/" });
  installBrowserGlobals(browser);
  const host = document.createElement("div");
  host.style.width = "800px";
  host.style.height = "400px";
  document.body.append(host);
  const dotNetReference = { async invokeMethodAsync() {} };

  const controller = await createAgGrid(
    host,
    {
      columnDefs: [{ field: "name", sortable: true, filter: true }],
      rowData: [{ name: "Motor" }, { name: "Pump" }],
      rowDragManaged: true,
      tooltipShowDelay: 500,
      rowSelection: {
        mode: "multiRow",
        checkboxes: true,
        headerCheckbox: true,
        selectAll: "filtered",
      },
      autoSizeStrategy: { type: "fitGridWidth" },
    },
    dotNetReference,
    { instanceId: "integration-grid", eventSubscriptions: [] }
  );
  await browser.happyDOM.whenAsyncComplete();

  assert.ok(host.querySelector(".ag-root-wrapper"));
  assert.match(host.textContent, /Motor/);
  assert.match(host.textContent, /Pump/);

  await controller.invoke("setFilterModel", [{
    name: { filterType: "text", type: "equals", filter: "Pump" },
  }]);
  await browser.happyDOM.whenAsyncComplete();
  assert.doesNotMatch(host.textContent, /Motor/);
  assert.match(host.textContent, /Pump/);

  await controller.invoke("setFilterModel", [null]);
  await controller.invoke("applyTransaction", [{ add: [{ name: "Valve" }] }]);
  await browser.happyDOM.whenAsyncComplete();
  assert.match(host.textContent, /Valve/);

  await controller.destroy();
  assert.equal(host.querySelector(".ag-root-wrapper"), null);
  await browser.close();
});

test("createAgGrid hosts an ICellRendererComp adapter", async () => {
  const browser = new Window({ url: "https://localhost/" });
  installBrowserGlobals(browser);
  const host = document.createElement("div");
  host.style.width = "800px";
  host.style.height = "400px";
  document.body.append(host);
  let created = 0;
  let refreshed = 0;
  let destroyed = 0;
  const StatusRenderer = createCellRendererComponent({
    create(params) {
      created += 1;
      const element = document.createElement("span");
      element.textContent = `Status: ${params.value}`;
      return element;
    },
    refresh(element, params) {
      refreshed += 1;
      element.textContent = `Status: ${params.value}`;
      return true;
    },
    destroy() {
      destroyed += 1;
    },
  });

  const controller = await createAgGrid(
    host,
    {
      columnDefs: [{ field: "status", cellRenderer: StatusRenderer }],
      rowData: [{ status: "Normal" }, { status: "Warning" }],
    },
    { async invokeMethodAsync() {} },
    { instanceId: "cell-renderer-grid", eventSubscriptions: [] }
  );
  await browser.happyDOM.whenAsyncComplete();

  assert.ok(created > 0);
  assert.match(host.textContent, /Status: Normal/);
  await controller.invoke("refreshCells", [{ force: true }]);
  assert.ok(refreshed > 0);

  await controller.destroy();
  assert.ok(destroyed > 0);
  await browser.close();
});

test("real Community API supports pagination, state, export, and projections", async () => {
  const browser = new Window({ url: "https://localhost/" });
  installBrowserGlobals(browser);
  const host = document.createElement("div");
  host.style.width = "800px";
  host.style.height = "400px";
  document.body.append(host);

  const controller = await createAgGrid(
    host,
    {
      columnDefs: [
        { field: "name", sortable: true, filter: true },
        { field: "value", editable: true },
      ],
      rowData: [
        { name: "Motor", value: 1 },
        { name: "Pump", value: 2 },
        { name: "Valve", value: 3 },
      ],
      pagination: true,
      paginationPageSize: 2,
      paginationPageSizeSelector: [2],
    },
    { async invokeMethodAsync() {} },
    { instanceId: "community-api-grid", eventSubscriptions: [] }
  );
  await browser.happyDOM.whenAsyncComplete();

  assert.equal(await controller.invoke("paginationGetPageSize", []), 2);
  assert.equal(await controller.invoke("paginationGetTotalPages", []), 2);
  assert.equal(await controller.invoke("paginationGetRowCount", []), 3);
  assert.equal(await controller.invoke("paginationIsLastPageFound", []), true);
  assert.equal(await controller.invoke("getQuickFilter", []), null);
  await controller.invoke("setGridOption", ["quickFilterText", "Pump"]);
  assert.equal(await controller.invoke("getQuickFilter", []), "Pump");
  await controller.invoke("resetQuickFilter", []);
  assert.equal(await controller.invoke("getQuickFilter", []), "Pump");
  await controller.invoke("setGridOption", ["quickFilterText", null]);
  assert.equal(await controller.invoke("getQuickFilter", []), null);

  const csv = await controller.invoke("getDataAsCsv", []);
  assert.match(csv, /"Name","Value"/);
  assert.deepEqual(controller.getAllDisplayedColumnIds(), ["name", "value"]);
  assert.equal(controller.getRenderedNodes().length, 2);

  const state = await controller.invoke("getState", []);
  assert.equal(typeof state, "object");
  await controller.invoke("setState", [state]);
  await controller.invoke("refreshHeader", []);
  await controller.invoke("flashCells", [{ columns: ["name"] }]);

  await controller.destroy();
  await browser.close();
});

test("createAgGrid bridges the infinite row model to .NET", async () => {
  const browser = new Window({ url: "https://localhost/" });
  installBrowserGlobals(browser);
  const host = document.createElement("div");
  host.style.width = "800px";
  host.style.height = "400px";
  document.body.append(host);
  const requests = [];
  const dotNetReference = {
    async invokeMethodAsync(method, request) {
      assert.equal(method, "GetInfiniteRowsAsync");
      requests.push(request);
      return { rows: [{ name: "Infinite motor" }], rowCount: 1 };
    },
  };

  const controller = await createAgGrid(
    host,
    { columnDefs: [{ field: "name" }], cacheBlockSize: 50 },
    dotNetReference,
    {
      instanceId: "infinite-grid",
      hasInfiniteDataSource: true,
      eventSubscriptions: [],
    }
  );
  await browser.happyDOM.whenAsyncComplete();

  assert.equal(requests.length, 1);
  assert.equal(requests[0].startRow, 0);
  assert.equal(requests[0].endRow, 50);
  assert.match(host.textContent, /Infinite motor/);

  await controller.destroy();
  await browser.close();
});

test("infinite datasource invokes the failure callback when .NET rejects", async () => {
  const datasource = createInfiniteDataSource(
    { async invokeMethodAsync() { throw new Error("backend unavailable"); } },
    "failing-grid"
  );
  let failures = 0;
  const originalError = console.error;
  console.error = () => {};
  try {
    await datasource.getRows({
      startRow: 0,
      endRow: 100,
      successCallback() { assert.fail("success callback should not run"); },
      failCallback() { failures += 1; },
    });
  } finally {
    console.error = originalError;
  }

  assert.equal(failures, 1);
});

test("createAgGrid rejects unsupported extension module protocols", async () => {
  const browser = new Window({ url: "https://localhost/" });
  installBrowserGlobals(browser);
  const host = document.createElement("div");

  await assert.rejects(
    () => createAgGrid(
      host,
      { columnDefs: [] },
      { async invokeMethodAsync() {} },
      { javaScriptModule: "data:text/javascript,export default {}" }
    ),
    /Unsupported AG Grid extension module protocol/
  );

  await browser.close();
});

function createApi(listeners) {
  return {
    destroyCount: 0,
    hideOverlayCount: 0,
    updatedOptions: null,
    rowData: null,
    columnDefs: null,
    addEventListener(name, listener) {
      listeners.set(name, listener);
    },
    removeEventListener(name) {
      listeners.delete(name);
    },
    getSelectedRows() {
      return [{ id: 1 }];
    },
    getSelectedNodes() {
      return [createRowNode()];
    },
    getDisplayedRowCount() {
      return 1;
    },
    getDisplayedRowAtIndex() {
      return createRowNode();
    },
    getRowNode(id) {
      return id === "1" ? createRowNode() : undefined;
    },
    getRenderedNodes() {
      return [createRowNode()];
    },
    getAllDisplayedColumns() {
      return [{ getColId: () => "name" }, { getColId: () => "status" }];
    },
    getFocusedCell() {
      return {
        rowIndex: 0,
        rowPinned: null,
        column: { getColId: () => "name" },
      };
    },
    ensureNodeVisible(node, position) {
      this.visibleNode = { id: node.id, position };
    },
    setRowNodeExpanded(node, expanded, expandParents, forceSync) {
      this.expandedNode = { id: node.id, expanded, expandParents, forceSync };
    },
    updateGridOptions(options) {
      this.updatedOptions = options;
    },
    setGridOption(name, value) {
      this[name] = value;
    },
    applyTransaction(transaction) {
      return {
        add: (transaction.add ?? []).map((data) => ({ data })),
        update: (transaction.update ?? []).map((data) => ({ data })),
        remove: (transaction.remove ?? []).map((data) => ({ data })),
      };
    },
    echo(value) {
      return value;
    },
    destroy() {
      this.destroyCount += 1;
    },
    hideOverlay() {
      this.hideOverlayCount += 1;
    },
  };
}

function createRowNode() {
  return {
    id: "1",
    rowIndex: 0,
    rowPinned: null,
    data: { id: 1, name: "Motor" },
    expanded: false,
    isSelected: () => true,
  };
}

function installBrowserGlobals(browser) {
  const globals = {
    window: browser,
    document: browser.document,
    navigator: browser.navigator,
    HTMLElement: browser.HTMLElement,
    Node: browser.Node,
    MutationObserver: browser.MutationObserver,
    ResizeObserver: browser.ResizeObserver,
    getComputedStyle: browser.getComputedStyle.bind(browser),
    requestAnimationFrame: browser.requestAnimationFrame.bind(browser),
    cancelAnimationFrame: browser.cancelAnimationFrame.bind(browser),
  };
  for (const [name, value] of Object.entries(globals)) {
    Object.defineProperty(globalThis, name, {
      value,
      configurable: true,
      writable: true,
    });
  }
}
