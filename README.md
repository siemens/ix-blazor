<!--
SPDX-FileCopyrightText: 2025 Siemens AG

SPDX-License-Identifier: MIT
-->

## Siemens IX for Blazor

## Installation

Install the `Siemens.IX.Blazor` package from the [NuGet](https://www.nuget.org/packages/Siemens.IX.Blazor) package manager.

## .NET CLI

```cmd
dotnet add package Siemens.IX.Blazor
```

## Package Manager

```cmd
NuGet\Install-Package Siemens.IX.Blazor
```

Add required `CSS` and `Javascript` packages into the `index.html` file.

```html
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Blazor App</title>

    <!--Deprecated after v0.4.0-->
    <link
      rel="stylesheet"
      href="_content/Siemens.IX.Blazor/css/siemens-ix/ix-icons.css"
    />

    <link
      rel="stylesheet"
      href="_content/Siemens.IX.Blazor/css/siemens-ix/siemens-ix.css"
    />
  </head>
  <body>
    ...
    <script src="_content/Siemens.IX.Blazor/js/siemens-ix/index.bundle.js"></script>
  </body>
</html>
```

> [!CAUTION]
> If you want to use this library with a `Blazor Web App`, you need to set the `render mode` to `InteractiveServer`.
> You can find more information at [here](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes?view=aspnetcore-8.0).

### Theme Switching

**Since v0.3.9**

Add `Theme` component to the page that you want to manipulate the theme.

```razor
<Theme @ref="themeProvider"></Theme>

<Button ClickEvent="SetDarkTheme">Set Dark Theme</Button>
<Button ClickEvent="ToggleTheme">Toggle Theme</Button>
<Button ClickEvent="ToggleSystemTheme">Toggle System Theme</Button>
```

Then use this methods to change theme.

```csharp
public partial class Index
{
    Theme themeProvider;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            await themeProvider.SetTheme("theme-classic-light");
        }

    }

    private async Task ToggleTheme()
    {
        await themeProvider.ToggleTheme();
    }

    private async Task SetDarkTheme()
    {
        await themeProvider.SetTheme("theme-classic-dark");
    }

    private async Task ToggleSystemTheme()
    {
        await themeProvider.ToggleSystemTheme(true);
    }
}
```

### Supported Components

- [Application](#application) **(since 0.4.8)**
- [Application Header](#application-header) **(since 0.4.8)**
- [Basic Navigation](#basic-navigation)
- [Menu](#menu)
- [About and Legal](#about-and-legal)
- [Menu Settings](#menu-settings)
- [Map Navigation](#map-navigation)
- [Popover News](#popover-news)
- [AGGrid (Preview)](#aggrid-preview) **(since 0.4.2)**
- [Avatar](#avatar) **(since v0.4.0)**
- [Blind](#blind)
- [Breadcrumb](#breadcrumb)
- [Button](#button)
- [Card](#card) **(since 0.5.0)**
- [Card List](#card-list) **(since v0.3.3)**
- [Push Card](#push-card) **(since v0.3.3)**
- [Action Card](#action-card) **(since v0.3.3)**
- [Icon Button](#icon-button)
- [Category Filter](#category-filter)
- [ECharts](#echarts) **(since v0.3.2)**
- [Checkbox](#checkbox)
- [Chip](#chip)
- [Content](#content) **(since 0.5.0)**
- [Content Header](#content-header) **(since v0.3.3)**
- [Date Dropdown](#date-dropdown)
- [Date Picker](#date-picker)
- [Date Time Picker](#date-time-picker) **(since 0.5.0)**
- [Divider](#divider)
- [Drawer](#drawer)
- [Dropdown Button](#dropdown-button)
- [Dropdown](#dropdown)
- [Dropdown Header](#dropdown-header)
- [Empty State](#empty-state) **(since v0.3.3)**
- [Event List](#event-list)
- [Expanding Search](#expanding-search)
- [Flip](#flip)
- [Group](#group)
- [HTML Table](#html-table)
- [Input](#input)
- [Key Value List](#key-value-list) **(since v0.3.3)**
- [Key Value](#key-value) **(since v0.3.3)**
- [KPI](#kpi)
- [Layout Grid](#layout-grid) **(since v0.4.0)**
- [Link Button](#link-button) **(since v0.4.0)**
- [Message Bar](#message-bar)
- [Modal](#modal)
- [Pane](#pane) **(since 0.5.0)**
- [Pagination](#pagination)
- [Pill](#pill)
- [Radio Button](#radio-button)
- [Select](#select)
- [Slider](#slider)
- [Spinner](#spinner)
- [Split Button](#split-button)
- [Tabs](#tabs)
- [Text Area](#text-area)
- [Tile](#tile)
- [Time Picker](#time-picker)
- [Toast](#toast)
- [Toggle Buttons](#toggle-buttons) **(since v0.4.0)**
- [Toggle](#toggle)
- [Tooltip](#tooltip)
- [Tree](#tree)
- [Typography](#typography)
- [Upload](#upload)
- [Validation Tooltip - Form Validation](#validation-tooltip-form-validation)
- [Workflow](#workflow)

## Application

```razor
<Application @ref="_app">
    <ApplicationHeader Name="My Application">
        <placeholder-logo slot="logo"></placeholder-logo>
    </ApplicationHeader>
    <Menu>
        <MenuItem>Item 1</MenuItem>
        <MenuItem>Item 2</MenuItem>
    </Menu>

    <ix-content>
        <ContentHeader
            Slot="header"
            HeaderTitle="My Content Page"
        >
        </ContentHeader>
    </ix-content>
</Application>
```

```csharp
Application _app;

// Set the app switch config when the component is rendered.
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if(firstRender)
    {
        AppSwitchConfig config = new()
        {
            CurrentAppId = "1",
            Apps =
            [
                new App()
                {
                    Id = "App1",
                    Name = "App 1",
                    Description = "Awesome app",
                    Url = "app1",
                    Target = "_self",
                    IconSrc = "..."
                }
            ]
        }

        _app.AppSwitchConfig = config;
    }
}
```

## Application Header

```razor
<ApplicationHeader Name="My Application">
    <placeholder-logo slot="logo"></placeholder-logo>
</ApplicationHeader>
```

## Basic Navigation

```razor
<BasicNavigation ApplicationName="Application name">
  <placeholder-logo slot="logo"></placeholder-logo>
  <Menu Id="nav-menu-1">
    <MenuItem>Item 1</MenuItem>
    <MenuItem>Item 2</MenuItem>
  </Menu>
</BasicNavigation>
```

## Menu

```razor
<Menu Id="nav-menu-1">
  <MenuItem Home="true" TabIcon="home"> Home </MenuItem>
  <MenuItem TabIcon="globe"> Normal Tab </MenuItem>
  <MenuItem TabIcon="star" disabled> Disabled tab </MenuItem>
  <MenuItem TabIcon="star"> With other icon </MenuItem>
  <MenuItem TabIcon="globe" Style="display: none">
    Should not be visible
  </MenuItem>
</Menu>
```

```razor
@* Menu Category *@
<BasicNavigation>
  <Menu>
    <MenuItem Home="true" Icon="home">Home</MenuItem>
    <MenuItem Icon="globe">Normal Tab</MenuItem>
    <MenuCategory Label="Top level Category" Icon="rocket">
      <MenuItem Icon="globe">Nested Tab</MenuItem>
      <MenuItem Icon="globe">Nested Tab</MenuItem>
    </MenuCategory>
  </Menu>
</BasicNavigation>
```

```razor
@* Menu Avatar *@
<Menu Id="nav-menu-1">
  <MenuAvatar Id="nav-avatar-menu-1" Image="https://ui-avatars.com/api/?name=John+Doe">
    <MenuAvatarItem Id="nav-avatar-item-1" Label="Option 1"></MenuAvatarItem>
  </MenuAvatar>
  <MenuItem Home="true" TabIcon="home"> Home </MenuItem>
  <MenuItem TabIcon="globe"> Normal Tab </MenuItem>
  <MenuItem TabIcon="star" Disabled="true"> Disabled tab </MenuItem>
  <MenuItem TabIcon="star"> With other icon </MenuItem>
  <MenuItem TabIcon="globe" Style="display: none">
    Should not be visible
  </MenuItem>
</Menu>
```

## About and Legal

```razor
<BasicNavigation>
  <placeholder-logo slot="logo"></placeholder-logo>
  <Menu Id="nav-menu-1">
    <MenuAbout @ref="menuAboutElement">
      <MenuAboutItem Label="Tab 1">Content 1</MenuAboutItem>
      <MenuAboutItem Label="Tab 2">Content 2</MenuAboutItem>
    </MenuAbout>
  </Menu>
</BasicNavigation>
```

```csharp
MenuAbout menuAboutElement;

menuAboutElement.ToggleAbout(true);
```

## Menu Settings

```razor
<BasicNavigation>
  <placeholder-logo slot="logo"></placeholder-logo>
  <Menu Id="nav-menu-1">
    <MenuSettings @ref="settingsMenuElement">
      <MenuSettingsItem
        Label="Example Setting 1"
      ></MenuSettingsItem>
      <MenuSettingsItem
        Label="Example Setting 2"
      ></MenuSettingsItem>
    </MenuSettings>
  </Menu>
</BasicNavigation>
```

```csharp
SettingsMenu settingsMenuElement;

menuAboutElement.ToggleSettings(true);
```

## Map Navigation

```razor
<MapNavigation
  ApplicationName="Test Application"
  NavigationTitle="Some other content"
>
  <placeholder-logo slot="logo"></placeholder-logo>
  <Menu Id="nav-menu-1">
    <MenuItem>Item 1</MenuItem>
    <MenuItem>Item 2</MenuItem>
    <MenuItem>Item 3</MenuItem>
  </Menu>
  <div slot="sidebar-content">Sidebar content</div>
  <div>Content</div>
</MapNavigation>
```
## Map Navigation Overlay

```razor
<MapNavigationOverlay  Id="overlay"></MapNavigationOverlay>
```

## Popover News

```razor
<BasicNavigation>
  <placeholder-logo slot="logo"></placeholder-logo>
  <Menu Id="nav-menu-1">
    <MenuAbout>
      <MenuAboutItem Label="Example"> </MenuAboutItem>
    </MenuAbout>
    <MenuAboutNews Label="Test" Show="true" AboutItemLabel="Example">
      Test
    </MenuAboutNews>
  </Menu>
</BasicNavigation>
```

## AGGrid Preview

This component is currently in **preview** version.

### Installation

Add necessary css files into the `index.html` file.

```html
<!-- Include the core CSS, this is needed by the grid -->
<link
  rel="stylesheet"
  href="https://cdn.jsdelivr.net/npm/ag-grid-community/styles/ag-grid.css"
/>

<!-- Include the theme CSS, only need to import the theme you are going to use -->
<link
  rel="stylesheet"
  href="https://cdn.jsdelivr.net/npm/ag-grid-community/styles/ag-theme-alpine.css"
/>

<link
  rel="stylesheet"
  href="_content/Siemens.IX.Blazor/css/siemens-ix/ix-aggrid.css"
/>
```

```razor
<AGGrid
    @ref="agGridRef"
    Id="grid1"
    Class="ag-theme-alpine-dark ag-theme-ix"
    Style="height: 150px; width: 600px">
</AGGrid>
```

```csharp
AGGrid agGridRef;
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if(firstRender)
    {
        Dictionary<string, dynamic> row1 = new()
        {
            { "type", "Equipment" },
            { "status", "Normal" },
            { "hwVersion", "2.0" },
            { "checked", false }
        };

        Dictionary<string, dynamic> row2 = new()
        {
            { "type", "Positioner" },
            { "status", "Maintenance" },
            { "hwVersion", "1.0" },
            { "checked", true }
        };

        Dictionary<string, dynamic> row3 = new()
        {
            { "type", "Pressure sensor" },
            { "status", "Unknown" },
            { "hwVersion", "N/A" },
            { "checked", false }
        };


        GridOptions options = new GridOptions()
        {
            ColumnDefs = new List<ColumnDefs>
            {
                new ColumnDefs()
                {
                    Field = "type",
                    HeaderName = "Type",
                    Resizable = true,
                    CheckboxSelection = true
                },
                new ColumnDefs()
                {
                    Field = "status",
                    HeaderName = "Status",
                    Resizable = true,
                    Sortable = true,
                    Filter = true
                },
                new ColumnDefs()
                {
                    Field = "hwVersion",
                    HeaderName = "HW version",
                    Resizable= true
                }
            },
            RowData = new List<Dictionary<string, dynamic>> { row1, row2, row3 },
            CheckboxSelection = true,
            RowSelection = "multiple",
            SuppressCellFocus = true
        };

        await agGridRef.CreateGrid(options);
    }

}
```

## Avatar

```razor
<Avatar
    Image="https://ui-avatars.com/api/?name=John+Doe">
</Avatar>
```

## Blind

```razor
<Blind
    Label="Test Blind"
    Id="blind1"
    CollapsedChangedEvent="(value) => BlindEventHandler(value)">
Test content
</Blind>
```

## Breadcrumb

```razor
<Breadcrumb Id="breadcrumb-1"
            Class="editor-breadcrumb"
            ItemClicked="(label) => ClickedOnBreadcrumb(label)">
    <BreadcrumbItem Label="Item 1"></BreadcrumbItem>
    <BreadcrumbItem Label="Item 2"></BreadcrumbItem>
</Breadcrumb>
```

## Button

```razor
<Button>Test Button</Button>
```

## Card

```razor
<Card Variant="CardVariant.info">
    <ix-icon name="capacity"></ix-icon>
    <ix-typography bold>Number of components</ix-typography>
    <ix-typography>
        Item 1<br />
        Item 2<br />
        Item 3
    </ix-typography>
    <ix-typography format="h1">3</ix-typography>
</Card>
```

## Card List

```razor
<CardList Id="carlist1" Label="Stack Layout" ShowAllCount="12" ListStyle="Enums.CardList.CardListStyle.Stack" CollapseChangedEvent="CardListCollapsedChanged"
ShowAllClickEvent="CardListShowAllClicked" ShowMoreCardClickEvent="CardListShowMoreClicked">
    <PushCard Icon="rocket"
              Notification="3"
              Heading="Heading content"
              SubHeading="Subheading"
              Variant="PushCardVariant.outline"></PushCard>
    <PushCard Icon="bulb"
              Notification="1"
              Heading="Heading content"
              SubHeading="Subheading"
              Variant="PushCardVariant.warning"></PushCard>
    <PushCard Icon="rocket"
              Notification="3"
              Heading="Heading content"
              SubHeading="Subheading"
              Variant="PushCardVariant.success"></PushCard>
</CardList>
```

## Push Card

```razor
<PushCard Icon="rocket"
        Notification="3"
        Heading="Heading content"
        SubHeading="Subheading"
        Variant="PushCardVariant.outline"></PushCard>
```

## Action Card

```razor
<ActionCard
      Icon="refresh"
      Heading="Scan for new devices"
      SubHeading="Secondary text"
      Variant="PushCardVariant.filled"
></ActionCard>
```

## Icon Button

```razor
<IconButton Icon="info"></IconButton>
```

## Category filter

```razor
<CategoryFilter
    @ref="categoryFilter"
    Id="category-filter-1"
    Placeholder="Filter by"
    RepeatCategories="false"
    FilterChangedEvent="FilterStateChanged"
    InputChangedEvent="InputStateChanged">
</CategoryFilter>
```

```csharp
CategoryFilter categoryFilter;
Dictionary<string, Category> categoriesDict;
FilterState filterState;

protected override void OnAfterRender(bool firstRender)
    {
        if(firstRender)
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
    }
```

## ECharts

```razor
<ECharts Id="chart1" @ref="chart1">
</ECharts>
```

```csharp
ECharts chart1;

// Create the dynamic object
var dynamicObject = new Dictionary<string, object>();

// Create the tooltip object
var tooltip = new Dictionary<string, object>
{
    { "trigger", "axis" },
    { "axisPointer", new Dictionary<string, object> { { "type", "shadow" } } }
};
dynamicObject.Add("tooltip", tooltip);

// Create the legend object
dynamicObject.Add("legend", new Dictionary<string, object>());

// Create the grid object
var grid = new Dictionary<string, object>
{
    { "left", "3%" },
    { "right", "4%" },
    { "bottom", "3%" },
    { "containLabel", true }
};
dynamicObject.Add("grid", grid);

// Create the xAxis object
var xAxis = new List<Dictionary<string, object>>
{
    new Dictionary<string, object>
    {
        { "type", "category" },
        { "data", new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" } }
    }
};
dynamicObject.Add("xAxis", xAxis);

// Create the yAxis object
var yAxis = new List<Dictionary<string, object>>
{
    new Dictionary<string, object> { { "type", "value" } }
};
dynamicObject.Add("yAxis", yAxis);

// Create the series object
var series = new List<Dictionary<string, object>>
{
    new Dictionary<string, object>
    {
        { "name", "Direct" },
        { "type", "bar" },
        { "stack", "Ad" },
        { "emphasis", new Dictionary<string, object> { { "focus", "series" } } },
        { "data", new List<int> { 320, 332, 301, 334, 390, 330, 320 } }
    },
    new Dictionary<string, object>
    {
        { "name", "Email" },
        { "type", "bar" },
        { "emphasis", new Dictionary<string, object> { { "focus", "series" } } },
        { "data", new List<int> { 120, 132, 101, 134, 90, 230, 210 } }
    },
    new Dictionary<string, object>
    {
        { "name", "Union Ads" },
        { "type", "bar" },
        { "emphasis", new Dictionary<string, object> { { "focus", "series" } } },
        { "data", new List<int> { 220, 182, 191, 234, 290, 330, 310 } }
    },
    // Add more series objects as needed
};
dynamicObject.Add("series", series);

chart1.InitialChart(object1);
```

## Checkbox

```razor
<div style="margin-bottom: 1rem">
  <input type="checkbox" id="checkbox_01" />
  <label for="checkbox_01">Simple checkbox</label>
</div>

<div>
  <input type="checkbox" id="checkbox_02" disabled />
  <label for="checkbox_02">Disabled checkbox</label>
</div>
```

## Chip

```razor
<Chip Icon="print"
      Label="Chip with icon"
      Id="chip1"
      Closable="true"
      TooltipText="Tooltip Text"
      ClosedEvent="@ChipClosedEventHandler">
</Chip>
```

##Content

```razor
<Content>
    <ContentHeader Id="myheader" HeaderTitle="My Content Page" />
    Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et
    accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,
    sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren,
    no sea takimata sanctus est Lorem ipsum dolor sit amet.
</Content>
```

## Content Header

```razor
<ContentHeader Id="content-header-1" HasBackButton="true"
            HeaderTitle="Content title"
            HeaderSubTitle="Subtitle"
            BackButtonClickedEvent="ContentHeaderBackButtonClicked">
    Test
</ContentHeader>
```

## Date Dropdown

```razor
<DateDropdown Id="datedropdown1" DateRangeId="last-7" Format="MM/dd/yyyy" DateRangeOptions="_dateRangeOptions" DateRangeChangeEvent="Callback"></DateDropdown>
```

```csharp
    readonly DateDropdownOption[] _dateRangeOptions =
{
    new()
    {
        Id = "last-7",
        Label = "Last 7 days",
        From = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy"),
        To = DateTime.Today.ToString("MM/dd/yyyy")
    },
    new()
    {
        Id = "today",
        Label = "Today",
        From = DateTime.Today.ToString("MM/dd/yyyy"),
        To = DateTime.Today.ToString("MM/dd/yyyy")
    }
};

private void Callback(DateDropdownResponse selectedDateDropdown)
{
    Console.WriteLine(selectedDateDropdown.Id);
}
```

## Date picker

```razor
<DatePicker From="2023/02/01"
            To="2023/02/15"
            Id="timepicker1"
            DateChangeEvent="(date) => DateChangeEventTest(date)">
</DatePicker>
```

## Date time picker

```razor
<DateTimePicker
        DateChangeEvent="(date) => DateChangeEventTest(date)"
        From="2023/02/01"
        To="2023/02/15"
        Id="datetimepicker1"
        TimeChangeEvent="(date) => DateChangeEventTest(date)">
</DateTimePicker>
```

## Divider

```razor
<Divider></Divider>
```

## Drawer

```razor
<Button ClickEvent="DrawerButtonClicked">Drawer Button</Button>
<Drawer @ref="drawer1" Id="drawer1">
    <span>Some content of drawer</span>
</Drawer>
```

```csharp
Drawer drawer1;

protected override void OnAfterRender(bool firstRender)
{
    if(firstRender)
    {
        drawer1.FullHeight = true;
        drawer1.CloseOnClickOutside = true;
    }
}

private void DrawerButtonClicked()
{
    drawer1.Show = !drawer1.Show;
}
```

## Dropdown Button

```razor
<DropdownButton Label="Dropdown" Variant="Primary" Icon="checkboxes">
    <DropdownItem Label="Item 1" Value="1"></DropdownItem>
    <DropdownItem Label="Item 2" Value="2"></DropdownItem>
    <DropdownItem Label="Item 3" Value="3"></DropdownItem>
</DropdownButton>
```

## Dropdown

```razor
<Button Id="triggerId">Open</Button>
<Dropdown Trigger="triggerId">
  <DropdownItem Label="Item 1"></DropdownItem>
  <DropdownItem Label="Item 2"></DropdownItem>
  <DropdownItem Label="Item 3"></DropdownItem>
</Dropdown>
```

## Dropdown Header

```razor
<Button Id="triggerId">Open</Button>
<Dropdown Trigger="triggerId">
  <DropdownHeader Label="Category"></DropdownHeader>
  <DropdownItem Label="Item 1"></DropdownItem>
  <DropdownItem Label="Item 2"></DropdownItem>
  <DropdownItem Label="Item 3"></DropdownItem>
</Dropdown>
```

## Empty State

```razor
<EmptyState
  Header="No elements available"
  SubHeader="Create an element first"
  Icon="add"
  Action="Create element"
></EmptyState>
```

## Event list

```razor
<EventList>
    <EventListItem Id="event-list-item-1" Label="Item 1" ItemCLickEvent="(label) => DropdownButtonClicked(label)"></EventListItem>
    <EventListItem Id="event-list-item-2" Label="Item 2" ItemCLickEvent="(label) => DropdownButtonClicked(label)"></EventListItem>
    <EventListItem Id="event-list-item-3" Label="Item 3" ItemCLickEvent="(label) => DropdownButtonClicked(label)"></EventListItem>
</EventList>
```

## Expanding search

```razor
<ExpandingSearch Id="exp-search"
                 ValueChangedEvent="(value) => SearchValueChanged(value)">
</ExpandingSearch>
```

## Flip

```razor
<FlipTile>
    <div slot="header">Flip header</div>

    <div slot="footer">
      <div>Predicted maintenance date</div>
      <div class="d-flex align-items-center">
        <ix-icon name="info" size="16"></ix-icon>2021-06-22
      </div>
    </div>

    <FlipTileContent> Example 1 </FlipTileContent>
    <FlipTileContent> Example 2 </FlipTileContent>
</FlipTile>
```

- [ ] AG grid

## Group

```razor
<Group Id="group1" Header="Header text" SubHeader="Subheader text">
    <GroupItem Id="groupitem1" Text="Example text 1"></GroupItem>
    <GroupItem Id="groupitem2" Text="Example text 2"></GroupItem>
    <GroupItem Id="groupitem3" Text="Example text 3" SelectedChangeEvent="GroupItemSelectedChanged"></GroupItem>
</Group>
```

## HTML table

```razor
<table class="table">
  <thead>
    <tr>
      <th scope="col">#</th>
      <th scope="col">First</th>
      <th scope="col">Last</th>
      <th scope="col">Handle</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th scope="row">1</th>
      <td>Mark</td>
      <td>Otto</td>
      <td>@mdo</td>
    </tr>
    <tr>
      <th scope="row">2</th>
      <td>Jacob</td>
      <td>Thornton</td>
      <td>@fat</td>
    </tr>
    <tr>
      <th scope="row">3</th>
      <td colspan="2">Larry the Bird</td>
      <td>@twitter</td>
    </tr>
  </tbody>
</table>
```

## Input

```razor
<form class="needs-validation m-2">
  <input
    class="form-control"
    defaultValue="Some example text"
    placeholder="Enter text here"
    type="text"
  />
</form>
```

## Key Value List

```razor
<KeyValueList>
  <KeyValue
    Label="Label"
    LabelPosition="left"
    Value="Value"
  ></KeyValue>

  <KeyValue
    Label="Label"
    LabelPosition="left"
    Value="Value"
  ></KeyValue>

  <KeyValue
    Label="Label"
    LabelPosition="left"
    Value="Value"
  ></KeyValue>
</KeyValueList>
```

## Key Value

```razor
<KeyValue Label="Label">
  <input
    class="form-control"
    placeholder="Enter text here"
    type="text"
    slot="custom-value"
  />
</KeyValue>
```

## KPI

```razor
<KPI Label="Motor speed" Value="Nominal"></KPI>
```

## Layout Grid

```razor
<LayoutGrid>
  <Row>
    <Col><ix-typography format="display">1</ix-typography></Col>
    <Col><ix-typography format="display">2</ix-typography></Col>
    <Col><ix-typography format="display">3</ix-typography></Col>
    <Col><ix-typography format="display">4</ix-typography></Col>
    <Col><ix-typography format="display">5</ix-typography></Col>
    <Col><ix-typography format="display">6</ix-typography></Col>
  </Row>
</LayoutGrid>
```

## Link Button

```razor
<LinkButton Url="https://ix.siemens.io/">Siemens IX</LinkButton>
```

## Message bar

```razor
<MessageBar ClosedChangeEvent="MessageboxClosed" Id="messagebar1" Type="MessageBarType.Info">
    <div class="d-flex align-items-center justify-content-between">
        Message text <ix-button>Action</ix-button>
    </div>
</MessageBar>
```

## Modal

```razor
<div class="@modalClass" style="display:@modalDisplay">
    <div class="modal-header">
        Message headline
        <IconButton
            Ghost="true"
            Icon="close"
            Class="dismiss-modal"
            ClickEvent="() => CloseModal()"
        ></IconButton>
    </div>
    <div class="modal-body">Message text lorem ipsum</div>
    <div class="modal-footer">
        <Button Outline="true"
                Class="dismiss-modal"
                ClickEvent="() => CloseModal()">
             Cancel
        </Button>
        <Button ClickEvent="() => CloseModal()"
                Class="close-modal">
             OK
        </Button>
    </div>
</div>
```

```csharp
string modalClass = "";
string modalDisplay = "none;";

private void OpenModal()
{
    modalDisplay = "block;";
    modalClass = "show";
}

private void CloseModal()
{
    modalDisplay = "none;";
    modalClass = "";
}
```

## Pagination

```razor
<Pagination Id="pagination-1"
    Advanced="true"
    Count="100"
    ItemCountChangedEvent="PaginationItemCountChanged"
    PageSelectedEvent="PaginationPageSelected">
</Pagination>
```

## Pane

```razor
<PaneLayout Id="pane-layout"
                Variant="@PaneVariant.floating"
                Layout="full-horizontal"
                Borderless="true">
    <Pane Id="pane1" Heading="Pane Left" Slot="left" Size="33%">
        <p>This is the left pane.</p>
    </Pane>

    <Pane Id="pane2"  Heading="Pane Top" Slot="top" Size="33%">
        <p>This is the top pane.</p>
    </Pane>

    <Pane Id="pane3" Heading="Pane Right" Slot="right" Size="33%">
        <p>This is the right pane.</p>
    </Pane>

    <Pane Id="pane4" Heading="Pane Bottom" Slot="bottom" Size="33%">
        <p>This is the bottom pane.</p>
    <Pane>
</PaneLayout>
```

## Pill

```razor
<Pill Variant="PillVariant.custom" PillColor="white" Background="purple" TooltipText="Tooltip Text">
    Label
</Pill>
```

## Radio button

```razor
<div class="example-list">
  <input checked id="checkbox_1_1" name="group_1" type="radio" />
  <label for="checkbox_1_1"> Checked </label>

  <input id="checkbox_1_2" name="group_1" type="radio" />
  <label for="checkbox_1_2"> Normal </label>

  <input disabled id="checkbox_1_3" name="group_1" type="radio" />
  <label for="checkbox_1_3"> Disabled </label>
</div>
```

## Select

```razor
<Select ItemSelectionChangeEvent=SelectItemSelectedChanged
AddItemEvent="SelectItemAdded" Mode="SelectMode.Single" SelectedIndices="2" Id="select1">
    <SelectItem Id="selectItem1" Label="Item 1" Value="1"></SelectItem>
    <SelectItem Id="selectItem2" Label="Item 2" Value="2"></SelectItem>
</Select>
```
## Slider

```razor
<Slider Id="slider-demo" Min="0" Max="50" Step="5" Value="0" Marker="[0, 10, 20, 30, 40, 50]">
    <span slot="label-start">0</span>
    <span slot="label-end">50</span>
</Slider>
```

## Spinner

```razor
<Spinner></Spinner>
```

## Split button

```razor
<SplitButton Id="split-button-1"
             Label="Split Button"
             SplitIcon="chevron-down-small"
             ButtonClickedEvent="SplitButtonClicked">
</SplitButton>
```

## Tabs

```razor
<div class="example">
    <Tabs Id="tabs-demo">
        <ix-tab-item data-tab-id="0">Tab 1</ix-tab-item>
        <ix-tab-item data-tab-id="1">Tab 2</ix-tab-item>
        <ix-tab-item data-tab-id="2">Tab 3</ix-tab-item>
    </Tabs>
    <div data-tab-content="0" class="show">Content Tab 1</div>
    <div data-tab-content="1">Content Tab 2</div>
    <div data-tab-content="2">Content Tab 3</div>
</div>
```

```css
.example {
  display: block;
  position: relative;
  width: 100%;
}

div[data-tab-content] {
  display: none;
}

div[data-tab-content].show {
  display: block;
}
```

## Text area

```razor
<textarea class="form-control" placeholder="Enter text here"></textarea>
```

## Tile

```razor
<Tile Size="TileSize.Medium" Class="mr-1">
    <div slot="header">Tile header</div>
    <div class="text-l">92.8 °C</div>
</Tile>
```

## Time picker

```razor
<TimePicker></TimePicker>
```

## Toast

```razor
<Toast @ref="toast"></Toast>
```

```csharp
private Toast toast;

ToastConfig config = new ToastConfig()
{
    Message = "Test message",
    Type = "info"
}

toast.ShowToast("test message", "info");
```

## Toggle Buttons

```razor
<ToggleButton>Normal</ToggleButton>
<ToggleButton Id="toggle-btn-1" Pressed="true">Pressed</ToggleButton>

<IconToggleButton Outline="true" Icon="checkboxes"></IconToggleButton>
<IconToggleButton
    Outline="true"
    Icon="checkboxes"
    Pressed="true"
></IconToggleButton>
```

## Toggle

```razor
<Toggle></Toggle>
```

## Tooltip

```razor
<Button class="any-class" aria-describedby="tooltip-1">
    Save
</Button>
<Tooltip Id="tooltip-1" For=".any-class">
    When you click, all changes will be saved
</Tooltip>
```

## Validation Tooltip - Form Validation

```razor
<form class="needs-validation" novalidate @onsubmit="()=>{}">
	<ValidationTooltip Message="Cannot be empty!" Placement="ValidationTooltipPlacement.Top">
		<label for="validationCustom01">Name</label>
		<input id="validationCustom01" value="" required />
	</ValidationTooltip>
	<Button Type="ButtonType.Submit">Submit</Button>
</form>
```

## Tree

```razor
<div style="height: 8rem; width: 100%">
    <Tree Id="tree-1" Root="root" ContextChangedEvent="TreeContextChangeEvent"
    NodeClickedEvent="TreeNodeClicked"
    NodeRemovedEvent="NodeRemoved"
    NodeToggledEvent="TreeNodeToggled"
    @ref="tree"></Tree>
</div>
```

```csharp
Tree tree;

Dictionary<string, TreeNode> treeNodes = new();

treeNodes.Add("root", new TreeNode()
{
    Id = "root",
    HasChildren = true,
    Children = new List<string>(){"sample"}
});

treeNodes.Add("sample", new TreeNode()
{
    Id = "sample",
    Data = new TreeData()
    {
        Name = "Sample"
    },
    HasChildren = true,
    Children = new List<string>(){"sample-child-1", "sample-child-2"}
});

treeNodes.Add("sample-child-1", new TreeNode()
{
    Id = "sample-child-1",
    Data = new TreeData()
    {
        Name = "Sample Child 1"
    },
    HasChildren = false,
    Children = new List<string>() {}
});

treeNodes.Add("sample-child-2", new TreeNode()
    {
        Id = "sample-child-2",
        Data = new TreeData()
        {
            Name = "Sample Child 2"
        },
        HasChildren = false,
        Children = new List<string>() { }
    });


tree.TreeModel = treeNodes;
```

## Typography

```razor
<Typography Format="TypographyFormat.Label" TextColor="TypographyColor.Std" TextDecoration="TextDecoration.None">Label, Std, None</Typography>
<Typography Bold="true" Format="TypographyFormat.Code_Lg" TextColor="TypographyColor.Contrast" TextDecoration="TextDecoration.Line_Through">Bold, Code_Lg, Contrast, Line_Through</Typography>
```

## Upload

```razor
<Upload Id="file-upload-test"
        FileChangedEvent="(data) => FileChanged(data)">
</Upload>
```

## Workflow

```razor
<WorkflowSteps Id="wf-steps" StepSelectedEvent="(index) => WfSelectedEvent(index)">
    <WorkflowStep Status="WorkflowStatus.Done">Step 1</WorkflowStep>
    <WorkflowStep Status="WorkflowStatus.Success">Step 2</WorkflowStep>
    <WorkflowStep Status="WorkflowStatus.Open">Step 3</WorkflowStep>
    <WorkflowStep Status="WorkflowStatus.Warning">Step 4</WorkflowStep>
    <WorkflowStep Status="WorkflowStatus.Error">Step 5</WorkflowStep>
    <WorkflowStep Disabled="true">Step 6</WorkflowStep>
</WorkflowSteps>
```

### Usage

You can follow the original documentation and use native `Javascript` components.

```razor
<ix-button class="m-1" outline variant="Secondary">
    Button
</ix-button>
```

Or you can use supported components as a native `Blazor` components.

```razor
<Button Class="m-1" Outline="true" Variant="Secondary">
    Button
</Button>
```

If you want to use native `siemens-ix` html elements, you have to handle events over `javascript interops`.

## 📝 License

Copyright © 2025 [Siemens AG](https://www.siemens.com/).

[Siemens Third-Party Software Disclosure Document](/docs/Siemens.IX.Blazor__0.5.3__READMEOSS.html)

This project is MIT licensed.
