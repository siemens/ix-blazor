// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.JSInterop;
using System.Linq;

namespace SiemensIXBlazor.Interops
{
    /// <summary>
    /// Base class for JavaScript interop operations providing common functionality for Siemens IX Blazor components.
    /// </summary>
    public abstract class BaseJSInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private readonly string modulePath;
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the BaseJSInterop class.
        /// </summary>
        /// <param name="jsRuntime">The JavaScript runtime instance.</param>
        /// <param name="modulePath">The path to the JavaScript module.</param>
        /// <exception cref="ArgumentNullException">Thrown when jsRuntime is null.</exception>
        /// <exception cref="ArgumentException">Thrown when modulePath is null or empty.</exception>
        protected BaseJSInterop(IJSRuntime jsRuntime, string modulePath)
        {
            ArgumentNullException.ThrowIfNull(jsRuntime);
            
            if (string.IsNullOrWhiteSpace(modulePath))
            {
                throw new ArgumentException("Module path cannot be null or empty.", nameof(modulePath));
            }
            
            this.modulePath = modulePath;
            moduleTask = new Lazy<Task<IJSObjectReference>>(() => LoadModuleAsync(jsRuntime));
        }

        /// <summary>
        /// Gets the JavaScript module reference, loading it if necessary.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing the module reference.</returns>
        /// <exception cref="ObjectDisposedException">Thrown when the object has been disposed.</exception>
        /// <exception cref="JSException">Thrown when module loading fails.</exception>
        protected async Task<IJSObjectReference> GetModuleAsync()
        {
            ValidateNotDisposed();
            
            try
            {
                return await moduleTask.Value;
            }
            catch (JSException jsEx)
            {
                throw new JSException($"Failed to load JavaScript module '{modulePath}': {jsEx.Message}", jsEx);
            }
        }

        /// <summary>
        /// Loads the JavaScript module asynchronously.
        /// </summary>
        /// <param name="jsRuntime">The JavaScript runtime instance.</param>
        /// <returns>A task that represents the asynchronous operation, containing the module reference.</returns>
        private async Task<IJSObjectReference> LoadModuleAsync(IJSRuntime jsRuntime)
        {
            try
            {
                return await jsRuntime.InvokeAsync<IJSObjectReference>("import", modulePath);
            }
            catch (JSException jsEx)
            {
                throw new JSException($"Failed to import JavaScript module '{modulePath}': {jsEx.Message}", jsEx);
            }
        }

        /// <summary>
        /// Validates that the object has not been disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Thrown when the object has been disposed.</exception>
        protected void ValidateNotDisposed()
        {
            ObjectDisposedException.ThrowIf(disposed, GetType().Name);
        }

        /// <summary>
        /// Validates parameters for event listener operations.
        /// </summary>
        /// <param name="classObject">The .NET object instance.</param>
        /// <param name="id">The DOM element ID.</param>
        /// <param name="eventName">The event name.</param>
        /// <param name="callbackFunctionName">The callback function name.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown when string parameters are null or empty.</exception>
        protected static void ValidateEventListenerParameters(object classObject, string id, string eventName, string callbackFunctionName)
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

        /// <summary>
        /// Validates an element ID parameter.
        /// </summary>
        /// <param name="id">The element ID.</param>
        /// <param name="parameterName">The parameter name for error reporting.</param>
        /// <exception cref="ArgumentException">Thrown when id is null or empty.</exception>
        protected static void ValidateElementId(string id, string parameterName = "id")
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Element ID cannot be null or empty.", parameterName);
            }
        }

        /// <summary>
        /// Invokes a JavaScript function with error handling and context.
        /// </summary>
        /// <param name="functionName">The JavaScript function name to invoke.</param>
        /// <param name="operationName">The operation name for error context.</param>
        /// <param name="args">The arguments to pass to the JavaScript function.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="JSException">Thrown when JavaScript interop fails.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an unexpected error occurs.</exception>
        protected async Task InvokeJSVoidAsync(string functionName, string operationName, params object[] args)
        {
            try
            {
                var module = await GetModuleAsync();
                await module.InvokeVoidAsync(functionName, args);
            }
            catch (JSException jsEx)
            {
                throw new JSException($"Failed to {operationName}: {jsEx.Message}", jsEx);
            }
            catch (Exception ex) when (ex is not ArgumentException && ex is not ObjectDisposedException)
            {
                throw new InvalidOperationException($"Unexpected error while {operationName}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Invokes a JavaScript function with automatic object reference handling for .NET callbacks.
        /// </summary>
        /// <param name="functionName">The JavaScript function name to invoke.</param>
        /// <param name="callbackObject">The .NET object to create a reference for (if not null).</param>
        /// <param name="args">Additional arguments to pass to the JavaScript function.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="JSException">Thrown when JavaScript interop fails.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an unexpected error occurs.</exception>
        protected async Task InvokeVoidAsync(string functionName, object? callbackObject, params object[] args)
        {
            ValidateNotDisposed();
            
            try
            {
                var module = await GetModuleAsync();
                
                if (callbackObject != null)
                {
                    using var objectReference = DotNetObjectReference.Create(callbackObject);
                    var allArgs = new object[] { objectReference }.Concat(args).ToArray();
                    await module.InvokeVoidAsync(functionName, allArgs);
                }
                else
                {
                    await module.InvokeVoidAsync(functionName, args);
                }
            }
            catch (JSException jsEx)
            {
                throw new JSException($"Failed to invoke JavaScript function '{functionName}': {jsEx.Message}", jsEx);
            }
            catch (Exception ex) when (ex is not ArgumentException && ex is not ObjectDisposedException)
            {
                throw new InvalidOperationException($"Unexpected error while invoking JavaScript function '{functionName}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Invokes a JavaScript function with a return value, with error handling and context.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="functionName">The JavaScript function name to invoke.</param>
        /// <param name="operationName">The operation name for error context.</param>
        /// <param name="args">The arguments to pass to the JavaScript function.</param>
        /// <returns>A task that represents the asynchronous operation, containing the result.</returns>
        /// <exception cref="JSException">Thrown when JavaScript interop fails.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an unexpected error occurs.</exception>
        protected async Task<T> InvokeJSAsync<T>(string functionName, string operationName, params object[] args)
        {
            try
            {
                var module = await GetModuleAsync();
                return await module.InvokeAsync<T>(functionName, args);
            }
            catch (JSException jsEx)
            {
                throw new JSException($"Failed to {operationName}: {jsEx.Message}", jsEx);
            }
            catch (Exception ex) when (ex is not ArgumentException && ex is not ObjectDisposedException)
            {
                throw new InvalidOperationException($"Unexpected error while {operationName}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Disposes of the JavaScript module and releases associated resources.
        /// </summary>
        /// <returns>A task that represents the asynchronous dispose operation.</returns>
        public async ValueTask DisposeAsync()
        {
            if (disposed)
            {
                return;
            }

            if (moduleTask.IsValueCreated)
            {
                try
                {
                    var module = await moduleTask.Value;
                    if (module != null)
                    {
                        await module.DisposeAsync();
                    }
                }
                catch (JSException jsEx)
                {
                    // Log JavaScript-specific errors but don't throw during disposal
                    Console.Error.WriteLine($"JavaScript error during {GetType().Name} disposal: {jsEx.Message}");
                }
                catch (Exception ex)
                {
                    // Log other errors but don't throw during disposal
                    Console.Error.WriteLine($"Error during {GetType().Name} disposal: {ex.Message}");
                }
            }

            disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
