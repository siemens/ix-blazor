// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

/**
 * Validates input parameters for event listener operations
 * @param {Object} caller - Blazor component reference
 * @param {string} elementId - DOM element ID
 * @param {string} eventName - Event name to listen for
 * @param {string} functionName - Blazor method name to invoke
 * @throws {Error} When validation fails
 */
function validateEventListenerInputs(caller, elementId, eventName, functionName) {
  if (!caller) {
    throw new Error('Caller reference is required for event listener setup');
  }
  
  if (!elementId || typeof elementId !== 'string') {
    throw new Error('Valid element ID is required for event listener setup');
  }
  
  if (!eventName || typeof eventName !== 'string') {
    throw new Error('Valid event name is required for event listener setup');
  }
  
  if (!functionName || typeof functionName !== 'string') {
    throw new Error('Valid function name is required for event listener setup');
  }
  
  if (typeof caller.invokeMethodAsync !== 'function') {
    throw new Error('Caller must have invokeMethodAsync method available');
  }
}

/**
 * Gets DOM element by ID with enhanced error handling
 * @param {string} elementId - Element ID
 * @param {string} eventName - Event name for error context
 * @returns {HTMLElement} The DOM element
 * @throws {Error} When element is not found
 */
function getElementForEventListener(elementId, eventName) {
  const element = document.getElementById(elementId);
  
  if (!element) {
    throw new Error(`Element with ID '${elementId}' not found. Cannot listen to event '${eventName}'`);
  }
  
  return element;
}

/**
 * Creates a safe event handler that catches and logs any errors during method invocation
 * @param {Object} caller - Blazor component reference
 * @param {string} functionName - Blazor method name to invoke
 * @param {string} elementId - Element ID for error context
 * @param {string} eventName - Event name for error context
 * @returns {Function} Event handler function
 */
function createSafeEventHandler(caller, functionName, elementId, eventName) {
  return async (event) => {
    try {
      await caller.invokeMethodAsync(functionName, event.detail);
    } catch (error) {
      console.error(
        `Failed to invoke Blazor method '${functionName}' for event '${eventName}' on element '${elementId}':`,
        error
      );
    }
  };
}

/**
 * Sets up an event listener on a DOM element that invokes a Blazor method when triggered
 * @param {Object} caller - Blazor component reference with invokeMethodAsync capability
 * @param {string} elementId - ID of the DOM element to attach the event listener to
 * @param {string} eventName - Name of the event to listen for (e.g., 'click', 'change')
 * @param {string} functionName - Name of the Blazor method to invoke when the event occurs
 * @returns {boolean} True if event listener was successfully attached, false otherwise
 */
export function listenEvent(caller, elementId, eventName, functionName) {
  try {
    // Validate all input parameters
    validateEventListenerInputs(caller, elementId, eventName, functionName);
    
    // Get the target element
    const element = getElementForEventListener(elementId, eventName);
    
    // Create a safe event handler
    const eventHandler = createSafeEventHandler(caller, functionName, elementId, eventName);
    
    // Attach the event listener
    element.addEventListener(eventName, eventHandler);
    
    return true;
  } catch (error) {
    console.error(
      `Failed to set up event listener for '${eventName}' on element '${elementId}':`,
      error.message
    );
    return false;
  }
}

/**
 * Removes an event listener from a DOM element
 * @param {string} elementId - ID of the DOM element to remove the event listener from
 * @param {string} eventName - Name of the event to stop listening for
 * @param {Function} handlerFunction - The specific handler function to remove
 * @returns {boolean} True if event listener was successfully removed, false otherwise
 */
export function removeEventListener(elementId, eventName, handlerFunction) {
  try {
    if (!elementId || typeof elementId !== 'string') {
      throw new Error('Valid element ID is required for removing event listener');
    }
    
    if (!eventName || typeof eventName !== 'string') {
      throw new Error('Valid event name is required for removing event listener');
    }
    
    if (!handlerFunction || typeof handlerFunction !== 'function') {
      throw new Error('Valid handler function is required for removing event listener');
    }
    
    const element = document.getElementById(elementId);
    
    if (!element) {
      throw new Error(`Element with ID '${elementId}' not found. Cannot remove event listener for '${eventName}'`);
    }
    
    element.removeEventListener(eventName, handlerFunction);
    
    return true;
  } catch (error) {
    console.error(
      `Failed to remove event listener for '${eventName}' on element '${elementId}':`,
      error.message
    );
    return false;
  }
}
