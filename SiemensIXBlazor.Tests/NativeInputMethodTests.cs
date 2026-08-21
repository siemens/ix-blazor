// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using SiemensIXBlazor.Components;
using SiemensIXBlazor.Components.DateInput;
using SiemensIXBlazor.Components.Input;
using SiemensIXBlazor.Components.NumberInput;
using SiemensIXBlazor.Components.TextArea;
using SiemensIXBlazor.Components.TimeInput;
using SelectComponent = SiemensIXBlazor.Components.Select;

namespace SiemensIXBlazor.Tests;

public class NativeInputMethodTests : TestContextBase
{
    [Fact]
    public async Task InputComponentsExposeNativeInputMethods()
    {
        var input = Render<Input>(parameters => parameters.Add(p => p.Id, "input"));
        var numberInput = Render<NumberInput>(parameters => parameters.Add(p => p.Id, "number-input"));
        var textarea = Render<TextArea>(parameters => parameters.Add(p => p.Id, "textarea"));
        var dateInput = Render<DateInput>(parameters => parameters.Add(p => p.Id, "date-input"));
        var timeInput = Render<TimeInput>(parameters => parameters.Add(p => p.Id, "time-input"));
        var select = Render<SelectComponent>(parameters => parameters.Add(p => p.Id, "select"));

        Assert.Null(await input.Instance.GetNativeInputElementAsync());
        Assert.Null(await numberInput.Instance.GetNativeInputElementAsync());
        Assert.Null(await textarea.Instance.GetNativeInputElementAsync());
        Assert.Null(await dateInput.Instance.GetNativeInputElementAsync());
        Assert.Null(await timeInput.Instance.GetNativeInputElementAsync());
        Assert.Null(await select.Instance.GetNativeInputElementAsync());
    }
}
