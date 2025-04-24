// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using SiemensIXBlazor.Playground.Models;

namespace SiemensIXBlazor.Playground.Services;

public static class MenuConfiguration
{
    public static List<MenuCategoryModel> Categories =>
    [
        new MenuCategoryModel
        {
            Label = "Application Frame",
            Icon = "frames",
            Items =
            [
                new() { Route = "application", Label = "Application" },
                new() { Route = "applicationheader", Label = "Application Header" },
                new() { Route = "applicationmenu", Label = "Application Menu" },
                new() { Route = "avatar", Label = "Avatar" },
                new() { Route = "content", Label = "Content" },
                new() { Route = "aboutandlegal", Label = "About And Legal" },
                new() { Route = "settings", Label = "Settings" }
            ]
        },

        new MenuCategoryModel
        {
            Label = "Navigation And Hierarchy",
            Icon = "navigation-left",
            Items =
            [
                new() { Route = "breadcrumb", Label = "Breadcrumb" },
                new() { Route = "group", Label = "Group" },
                new() { Route = "pagination", Label = "Pagination" },
                new() { Route = "tabs", Label = "Tabs" },
                new() { Route = "tree", Label = "Tree" },
                new() { Route = "workflow", Label = "Workflow" }
            ]
        },

        new MenuCategoryModel
        {
            Label = "Containers And Layout",
            Icon = "layers",
            Items =
            [
                new() { Route = "blind", Label = "Blind" },
                new() { Route = "card", Label = "Card" },
                new() { Route = "cardlist", Label = "Card List" },
                new() { Route = "flip", Label = "Flip" },
                new() { Route = "eventlist", Label = "Event List" },
                new() { Route = "drawer", Label = "Drawer" },
                new() { Route = "layoutauto", Label = "Layout Auto" },
                new() { Route = "layoutgrid", Label = "Layout Grid" },
                new() { Route = "modal", Label = "Modal" },
                new() { Route = "panes", Label = "Panes" },
                new() { Route = "tile", Label = "Tile" }
            ]
        },

        new MenuCategoryModel
        {
            Label = "Forms",
            Icon = "pen",
            Items =
            [
                new() { Route = "formsfield", Label = "Forms Field" },
                new() { Route = "formslayout", Label = "Forms Layout" },
                new() { Route = "formsvalidation", Label = "Forms Validation" },
                new() { Route = "formsbehavior", Label = "Forms Behavior" }
            ]
        },

        new MenuCategoryModel
        {
            Label = "Input Fields And Selections",
            Icon = "control-io-field",
            Items =
            [
                new() { Route = "categoryfilter", Label = "Category Filter" },
                new() { Route = "checkbox", Label = "Checkbox" },
                new() { Route = "customfield", Label = "Custom Field" },
                new() { Route = "datedropdown", Label = "Date Dropdown" },
                new() { Route = "dateinput", Label = "Date Input" },
                new() { Route = "datepicker", Label = "Date Picker" },
                new() { Route = "datetimepicker", Label = "Date Time Picker" },
                new() { Route = "expandingsearch", Label = "Expanding Search" },
                new() { Route = "numberinput", Label = "Number Input" },
                new() { Route = "radio", Label = "Radio" },
                new() { Route = "select", Label = "Select" },
                new() { Route = "slider", Label = "Slider" },
                new() { Route = "input", Label = "Input" },
                new() { Route = "textarea", Label = "Textarea" },
                new() { Route = "timepicker", Label = "Time Picker" },
                new() { Route = "toggle", Label = "Toggle" },
                new() { Route = "upload", Label = "Upload" }
            ]
        },

        new MenuCategoryModel
        {
            Label = "Buttons And Actions",
            Icon = "control-button",
            Items =
            [
                new() { Route = "button", Label = "Button" },
                new() { Route = "dropdownbutton", Label = "Dropdown Button" },
                new() { Route = "iconbutton", Label = "Icon Button" },
                new() { Route = "linkbutton", Label = "Link Button" },
                new() { Route = "splitbutton", Label = "Split Button" },
                new() { Route = "togglebutton", Label = "Toggle Button" }
            ]
        },

        new MenuCategoryModel
        {
            Label = "System Feedback And Status",
            Icon = "feedback",
            Items =
            [
                new() { Route = "chip", Label = "Chip" },
                new() { Route = "emptystate", Label = "Empty State" },
                new() { Route = "messagebar", Label = "Message Bar" },
                new() { Route = "pill", Label = "Pill" },
                new() { Route = "spinner", Label = "Spinner" },
                new() { Route = "toast", Label = "Toast" },
                new() { Route = "tooltip", Label = "Tooltip" }
            ]
        },

        new MenuCategoryModel
        {
            Label = "Data Display",
            Icon = "table",
            Items =
            [
                new() { Route = "angulardatagrid", Label = "Angular Data Grid" },
                new() { Route = "htmltable", Label = "HTML Table" },
                new() { Route = "keyvalue", Label = "Key Value" },
                new() { Route = "keyvaluelist", Label = "Key Value List" },
                new() { Route = "kpi", Label = "KPI" }
            ]
        },

        new MenuCategoryModel
        {
            Label = "Charts",
            Icon = "barchart",
            Items =
            [
                new() { Route = "chartoverview", Label = "Chart Overview" },
                new() { Route = "linechart", Label = "Line Chart" },
                new() { Route = "barchart", Label = "Bar Chart" },
                new() { Route = "gaugechart", Label = "Gauge Chart" },
                new() { Route = "piechart", Label = "Pie Chart" },
                new() { Route = "threedchart", Label = "Three D Chart" },
                new() { Route = "specialchart", Label = "Special Chart" }
            ]
        }
    ];
}
