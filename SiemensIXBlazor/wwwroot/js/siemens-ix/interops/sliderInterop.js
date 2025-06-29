// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

/**
 * Validates input parameters for slider marker operations
 * @param {string} id - Element ID
 * @param {Array} markerArray - Array of marker data
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When validation fails
 */
function validateMarkerInputs(id, markerArray, operation) {
  if (!id || typeof id !== 'string') {
    throw new Error(`Invalid ID provided for ${operation}`);
  }
  
  if (!Array.isArray(markerArray)) {
    throw new Error(`Marker data must be an array for ${operation}`);
  }
}

/**
 * Validates input parameters for slider event listener operations
 * @param {Object} dotNetRef - .NET reference object
 * @param {string} id - Element ID
 * @param {string} eventName - Event name to listen for
 * @param {string} callbackName - Callback method name
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When validation fails
 */
function validateEventInputs(dotNetRef, id, eventName, callbackName, operation) {
  if (!dotNetRef) {
    throw new Error(`DotNet reference is required for ${operation}`);
  }
  
  if (!id || typeof id !== 'string') {
    throw new Error(`Invalid ID provided for ${operation}`);
  }
  
  if (!eventName || typeof eventName !== 'string') {
    throw new Error(`Valid event name is required for ${operation}`);
  }
  
  if (!callbackName || typeof callbackName !== 'string') {
    throw new Error(`Valid callback name is required for ${operation}`);
  }
  
  if (typeof dotNetRef.invokeMethodAsync !== 'function') {
    throw new Error(`DotNet reference must have invokeMethodAsync method for ${operation}`);
  }
}

/**
 * Gets DOM element by ID with error handling for slider operations
 * @param {string} id - Element ID
 * @param {string} operation - Operation name for error reporting
 * @returns {HTMLElement} The DOM element
 * @throws {Error} When element is not found
 */
function getSliderElement(id, operation) {
  const element = document.getElementById(id);
  
  if (!element) {
    throw new Error(`Element with ID '${id}' not found for ${operation}`);
  }
  
  return element;
}

/**
 * Creates a safe event handler for slider events
 * @param {Object} dotNetRef - .NET reference object
 * @param {string} callbackName - Callback method name
 * @param {string} id - Element ID for error context
 * @param {string} eventName - Event name for error context
 * @returns {Function} Event handler function
 */
function createSliderEventHandler(dotNetRef, callbackName, id, eventName) {
  return async (event) => {
    try {
      // Only handle specific callback types for now
      if (callbackName === "ValueChanged") {
        await dotNetRef.invokeMethodAsync(callbackName, event.detail);
      }
    } catch (error) {
      console.error(
        `Failed to invoke callback '${callbackName}' for event '${eventName}' on slider element '${id}':`,
        error.message
      );
    }
  };
}

/**
 * Sets marker array for a slider element
 * @param {string} id - ID of the slider element
 * @param {Array} markerArray - Array of marker data to set on the slider
 */
export function setMarker(id, markerArray) {
  const operation = 'set slider markers';
  
  try {
    // Validate input parameters
    validateMarkerInputs(id, markerArray, operation);
    
    // Get the target element
    const element = getSliderElement(id, operation);
    
    // Set the marker array on the element
    element.marker = markerArray;
    
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
  }
}

/**
 * Sets up an event listener on a slider element that invokes a .NET callback method
 * @param {Object} dotNetRef - .NET reference object with invokeMethodAsync capability
 * @param {string} id - ID of the slider element to attach the event listener to
 * @param {string} eventName - Name of the event to listen for
 * @param {string} callbackName - Name of the .NET callback method to invoke
 */
export function listenEvent(dotNetRef, id, eventName, callbackName) {
  const operation = 'set up slider event listener';
  
  try {
    // Validate input parameters
    validateEventInputs(dotNetRef, id, eventName, callbackName, operation);
    
    // Get the target element
    const element = getSliderElement(id, operation);
    
    // Create a safe event handler
    const eventHandler = createSliderEventHandler(dotNetRef, callbackName, id, eventName);
    
    // Attach the event listener
    element.addEventListener(eventName, eventHandler);
    
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
  }
}
