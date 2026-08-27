// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

import assert from "node:assert/strict";
import test from "node:test";
import { Window } from "happy-dom";
import * as applicationInterop from "../../wwwroot/js/siemens-ix/interops/applicationInterop.js";
import * as baseInterop from "../../wwwroot/js/siemens-ix/interops/baseJsInterop.js";
import * as categoryFilterInterop from "../../wwwroot/js/siemens-ix/interops/categoryFilterInterop.js";
import * as checkboxInterop from "../../wwwroot/js/siemens-ix/interops/checkboxInterop.js";
import * as dropdownInterop from "../../wwwroot/js/siemens-ix/interops/dropdownInterop.js";
import * as elementUtils from "../../wwwroot/js/siemens-ix/interops/elementUtils.js";
import * as fileUploadInterop from "../../wwwroot/js/siemens-ix/interops/fileUploadInterop.js";
import * as menuInterop from "../../wwwroot/js/siemens-ix/interops/menuInterop.js";
import * as sliderInterop from "../../wwwroot/js/siemens-ix/interops/sliderInterop.js";
import * as toastInterop from "../../wwwroot/js/siemens-ix/interops/toastInterop.js";
import * as treeInterop from "../../wwwroot/js/siemens-ix/interops/treeInterop.js";

function useDocument() {
  const window = new Window();
  globalThis.document = window.document;
  globalThis.FileReader = window.FileReader;
  return window;
}

test("application interop assigns configuration and breakpoints", () => {
  const window = useDocument();
  const element = window.document.createElement("ix-application");
  element.id = "application-element";
  window.document.body.appendChild(element);

  applicationInterop.setApplicationConfig(element.id, '{"appName":"Demo"}');
  applicationInterop.setBreakpoints(element.id, { sm: 480, md: 768 });

  assert.deepEqual(element.appSwitchConfig, { appName: "Demo" });
  assert.deepEqual(element.breakpoints, { sm: 480, md: 768 });
});

test("checkbox interop assigns parsed values and tolerates missing elements", () => {
  const window = useDocument();
  const element = window.document.createElement("ix-checkbox");
  element.id = "checkbox-element";
  window.document.body.appendChild(element);

  checkboxInterop.setChecked(element.id, "true");
  checkboxInterop.setIndeterminate(element.id, "true");
  checkboxInterop.setValue(element.id, '"selected"');

  assert.equal(element.checked, true);
  assert.equal(element.indeterminate, true);
  assert.equal(element.value, "selected");
  assert.doesNotThrow(() => checkboxInterop.setChecked("missing-checkbox", "false"));
});

test("menu interop forwards optional and required arguments", async () => {
  const window = useDocument();
  const element = window.document.createElement("ix-menu");
  element.id = "menu-element";
  const calls = [];
  element.toggleMenu = (...args) => calls.push(["toggleMenu", ...args]);
  element.toggleMapExpand = (...args) => calls.push(["toggleMapExpand", ...args]);
  element.toggleSettings = (...args) => calls.push(["toggleSettings", ...args]);
  element.toggleAbout = (...args) => calls.push(["toggleAbout", ...args]);
  window.document.body.appendChild(element);

  await menuInterop.toggleMenu(element.id);
  await menuInterop.toggleMenu(element.id, true);
  await menuInterop.toggleMenu(element.id, false);
  await menuInterop.toggleMapExpand(element.id, true);
  await menuInterop.toggleSettings(element.id, false);
  await menuInterop.toggleAbout(element.id, true);

  assert.deepEqual(calls, [
    ["toggleMenu"],
    ["toggleMenu", true],
    ["toggleMenu", false],
    ["toggleMapExpand", true],
    ["toggleSettings", false],
    ["toggleAbout", true],
  ]);
  await assert.rejects(
    () => menuInterop.toggleMenu("missing-menu"),
    /Element with ID missing-menu not found/
  );
});

test("file upload interop serializes files and supports programmatic selection", async () => {
  const window = useDocument();
  const element = window.document.createElement("ix-upload");
  element.id = "upload-element";
  let assignedFiles = null;
  element.setFilesToUpload = (files) => {
    assignedFiles = files;
  };
  window.document.body.appendChild(element);
  const calls = [];
  const caller = {
    invokeMethodAsync: (...args) => {
      calls.push(args);
      return Promise.resolve();
    },
  };
  const listenerId = fileUploadInterop.fileUploadEventHandler(
    caller,
    element.id,
    "filesChanged",
    "FilesChanged"
  );
  const file = new window.File(["hello"], "hello.txt", { type: "text/plain" });
  element.dispatchEvent(new window.CustomEvent("filesChanged", { detail: [file] }));
  await new Promise((resolve) => setTimeout(resolve, 50));

  fileUploadInterop.setFilesToUpload(element.id, [file]);
  fileUploadInterop.removeFileUploadEventHandler(listenerId);

  assert.equal(listenerId, "file-upload-listener-1");
  assert.deepEqual(assignedFiles, [file]);
  assert.deepEqual(calls, [["FilesChanged", [{
    name: "hello.txt",
    size: 5,
    type: "text/plain",
    data: "aGVsbG8=",
  }]]]);
});

test("file upload interop handles missing elements and empty selections", async () => {
  const window = useDocument();
  const caller = { invokeMethodAsync: () => Promise.resolve() };
  const originalConsoleError = console.error;
  console.error = () => {};

  try {
    assert.equal(
      fileUploadInterop.fileUploadEventHandler(
        caller,
        "missing-upload",
        "filesChanged",
        "FilesChanged"
      ),
      undefined
    );
    assert.doesNotThrow(() => fileUploadInterop.removeFileUploadEventHandler(null));
    assert.throws(
      () => fileUploadInterop.setFilesToUpload("missing-upload", []),
      /Upload element with id missing-upload not found/
    );

    const element = window.document.createElement("ix-upload");
    element.id = "empty-upload";
    window.document.body.appendChild(element);
    const calls = [];
    const listenerId = fileUploadInterop.fileUploadEventHandler(
      { invokeMethodAsync: (...args) => calls.push(args) },
      element.id,
      "filesChanged",
      "FilesChanged"
    );
    element.dispatchEvent(new window.CustomEvent("filesChanged", { detail: [] }));
    await new Promise((resolve) => setTimeout(resolve, 0));

    assert.ok(listenerId);
    assert.deepEqual(calls, []);
  } finally {
    console.error = originalConsoleError;
  }
});

test("file upload interop rejects a missing setFilesToUpload method", () => {
  const window = useDocument();
  const element = window.document.createElement("ix-upload");
  element.id = "unsupported-upload";
  window.document.body.appendChild(element);

  assert.throws(
    () => fileUploadInterop.setFilesToUpload(element.id, []),
    /Upload element with id unsupported-upload not found/
  );
});

test("file upload interop does not invoke Blazor when FileReader fails", async () => {
  const window = useDocument();
  const element = window.document.createElement("ix-upload");
  element.id = "failed-upload";
  window.document.body.appendChild(element);
  const calls = [];
  const caller = { invokeMethodAsync: (...args) => calls.push(args) };
  const listenerId = fileUploadInterop.fileUploadEventHandler(
    caller,
    element.id,
    "filesChanged",
    "FilesChanged"
  );
  const originalFileReader = globalThis.FileReader;
  const originalConsoleError = console.error;
  globalThis.FileReader = class {
    readAsDataURL() {
      queueMicrotask(() => this.onerror(new Error("read failed")));
    }
  };

  try {
    console.error = () => {};
    const file = new window.File(["content"], "failed.txt", { type: "text/plain" });
    element.dispatchEvent(new window.CustomEvent("filesChanged", { detail: [file] }));
    await new Promise((resolve) => setTimeout(resolve, 0));
    assert.deepEqual(calls, []);
  } finally {
    console.error = originalConsoleError;
    globalThis.FileReader = originalFileReader;
    fileUploadInterop.removeFileUploadEventHandler(listenerId);
  }
});

test("element lookup distinguishes optional and required element access", () => {
  useDocument();

  assert.equal(elementUtils.getElement("missing-element"), null);
  assert.throws(
    () => elementUtils.getElementOrThrow("missing-element"),
    /Element with ID missing-element not found/
  );
});

test("base interop registers and removes only its listener", () => {
  const window = useDocument();
  const element = window.document.createElement("div");
  element.id = "base-listener-element";
  window.document.body.appendChild(element);
  const calls = [];
  const caller = {
    invokeMethodAsync: (...args) => {
      calls.push(args);
      return Promise.resolve();
    },
  };

  const listenerId = baseInterop.listenEvent(caller, element.id, "change", "Changed");
  element.dispatchEvent(new window.CustomEvent("change", { detail: { value: 1 } }));
  baseInterop.removeEventListener(listenerId);
  element.dispatchEvent(new window.CustomEvent("change", { detail: { value: 2 } }));

  assert.equal(listenerId, "base-listener-1");
  assert.deepEqual(calls, [["Changed", { value: 1 }]]);
});

test("dropdown interop replaces, updates, and detaches listeners", async () => {
  const window = useDocument();
  const element = window.document.createElement("div");
  element.id = "dropdown-element";
  let positionUpdates = 0;
  element.updatePosition = () => {
    positionUpdates++;
  };
  window.document.body.appendChild(element);
  const calls = [];
  const caller = {
    invokeMethodAsync: (...args) => {
      calls.push(args);
      return Promise.resolve();
    },
  };

  dropdownInterop.attachEvent(caller, element.id, "showChange", "ShowChanged");
  element.dispatchEvent(new window.CustomEvent("showChange", { detail: true }));
  await dropdownInterop.updatePosition(element.id);
  dropdownInterop.detachEvents(element.id);
  element.dispatchEvent(new window.CustomEvent("showChange", { detail: false }));

  assert.deepEqual(calls, [["ShowChanged", true]]);
  assert.equal(positionUpdates, 1);
});

test("dropdown interop rejects missing elements", async () => {
  await assert.rejects(
    () => dropdownInterop.updatePosition("missing-dropdown"),
    /Element with ID missing-dropdown not found/
  );
  assert.throws(
    () => dropdownInterop.attachEvent({}, "missing-dropdown", "showChange", "ShowChanged"),
    /Element with ID missing-dropdown not found/
  );
});

test("slider interop sets markers and removes value listeners", () => {
  const window = useDocument();
  const element = window.document.createElement("div");
  element.id = "slider-element";
  window.document.body.appendChild(element);
  const calls = [];
  const caller = {
    invokeMethodAsync: (...args) => {
      calls.push(args);
      return Promise.resolve();
    },
  };

  sliderInterop.setMarker(element.id, [0, 50, 100]);
  const listenerId = sliderInterop.listenEvent(caller, element.id, "valueChange", "ValueChanged");
  element.dispatchEvent(new window.CustomEvent("valueChange", { detail: 50 }));
  sliderInterop.removeEventListener(listenerId);
  element.dispatchEvent(new window.CustomEvent("valueChange", { detail: 75 }));

  assert.deepEqual(element.marker, [0, 50, 100]);
  assert.deepEqual(calls, [["ValueChanged", 50]]);
});

test("tree interop projects removed node ids and cleans up", () => {
  const window = useDocument();
  const element = window.document.createElement("div");
  element.id = "tree-element";
  const first = window.document.createElement("div");
  first.setAttribute("data-tree-node-id", "first");
  const second = window.document.createElement("div");
  second.setAttribute("data-tree-node-id", "second");
  window.document.body.appendChild(element);
  const calls = [];
  const caller = {
    invokeMethodAsync: (...args) => {
      calls.push(args);
      return Promise.resolve();
    },
  };

  const listenerId = treeInterop.listenNodeRemoved(caller, element.id);
  element.dispatchEvent(new window.CustomEvent("nodeRemoved", { detail: [first, second, {}] }));
  treeInterop.removeNodeRemovedListener(listenerId);
  element.dispatchEvent(new window.CustomEvent("nodeRemoved", { detail: [first] }));

  assert.deepEqual(calls, [["NodeRemoved", { nodeIds: ["first", "second"] }]]);
});

test("category filter interop replaces listeners and prevents clear by default", async () => {
  const window = useDocument();
  const element = window.document.createElement("div");
  element.id = "category-filter-element";
  window.document.body.appendChild(element);
  const calls = [];
  const caller = {
    invokeMethodAsync: (...args) => {
      calls.push(args);
      return Promise.resolve(false);
    },
  };

  categoryFilterInterop.initialize(caller, element.id);
  const clearEvent = new window.Event("filterCleared", { cancelable: true });
  element.dispatchEvent(clearEvent);
  await new Promise((resolve) => setTimeout(resolve, 0));
  categoryFilterInterop.initialize(caller, element.id);
  element.dispatchEvent(new window.CustomEvent("categoryChanged", { detail: "new" }));

  assert.equal(clearEvent.defaultPrevented, true);
  assert.deepEqual(calls, [["FilterCleared"], ["CategoryChanged", "new"]]);
  categoryFilterInterop.dispose(element.id);
});

test("toast close listeners remain isolated between instances", () => {
  const window = useDocument();
  const first = window.document.createElement("div");
  const second = window.document.createElement("div");
  first.id = "toast-first";
  second.id = "toast-second";
  window.document.body.append(first, second);
  const calls = [];
  const firstCaller = { invokeMethodAsync: (...args) => calls.push(["first", ...args]) };
  const secondCaller = { invokeMethodAsync: (...args) => calls.push(["second", ...args]) };

  toastInterop.listenCloseToast(firstCaller, first.id);
  toastInterop.listenCloseToast(secondCaller, second.id);
  first.dispatchEvent(new window.Event("closeToast"));
  second.dispatchEvent(new window.Event("closeToast"));
  toastInterop.removeCloseToast(first.id);
  first.dispatchEvent(new window.Event("closeToast"));
  second.dispatchEvent(new window.Event("closeToast"));
  toastInterop.removeCloseToast(second.id);

  assert.deepEqual(calls, [
    ["first", "CloseToast"],
    ["second", "CloseToast"],
    ["second", "CloseToast"],
  ]);
});
