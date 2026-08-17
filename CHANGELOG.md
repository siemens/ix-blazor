<!--
SPDX-FileCopyrightText: 2024 Siemens AG

SPDX-License-Identifier: MIT
-->

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
