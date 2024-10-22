using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components;

public partial class DropdownHeader
{
    [Parameter]
    public string? Label { get; set; }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}