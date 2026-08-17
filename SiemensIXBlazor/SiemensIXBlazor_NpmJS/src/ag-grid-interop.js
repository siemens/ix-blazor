// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

import * as agGrid from "ag-grid-community";
import { getIxTheme } from "@siemens/ix-aggrid";

const {
  AllCommunityModule,
  ModuleRegistry,
  createGrid,
} = agGrid;

ModuleRegistry.registerModules([AllCommunityModule]);

const ixTheme = getIxTheme(agGrid);
const communityModuleNames = Object.freeze(
  (AllCommunityModule.dependsOn ?? [])
    .map((module) => module.moduleName)
    .sort()
);
const excludedEventProperties = new Set([
  "api",
  "columnApi",
  "context",
  "event",
  "node",
  "column",
  "columnGroup",
  "columns",
  "selectedNodes",
  "sourceEvent",
]);

function resolveModuleUrl(moduleUrl) {
  const resolved = new URL(moduleUrl, document.baseURI);
  if (resolved.protocol !== "http:" && resolved.protocol !== "https:") {
    throw new Error(`Unsupported AG Grid extension module protocol '${resolved.protocol}'.`);
  }
  return resolved.href;
}

function toSerializable(value, seen = new WeakSet(), depth = 0) {
  if (value === null || value === undefined) return value ?? null;
  if (["string", "number", "boolean"].includes(typeof value)) return value;
  if (typeof value === "bigint") return value.toString();
  if (
    typeof value === "function" ||
    (typeof globalThis.Node !== "undefined" && value instanceof globalThis.Node)
  ) {
    return undefined;
  }
  if (value instanceof Date) return value.toISOString();
  if (depth >= 12 || typeof value !== "object") return undefined;
  if (seen.has(value)) return undefined;

  seen.add(value);
  if (Array.isArray(value)) {
    return value
      .map((item) => toSerializable(item, seen, depth + 1))
      .filter((item) => item !== undefined);
  }

  const projected = {};
  for (const [key, item] of Object.entries(value)) {
    if (excludedEventProperties.has(key)) continue;
    const serialized = toSerializable(item, seen, depth + 1);
    if (serialized !== undefined) projected[key] = serialized;
  }
  return projected;
}

function createInfiniteDataSource(dotNetReference, instanceId) {
  return {
    getRows: async (params) => {
      const requestId = globalThis.crypto?.randomUUID?.() ?? `${Date.now()}-${Math.random()}`;
      try {
        const block = await dotNetReference.invokeMethodAsync(
          "GetInfiniteRowsAsync",
          {
            requestId,
            startRow: params.startRow,
            endRow: params.endRow,
            sortModel: params.sortModel ?? [],
            filterModel: params.filterModel ?? {},
          }
        );
        params.successCallback(block.rows ?? [], block.rowCount ?? undefined);
      } catch (error) {
        console.error(
          `[SiemensIXBlazor.AGGrid:${instanceId}] Infinite datasource request failed.`,
          error
        );
        params.failCallback();
      }
    },
  };
}

function projectTransactionResult(result) {
  if (!result) return null;
  const data = (nodes) => (nodes ?? []).map((node) => node.data);
  return {
    add: data(result.add),
    update: data(result.update),
    remove: data(result.remove),
  };
}

function projectRowNode(node) {
  if (!node) return null;
  return {
    id: node.id ?? null,
    rowIndex: node.rowIndex ?? null,
    rowPinned: node.rowPinned ?? null,
    data: toSerializable(node.data),
    selected: typeof node.isSelected === "function" ? node.isSelected() : null,
    expanded: node.expanded ?? null,
  };
}

function projectCellPosition(position) {
  if (!position) return null;
  return {
    rowIndex: position.rowIndex,
    columnId: position.column?.getColId?.() ?? null,
    rowPinned: position.rowPinned ?? null,
  };
}

/**
 * @param {{
 *   create?: (params: any) => HTMLElement,
 *   refresh?: (element: HTMLElement, params: any) => boolean | undefined,
 *   destroy?: (element: HTMLElement | null, params: any) => void,
 * } | undefined} definition
 */
function createCellRendererComponent({ create, refresh, destroy } = {}) {
  if (typeof create !== "function") {
    throw new TypeError("createCellRendererComponent requires a create function.");
  }

  return class AgGridCellRenderer {
    constructor() {
      this.gui = null;
      this.params = null;
      this.destroyed = false;
    }

    init(params) {
      if (this.destroyed) return;
      const gui = create(params);
      if (
        typeof globalThis.HTMLElement === "undefined" ||
        !(gui instanceof globalThis.HTMLElement)
      ) {
        throw new TypeError("A cell renderer create function must return an HTMLElement.");
      }
      this.params = params;
      this.gui = gui;
    }

    getGui() {
      if (!this.gui) {
        throw new Error("The AG Grid cell renderer has not been initialized.");
      }
      return this.gui;
    }

    refresh(params) {
      if (this.destroyed || !this.gui) return false;
      this.params = params;
      if (typeof refresh !== "function") return false;
      return refresh(this.gui, params) !== false;
    }

    destroy() {
      if (this.destroyed) return;
      this.destroyed = true;
      const gui = this.gui;
      const params = this.params;
      try {
        destroy?.(gui, params);
      } finally {
        this.gui = null;
        this.params = null;
      }
    }
  };
}

function registerCellRenderer(options, name, component) {
  if (!name || typeof name !== "string") {
    throw new TypeError("registerCellRenderer requires a renderer name.");
  }
  if (typeof component !== "function") {
    throw new TypeError("registerCellRenderer requires a renderer component.");
  }

  return {
    ...(options ?? {}),
    components: {
      ...(options?.components ?? {}),
      [name]: component,
    },
  };
}

class AgGridController {
  constructor(api, dotNetReference, instanceId, extensionModule) {
    this.api = api;
    this.dotNetReference = dotNetReference;
    this.instanceId = instanceId;
    this.extensionModule = extensionModule;
    this.eventListeners = [];
    this.destroyed = false;
  }

  addEventSubscriptions(eventNames) {
    for (const eventName of new Set(eventNames ?? [])) {
      const listener = async (event) => {
        if (this.destroyed) return;
        try {
          let payload;
          if (typeof this.extensionModule?.projectAgGridEvent === "function") {
            payload = await this.extensionModule.projectAgGridEvent(eventName, event);
          }
          payload ??= toSerializable(event);
          if (event?.column?.getColId && payload && !payload.columnId) {
            payload.columnId = event.column.getColId();
          }
          await this.dotNetReference.invokeMethodAsync(
            "DispatchEventAsync",
            eventName,
            payload ?? {}
          );
        } catch (error) {
          if (!this.destroyed) {
            console.error(
              `[SiemensIXBlazor.AGGrid:${this.instanceId}] Failed to dispatch '${eventName}'.`,
              error
            );
          }
        }
      };
      this.api.addEventListener(eventName, listener);
      this.eventListeners.push([eventName, listener]);
    }
  }

  updateOptions(options) {
    this.ensureActive();
    this.api.updateGridOptions(options ?? {});
  }

  setRowData(rowData) {
    this.ensureActive();
    this.api.setGridOption("rowData", rowData ?? []);
  }

  setColumnDefinitions(columnDefs) {
    this.ensureActive();
    this.api.setGridOption("columnDefs", columnDefs ?? []);
  }

  getSelectedRows() {
    this.ensureActive();
    return this.api.getSelectedRows();
  }

  getSelectedNodes() {
    this.ensureActive();
    return this.api.getSelectedNodes().map(projectRowNode);
  }

  getDisplayedRowCount() {
    this.ensureActive();
    return this.api.getDisplayedRowCount();
  }

  getDisplayedRowAtIndex(index) {
    this.ensureActive();
    return projectRowNode(this.api.getDisplayedRowAtIndex(index));
  }

  getRowNode(id) {
    this.ensureActive();
    return projectRowNode(this.api.getRowNode(id));
  }

  getRenderedNodes() {
    this.ensureActive();
    return this.api.getRenderedNodes().map(projectRowNode);
  }

  getAllDisplayedColumnIds() {
    this.ensureActive();
    return this.api.getAllDisplayedColumns().map((column) => column.getColId());
  }

  getFocusedCell() {
    this.ensureActive();
    return projectCellPosition(this.api.getFocusedCell());
  }

  ensureNodeVisible(id, position) {
    this.ensureActive();
    const node = this.api.getRowNode(id);
    if (!node) return false;
    this.api.ensureNodeVisible(node, position ?? null);
    return true;
  }

  setRowNodeExpanded(id, expanded, expandParents, forceSync) {
    this.ensureActive();
    const node = this.api.getRowNode(id);
    if (!node) return false;
    this.api.setRowNodeExpanded(node, Boolean(expanded), expandParents ?? undefined, forceSync ?? undefined);
    return true;
  }

  setLoading(loading) {
    this.ensureActive();
    this.api.setGridOption("loading", Boolean(loading));
  }

  removeSelectedRows() {
    this.ensureActive();
    return projectTransactionResult(
      this.api.applyTransaction({ remove: this.api.getSelectedRows() })
    );
  }

  applyTransaction(transaction) {
    this.ensureActive();
    return projectTransactionResult(this.api.applyTransaction(transaction ?? {}));
  }

  async invoke(method, args) {
    this.ensureActive();
    const target = this.api[method];
    if (typeof target !== "function") {
      throw new Error(`AG Grid API method '${method}' does not exist.`);
    }
    const result = await target.apply(this.api, args ?? []);
    return toSerializable(
      method === "applyTransaction" ? projectTransactionResult(result) : result
    );
  }

  ensureActive() {
    if (this.destroyed) {
      throw new Error("The AG Grid instance has already been destroyed.");
    }
  }

  hideOverlay() {
    this.ensureActive();
    this.api.setGridOption("loading", false);
    this.api.hideOverlay();
  }

  async destroy() {
    if (this.destroyed) return;
    this.destroyed = true;

    for (const [eventName, listener] of this.eventListeners) {
      this.api.removeEventListener(eventName, listener);
    }
    this.eventListeners.length = 0;

    try {
      if (typeof this.extensionModule?.disposeAgGrid === "function") {
        await this.extensionModule.disposeAgGrid({
          api: this.api,
          instanceId: this.instanceId,
        });
      }
    } finally {
      this.api.destroy();
      this.dotNetReference = null;
      this.extensionModule = null;
    }
  }
}

export async function createAgGrid(
  element,
  options,
  dotNetReference,
  settings
) {
  if (!(element instanceof HTMLElement)) {
    throw new Error("AG Grid requires a rendered host element.");
  }

  const instanceId = settings?.instanceId ?? globalThis.crypto?.randomUUID?.() ?? `${Date.now()}`;
  let extensionModule = null;
  let configuredOptions = { ...(options ?? {}) };

  if (settings?.javaScriptModule) {
    const moduleUrl = resolveModuleUrl(settings.javaScriptModule);
    extensionModule = await import(/* webpackIgnore: true */ moduleUrl);
    if (typeof extensionModule.configureAgGrid !== "function") {
      throw new Error(
        `AG Grid extension module '${moduleUrl}' must export configureAgGrid.`
      );
    }
    const extensionOptions = await extensionModule.configureAgGrid({
      agGrid,
      ixTheme,
      options: configuredOptions,
      element,
      instanceId,
      createCellRendererComponent,
      registerCellRenderer,
    });
    if (extensionOptions !== undefined) {
      if (!extensionOptions || typeof extensionOptions !== "object") {
        throw new Error("configureAgGrid must return an options object or undefined.");
      }
      configuredOptions = extensionOptions;
    }
  }

  configuredOptions.theme ??= ixTheme;

  if (settings?.hasInfiniteDataSource) {
    configuredOptions.rowModelType = "infinite";
    configuredOptions.datasource = createInfiniteDataSource(
      dotNetReference,
      instanceId
    );
    delete configuredOptions.rowData;
  }

  const api = createGrid(element, configuredOptions);
  const controller = new AgGridController(
    api,
    dotNetReference,
    instanceId,
    extensionModule
  );
  try {
    controller.addEventSubscriptions(settings?.eventSubscriptions);
    return controller;
  } catch (error) {
    await controller.destroy();
    throw error;
  }
}

export const __testing = {
  AgGridController,
  communityModuleNames,
  createInfiniteDataSource,
  createCellRendererComponent,
  registerCellRenderer,
  projectTransactionResult,
  toSerializable,
};
