// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.JSInterop;

namespace SiemensIXBlazor.Interops
{
    /// <summary>
    /// File upload interop class providing JavaScript interop functionality for file upload operations in Siemens IX Blazor components.
    /// </summary>
    internal class FileUploadInterop : BaseJSInterop
    {
        /// <summary>
        /// Initializes a new instance of the FileUploadInterop class.
        /// </summary>
        /// <param name="jsRuntime">The JavaScript runtime instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when jsRuntime is null.</exception>
        public FileUploadInterop(IJSRuntime jsRuntime) 
            : base(jsRuntime, "./_content/Siemens.IX.Blazor/js/siemens-ix/interops/fileUploadInterop.js")
        {
        }

        /// <summary>
        /// Adds a file upload event listener to a DOM element that will invoke a .NET callback method when files are uploaded.
        /// </summary>
        /// <param name="classObject">The .NET object instance that contains the callback method.</param>
        /// <param name="id">The ID of the DOM element to attach the file upload event listener to.</param>
        /// <param name="eventName">The name of the JavaScript event to listen for (e.g., 'filesSelected', 'change').</param>
        /// <param name="callbackFunctionName">The name of the .NET method to invoke when files are uploaded.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown when string parameters are null or empty.</exception>
        /// <exception cref="ObjectDisposedException">Thrown when the object has been disposed.</exception>
        /// <exception cref="JSException">Thrown when JavaScript interop fails.</exception>
        public async Task AddEventListener(object classObject, string id, string eventName, string callbackFunctionName)
        {
            // Validate input parameters
            ValidateFileUploadEventListenerParameters(classObject, id, eventName, callbackFunctionName);

            await InvokeVoidAsync("fileUploadEventHandler", classObject, id, eventName, callbackFunctionName);
        }

        /// <summary>
        /// Validates parameters for the AddEventListener method.
        /// </summary>
        /// <param name="classObject">The .NET object instance.</param>
        /// <param name="id">The DOM element ID.</param>
        /// <param name="eventName">The event name.</param>
        /// <param name="callbackFunctionName">The callback function name.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown when string parameters are null or empty.</exception>
        private static void ValidateFileUploadEventListenerParameters(object classObject, string id, string eventName, string callbackFunctionName)
        {
            ArgumentNullException.ThrowIfNull(classObject);
            
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Element ID cannot be null or empty.", nameof(id));
            }
            
            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentException("Event name cannot be null or empty.", nameof(eventName));
            }
            
            if (string.IsNullOrWhiteSpace(callbackFunctionName))
            {
                throw new ArgumentException("Callback function name cannot be null or empty.", nameof(callbackFunctionName));
            }
        }
    }
}
