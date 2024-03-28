using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components;

public partial class Content
{
    [Parameter]
    public RenderFragment? HeaderContent { get; set; }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}