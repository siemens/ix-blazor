// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public class IXBaseComponent : ComponentBase, IAsyncDisposable, IInteropOwner
    {
        private readonly HashSet<IAsyncDisposable> interops = [];
        private readonly object interopLock = new();
        private bool disposed;

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? UserAttributes { get; set; }
        [Parameter]
        public string? Class { get; set; }
        [Parameter]
        public string? Style { get; set; }

        protected void RegisterDisposable(IAsyncDisposable disposable)
        {
            ArgumentNullException.ThrowIfNull(disposable);

            lock (interopLock)
            {
                if (!disposed)
                {
                    interops.Add(disposable);
                    return;
                }
            }

            _ = DisposeLateResourceAsync(disposable);
        }

        void IInteropOwner.RegisterDisposable(IAsyncDisposable disposable) => RegisterDisposable(disposable);

        public virtual async ValueTask DisposeAsync()
        {
            IAsyncDisposable[] resources;
            lock (interopLock)
            {
                if (disposed)
                {
                    return;
                }

                disposed = true;
                resources = interops.ToArray();
                interops.Clear();
            }

            foreach (var resource in resources)
            {
                await DisposeResourceAsync(resource);
            }
        }

        private static async Task DisposeLateResourceAsync(IAsyncDisposable resource)
        {
            await DisposeResourceAsync(resource);
        }

        private static async Task DisposeResourceAsync(IAsyncDisposable resource)
        {
            try
            {
                await resource.DisposeAsync();
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine($"Failed to dispose component interop: {exception.Message}");
            }
        }
    }
}
