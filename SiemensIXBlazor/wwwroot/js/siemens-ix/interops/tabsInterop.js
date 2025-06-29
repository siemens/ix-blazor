// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

/**
 * Validates input parameters for tabs initialization
 * @param {string} id - Element ID
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When validation fails
 */
function validateTabsInitInputs(id, operation) {
  if (!id || typeof id !== 'string') {
    throw new Error(`Invalid ID provided for ${operation}`);
  }
}

/**
 * Validates input parameters for event subscription
 * @param {Object} caller - Blazor component reference
 * @param {string} id - Element ID
 * @param {string} eventName - Event name to listen for
 * @param {string} functionName - Blazor method name to invoke
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When validation fails
 */
function validateEventSubscriptionInputs(caller, id, eventName, functionName, operation) {
  if (!caller) {
    throw new Error(`Caller reference is required for ${operation}`);
  }
  
  if (!id || typeof id !== 'string') {
    throw new Error(`Invalid ID provided for ${operation}`);
  }
  
  if (!eventName || typeof eventName !== 'string') {
    throw new Error(`Valid event name is required for ${operation}`);
  }
  
  if (!functionName || typeof functionName !== 'string') {
    throw new Error(`Valid function name is required for ${operation}`);
  }
  
  if (typeof caller.invokeMethodAsync !== 'function') {
    throw new Error(`Caller must have invokeMethodAsync method for ${operation}`);
  }
}

/**
 * Gets DOM element by ID with error handling for tabs operations
 * @param {string} id - Element ID
 * @param {string} operation - Operation name for error reporting
 * @returns {HTMLElement} The DOM element
 * @throws {Error} When element is not found
 */
function getTabsElement(id, operation) {
  const element = document.getElementById(id);
  
  if (!element) {
    throw new Error(`Element with ID '${id}' not found for ${operation}`);
  }
  
  return element;
}

/**
 * Waits for custom elements to be defined with timeout
 * @param {string} tagName - Custom element tag name
 * @param {number} timeout - Timeout in milliseconds (default: 10000)
 * @returns {Promise<void>} Promise that resolves when element is defined
 * @throws {Error} When timeout is reached
 */
async function waitForCustomElement(tagName, timeout = 10000) {
  const timeoutPromise = new Promise((_, reject) => {
    setTimeout(() => {
      reject(new Error(`Timeout waiting for custom element '${tagName}' to be defined`));
    }, timeout);
  });
  
  const definedPromise = window.customElements.whenDefined(tagName);
  
  await Promise.race([definedPromise, timeoutPromise]);
}

/**
 * Finds and validates tab items within a container
 * @param {HTMLElement} tabsElement - Container element
 * @param {string} operation - Operation name for error reporting
 * @returns {NodeList} List of tab elements
 * @throws {Error} When no tabs are found
 */
function findTabItems(tabsElement, operation) {
  const tabs = tabsElement.querySelectorAll("ix-tab-item[data-tab-id]");
  
  if (tabs.length === 0) {
    throw new Error(`No tabs with data-tab-id attribute found for ${operation}`);
  }
  
  return tabs;
}

/**
 * Handles tab click events and manages content visibility
 * @param {HTMLElement} clickedTab - The clicked tab element
 * @param {HTMLElement} containerElement - The tabs container element
 */
function handleTabClick(clickedTab, containerElement) {
  try {
    const clickedTabId = clickedTab.dataset.tabId;
    
    if (!clickedTabId) {
      console.warn('Clicked tab has no data-tab-id attribute');
      return;
    }
    
    // Find content elements in the parent container
    const parentContainer = containerElement.parentElement;
    if (!parentContainer) {
      console.warn('Tabs container has no parent element');
      return;
    }
    
    const contentList = parentContainer.querySelectorAll("[data-tab-content]");
    
    if (contentList.length === 0) {
      console.warn('No tab content elements found with data-tab-content attribute');
      return;
    }
    
    // Update content visibility
    contentList.forEach((content) => {
      const shouldShow = content.dataset.tabContent === clickedTabId;
      content.classList.toggle("show", shouldShow);
    });
    
  } catch (error) {
    console.error('Error handling tab click:', error.message);
  }
}

/**
 * Registers click event listener for a single tab
 * @param {HTMLElement} tab - Tab element
 * @param {HTMLElement} containerElement - Container element
 */
function registerTabClickListener(tab, containerElement) {
  try {
    if (!tab || !containerElement) {
      throw new Error('Invalid tab or container element for click listener registration');
    }
    
    tab.addEventListener("click", () => handleTabClick(tab, containerElement));
    
  } catch (error) {
    console.error('Error registering tab click listener:', error.message);
  }
}

/**
 * Creates a safe event handler for tabs events
 * @param {Object} caller - Blazor component reference
 * @param {string} functionName - Blazor method name to invoke
 * @param {string} id - Element ID for error context
 * @param {string} eventName - Event name for error context
 * @returns {Function} Event handler function
 */
function createTabsEventHandler(caller, functionName, id, eventName) {
  return async (event) => {
    try {
      await caller.invokeMethodAsync(functionName, event.detail);
    } catch (error) {
      console.error(`Error invoking method '${functionName}' for event '${eventName}' on tabs element '${id}':`, error.message);
    }
  };
}

/**
 * Initializes tabs functionality by setting up click handlers for tab navigation
 * @param {string} id - ID of the tabs container element
 */
export const initializeTabs = async (id) => {
  const operation = 'initialize tabs';
  
  try {
    // Validate input parameters
    validateTabsInitInputs(id, operation);
    
    // Wait for custom elements to be defined with timeout
    await waitForCustomElement("ix-tabs");
    
    // Get the tabs container element
    const tabsElement = getTabsElement(id, operation);
    
    // Find and validate tab items
    const tabs = findTabItems(tabsElement, operation);
    
    // Register click listeners for each tab
    tabs.forEach(tab => registerTabClickListener(tab, tabsElement));
    
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
  }
};

/**
 * Subscribes to events on a tabs element and invokes Blazor callback methods
 * @param {Object} caller - Blazor component reference with invokeMethodAsync capability
 * @param {string} id - ID of the tabs element to subscribe to events on
 * @param {string} eventName - Name of the event to listen for
 * @param {string} functionName - Name of the Blazor method to invoke when the event occurs
 */
export const subscribeEvents = (caller, id, eventName, functionName) => {
  const operation = 'subscribe to tabs events';
  
  try {
    // Validate input parameters
    validateEventSubscriptionInputs(caller, id, eventName, functionName, operation);
    
    // Get the target element
    const element = getTabsElement(id, operation);
    
    // Create a safe event handler
    const eventHandler = createTabsEventHandler(caller, functionName, id, eventName);
    
    // Attach the event listener
    element.addEventListener(eventName, eventHandler);
    
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
  }
};
