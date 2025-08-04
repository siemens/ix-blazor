// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Playground.Components.Pages.InputFieldsAndSelections.CategoryFilter;

public partial class CategoryFilter
{
    private int activeTab = 0;
    private int activeTabForWithoutState = 0;


    SiemensIXBlazor.Components.CategoryFilter.CategoryFilter categoryFilter;
    SiemensIXBlazor.Components.CategoryFilter.CategoryFilter categoryFilterForWithoutState;
    Dictionary<string, Category> categoriesDict;
    FilterState filterState;
    String[] suggestions = new string[]
    {
        "Apple", "MS", "Siemens", "IBM", "Google", "Amazon", "Tesla", "Meta"
    };

    public string Content { get; private set; } = @"
    public class CategoryFilterExample
    {
        private CategoryFilter categoryFilter;
        private Dictionary<string, Category> categoriesDict;
        private FilterState filterState;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                categoriesDict = new();
                categoriesDict.Add(""ID_1"", new Category()
                {
                    Label = ""Vendor"",
                    Options = new string[]
                    {
                        ""Apple"", ""MS"", ""Siemens""
                    }
                });

                filterState = new()
                {
                    Tokens = new string[] { ""Custom filter text"" },
                    Categories = new FilterStateCategory[]
                    {
                        new FilterStateCategory()
                        {
                            Id = ""ID_1"",
                            Value = ""IBM"",
                            Operator = ""Not Equal""
                        }
                    }
                };

                categoryFilter.Categories = categoriesDict;
                categoryFilter.FilterState = filterState;
            }
        }
    }";

    public string ContentForWithoutState { get; private set; } = @"
    public class CategoryFilterExample
    {
        private CategoryFilter categoryFilter;
        private Dictionary<string, Category> categoriesDict;
        private FilterState filterState;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                categoriesDict = new();
                categoriesDict.Add(""ID_1"", new Category()
                {
                    Label = ""Vendor"",
                    Options = new string[]
                    {
                        ""Apple"", ""MS"", ""Siemens""
                    }
                });

                filterState = new()
                {
                    Tokens = new string[] { ""Custom filter text"" },
                    Categories = new FilterStateCategory[]
                    {
                        new FilterStateCategory()
                        {
                            Id = ""ID_1"",
                            Value = ""IBM"",
                            Operator = ""Not Equal""
                        }
                    }
                };

                categoryFilter.Categories = categoriesDict;
                categoryFilter.FilterState = filterState;
            }
        }
    }";

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            CreateCategoriesForBasic(categoryFilter);
        }
    }


    private void CreateCategoriesForBasic(SiemensIXBlazor.Components.CategoryFilter.CategoryFilter categoryFilter)
    {
        categoriesDict = new();
        categoriesDict.Add("ID_1", new Category()
        {
            Label = "Vendor",
            Options = new string[]
            {
                    "Apple", "MS", "Siemens"
            }
        });

        filterState = new()
        {
            Tokens = new string[]
            {
                    "Custom filter text"
            },
            Categories = new FilterStateCategory[]
            {
                    new FilterStateCategory()
                    {
                        Id = "ID_1",
                        Value = "IBM",
                        Operator = "Not Equal"
                    }
            }
        };

        categoryFilter.Categories = categoriesDict;
        categoryFilter.FilterState = filterState;
      
    }
    
    private void CreateCategoriesForWithoutState(SiemensIXBlazor.Components.CategoryFilter.CategoryFilter categoryFilter)
    {
        categoryFilter.Suggestions = suggestions;
    }
}
