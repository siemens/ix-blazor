using SiemensIXBlazor.Enums.LinkButton;

namespace SiemensIXBlazor.Components
{
    public partial class LinkButton
    {
        public bool Disabled { get; set; } = false;
        public LinkButtonTarget Target { get; set; } = LinkButtonTarget._self;
        public string? Url { get; set; }
    }
}
