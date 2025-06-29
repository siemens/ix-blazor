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
    /// Tabs interop class providing JavaScript interop functionality for tabs operations in Siemens IX Blazor components.
    /// </summary>
    internal class TabsInterop : BaseJSInterop
    {
        /// <summary>
        /// Initializes a new instance of the TabsInterop class.
        /// </summary>
        /// <param name="jsRuntime">The JavaScript runtime instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when jsRuntime is null.</exception>
        public TabsInterop(IJSRuntime jsRuntime) 
            : base(jsRuntime, "./_content/Siemens.IX.Blazor/js/siemens-ix/interops/tabsInterop.js")
        {
        }

        /// <summary>
        /// Initializes the tabs component by setting up click handlers for tab navigation.
        /// </summary>
        /// <param name="id">The ID of the tabs container element.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when id is null or empty.</exception>
        /// <exception cref="ObjectDisposedException">Thrown when the object has been disposed.</exception>
        /// <exception cref="JSException">Thrown when JavaScript interop fails.</exception>
        public async Task InitialComponent(string id)
        {
            // Validate input parameters
            ValidateElementId(id, nameof(id));

            await InvokeJSVoidAsync("initializeTabs", $"initialize tabs component for element '{id}'", id);
        }

        /// <summary>
        /// Subscribes to events on a tabs element and invokes .NET callback methods when events occur.
        /// </summary>
        /// <param name="classObject">The .NET object instance that contains the callback method.</param>
        /// <param name="id">The ID of the tabs element to subscribe to events on.</param>
        /// <param name="eventName">The name of the JavaScript event to listen for.</param>
        /// <param name="methodName">The name of the .NET method to invoke when the event occurs.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown when string parameters are null or empty.</exception>
        /// <exception cref="ObjectDisposedException">Thrown when the object has been disposed.</exception>
        /// <exception cref="JSException">Thrown when JavaScript interop fails.</exception>
        public async Task SubscribeEvents(object classObject, string id, string eventName, string methodName)
        {
            // Validate input parameters
            ValidateTabsEventSubscriptionParameters(classObject, id, eventName, methodName);

            await InvokeVoidAsync("subscribeEvents", classObject, id, eventName, methodName);
        }

        /// <summary>
        /// Validates parameters for the SubscribeEvents method.
        /// </summary>
        /// <param name="classObject">The .NET object instance.</param>
        /// <param name="id">The tabs element ID.</param>
        /// <param name="eventName">The event name.</param>
        /// <param name="methodName">The callback method name.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown when string parameters are null or empty.</exception>
        private static void ValidateTabsEventSubscriptionParameters(object classObject, string id, string eventName, string methodName)
        {
            ArgumentNullException.ThrowIfNull(classObject);
            
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Tabs element ID cannot be null or empty.", nameof(id));
            }
            
            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentException("Event name cannot be null or empty.", nameof(eventName));
            }
            
            if (string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentException("Method name cannot be null or empty.", nameof(methodName));
            }
        }
    }
}
