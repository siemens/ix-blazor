using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.Radio
{
    public partial class RadioGroup
    {
        private string? _label;
        private string? _infoText;
        private string? _warningText;
        private string? _validText;
        private string? _invalidText;
        private string? _cssClass;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public string? Label
        {
            get => _label;
            set => _label = value;
        }

        [Parameter]
        public string? InfoText
        {
            get => _infoText;
            set => _infoText = value;
        }

        [Parameter]
        public string? WarningText
        {
            get => _warningText;
            set => _warningText = value;
        }

        [Parameter]
        public string? ValidText
        {
            get => _validText;
            set => _validText = value;
        }

        [Parameter]
        public string? InvalidText
        {
            get => _invalidText;
            set => _invalidText = value;
        }


        [Parameter]
        public string? CssClass
        {
            get => _cssClass;
            set => _cssClass = value;
        }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}