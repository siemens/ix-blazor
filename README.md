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

```html
<Blind 
    Label="Test Blind" 
    Id="blind1" 
    CollapsedChangedEvent="(value) => BlindEventHandler(value)">
Test content
</Blind>
```

- [x] Breadcrumb

```html
<Breadcrumb Id="breadcrumb-1" 
            Class="editor-breadcrumb" 
            ItemClicked="(label) => ClickedOnBreadcrumb(label)">
    <BreadcrumbItem Label="Item 1"></BreadcrumbItem>
    <BreadcrumbItem Label="Item 2"></BreadcrumbItem>
</Breadcrumb>
```

- [x] Button

```html
<Button>Test Button</Button>
```

- [x] Icon Button

```html
<IconButton Icon="info"></IconButton>
```

- [ ] Category filter
- [ ] ECharts
- [x] Checkbox

```html
<Checkbox Id="testcbx" 
          Label="Test Cbx" 
          OnChangeEvent="(value) => CbxOnChanged(value)">
</Checkbox>
```

- [x] Chip

```html
<Chip Icon="print" 
      Label="Chip with icon" 
      Id="chip1" 
      Closable="true" 
      ClosedEvent="@ChipClosedEventHandler">
</Chip>
```

- [x] Date picker

```html
<DatePicker From="2023/02/01" 
            To="2023/02/15" 
            Id="timepicker1" 
            DateChangeEvent="(date) => DateChangeEventTest(date)">
</DatePicker>
```

- [x] Date time picker

```html
<DateTimePicker 
        DateChangeEvent="(date) => DateChangeEventTest(date)" 
        From="2023/02/01" 
        To="2023/02/15" 
        Id="datetimepicker1"
        TimeChangeEvent="(date) => DateChangeEventTest(date)">
</DateTimePicker>
```

- [x] Drawer

```html
<Drawer Id="drawer1">
    <span>Some content of drawer</span>
</Drawer>
```

- [x] Dropdown button

```html
<DropdownButton Label="Dropdown" Variant="Primary" Icon="checkboxes">
    <DropdownItem Label="Item 1" Value="1"></DropdownItem>
    <DropdownItem Label="Item 2" Value="2"></DropdownItem>
    <DropdownItem Label="Item 3" Value="3"></DropdownItem>
</DropdownButton>
```

- [x] Dropdown

```html
<Button Id="triggerId">Open</Button>
<Dropdown Trigger="triggerId">
  <DropdownItem Label="Item 1"></DropdownItem>
  <DropdownItem Label="Item 2"></DropdownItem>
  <DropdownItem Label="Item 3"></DropdownItem>
</Dropdown>
```

- [x] Event list

```html
<EventList>
    <EventListItem Id="event-list-item-1" Label="Item 1" ItemCLickEvent="(label) => DropdownButtonClicked(label)"></EventListItem>
    <EventListItem Id="event-list-item-2" Label="Item 2" ItemCLickEvent="(label) => DropdownButtonClicked(label)"></EventListItem>
    <EventListItem Id="event-list-item-3" Label="Item 3" ItemCLickEvent="(label) => DropdownButtonClicked(label)"></EventListItem>
</EventList>
```

- [x] Expanding search

```html
<ExpandingSearch Id="exp-search" 
                 ValueChangedEvent="(value) => SearchValueChanged(value)">
</ExpandingSearch>
```

- [x] Flip

```html
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
- [ ] Group
- [x] HTML table

```html
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

- [x] Input

```html
<form class="needs-validation m-2">
  <input
    class="form-control"
    defaultValue="Some example text"
    placeholder="Enter text here"
    type="text"
  />
</form>
```

- [x] KPI

```html
<KPI Label="Motor speed" Value="Nominal"></KPI>
```

- [x] Message bar

```html
<MessageBar>Message text</MessageBar>
```

- [x] Modal

```html
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

- [x] Pill

```html
<Pill Variant="custom" Color="white" Background="purple">
  Label
</Pill>
```

- [x] Radio button

```html
<div class="example-list">
  <input checked id="checkbox_1_1" name="group_1" type="radio" />
  <label for="checkbox_1_1"> Checked </label>

  <input id="checkbox_1_2" name="group_1" type="radio" />
  <label for="checkbox_1_2"> Normal </label>

  <input disabled id="checkbox_1_3" name="group_1" type="radio" />
  <label for="checkbox_1_3"> Disabled </label>
</div>
```

- [x] Select

```html
<Select selected-indices="1">
  <SelectItem Label="Item 1" Value="1"></SelectItem>
  <SelectItem Label="Item 2" Value="2"></SelectItem>
  <SelectItem Label="Item 3" Value="3"></SelectItem>
  <SelectItem Label="Item 4" Value="4"></SelectItem>
</Select>
```

- [x] Spinner

```html
<Spinner></Spinner>
```

- [x] Split button

```html
<SplitButton Id="split-button-1" 
             Label="Split Button" 
             SplitIcon="chevron-down-small" 
             ButtonClickedEvent="SplitButtonClicked">
</SplitButton>
```

- [ ] Tabs
- [x] Text area

```html
<textarea class="form-control" placeholder="Enter text here"></textarea>
```

- [x] Tile

```html
<Tile Size="small">92.8 °C</Tile>
```

- [x] Time picker

```html
<TimePicker></TimePicker>
```

- [x] Toast

```html
<Toast @ref="toast"></Toast>
```

```csharp
private Toast toast;

toast.ShowToast("test message", "info");
```

- [x] Toggle

```html
<Toggle></Toggle>
```

- [ ] Tree
- [x] Upload

```html
<Upload Id="file-upload-test" 
        FileChangedEvent="(data) => FileChanged(data)">
</Upload>
```

- [x] Form validation

```html
<form class="row g-3 needs-validation" novalidate>
  <div class="row">
    <div class="col-md-4">
      <label for="validationCustom01" class="form-label">
        First name
      </label>
      <input
        type="text"
        class="form-control"
        id="validationCustom01"
        value=""
        required
      />
      <div class="valid-feedback">Looks good!</div>
    </div>
  </div>
  <div class="row">
    <div class="col-md-4">
      <ix-validation-tooltip message="Cannot be empty!">
        <label for="validationCustom02" class="form-label">
          Last name
        </label>
        <input
          type="text"
          class="form-control"
          id="validationCustom02"
          value=""
          required
        />
      </ix-validation-tooltip>
      <div class="valid-feedback">Looks good!</div>
    </div>
  </div>
  <div class="row">
    <div class="col-md-4">
      <label for="validationCustomUsername" class="form-label">
        Username
      </label>
      <input
        type="text"
        class="form-control"
        id="validationCustomUsername"
        aria-describedby="inputGroupPrepend"
        required
        minlength="4"
      />
      <div class="invalid-feedback">Please choose a username.</div>
    </div>
  </div>
  <div class="col-12">
    <button class="btn btn-primary" type="submit">Submit form</button>
  </div>
</form>
```

- [ ] Workflow

### Usage

You can follow the original documentation and use native `Javascript` components.

```html
<ix-button class="m-1" outline variant="Secondary">
    Button
</ix-button>
```

Or you can use supported components as a native `Blazor` components.

```html
<Button Class="m-1" Outline="true" Variant="Secondary">
    Button
</Button>
```

If you want to use native `siemens-ix` html elements, you have to handle events over `javascript interops`. 
