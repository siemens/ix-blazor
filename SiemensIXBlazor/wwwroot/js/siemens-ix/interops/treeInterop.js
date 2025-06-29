// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

/**
 * Validates input parameters for tree operations
 * @param {string} id - Element ID
 * @param {string} data - JSON data to be processed
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When validation fails
 */
function validateTreeInputs(id, data, operation) {
  if (!id || typeof id !== 'string') {
    throw new Error(`Invalid ID provided for ${operation}`);
  }
  
  if (data === null || data === undefined) {
    throw new Error(`Invalid data provided for ${operation}`);
  }
  
  if (typeof data !== 'string') {
    throw new Error(`Data must be a JSON string for ${operation}`);
  }
}

/**
 * Gets DOM element by ID with error handling for tree operations
 * @param {string} id - Element ID
 * @param {string} operation - Operation name for error reporting
 * @returns {HTMLElement} The DOM element
 * @throws {Error} When element is not found
 */
function getTreeElement(id, operation) {
  const element = document.getElementById(id);
  
  if (!element) {
    throw new Error(`Element with ID '${id}' not found for ${operation}`);
  }
  
  return element;
}

/**
 * Parses and validates JSON data for tree operations
 * @param {string} data - JSON string to parse
 * @param {string} operation - Operation name for error reporting
 * @returns {Object} Parsed JSON object
 * @throws {Error} When JSON parsing fails or data is invalid
 */
function parseTreeData(data, operation) {
  let parsedData;
  
  try {
    parsedData = JSON.parse(data);
  } catch (err) {
    throw new Error(`Failed to parse JSON data for ${operation}: ${err.message}`);
  }
  
  if (typeof parsedData !== 'object' || parsedData === null) {
    throw new Error(`Parsed data must be a valid object for ${operation}`);
  }
  
  return parsedData;
}

/**
 * Validates tree model structure
 * @param {Object} model - Parsed tree model
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When model structure is invalid
 */
function validateTreeModel(model, operation) {
  // Basic validation - tree models are typically arrays or objects with specific structure
  if (Array.isArray(model)) {
    // If it's an array, validate it's not empty and contains valid items
    if (model.length === 0) {
      console.warn(`Tree model is empty for ${operation}`);
    }
  } else if (typeof model === 'object' && model !== null) {
    // If it's an object, basic validation passed
    // Additional specific validations can be added here based on expected tree model structure
  } else {
    throw new Error(`Tree model must be an object or array for ${operation}`);
  }
}

/**
 * Validates tree context structure
 * @param {Object} context - Parsed tree context
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When context structure is invalid
 */
function validateTreeContext(context, operation) {
  // Basic validation for tree context
  if (typeof context !== 'object' || context === null) {
    throw new Error(`Tree context must be a valid object for ${operation}`);
  }
  
  // Additional specific validations can be added here based on expected context structure
}

/**
 * Sets the tree model for a tree element
 * @param {string} id - ID of the tree element
 * @param {string} treeModel - JSON string containing the tree model data
 */
export function setTreeModel(id, treeModel) {
  const operation = 'set tree model';
  
  try {
    // Validate input parameters
    validateTreeInputs(id, treeModel, operation);
    
    // Get the target element
    const element = getTreeElement(id, operation);
    
    // Parse and validate tree model data
    const parsedModel = parseTreeData(treeModel, operation);
    
    // Validate tree model structure
    validateTreeModel(parsedModel, operation);
    
    // Set the model on the element
    element.model = parsedModel;
    
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
  }
}

/**
 * Sets the tree context for a tree element
 * @param {string} id - ID of the tree element
 * @param {string} treeContext - JSON string containing the tree context data
 */
export function setTreeContext(id, treeContext) {
  const operation = 'set tree context';
  
  try {
    // Validate input parameters
    validateTreeInputs(id, treeContext, operation);
    
    // Get the target element
    const element = getTreeElement(id, operation);
    
    // Parse and validate tree context data
    const parsedContext = parseTreeData(treeContext, operation);
    
    // Validate tree context structure
    validateTreeContext(parsedContext, operation);
    
    // Set the context on the element
    element.context = parsedContext;
    
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
  }
}
