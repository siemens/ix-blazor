## Siemens IX for Blazor

### Installation

#### Install the Nuget package

```cmd
dotnet add package SiemensIXBlazor
```

#### Import Required CSS and JS Packages

```html
<link rel="stylesheet" href="_content/SiemensIXBlazor/css/siemens-ix/ix-icons.css" />
<link rel="stylesheet" href="_content/SiemensIXBlazor/css/siemens-ix/siemens-ix.css" />

...

<script src="_content/SiemensIXBlazor/js/siemens-ix/index.bundle.js"></script>
```

### Supported Components

- [x] Blind
- [x] Breadcrumb (without nextClick event)
- [x] Button
- [x] Icon Button
- [ ] Category filter
- [ ] ECharts
- [x] Checkbox (will check other input attributes)
- [x] Chip
- [x] Date picker
- [x] Date time picker
- [x] Drawer
- [x] Dropdown button
- [x] Dropdown
- [x] Event list
- [x] Expanding search
- [x] Flip
- [ ] AG grid
- [ ] Group
- [x] HTML table (No need to implement)
- [x] Input (No need to implement)
- [x] KPI
- [x] Message bar
- [x] Modal (No need to implement)
- [x] Pill
- [x] Radio button (No need to implement)
- [x] Select (should do tests)
- [x] Spinner
- [x] Split button
- [ ] Tabs
- [x] Text area (No need to implement)
- [x] Tile
- [x] Time picker
- [x] Toast
- [x] Toggle
- [ ] Tree
- [x] Upload
- [ ] Form validation
- [ ] Workflow

### Usage

You can follow the original documentation and use native `Javascript` components.

```html
<ix-button class="m-1" outline variant="Secondary">
    Button
</ix-button>
```

Or you can use supported components as a native `Blazor` components.

```blazor
<Button Class="m-1" Outline="true" Variant="Secondary">
    Button
</Button>
```
