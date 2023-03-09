using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class TimePicker
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public string Corners { get; set; } = "rounded";
        [Parameter]
        public static string Format { get; set; } = "yyyy/MM/dd";
        [Parameter]
        public bool ShowHour { get; set; } = false;
        [Parameter]
        public bool ShowMinutes { get; set; } = false;
        [Parameter]
        public bool ShowSeconds { get; set; } = false;
        [Parameter]
        public string ShowTimeReference { get; set; } = Format;
        [Parameter]
        public string TextSelectTime { get; set; } = "Done";
        [Parameter]
        public string Time { get; set; } = DateTime.Now.ToString(Format);
        [Parameter]
        public EventCallback<string> DoneEvent { get; set; }
        [Parameter]
        public EventCallback<string> TimeChangeEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "done", "Done");
                await _interop.AddEventListener(this, Id, "timeChange", "TimeChanged");
            }
        }

        [JSInvokable]
        public async void Done(string date)
        {
            await DoneEvent.InvokeAsync(date);
        }

        [JSInvokable]
        public async void TimeChanged(string date)
        {
            await TimeChangeEvent.InvokeAsync(date);
        }
    }
}
