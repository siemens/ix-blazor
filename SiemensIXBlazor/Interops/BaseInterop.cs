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
    /// Base interop class providing common JavaScript interop functionality for Siemens IX Blazor components.
    /// </summary>
    public class BaseInterop : BaseJSInterop
    {
        /// <summary>
        /// Initializes a new instance of the BaseInterop class.
        /// </summary>
        /// <param name="jsRuntime">The JavaScript runtime instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when jsRuntime is null.</exception>
        public BaseInterop(IJSRuntime jsRuntime) 
            : base(jsRuntime, "./_content/Siemens.IX.Blazor/js/siemens-ix/interops/baseJsInterop.js")
        {
        }

        /// <summary>
        /// Adds an event listener to a DOM element that will invoke a .NET callback method.
        /// </summary>
        /// <param name="classObject">The .NET object instance that contains the callback method.</param>
        /// <param name="id">The ID of the DOM element to attach the event listener to.</param>
        /// <param name="eventName">The name of the JavaScript event to listen for.</param>
        /// <param name="callbackFunctionName">The name of the .NET method to invoke when the event occurs.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown when string parameters are null or empty.</exception>
        /// <exception cref="ObjectDisposedException">Thrown when the object has been disposed.</exception>
        /// <exception cref="JSException">Thrown when JavaScript interop fails.</exception>
        public async Task AddEventListener(object classObject, string id, string eventName, string callbackFunctionName)
        {
            // Validate input parameters
            ValidateNotDisposed();
            ValidateEventListenerParameters(classObject, id, eventName, callbackFunctionName);

            using var objectReference = DotNetObjectReference.Create(classObject);
            await InvokeJSVoidAsync("listenEvent", 
                $"add event listener for element '{id}' with event '{eventName}'",
                objectReference, id, eventName, callbackFunctionName);
        }
    }
}
