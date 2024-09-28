// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

export const initializeTabs = async (id) => {
  // Ensure that the custom element 'ix-tabs' is defined before proceeding
  await window.customElements.whenDefined("ix-tabs");

  const tabsElement = document.getElementById(id);
  if (!tabsElement) {
    console.error(`Element with ID ${id} not found.`);
    return;
  }

  const tabs = tabsElement.querySelectorAll("ix-tab-item[data-tab-id]");
  if (tabs.length === 0) {
    console.warn(`No tabs found within element with ID ${id}.`);
    return;
  }

  // Register tab click listeners
  tabs.forEach(registerTabClickListener);

  function registerTabClickListener(tab) {
    tab.addEventListener("click", () => handleTabClick(tab, tabsElement));
  }

  function handleTabClick(clickedTab, containerElement) {
    const contentList =
      containerElement.parentElement.querySelectorAll("[data-tab-content]");
    contentList.forEach((content) => {
      content.classList.toggle(
        "show",
        content.dataset.tabContent === clickedTab.dataset.tabId
      );
    });
  }
};

export const subscribeEvents = (caller, id, eventName, functionName) => {
  const element = document.getElementById(id);
  if (!element) {
    console.error(`Element with ID ${id} not found.`);
    return;
  }

  element.addEventListener(eventName, (e) => {
    if (caller && typeof caller.invokeMethodAsync === "function") {
      caller.invokeMethodAsync(functionName, e.detail).catch((error) => {
        console.error(`Error invoking method '${functionName}':`, error);
      });
    } else {
      console.error("Invalid caller or missing invokeMethodAsync function.");
    }
  });
};
