<!--
SPDX-FileCopyrightText: 2025 Siemens AG

SPDX-License-Identifier: MIT
-->

## Siemens IX for Blazor

## Installation

Install the `Siemens.IX.Blazor` package from [NuGet](https://www.nuget.org/packages/Siemens.IX.Blazor).

## .NET CLI

```cmd
dotnet add package Siemens.IX.Blazor
```

## Package Manager

```cmd
NuGet\Install-Package Siemens.IX.Blazor
```

Add the required stylesheet and JavaScript bundle to `index.html`.

```html
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Blazor App</title>

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
> When using this library with a Blazor Web App, set the render mode to `InteractiveServer`.
> Learn more about [ASP.NET Core Blazor render modes](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes?view=aspnetcore-10.0).

### Theme Switching

**Since v0.3.9**

Add the `Theme` component to any page where the theme can be changed.

```razor
<Theme @ref="themeProvider"></Theme>

<Button ClickEvent="SetClassicTheme">Set Classic Theme</Button>
<Button ClickEvent="ToggleTheme">Toggle Theme</Button>
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
            await themeProvider.SetTheme("classic");
        }

    }

    private async Task ToggleTheme()
    {
        await themeProvider.ToggleTheme();
    }

    private async Task SetClassicTheme()
    {
        await themeProvider.SetTheme("classic");
    }
}
```

In iX v5, configure the theme and `Application.ColorSchema` (`Light`, `Dark`, or `System`) separately. Use a theme such as `classic` instead of the legacy `theme-classic-light` or `theme-classic-dark` values.

### Supported Components

- [Application](#application) **(since 0.4.8)**
- [Application Header](#application-header) **(since 0.4.8)**
- [Basic Navigation](#basic-navigation)
- [Menu](#menu)
- [About and Legal](#about-and-legal)
- [Menu Settings](#menu-settings)
- [Popover News](#popover-news)
- [AG Grid](#ag-grid) **(since v0.6.0)**
- [Avatar](#avatar) **(since v0.4.0)**
- [Badge](#badge) **(since v0.6.0)**
- [Blind](#blind)
- [Breadcrumb](#breadcrumb)
- [Button](#button)
- [Card](#card) **(since 0.5.0)**
- [Card List](#card-list) **(since v0.3.3)**
- [Chat](#chat) **(since v0.6.0)**
- [Push Card](#push-card) **(since v0.3.3)**
- [Action Card](#action-card) **(since v0.3.3)**
- [Icon Button](#icon-button)
- [Category Filter](#category-filter)
- [ECharts](#echarts) **(since v0.3.2)**
- [Checkbox](#checkbox)
- [Checkbox group](#checkbox-group)
- [Chip](#chip)
- [Content](#content) **(since 0.5.0)**
- [Content Header](#content-header) **(since v0.3.3)**
- [Date Dropdown](#date-dropdown)
- [Date Picker](#date-picker)
- [Date Input](#date-input)
- [Date Time Input](#date-time-input)
- [Date Time Picker](#date-time-picker) **(since 0.5.0)**
- [Range Field](#range-field)
- [Divider](#divider)
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
- [Number Input](#number-input)
- [Custom Field](#custom-field)
- [Field Label](#field-label)
- [Helper Text](#helper-text)
- [Key Value List](#key-value-list) **(since v0.3.3)**
- [Key Value](#key-value) **(since v0.3.3)**
- [KPI](#kpi)
- [Layout Grid](#layout-grid) **(since v0.4.0)**
- [Layout Auto](#layout-auto)
- [Link Button](#link-button) **(since v0.4.0)**
- [Message Bar](#message-bar)
- [Modal](#modal)
- [Popover](#popover)
- [Pane](#pane) **(since 0.5.0)**
- [Pagination](#pagination)
- [Pill](#pill)
- [Progress Indicator](#progress-indicator) **(since 0.5.4)**
- [Radio Button](#radio-button)
- [Radio group](#radio-group)
- [Select](#select)
- [Slider](#slider)
- [Spinner](#spinner)
- [Split Button](#split-button)
- [Tabs](#tabs)
- [Text Area](#text-area)
- [Tile](#tile)
- [Time Picker](#time-picker)
- [Time Input](#time-input)
- [Toast](#toast)
- [Toggle Buttons](#toggle-buttons) **(since v0.4.0)**
- [Toggle](#toggle)
- [Tooltip](#tooltip)
- [Tree](#tree)
- [Typography](#typography)
- [Upload](#upload)
- [Workflow](#workflow)

## Application

```razor
<Application Id="application" @ref="_app" Theme="classic" ColorSchema="System">
    <ApplicationHeader Id="application-header" Name="My Application">
        <Logo>
            <placeholder-logo></placeholder-logo>
        </Logo>
        <Button Variant="tertiary">Header action</Button>
        <ix-avatar slot="ix-application-header-avatar"
                   initials="JD"
                   username="Jane Doe"
                   extra="Product Engineering"></ix-avatar>
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
<ApplicationHeader Id="application-header" Name="My Application">
    <Logo>
        <placeholder-logo></placeholder-logo>
    </Logo>
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
<Menu Id="nav-menu-1"
      ApplicationName="Application"
      ApplicationDescription="Application description"
      ShowAbout="true"
      ShowSettings="true"
      I18nAriaLabelMenu="Application menu"
      I18nNavigationHint="Use arrow keys to navigate"
      @ref="menu">
  <MenuItem Home="true" Icon="home" Label="Home" />
  <MenuItem Icon="globe" Label="Overview" Href="/overview" />
  <MenuCategory Label="Administration" Icon="cogwheel">
    <MenuItem Icon="user" Label="Users" Href="/users" />
    <MenuItem Icon="lock" Label="Permissions" Href="/permissions" />
  </MenuCategory>
  <MenuItem Slot="bottom" Icon="info" Label="Help" Href="/help" />
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
    <MenuAvatarItem Id="nav-avatar-item-1" Icon="user" Label="Profile" />
  </MenuAvatar>
  <MenuItem Home="true" Icon="home" Label="Home" />
  <MenuItem Icon="globe" Label="Normal tab" />
  <MenuItem Icon="star" Label="Disabled tab" Disabled="true" />
</Menu>
```

## About and Legal

```razor
<BasicNavigation>
  <placeholder-logo slot="logo"></placeholder-logo>
  <Menu Id="nav-menu-1">
    <MenuAbout Id="about" SuppressLegacyTabs="true">
      <Tabs Id="about-tabs" ActiveTabKey="legal">
        <TabItem TabKey="legal" Label="Legal" />
        <TabItem TabKey="licenses" Label="Licenses" />
      </Tabs>
      <section role="tabpanel" aria-label="About and legal content">
        Legal information
      </section>
    </MenuAbout>
  </Menu>
</BasicNavigation>
```

```csharp
Menu? menu;

await menu.ToggleAboutAsync(true);
```

## Menu Settings

```razor
<BasicNavigation>
  <placeholder-logo slot="logo"></placeholder-logo>
  <Menu Id="nav-menu-1">
    <MenuSettings Id="settings" SuppressLegacyTabs="true">
      <Tabs Id="settings-tabs" ActiveTabKey="general">
        <TabItem TabKey="general" Label="General" />
        <TabItem TabKey="preferences" Label="Preferences" />
      </Tabs>
      <section role="tabpanel" aria-label="Settings content">
        General settings
      </section>
    </MenuSettings>
  </Menu>
</BasicNavigation>
```

```csharp
Menu? menu;

await menu.ToggleSettingsAsync(true);
```

## Popover News

```razor
<BasicNavigation>
  <placeholder-logo slot="logo"></placeholder-logo>
  <Menu Id="nav-menu-1">
    <MenuAbout Id="about" SuppressLegacyTabs="true">
      <Tabs Id="about-tabs" ActiveTabKey="news">
        <TabItem TabKey="news" Label="News" />
      </Tabs>
    </MenuAbout>
    <MenuAboutNews Id="news" Label="Release notes" Show="true" AboutItemLabel="News" ActiveAboutTabKey="news">
      Latest release notes
    </MenuAboutNews>
  </Menu>
</BasicNavigation>
```

## AG Grid

`AGGrid<TData>` wraps AG Grid Community for Blazor. It registers the Community modules, applies the Siemens IX theme, and loads the JavaScript bundle automatically. Refer to the official [AG Grid documentation](https://www.ag-grid.com/javascript-data-grid/) for the underlying grid options and behavior.

Set `AgGridOptions.StripedRows` to enable the optional alternating-row styling supplied by `@siemens/ix-aggrid`:

```csharp
private readonly AgGridOptions options = new()
{
    StripedRows = true,
};
```

### Setup and typed API

Give the host a non-zero height and provide typed row data and column definitions. `GridReady` exposes `AgGridApi<TData>` after the grid has rendered:

```razor
<AGGrid TData="EquipmentRow"
        RowData="rows"
        ColumnDefinitions="columns"
        Options="options"
        GridReady="OnGridReady"
        Style="height: 24rem; width: 100%" />

@code {
    private AgGridApi<EquipmentRow>? gridApi;

    private readonly EquipmentRow[] rows =
    [
        new("Equipment", "Normal"),
    ];

    private readonly IAgGridColumnDefinition[] columns =
    [
        new AgGridColumnDefinition { Field = "type", HeaderName = "Type" },
        new AgGridColumnDefinition { Field = "status", Sortable = true, Filter = true },
    ];

    private void OnGridReady(AgGridReadyEvent<EquipmentRow> gridEvent) =>
        gridApi = gridEvent.Api;

    private sealed record EquipmentRow(string Type, string Status);
}
```

Configure common Community features with typed options and call stable grid operations through the typed API:

```csharp
private readonly AgGridOptions options = new()
{
    Pagination = true,
    PaginationPageSize = 10,
    DefaultColumnDefinition = new AgGridColumnDefinition
    {
        Sortable = true,
        Resizable = true,
        Filter = true,
    },
};

private async Task ReadGridAsync()
{
    int displayedRows = await gridApi!.GetDisplayedRowCountAsync();
    AgGridCellPosition? focusedCell = await gridApi.GetFocusedCellAsync();
    await gridApi.SetFocusedCellAsync(new AgGridCellPosition(0, "status"));
    await gridApi.PaginationGoToNextPageAsync();
}
```

The typed surface covers common selection, pagination, editing, state, column state, row visibility, overlays, CSV export, and infinite-row operations. Common component events include cell and row interaction, selection, filtering, sorting, pagination, editing, column changes, scrolling, and lifecycle events.

### JSON extension points

Use `AdditionalOptions` for a JSON-compatible option that does not yet have a typed property; it cannot duplicate a typed option. Use `InvokeAsync` for a serializable Community API that is not yet wrapped:

```csharp
AgGridOptions options = new()
{
    AdditionalOptions =
    {
        ["suppressRowClickSelection"] = true,
        ["autoGroupColumnDef"] = new { minWidth = 240 },
    },
};
int displayedRows = await gridApi!.InvokeAsync<int>("getDisplayedRowCount");
System.Text.Json.JsonElement filterModel = await gridApi.GetFilterModelAsync();
await gridApi.SetFilterModelAsync(filterModel);
```

Filter models, grid state, column groups, CSV parameters, cache state, and other filter-specific structures remain `JsonElement`/`object` because their shapes vary by AG Grid feature.

### Events and custom cell renderers

Handle common events directly on `AGGrid`; the event contains the typed row data when available:

```razor
<AGGrid TData="EquipmentRow"
        RowData="rows"
        ColumnDefinitions="columns"
        RowClicked="OnRowClicked"
        Style="height: 24rem; width: 100%" />

@code {
    private string? selectedType;

    private void OnRowClicked(AgGridEvent<EquipmentRow> eventData)
    {
        selectedType = eventData.Data?.Type;
    }
}
```

Use `EventSubscriptions` with `AgGridEventNames` and `EventReceived` for other public events. Custom cell renderers remain JavaScript because AG Grid virtualizes cells and expects their lifecycle to be synchronous. Set `JavaScriptModule` to an ES module and refer to the registered renderer by name:

```csharp
new AgGridColumnDefinition
{
    Field = "status",
    CellRenderer = "statusRenderer",
    CellRendererParams = new { prefix = "Status: " },
}
```

Example module:

```javascript
export function configureAgGrid({ options, createCellRendererComponent, registerCellRenderer }) {
    const StatusRenderer = createCellRendererComponent({
        create(params) {
            const element = document.createElement("span");
            element.textContent = `${params.prefix}${params.value}`;
            return element;
        },
        refresh(element, params) {
            element.textContent = `${params.prefix}${params.value}`;
            return true;
        },
        destroy(element) {
            element.replaceChildren();
        },
    });

    return registerCellRenderer(options, "statusRenderer", StatusRenderer);
}
```

The module receives `createCellRendererComponent` and `registerCellRenderer` helpers. The adapter implements AG Grid's `init`, `getGui`, `refresh`, and `destroy` lifecycle; `create` must return an `HTMLElement`, and `refresh` must synchronously return `true` when it updates the existing element or `false` when AG Grid should recreate it. The module may also export `projectAgGridEvent(name, event)` and `disposeAgGrid({ api, instanceId })`.

### Important boundaries

- Use a non-zero host height; the grid cannot render correctly in a zero-height container.
- Do not add AG Grid CDN assets, theme classes, manual `createGrid` calls, or `ix-aggrid.css`; the wrapper provides the Community bundle and Siemens IX theme.
- Keep callbacks, DOM values, custom components, and high-frequency renderer logic in JavaScript; pass JSON-compatible values across the Blazor boundary.
- Use `InfiniteDataSource` with an `IAgGridInfiniteDataSource<TData>` implementation for Blazor-backed infinite data; its requests include sorting, filtering, failure reporting, and cancellation.
- Use `RowData` for normal client-side data and transactions or refresh methods for in-place updates.

## Avatar

```razor
<Avatar
    Image="https://ui-avatars.com/api/?name=John+Doe"
    TooltipText="John Doe"
    AriaLabelTooltip="John Doe">
</Avatar>
```

## Blind

```razor
<Blind
    Id="blind1"
    CollapsedChangedEvent="(value) => BlindEventHandler(value)">
    <CustomHeader>
        <span>Custom header</span>
    </CustomHeader>
    <HeaderActions>
        <Button Variant="@ButtonVariant.secondary">Action</Button>
    </HeaderActions>
    <ChildContent>
        <p>Test content</p>
    </ChildContent>
</Blind>
```

## Breadcrumb

```razor
<Breadcrumb Id="breadcrumb-1"
            Class="editor-breadcrumb"
            ItemClicked="(item) => ClickedOnBreadcrumb(item)">
    <BreadcrumbItem BreadcrumbKey="item-1" Label="Item 1"></BreadcrumbItem>
    <BreadcrumbItem BreadcrumbKey="item-2" Label="Item 2"></BreadcrumbItem>
</Breadcrumb>
```

## Button

```razor
<Button Variant="ButtonVariant.primary" Icon="save-all" ClickEvent="Save">Save</Button>
<Button Variant="ButtonVariant.secondary" Href="https://ix.siemens.io/" Target="ButtonTarget._blank">Open docs</Button>
```

## Card

```razor
<Card Variant="CardVariant.info" Selected="true">
    <CardTitle>
        <ChildContent>Components</ChildContent>
        <TitleActions>
            <span>Details</span>
        </TitleActions>
    </CardTitle>
    <p>Card content is rendered in the default slot.</p>
    <CardAccordion Variant="CardAccordionVariant.info">
        <p>Expandable card content.</p>
    </CardAccordion>
</Card>
```

## Card List

```razor
<CardList Id="carlist1" Label="Stack Layout" ShowAllCount="12" ListStyle="Enums.CardList.CardListStyle.Stack" CollapseChangedEvent="CardListCollapsedChanged"
          I18nShowLess="Show fewer" ShowAllClickEvent="CardListShowAllClicked" ShowMoreCardClickEvent="CardListShowMoreClicked">
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

Card List click callbacks receive `CardListClickEventArgs`.

## Badge

```razor
<Badge Type="BadgeType.Label" Label="New" Variant="BadgeVariant.Info" Icon="info" />
<Badge Type="BadgeType.Dot" Variant="BadgeVariant.Success" TooltipText="Ready" />
<Badge Type="BadgeType.Counter" Label="3" Border="true">
    <Button Variant="ButtonVariant.secondary">Notifications</Button>
</Badge>
```

Use child content for an attached badge. `TooltipText` accepts text or a Boolean value.

## Chat

```razor
<Chat>
    <ChildContent>
        <ChatAiMessage>
            <ChildContent>Answer from the assistant.</ChildContent>
            <Actions><Button Variant="ButtonVariant.secondary">Copy</Button></Actions>
            <Sources>Source details</Sources>
        </ChatAiMessage>
        <ChatUserMessage Message="The user's question." />
    </ChildContent>
    <Prompt>
        <ChatInput Value="@Prompt" ValueChange="OnPromptChanged" PromptSubmit="SubmitPrompt">
            <Attachments>
                <ChatAttachment FileName="report.pdf" PreviewSupported="true" />
            </Attachments>
        </ChatInput>
    </Prompt>
</Chat>

@code {
    private string Prompt { get; set; } = string.Empty;
    private void OnPromptChanged(string value) => Prompt = value;
    private Task SubmitPrompt(string value) => Task.CompletedTask;
}
```

Chat supports named content slots and typed attachment/input events.

## Push Card

```razor
<PushCard Icon="rocket"
        AriaLabelIcon="Status"
        Notification="3"
        Heading="Heading content"
        SubHeading="Subheading"
        Variant="PushCardVariant.outline">
    <TitleAction>Action</TitleAction>
    <ChildContent><p>Expandable content.</p></ChildContent>
</PushCard>
```

## Action Card

```razor
<ActionCard
      Icon="refresh"
      Heading="Scan for new devices"
      SubHeading="Secondary text"
      Variant="ActionCardVariant.filled">
    <span>Additional non-interactive content</span>
</ActionCard>
```

## Icon Button

```razor
<IconButton Icon="info" Variant="ButtonVariant.subtle_primary" />
```

## Category Filter

```razor
<CategoryFilter
    Id="category-filter-1"
    Placeholder="Filter by"
    Categories="@Categories"
    FilterState="@CurrentFilter"
    UniqueCategories="true"
    FilterChangedEvent="FilterStateChanged"
    InputChangedEvent="InputStateChanged"
    CategoryChangedEvent="CategoryStateChanged"
    FilterClearedEvent="FilterCleared">
</CategoryFilter>
```

```csharp
private Dictionary<string, Category> Categories { get; } = new()
{
    ["ID_1"] = new Category
    {
        Label = "Vendor",
        Options = ["Apple", "Microsoft", "Siemens"]
    }
};

private FilterState CurrentFilter { get; set; } = new()
{
    Tokens = ["Custom filter text"],
    Categories =
    [
        new FilterStateCategory
        {
            Id = "ID_1",
            Value = "Siemens",
            Operator = LogicalFilterOperator.NotEqual
        }
    ]
};

private void FilterStateChanged(FilterState state) => CurrentFilter = state;
private void InputStateChanged(InputState state) { }
private void CategoryStateChanged(string? category) { }
private void FilterCleared(FilterClearedEventArgs eventArgs)
{
    // Set eventArgs.Cancel = true to keep the current filter.
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
      Id="chip1"
      AriaLabelIcon="Print"
      Closable
      TooltipText="@("Tooltip Text")"
      ClosedEvent="@ChipClosedEventHandler">
    Chip with icon
</Chip>

<Chip Id="chip-text-tooltip"
      TooltipText="@true">
    Uses chip text as tooltip
</Chip>
```

## Content

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
    <HeaderContent>
        <span>Draft</span>
    </HeaderContent>
    <Button>Save</Button>
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

## Date Picker

```razor
<DatePicker From="2023/02/01"
            To="2023/02/15"
            Id="timepicker1"
            DateChangeEvent="(date) => DateChangeEventTest(date)">
</DatePicker>
```

## Date Input

```razor
<DateInput Id="date-input"
           Label="Date"
           Value="2026/01/15"
           MinDate="2026/01/01"
           MaxDate="2026/12/31"
           ValueChangeEvent="OnDateChanged"
           ValidityStateChangeEvent="OnDateValidityChanged">
    <StartSlot>
        <span>Start</span>
    </StartSlot>
</DateInput>
```

`DateInput` exposes the official date-input properties, `start` and `end` slots, and typed value, `ixChange`, and validity-state callbacks.

## Date Time Input

```razor
<DateTimeInput Id="datetime-input"
               Label="Date and time"
               Format="yyyy/LL/dd HH:mm:ss"
               Value="2026/01/15 09:30:00"
               ValueChangeEvent="OnDateTimeChanged" />
```

## Range field

```razor
<RangeField Type="@RangeFieldType.DateRange">
    <DateInput Id="date-range-from" />
    <DateInput Id="date-range-to" />
</RangeField>
```

`RangeFieldType` maps to the official `time-range`, `date-range`, or `datetime-range` type. `RangeField` accepts exactly the two range inputs required by IX and supports `HideArrow`.

## Date Time Picker

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

## Dropdown Button

```razor
<DropdownButton Label="Dropdown" Variant="ButtonVariant.primary" Icon="checkboxes">
    <DropdownItem Label="Item 1" Checked="true"></DropdownItem>
    <DropdownItem Label="Item 2"></DropdownItem>
    <DropdownItem Label="Item 3" Disabled="true"></DropdownItem>
</DropdownButton>
```

## Dropdown

```razor
<Button Id="triggerId">Open</Button>
<Dropdown Trigger="@TriggerId">
  <DropdownItem Label="Item 1" Icon="save-all"></DropdownItem>
  <DropdownItem Label="Item 2"></DropdownItem>
  <DropdownItem Label="Item 3"></DropdownItem>
</Dropdown>

@code {
    private object TriggerId { get; } = "triggerId";
}
```

## Dropdown Header

```razor
<Button Id="triggerId">Open</Button>
<Dropdown Trigger="@TriggerId">
  <DropdownHeader Label="Category"></DropdownHeader>
  <DropdownItem Label="Item 1"></DropdownItem>
  <DropdownItem Label="Item 2"></DropdownItem>
  <DropdownItem Label="Item 3"></DropdownItem>
  <DropdownQuickActions>
    <IconButton Icon="save-all" aria-label="Save"></IconButton>
  </DropdownQuickActions>
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

## Event List

```razor
<EventList Animated="true" Compact="true" Chevron="true" ItemHeight="@("L")">
    <EventListItem Id="event-list-item-1" ItemColor="color-success" Selected="true">Item 1</EventListItem>
    <EventListItem Id="event-list-item-2" ItemColor="color-warning" Chevron="true">Item 2</EventListItem>
    <EventListItem Id="event-list-item-3" Disabled="true">Item 3</EventListItem>
</EventList>
```

## Expanding Search

```razor
<ExpandingSearch Id="exp-search"
                 ValueChangedEvent="(value) => SearchValueChanged(value)">
</ExpandingSearch>
```

## Flip

```razor
<FlipTile Id="flip-tile-example"
          Height="@("auto")"
          Width="@("auto")"
          AriaLabelEyeIconButton="Toggle view">
    <HeaderContent>Flip header</HeaderContent>
    <ChildContent>
        <FlipTileContent>Example 1</FlipTileContent>
        <FlipTileContent>Example 2</FlipTileContent>
    </ChildContent>
    <FooterContent>Predicted maintenance date: 2021-06-22</FooterContent>
</FlipTile>
```

## Group

```razor
<Group Id="group1" Header="Header text" SubHeader="Subheader text">
    <HeaderContent>Custom header</HeaderContent>
    <ChildContent>
        <GroupItem Id="groupitem1" Text="Example text 1"></GroupItem>
        <GroupItem Id="groupitem2" Text="Example text 2"></GroupItem>
        <GroupItem Id="groupitem3" Text="Example text 3" SelectedChangeEvent="GroupItemSelectedChanged"></GroupItem>
    </ChildContent>
    <FooterContent>Group footer</FooterContent>
</Group>
```

## HTML Table

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
<Input Id="input-example"
       Type="InputType.Email"
       Label="Email"
       Placeholder="name@example.com"
       TextAlignment="TextAlignment.Start"
       SuppressSubmitOnEnter="true"
       ValueChangeEvent="OnInputValueChanged"
       IxChangeEvent="OnInputChanged" />
```

## Number Input

```razor
<NumberInput Id="number-input-example"
             Label="Quantity"
             Value="10"
             Min="0"
             Max="100"
             Step="1"
             AllowEmptyValueChange="true"
             ValueChangeEvent="OnNumberChanged"
             IxChangeEvent="OnNumberCommitted" />
```

## Text Area

```razor
<TextArea Id="text-area-example"
          Label="Description"
          TextareaRows="4"
          ResizeBehavior="TextAreaResizeBehavior.Vertical"
          ValueChangeEvent="OnTextChanged"
          IxChangeEvent="OnTextCommitted" />
```

## Custom Field

```razor
<CustomField Id="custom-field-example"
             Label="Custom control"
             HelperText="Choose an option">
  <select aria-label="Custom control">
    <option>Option one</option>
  </select>
</CustomField>
```

## Field Label

```razor
<FieldLabel HtmlFor="custom-control" Required="true">
  Custom control
</FieldLabel>
```

## Helper Text

```razor
<HelperText HtmlFor="custom-control"
            HelperText="Choose an option"
            InvalidText="This value is required" />
```

## Key Value List

```razor
<KeyValueList>
  <KeyValue
    Label="Label"
    LabelPosition="@(SiemensIXBlazor.Enums.KeyValue.KeyValueLabelPosition.left)"
    Value="Value"
  />

  <KeyValue
    Label="Label"
    LabelPosition="@(SiemensIXBlazor.Enums.KeyValue.KeyValueLabelPosition.left)"
    Value="Value"
  />

  <KeyValue
    Label="Label"
    LabelPosition="@(SiemensIXBlazor.Enums.KeyValue.KeyValueLabelPosition.left)"
    Value="Value"
  />
</KeyValueList>
```

## Key Value

```razor
<KeyValue Label="Label">
  <CustomValue>
    <input class="form-control" placeholder="Enter text here" type="text" />
  </CustomValue>
</KeyValue>
```

## KPI

```razor
<KPI Label="Motor speed"
     Value="@("Nominal")"
     Unit="rpm"
     State="@KpiState.Warning"
     AriaLabelWarningIcon="Motor speed warning" />

<KPI Label="Temperature" Value="@(42)" Unit="°C" />
```

## Layout Grid

```razor
<LayoutGrid>
  <Row>
    <Col Size="ColumnSize._6">First column</Col>
    <Col Size="ColumnSize._6">Second column</Col>
  </Row>
</LayoutGrid>
```

## Layout Auto

```razor
<LayoutAuto Id="layout-auto-example" Layout="@Layout">
  <Input Label="First name" />
  <Input Label="Last name" />
</LayoutAuto>

@code {
  private LayoutAutoItem[] Layout =
  [
    new() { MinWidth = "0", Columns = 1 },
    new() { MinWidth = "48em", Columns = 2 }
  ];
}
```

## Link Button

```razor
<LinkButton Url="https://ix.siemens.io/">Siemens IX</LinkButton>
```

## Checkbox Group

```razor
<CheckboxGroup Id="notification-options"
               Label="Notifications"
               HelperText="Choose any options"
               Direction="CheckboxGroupDirection.Row">
    <Checkbox Id="email-notifications" Label="Email" Value="email" />
    <Checkbox Id="sms-notifications" Label="SMS" Value="sms" />
    <Checkbox Id="push-notifications" Label="Push" Value="push" />
</CheckboxGroup>
```

## Radio Group

```razor
<RadioGroup Id="storage-options"
            Label="Storage options"
            Direction="RadioGroupDirection.Row"
            Value="512"
            ValueChangeEvent="OnStorageChanged">
    <Radio Label="256GB SSD storage" Value="256" Name="storage" />
    <Radio Label="512GB SSD storage" Value="512" Name="storage" />
    <Radio Label="1TB SSD storage" Value="1024" Name="storage" />
</RadioGroup>
```

## Message Bar

```razor
<MessageBar ClosedChangeEvent="MessageboxClosed"
            CloseAnimationCompletedEvent="MessageboxCloseAnimationCompleted"
            Id="messagebar1"
            Type="MessageBarType.Info">
    <div class="d-flex align-items-center justify-content-between">
        Message text <ix-button>Action</ix-button>
    </div>
</MessageBar>
```

## Modal

Register the services once and render one `ModalHost` in the application layout:

```csharp
builder.Services.AddScoped<ModalService>();
builder.Services.AddScoped<LoadingService>();
```

```razor
<ModalHost />
<Button ClickEvent="OpenModal">Open modal</Button>
```

```razor
@inject ModalService ModalService

@code {
private ModalInstance<string>? _modal;

private async Task OpenModal()
{
  _modal = await ModalService.ShowAsync<string>(new ModalConfig
  {
    Centered = true,
    Animation = true,
    Backdrop = true,
    Content = @<text>
      <ModalHeader>Message headline</ModalHeader>
      <ModalContent>Message text</ModalContent>
      <ModalFooter>
        <Button Variant="ButtonVariant.primary" ClickEvent="CloseModal">OK</Button>
        <Button ClickEvent="DismissModal">Cancel</Button>
      </ModalFooter>
    </text>
  });
}

private async Task CloseModal()
{
  if (_modal is not null)
    await _modal.CloseAsync("ok");
}

private async Task DismissModal()
{
  if (_modal is not null)
    await _modal.DismissAsync("cancel");
}
}
```

`ModalHeader`, `ModalContent`, and `ModalFooter` are composition wrappers. The services manage `ix-modal` and the internal `ix-modal-loading` element; applications do not instantiate them directly.

```csharp
var loading = await LoadingService.ShowModalLoadingAsync(new ModalLoadingOptions
{
  Message = "Loading...",
  Centered = true
});

await loading.UpdateAsync("Almost finished...");
await loading.FinishAsync("Done");
await loading.DisposeAsync();
```

## Popover

```razor
<Button Id="popover-trigger">Show details</Button>

<Popover Id="popover"
         Trigger="popover-trigger"
         Placement="PopoverPlacement.Bottom"
         HasSpike="true">
    <PopoverHeader Id="popover-header"
                   Icon="info">
        <ChildContent>Details</ChildContent>
        <AdditionalItems>
            <Pill Variant="PillVariant.info">New</Pill>
        </AdditionalItems>
    </PopoverHeader>
    <PopoverImage Id="popover-image"
                  Image="data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='400' height='160'%3E%3Crect fill='%232a2a4a' width='100%25' height='100%25'/%3E%3C/svg%3E"
                  ImageAlt="Release preview" />
    <PopoverContent Id="popover-content">
        Additional information.
    </PopoverContent>
    <PopoverFooter Id="popover-footer">
        <StartContent>Step 1 of 2</StartContent>
        <ChildContent><Button>Close</Button></ChildContent>
    </PopoverFooter>
</Popover>
```

Use `PopoverTriggerMode.Hover` for hover and focus interaction, `PopoverPlacement` for the preferred position, and `PopoverFooterAlignment.Vertical` for vertically arranged footer actions. `Popover.ShowPopover()` and `Popover.HidePopover()` expose the official programmatic methods.

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
    <Left>
        <Pane Id="pane-left" Heading="Pane Left" Slot="left" Size="33%">
            <p>This is the left pane.</p>
        </Pane>
    </Left>
    <Top>
        <div>This is the top content.</div>
    </Top>
    <Content>
        <div>This is the main content.</div>
    </Content>
    <Bottom>
        <div>This is the bottom content.</div>
    </Bottom>
    <Right>
        <Pane Id="pane-right" Heading="Pane Right" Slot="right" Size="33%" NoPadding="true">
            <p>This pane has no content padding.</p>
        </Pane>
    </Right>
</PaneLayout>
```

## Pill

```razor
<Pill Variant="PillVariant.custom" PillColor="white" Background="purple" TooltipText="Tooltip Text">
    Label
</Pill>
```

## Progress Indicator

```razor
<ProgressIndicator Value="75"
                   Label="Processing data..."
                   HelperText="Please wait while we process your request"
                   Size="@ProgressIndicatorSize.lg"
                   Status="@ProgressIndicatorStatus.info" />

<ProgressIndicator Value="75"
                   Label="Processing data..."
                   Size="@ProgressIndicatorSize.lg"
                   Status="@ProgressIndicatorStatus.info">
    <HelperTextContent>
        <span>Custom helper text</span>
    </HelperTextContent>
    <span>75%</span>
</ProgressIndicator>
```

## Radio Button

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
<Select Id="select1"
        Mode="SelectMode.Multiple"
        Value="selectedValues"
        ValueChangeEvent="OnValuesChanged"
        I18nPlaceholder="Select a value">
    <SelectItem Id="selectItem1" Label="Item 1" Value="1" />
    <SelectItem Id="selectItem2" Label="Item 2" Value="2" Disabled="true" />
</Select>

@code {
    private string[] selectedValues = ["1"];

    private void OnValuesChanged(object? value)
    {
        selectedValues = value as string[] ?? [];
    }
}
```

## Slider

```razor
<Slider Id="slider-demo"
        Label="Range"
        HelperText="Select a value"
        Min="0"
        Max="50"
        Step="5"
        Value="0"
        Marker="[0, 10, 20, 30, 40, 50]">
    <LabelStart>
        <span>0</span>
    </LabelStart>
    <LabelEnd>
        <span>50</span>
    </LabelEnd>
</Slider>
```

## Spinner

```razor
<Spinner></Spinner>
```

## Split Button

```razor
<SplitButton Id="split-button-1"
             Label="Split Button"
             SplitIcon="chevron-down-small"
             ButtonClickedEvent="SplitButtonClicked">
    <DropdownItem Label="Save"></DropdownItem>
    <DropdownItem Label="Save as"></DropdownItem>
</SplitButton>
```

## Tabs

```razor
<Tabs Id="tabs-demo"
      ActiveTabKey="@_activeTabKey"
      TabChangeEvent="OnTabChanged">
    <TabItem TabKey="overview" Label="Overview" />
    <TabItem TabKey="details" Label="Details" Counter="2" />
    <TabItem TabKey="history" Label="History" Closable="true" />
</Tabs>
@if (_activeTabKey == "overview")
{
  <h5>Content of the overview tab</h5>
}
else if (_activeTabKey == "details")
{
  <h5>Content of the details tab</h5>
}
else if (_activeTabKey == "history")
{
  <h5>Content of the history tab</h5>
}

@code {
    private string? _activeTabKey = "overview";

    private Task OnTabChanged(string? tabKey)
    {
        _activeTabKey = tabKey;
        return Task.CompletedTask;
    }
}
```

## Tile

```razor
<Tile Size="TileSize.Medium" Class="mr-1">
    <HeaderContent>Tile header</HeaderContent>
    <SubheaderContent>Temperature</SubheaderContent>
    <ChildContent><div class="text-l">92.8 °C</div></ChildContent>
    <FooterContent>Updated now</FooterContent>
</Tile>
```

## Time Picker

```razor
<TimePicker Id="timePicker1"
            Class="my-time-picker"
            HourInterval="1"
            MinuteInterval="15"
            SecondInterval="30"
            HideHeader="false"
            Corners="@TimePickerCorners.Rounded"
            Format="HH:mm:ss"
            MinTime="09:00:00"
            MaxTime="17:30:00">
</TimePicker>
```

## Time Input

```razor
<TimeInput Id="time-input"
           Label="Time"
           Value="09:30:00"
           MinuteInterval="15"
           ValueChangeEvent="OnTimeChanged" />
```

## Toast

```razor
<ToastContainer @ref="toastContainer" Id="toast-container" Position="ToastPosition.TopRight" />

<Toast Id="toast"
       Type="ToastType.Success"
       ToastTitle="Changes applied"
       PreventAutoClose="true">
    <ChildContent>Your settings were saved successfully.</ChildContent>
    <ActionContent>
        <ix-button variant="tertiary">Undo</ix-button>
    </ActionContent>
</Toast>
```

```csharp
private ToastContainer toastContainer = default!;
private ToastResult? toastResult;

toastResult = await toastContainer.ShowToast(new ToastConfig
{
    Title = "Changes applied",
    Message = "Your settings were saved successfully.",
    Type = ToastType.Success,
    AutoClose = false
});

toastResult.OnClose += (_, _) => Console.WriteLine("Toast closed");

await toastResult.PauseAsync();
await toastResult.ResumeAsync();
await toastResult.CloseAsync();
```

## Toggle Buttons

```razor
<ToggleButton Id="toggle-btn-1" Variant="ToggleButtonVariant.subtle_secondary">
    Normal
</ToggleButton>
<ToggleButton Id="toggle-btn-2" Pressed="true" Icon="star">
    Pressed
</ToggleButton>

<IconToggleButton Icon="checkboxes"
                   Variant="ButtonVariant.subtle_secondary"
                   Size="IconButtonSize._16"
                   Pressed="true"
                   aria-label="Toggle checkboxes">
</IconToggleButton>
<IconToggleButton
    Outline="true"
    Icon="checkboxes"
    aria-label="Toggle checkboxes outline"
></IconToggleButton>
```

## Toggle

```razor
<Toggle Id="notifications" Name="notifications" Value="enabled"></Toggle>
```

## Tooltip

```razor
<Button class="any-class" aria-describedby="tooltip-1">
    Save
</Button>
<Tooltip Id="tooltip-1" For=".any-class">
    <TitleIconContent><ix-icon name="info" size="16"></ix-icon></TitleIconContent>
    <TitleContentSlot>Save changes</TitleContentSlot>
    <ChildContent>When you click, all changes will be saved</ChildContent>
</Tooltip>
```

## Tree

```razor
<div style="height: 8rem; width: 100%">
    <Tree Id="tree-1"
          Root="root"
          Model="@treeNodes"
          Context="@treeContext"
          ToggleOnItemClick="true"
          ContextChangedEvent="TreeContextChangeEvent"
          NodeClickedEvent="TreeNodeClicked"
          NodeRemovedEvent="NodeRemoved"
          NodeRemovedDetailsEvent="TreeNodesRemoved"
          NodeToggledEvent="TreeNodeToggled"
          @ref="tree">
    </Tree>
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
        Name = "Sample Child 1",
        Icon = "star"
    },
    Disabled = false,
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

Dictionary<string, TreeContextNode> treeContext = new()
{
    ["sample"] = new TreeContextNode { IsExpanded = true, IsSelected = false },
    ["sample-child-1"] = new TreeContextNode { IsExpanded = false, IsSelected = true }
};

// Update data through the component parameters, or call the public methods:
await tree.MarkItemsAsDirty("sample-child-1");
await tree.RefreshTree(new RefreshTreeOptions { Force = true });

void TreeNodesRemoved(TreeNodeRemovedEventArgs details)
{
    var removedIds = details.NodeIds;
}
```

Use `TreeItem` for a standalone official tree item, including custom content:

```razor
<TreeItem Text="Custom item"
          HasChildren="true"
          Context="@treeItemContext"
          ToggleEvent="OnTreeItemToggle"
          ItemClickEvent="OnTreeItemClick">
    Custom content
</TreeItem>
```

## Typography

```razor
<Typography Format="TypographyFormat.Label" TextColor="TypographyColor.Std" TextDecoration="TextDecoration.None">Label, Std, None</Typography>
<Typography Bold="true" Format="TypographyFormat.Code_Lg" TextColor="TypographyColor.Contrast" TextDecoration="TextDecoration.Line_Through">Bold, Code_Lg, Contrast, Line_Through</Typography>
```

## Upload

```razor
<Upload Id="file-upload-test"
        DirectoryUpload="true"
        State="UploadFileState.SELECT_FILE"
        FileChangedEvent="(data) => FileChanged(data)">
</Upload>
```

## Workflow

```razor
<WorkflowSteps Id="wf-steps" StepSelectedEvent="(index) => WfSelectedEvent(index)">
    <WorkflowStep Status="WorkflowStatus.Done">
        <CustomIcon>
            <ix-icon name="star"></ix-icon>
        </CustomIcon>
        <ChildContent>Step 1</ChildContent>
    </WorkflowStep>
    <WorkflowStep Status="WorkflowStatus.Success">Step 2</WorkflowStep>
    <WorkflowStep Status="WorkflowStatus.Open">Step 3</WorkflowStep>
    <WorkflowStep Status="WorkflowStatus.Warning">Step 4</WorkflowStep>
    <WorkflowStep Status="WorkflowStatus.Error">Step 5</WorkflowStep>
    <WorkflowStep Disabled="true">Step 6</WorkflowStep>
</WorkflowSteps>
```

### Native iX Elements

Native iX custom elements can be used directly when a Blazor wrapper is not required.

```razor
<ix-button class="m-1" variant="Secondary">
    Button
</ix-button>
```

For supported components, use the corresponding Blazor wrapper.

```razor
<Button Class="m-1" Variant="Secondary">
    Button
</Button>
```

Handle events from native iX custom elements through JavaScript interop.

## 📝 License

Copyright © 2026 [Siemens AG](https://www.siemens.com/).

[Siemens Third-Party Software Disclosure Document](/docs/Siemens.IX.Blazor__0.5.5__READMEOSS.html)

This project is MIT licensed.
