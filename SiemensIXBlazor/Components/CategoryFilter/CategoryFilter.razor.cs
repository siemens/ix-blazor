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
using SiemensIXBlazor.Enums.CategoryFilter;
using SiemensIXBlazor.Objects;
using System.Text.Json;

namespace SiemensIXBlazor.Components.CategoryFilter
{
    public partial class CategoryFilter : IAsyncDisposable
    {
        private Lazy<Task<IJSObjectReference>>? moduleTask;
        private DotNetObjectReference<CategoryFilter>? _objectReference;
        private string? _initializedId;
        private string? _categoriesSnapshot;
        private string? _filterStateSnapshot;
        private string? _nonSelectableCategoriesSnapshot;
        private string? _suggestionsSnapshot;
        private LogicalFilterOperator? _staticOperatorSnapshot;
        private bool _categoriesChanged = true;
        private bool _filterStateChanged = true;
        private bool _nonSelectableCategoriesChanged = true;
        private bool _suggestionsChanged = true;
        private bool _staticOperatorChanged = true;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public Dictionary<string, Category>? Categories { get; set; }

        [Parameter]
        public FilterState? FilterState { get; set; }

        [Parameter]
        public bool HideIcon { get; set; } = false;
        [Parameter]
        public string I18nPlainText { get; set; } = "Filter by text";
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string LabelCategories { get; set; } = "Categories";
        [Parameter]
        public string? AriaLabelFilterInput { get; set; }
        [Parameter]
        public string? AriaLabelOperatorButton { get; set; }
        [Parameter]
        public string? AriaLabelResetButton { get; set; }

        [Parameter]
        public Dictionary<string, string>? NonSelectableCategories { get; set; }

        [Parameter]
        public string? Placeholder { get; set; }
        [Parameter]
        public bool UniqueCategories { get; set; } = false;
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool EnableTopLayer { get; set; } = false;
        [Parameter]
        public bool Readonly { get; set; } = false;

        [Parameter]
        public string[]? Suggestions { get; set; }

        [Parameter]
        public LogicalFilterOperator? StaticOperator { get; set; }

        [Parameter]
        public EventCallback<string?> CategoryChangedEvent { get; set; }

        [Parameter]
        public EventCallback<FilterState> FilterChangedEvent { get; set; }

        [Parameter]
        public EventCallback<FilterClearedEventArgs> FilterClearedEvent { get; set; }

        [Parameter]
        public EventCallback<InputState> InputChangedEvent { get; set; }

        protected override void OnParametersSet()
        {
            TrackChange(
                Categories?.OrderBy(category => category.Key),
                ref _categoriesSnapshot,
                ref _categoriesChanged);
            TrackChange(FilterState, ref _filterStateSnapshot, ref _filterStateChanged);
            TrackChange(
                NonSelectableCategories?.OrderBy(category => category.Key),
                ref _nonSelectableCategoriesSnapshot,
                ref _nonSelectableCategoriesChanged);
            TrackChange(Suggestions, ref _suggestionsSnapshot, ref _suggestionsChanged);

            if (_staticOperatorSnapshot != StaticOperator)
            {
                _staticOperatorSnapshot = StaticOperator;
                _staticOperatorChanged = true;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                moduleTask = new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./_content/Siemens.IX.Blazor/js/siemens-ix/interops/categoryFilterInterop.js").AsTask());
                _objectReference = DotNetObjectReference.Create(this);
            }

            var module = await GetModuleAsync();
            if (!string.Equals(_initializedId, Id, StringComparison.Ordinal))
            {
                if (_initializedId is not null)
                {
                    await module.InvokeVoidAsync("dispose", _initializedId);
                }

                await module.InvokeVoidAsync("initialize", _objectReference, Id);
                _initializedId = Id;
                _categoriesChanged = true;
                _filterStateChanged = true;
                _nonSelectableCategoriesChanged = true;
                _suggestionsChanged = true;
                _staticOperatorChanged = true;
            }

            await ApplyParametersAsync(module);
        }

        [JSInvokable]
        public async Task CategoryChanged(string? category)
        {
            await CategoryChangedEvent.InvokeAsync(category);
        }

        [JSInvokable]
        public async Task FilterChanged(JsonElement filterState)
        {
            FilterState state = JsonSerializer.Deserialize<FilterState>(filterState.GetRawText()) ?? new();
            await FilterChangedEvent.InvokeAsync(state);
        }

        [JSInvokable]
        public async Task<bool> FilterCleared()
        {
            var eventArgs = new FilterClearedEventArgs();
            await FilterClearedEvent.InvokeAsync(eventArgs);
            return eventArgs.Cancel;
        }

        [JSInvokable]
        public async Task InputChanged(JsonElement inputState)
        {
            InputState state = JsonSerializer.Deserialize<InputState>(inputState.GetRawText()) ?? new();
            await InputChangedEvent.InvokeAsync(state);
        }

        private static void TrackChange<T>(T value, ref string? snapshot, ref bool changed)
        {
            var currentSnapshot = JsonSerializer.Serialize(value);
            if (!string.Equals(snapshot, currentSnapshot, StringComparison.Ordinal))
            {
                snapshot = currentSnapshot;
                changed = true;
            }
        }

        private async Task<IJSObjectReference> GetModuleAsync()
        {
            if (moduleTask is null)
            {
                throw new InvalidOperationException("CategoryFilter interop is not initialized.");
            }

            return await moduleTask.Value;
        }

        private async Task ApplyParametersAsync(IJSObjectReference module)
        {
            var categoriesChanged = _categoriesChanged;
            var filterStateChanged = _filterStateChanged;
            var nonSelectableCategoriesChanged = _nonSelectableCategoriesChanged;
            var suggestionsChanged = _suggestionsChanged;
            var staticOperatorChanged = _staticOperatorChanged;

            _categoriesChanged = false;
            _filterStateChanged = false;
            _nonSelectableCategoriesChanged = false;
            _suggestionsChanged = false;
            _staticOperatorChanged = false;

            if (categoriesChanged)
            {
                await module.InvokeVoidAsync("setCategories", Id, Categories);
            }

            if (filterStateChanged)
            {
                await module.InvokeVoidAsync("setFilterState", Id, FilterState);
            }

            if (nonSelectableCategoriesChanged)
            {
                await module.InvokeVoidAsync("setNonSelectableCategories", Id, NonSelectableCategories);
            }

            if (suggestionsChanged)
            {
                await module.InvokeVoidAsync("setSuggestions", Id, Suggestions);
            }

            if (staticOperatorChanged)
            {
                await module.InvokeVoidAsync(
                    "setStaticOperator",
                    Id,
                    StaticOperator?.ToEnumString());
            }
        }

        public override async ValueTask DisposeAsync()
        {
            try
            {
                if (moduleTask is not null && moduleTask.IsValueCreated)
                {
                    var module = await moduleTask.Value;
                    if (_initializedId is not null)
                    {
                        await module.InvokeVoidAsync("dispose", _initializedId);
                    }
                    await module.DisposeAsync();
                }
            }
            catch (JSDisconnectedException)
            {
            }
            finally
            {
                _objectReference?.Dispose();
                _objectReference = null;
                GC.SuppressFinalize(this);
            }
        }

    }
}
