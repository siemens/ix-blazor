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

namespace SiemensIXBlazor.Components.ECharts
{
    public partial class ECharts
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        public async void InitialChart(dynamic options)
        {
            string serializedOptions = JsonConvert.SerializeObject(options);

            await JSRuntime.InvokeVoidAsync("siemensIXInterop.initializeChart", Id, serializedOptions);
        }
    }
}
