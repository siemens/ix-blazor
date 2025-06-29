// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

/**
 * Validates input parameters for file upload event handler operations
 * @param {Object} caller - Blazor component reference
 * @param {string} elementId - DOM element ID
 * @param {string} eventName - Event name to listen for
 * @param {string} functionName - Blazor method name to invoke
 * @throws {Error} When validation fails
 */
function validateFileUploadInputs(caller, elementId, eventName, functionName) {
  if (!caller) {
    throw new Error('Caller reference is required for file upload event handler setup');
  }
  
  if (!elementId || typeof elementId !== 'string') {
    throw new Error('Valid element ID is required for file upload event handler setup');
  }
  
  if (!eventName || typeof eventName !== 'string') {
    throw new Error('Valid event name is required for file upload event handler setup');
  }
  
  if (!functionName || typeof functionName !== 'string') {
    throw new Error('Valid function name is required for file upload event handler setup');
  }
  
  if (typeof caller.invokeMethodAsync !== 'function') {
    throw new Error('Caller must have invokeMethodAsync method available');
  }
}

/**
 * Gets DOM element by ID with error handling for file upload operations
 * @param {string} elementId - Element ID
 * @param {string} operation - Operation name for error reporting
 * @returns {HTMLElement} The DOM element
 * @throws {Error} When element is not found
 */
function getFileUploadElement(elementId, operation) {
  const element = document.getElementById(elementId);
  
  if (!element) {
    throw new Error(`Element with ID '${elementId}' not found for ${operation}`);
  }
  
  return element;
}

/**
 * Validates file data from upload event
 * @param {FileList|Array} files - Files from the upload event
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When files are invalid
 */
function validateFiles(files, operation) {
  if (!files) {
    throw new Error(`No files provided for ${operation}`);
  }
  
  if (files.length === 0) {
    throw new Error(`Empty file list provided for ${operation}`);
  }
  
  // Validate each file
  Array.from(files).forEach((file, index) => {
    if (!file || typeof file !== 'object') {
      throw new Error(`Invalid file at index ${index} for ${operation}`);
    }
    
    if (!file.name || typeof file.name !== 'string') {
      throw new Error(`File at index ${index} has invalid name for ${operation}`);
    }
    
    if (typeof file.size !== 'number' || file.size < 0) {
      throw new Error(`File '${file.name}' has invalid size for ${operation}`);
    }
  });
}

/**
 * Reads a single file and converts it to base64 with metadata
 * @param {File} file - File to read
 * @param {number} index - File index for error reporting
 * @returns {Promise<Object>} Promise that resolves to file data object
 */
function readFileAsBase64(file, index) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    
    reader.onloadend = () => {
      try {
        if (!reader.result || typeof reader.result !== 'string') {
          throw new Error(`FileReader result is invalid for file '${file.name}'`);
        }
        
        const base64Data = reader.result.split(",")[1];
        
        if (!base64Data) {
          throw new Error(`Failed to extract base64 data for file '${file.name}'`);
        }
        
        resolve({
          name: file.name,
          size: file.size,
          type: file.type || 'application/octet-stream',
          data: base64Data,
          index: index
        });
      } catch (error) {
        reject(new Error(`Error processing file '${file.name}': ${error.message}`));
      }
    };
    
    reader.onerror = () => {
      const errorMessage = reader.error ? reader.error.message : 'Unknown error';
      reject(new Error(`FileReader error for file '${file.name}': ${errorMessage}`));
    };
    
    reader.onabort = () => {
      reject(new Error(`File reading was aborted for file '${file.name}'`));
    };
    
    try {
      reader.readAsDataURL(file);
    } catch (error) {
      reject(new Error(`Failed to start reading file '${file.name}': ${error.message}`));
    }
  });
}

/**
 * Processes multiple files and converts them to base64 format
 * @param {FileList|Array} files - Files to process
 * @returns {Promise<Array>} Promise that resolves to array of file data objects
 */
async function processFiles(files) {
  const operation = 'process files';
  
  try {
    validateFiles(files, operation);
    
    const fileArray = Array.from(files);
    const filePromises = fileArray.map((file, index) => readFileAsBase64(file, index));
    
    const fileDataArray = await Promise.all(filePromises);
    
    // Sort by original index to maintain file order
    return fileDataArray.sort((a, b) => a.index - b.index);
  } catch (error) {
    throw new Error(`Failed to ${operation}: ${error.message}`);
  }
}

/**
 * Creates a safe file upload event handler
 * @param {Object} caller - Blazor component reference
 * @param {string} functionName - Blazor method name to invoke
 * @param {string} elementId - Element ID for error context
 * @param {string} eventName - Event name for error context
 * @returns {Function} Event handler function
 */
function createFileUploadEventHandler(caller, functionName, elementId, eventName) {
  return async (event) => {
    const operation = `handle file upload event '${eventName}' on element '${elementId}'`;
    
    try {
      const files = event.detail;
      
      // Early exit if no files selected (not an error condition)
      if (!files || files.length === 0) {
        console.info(`No files selected for ${operation}`);
        return;
      }
      
      // Process files and convert to base64
      const fileDataArray = await processFiles(files);
      
      // Invoke Blazor method with processed file data
      await caller.invokeMethodAsync(functionName, fileDataArray);
      
    } catch (error) {
      console.error(`Failed to ${operation}:`, error.message);
      
      // Optionally invoke error callback if available
      if (typeof caller.invokeMethodAsync === 'function') {
        try {
          const errorFunctionName = `${functionName}Error`;
          await caller.invokeMethodAsync(errorFunctionName, error.message);
        } catch (callbackError) {
          // Error callback is optional, so we just log if it fails
          console.warn(`Error callback '${errorFunctionName}' is not available or failed:`, callbackError.message);
        }
      }
    }
  };
}

/**
 * Sets up a file upload event handler on a DOM element that processes files and invokes a Blazor method
 * @param {Object} caller - Blazor component reference with invokeMethodAsync capability
 * @param {string} elementId - ID of the DOM element to attach the event listener to
 * @param {string} eventName - Name of the event to listen for (e.g., 'filesSelected', 'change')
 * @param {string} functionName - Name of the Blazor method to invoke when files are uploaded
 * @returns {boolean} True if event handler was successfully attached, false otherwise
 */
export function fileUploadEventHandler(caller, elementId, eventName, functionName) {
  const operation = 'set up file upload event handler';
  
  try {
    // Validate all input parameters
    validateFileUploadInputs(caller, elementId, eventName, functionName);
    
    // Get the target element
    const element = getFileUploadElement(elementId, operation);
    
    // Create the event handler
    const eventHandler = createFileUploadEventHandler(caller, functionName, elementId, eventName);
    
    // Attach the event listener
    element.addEventListener(eventName, eventHandler);
    
    return true;
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
    return false;
  }
}

/**
 * Removes a file upload event listener from a DOM element
 * @param {string} elementId - ID of the DOM element to remove the event listener from
 * @param {string} eventName - Name of the event to stop listening for
 * @param {Function} handlerFunction - The specific handler function to remove
 * @returns {boolean} True if event listener was successfully removed, false otherwise
 */
export function removeFileUploadEventHandler(elementId, eventName, handlerFunction) {
  const operation = 'remove file upload event handler';
  
  try {
    if (!elementId || typeof elementId !== 'string') {
      throw new Error('Valid element ID is required for removing file upload event handler');
    }
    
    if (!eventName || typeof eventName !== 'string') {
      throw new Error('Valid event name is required for removing file upload event handler');
    }
    
    if (!handlerFunction || typeof handlerFunction !== 'function') {
      throw new Error('Valid handler function is required for removing file upload event handler');
    }
    
    const element = getFileUploadElement(elementId, operation);
    
    element.removeEventListener(eventName, handlerFunction);
    
    return true;
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
    return false;
  }
}

/**
 * Validates file types against allowed extensions or MIME types
 * @param {Array} files - Array of file objects to validate
 * @param {Array} allowedTypes - Array of allowed file extensions (e.g., ['.jpg', '.png']) or MIME types
 * @returns {Object} Validation result with valid and invalid files
 */
export function validateFileTypes(files, allowedTypes) {
  const operation = 'validate file types';
  
  try {
    if (!Array.isArray(files)) {
      throw new Error('Files must be an array for file type validation');
    }
    
    if (!Array.isArray(allowedTypes) || allowedTypes.length === 0) {
      throw new Error('Allowed types must be a non-empty array for file type validation');
    }
    
    const validFiles = [];
    const invalidFiles = [];
    
    files.forEach((file, index) => {
      if (!file || !file.name) {
        invalidFiles.push({ file, index, reason: 'Invalid file object' });
        return;
      }
      
      const fileName = file.name.toLowerCase();
      const fileType = (file.type || '').toLowerCase();
      
      const isValid = allowedTypes.some(allowedType => {
        const type = allowedType.toLowerCase();
        
        // Check by extension
        if (type.startsWith('.')) {
          return fileName.endsWith(type);
        }
        
        // Check by MIME type
        return fileType === type || fileType.startsWith(type + '/');
      });
      
      if (isValid) {
        validFiles.push({ file, index });
      } else {
        invalidFiles.push({ 
          file, 
          index, 
          reason: `File type not allowed. Allowed types: ${allowedTypes.join(', ')}` 
        });
      }
    });
    
    return {
      valid: validFiles,
      invalid: invalidFiles,
      allValid: invalidFiles.length === 0
    };
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
    return {
      valid: [],
      invalid: files.map((file, index) => ({ file, index, reason: error.message })),
      allValid: false
    };
  }
}
