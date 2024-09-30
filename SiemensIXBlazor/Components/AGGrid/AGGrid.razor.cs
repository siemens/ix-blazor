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
using Newtonsoft.Json;

namespace SiemensIXBlazor.Components.AGGrid
{
    public partial class AGGrid
    {
        [Parameter]
        public EventCallback OnCellClicked { get; set; }

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        private DotNetObjectReference<AGGrid>? dotNetHelper;

        public async Task<IJSObjectReference?> CreateGrid(GridOptions options)
        {
            if (Id == string.Empty)
            {
                return null;
            }

            dotNetHelper = DotNetObjectReference.Create(this);

            return await JSRuntime.InvokeAsync<IJSObjectReference?>("siemensIXInterop.agGridInterop.createGrid", dotNetHelper, Id, JsonConvert.SerializeObject(options));
        }

        public async Task<object?> GetSelectedRows(IJSObjectReference grid)
        {
            return await JSRuntime.InvokeAsync<object>("siemensIXInterop.agGridInterop.getSelectedRows", grid);

        }

        [JSInvokable]
        public async Task OnCellClickedCallback()
        {
            await OnCellClicked.InvokeAsync();
        }
    }
}
