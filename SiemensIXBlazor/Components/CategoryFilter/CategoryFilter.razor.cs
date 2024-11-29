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
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;
using System.Text.Json;

namespace SiemensIXBlazor.Components.CategoryFilter
{
    public partial class CategoryFilter
    {
        private Dictionary<string, Category>? _categories;
        private FilterState? _filterState;
        private Dictionary<string, string>? _nonSelectableCategories;
        private string[] _suggestions = [];
        private Lazy<Task<IJSObjectReference>>? moduleTask;
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        public Dictionary<string, Category>? Categories
        { 
            get => _categories;
            set
            {
                _categories = value;
                InitialParameter("setCategories", _categories);
            }
        }
        public FilterState? FilterState
        {
            get => _filterState;
            set
            {
                _filterState = value;
                InitialParameter("setFilterState", _filterState);
            }
        }
        [Parameter]
        public bool? HideIcon { get; set; }
        [Parameter]
        public string I18nPlainText { get; set; } = "Filter by text";
        [Parameter]
        public string Icon { get; set; } = "search";
        [Parameter]
        public string LabelCategories { get; set; } = "Categories";
        public Dictionary<string, string>? NonSelectableCategories
        {
            get => _nonSelectableCategories;
            set
            {
                _nonSelectableCategories = value;
                InitialParameter("setNonSelectableCategories", _nonSelectableCategories);
            }
        }
        [Parameter]
        public string? Placeholder { get; set; }
        [Parameter]
        public bool RepeatCategories { get; set; } = true;
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool Readonly { get; set; } = false;
        [Parameter]
        public string[]? Suggestions
        {
            get => _suggestions;
            set
            {
                _suggestions = value;
                InitialParameter("setSuggestions", new Dictionary<string, string[]> { { "suggestions", _suggestions } });
            }
        }
        [Parameter]
        public EventCallback<FilterState> FilterChangedEvent { get; set; }
        [Parameter]
        public EventCallback<dynamic> InputChangedEvent { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if(firstRender)
            {
                _interop = new(JSRuntime);

                Task.Run(async () =>
                {
                    await _interop.AddEventListener(this, Id, "filterChanged", "FilterChanged");
                    await _interop.AddEventListener(this, Id, "inputChanged", "InputChanged");
                });
            }
        }

        [JSInvokable]
        public async void FilterChanged(JsonElement filterState)
        {
            string jsonDataText = filterState.GetRawText();
            FilterState? state = JsonConvert.DeserializeObject<FilterState>(jsonDataText);
            await FilterChangedEvent.InvokeAsync(state);
        }

        [JSInvokable]
        public async void InputChanged(JsonElement inputState)
        {
            string jsonDataText = inputState.GetRawText();
            dynamic? state = JsonConvert.DeserializeObject<dynamic>(jsonDataText);
            await InputChangedEvent.InvokeAsync(state);
        }

        private void InitialParameter(string functionName, object param)
        {

            moduleTask = new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/Siemens.IX.Blazor/js/siemens-ix/interops/categoryFilterInterop.js").AsTask());

            Task.Run(async () =>
            {
                var module = await moduleTask.Value;
                if (module != null)
                {
                    await module.InvokeVoidAsync(functionName, Id, JsonConvert.SerializeObject(param));
                }
            });
        }
    }
}
