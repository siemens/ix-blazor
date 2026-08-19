// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------



using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.Tooltip;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Tooltip : IAsyncDisposable
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public bool Interactive { get; set; } = false;
        [Parameter]
        public string? TitleContent { get; set; }
        [Parameter]
        public TooltipVariant Placement { get; set; } = TooltipVariant.top;
        [Parameter]
        public object? For { get; set; }
        [Parameter]
        public RenderFragment? TitleIconContent { get; set; }
        [Parameter]
        public RenderFragment? TitleContentSlot { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private BaseInterop? _interop;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _interop ??= new(JSRuntime);

            if (For is not null and not string)
            {
                await _interop.SetElementProperty(Id, "for", For);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_interop is not null)
            {
                await _interop.DisposeAsync();
            }
        }
    }
}
