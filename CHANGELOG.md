<!--
SPDX-FileCopyrightText: 2024 Siemens AG

SPDX-License-Identifier: MIT
-->

## 1.0.0 - 2026-09-22

### Highlights

- The Blazor wrappers now cover the public iX 5.2.1 component API.
- Typed AG Grid Community integration is available through `AGGrid<TData>`, including grid options, columns, column groups, events, APIs, transactions, infinite data sources, JSON extension points, and JavaScript module support for custom cell renderers. Enterprise modules are not included.
- Public wrappers are available for `Badge`, the `Chat` family, `CardAccordion`, `CardContent`, `CardTitle`, `DateTimeInput`, `DropdownQuickActions`, `FieldLabel`, `FilterChip`, `GroupContextMenu`, `HelperText`, `LayoutAuto`, the `Popover` family, and `RangeField`.
- The current modal and notification APIs are available through `ModalService`, `ModalHost`, `LoadingService`, declarative `Toast`, and `ToastContainer` lifecycle operations.

### Component and API updates

- Parameters, defaults, enums, slots, event payloads, accessibility attributes, serialization, and rendering behavior now follow the iX 5.2.1 component contracts.
- Typed contracts and native methods are available across Application, Button, Cards, Checkbox, Radio, CategoryFilter, date/time controls, Dropdown, form fields, Menu, Pane, Select, Slider, Tabs, Toast, Tree, Upload, Workflow, and related components.
- Named content and slot mappings are available across content headers, progress indicators, blinds, panes, workflow steps, key-value components, flip tiles, groups, tooltips, cards, and chat components.
- CategoryFilter clearing can be canceled; typed Tree node-removal details, element-backed Tooltip and Popover targets, Toggle form integration, Upload directory/state handling, `AgGridOptions.StripedRows`, and `CardList.I18nShowLess` are available.
- Current iX value shapes can be passed for numeric and string KPI values, numeric and `"auto"` dimensions, numeric and `"auto"` item heights, and single or multiple Select values.

### Runtime and behavior improvements

- Boolean and optional-attribute serialization now follows iX semantics: `false` values are omitted and enabled presence attributes are emitted as `"true"`.
- Interop lifecycle ownership, listener cleanup, native element access, browser-object handling, and ECharts theme resolution have been improved.
- ECharts is lazy-loaded on first chart use, and bundled icon URLs are resolved relative to the host application base path.
- The iX icon assets have been refreshed, with current validation, color, localization, and accessibility behavior.

### Breaking Changes

- The following APIs were removed: `Drawer`, `MapNavigationOverlay`, `ValidationTooltip`, legacy modal components, and the preview AG Grid contract.
- Tabs and Menu navigation now use stable keys and current iX control methods.
- Loose string, dynamic, and raw JSON contracts have been replaced with typed parameter and event models; public localization parameters use `I18n...`.
- Current iX defaults and enum-backed APIs are used. See [Breaking Changes in 1.0.0](BREAKING_CHANGES/1.0.0.md) for migration details.

### Runtime dependencies

- iX packages: `@siemens/ix` 5.2.1, `@siemens/ix-aggrid` 5.1.0, `@siemens/ix-echarts` 4.1.1, and `@siemens/ix-icons` 3.5.0.
- Runtime packages: AG Grid Community 35.2.0, ECharts 6.1.0, and Siemens.IX.Blazor 1.0.0.

## 0.5.5 - 2026-06-22

### What's Changed

- feat: upgrade to .NET 10 (#232)

### Notice

This release includes no functional changes compared to `0.5.4`; the only change is the upgrade to `.NET 10`.

Starting with `0.5.5`, `.NET 8` builds are no longer provided.

## 0.5.4 - 2026-06-01

### What's Changed

#### Features

- Updated Siemens IX to 4.4.0 (#215)
- Added input components (#166)
- Added ProgressIndicator support and additional component properties (#162)
- Added EventListVariant support (#180)
- Added TreeData icon support (#181)
- Added secondary slot support and additional properties to ApplicationHeader (#182)
- Added toast positioning and action support (#184)
- Added aria-label support across multiple components (#194)
- Added 3D chart support and dynamic theme support (#198)
- Improved component alignment with Siemens IX 4.3.0 (#206)

#### Fixes and Improvements

- Fixed unsupported ToggleButton oval property (#163)
- Fixed EmptyState layout behavior (#177)
- Fixed dismissible MessageBar behavior (#177)
- Fixed Breadcrumb attribute mapping (#185)
- Fixed Breadcrumb nextItems handling (#187)
- Fixed Tree JavaScript interop behavior (#191)
- Fixed TimePicker behavior (#199)
- Improved component defaults and JSON handling (#213)
- Improved compatibility and test coverage (#214)

#### Dependencies and Maintenance

- Centralized package version management (#169)
- Updated ix CSS and ix-icons assets (#186)
- Updated NuGet dependencies (#202, #208, #209)

## 0.5.3 - 2025-05-22

### What's Changed

- Add New Properties to Select and DateDropdown Components
- Add New Properties to DateDropdown and ExpandingSearch
- update: ix and ix-icons updated
- Refactor color properties
- Refactor card variants
- Component version updates
- tests: add TimePicker and Toast tests
- fix: remove InitialComponent interop
- feat: add TooltipText support and tests for Chip and Pill components
- feat(MessageBar): add new types and deprecate danger
- feat: update ix version to 3.0.0
- Update : Events added for Menu , Tests updated
- Update : Toggle Event Added for Flip Tile
- Fix : Remove Wrong Component Type in MenuTest
- Add Slider marker feature and unit tests for Slider, Tree, and Theme

## 0.5.2 - 2024-12-10

### What's Changed

#### Added

- **Menu**: `Pinned` property.
- **MenuItem**: `Label` property.
- **CategoryFilter**: `disabled` and `readonly` states.
- **Button**: New `danger` variant.
- **CardList**: `HideShowAll` property.
- **DateDropdown**: `Disabled` property.
- **SplitButton**: Close behavior.
- **Typography**: New component with tests and documentation.
- **ValidationTooltip**: New component with tests and documentation.
- **MapNavigationOverlay**: New component.

#### Updated

- **Dropdown**: Improved close behavior and tests.
- **Group Context Menu**: Implementation reverted.
- **Interops**: Fixed path issues.
- **Static Files**: Improved handling.

#### Removed

- Old form validation section.

#### Testing

- Unit tests added for **Tooltip**, **Typography**, and
  **ValidationTooltip** components.
