// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;
using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components.CustomField;
using SiemensIXBlazor.Components.FieldLabel;
using SiemensIXBlazor.Components.HelperText;
using SiemensIXBlazor.Components.Input;
using SiemensIXBlazor.Components.NumberInput;
using SiemensIXBlazor.Components.TextArea;
using SiemensIXBlazor.Enums.Input;
using SiemensIXBlazor.Enums.TextArea;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Tests;

public class FormFieldTests : TestContextBase
{
    [Fact]
    public void InputRendersOfficialDefaultsAndProperties()
    {
        var cut = Render<Input>(parameters => parameters
            .Add(p => p.Id, "input")
            .Add(p => p.Type, InputType.Email)
            .Add(p => p.SuppressSubmitOnEnter, true)
            .Add(p => p.TextAlignment, TextAlignment.End));

        var element = cut.Find("ix-input");
        Assert.Equal("email", element.GetAttribute("type"));
        Assert.Equal("true", element.GetAttribute("suppress-submit-on-enter"));
        Assert.Equal("end", element.GetAttribute("text-alignment"));
    }

    [Fact]
    public async Task InputForwardsValueChangeChangeBlurAndValidityEvents()
    {
        string? valueChange = null;
        string? change = null;
        var blurred = false;
        ValidityState? validity = null;
        var cut = Render<Input>(parameters => parameters
            .Add(p => p.Id, "input")
            .Add(p => p.ValueChangeEvent, EventCallback.Factory.Create<string>(this, value => valueChange = value))
            .Add(p => p.IxChangeEvent, EventCallback.Factory.Create<string>(this, value => change = value))
            .Add(p => p.IxBlurEvent, EventCallback.Factory.Create(this, () => blurred = true))
            .Add(p => p.ValidityStateChangeEvent, EventCallback.Factory.Create<ValidityState>(this, value => validity = value)));

        await cut.InvokeAsync(() => cut.Instance.ValueChange(JsonDocument.Parse("\"updated\"").RootElement));
        await cut.InvokeAsync(() => cut.Instance.IxChange(JsonDocument.Parse("\"changed\"").RootElement));
        await cut.InvokeAsync(() => cut.Instance.IxBlur());
        await cut.InvokeAsync(() => cut.Instance.ValidityStateChange(new ValidityState { Valid = true, ValueMissing = false }));

        Assert.Equal("updated", valueChange);
        Assert.Equal("updated", cut.Instance.Value);
        Assert.Equal("changed", change);
        Assert.True(blurred);
        Assert.True(validity!.Valid);
    }

    [Fact]
    public void NumberInputSupportsNullableValueAndOfficialProperties()
    {
        var cut = Render<NumberInput>(parameters => parameters
            .Add(p => p.Id, "number")
            .Add(p => p.AllowEmptyValueChange, true)
            .Add(p => p.Value, null)
            .Add(p => p.TextAlignment, TextAlignment.Start));

        var element = cut.Find("ix-number-input");
        Assert.Equal("true", element.GetAttribute("allow-empty-value-change"));
        Assert.Equal("start", element.GetAttribute("text-alignment"));
        Assert.Null(element.GetAttribute("value"));
    }

    [Fact]
    public async Task NumberInputEmitsNullableValue()
    {
        double? received = 1;
        var cut = Render<NumberInput>(parameters => parameters
            .Add(p => p.Id, "number")
            .Add(p => p.ValueChangeEvent, EventCallback.Factory.Create<double?>(this, value => received = value)));

        await cut.Instance.ValueChange(JsonDocument.Parse("null").RootElement);

        Assert.Null(received);
        Assert.Null(cut.Instance.Value);
    }

    [Fact]
    public async Task NumberInputParsesNumericStringsAndInvalidValues()
    {
        double? changed = null;
        double? invalid = 1;
        var cut = Render<NumberInput>(parameters => parameters
            .Add(p => p.Id, "number")
            .Add(p => p.IxChangeEvent, EventCallback.Factory.Create<double?>(this, value =>
            {
                if (changed is null)
                {
                    changed = value;
                }
                else
                {
                    invalid = value;
                }
            })));

        await cut.Instance.IxChange(JsonDocument.Parse("\"12.5\"").RootElement);
        await cut.Instance.IxChange(JsonDocument.Parse("\"not-a-number\"").RootElement);

        Assert.Equal(12.5, changed);
        Assert.Null(invalid);
    }

    [Fact]
    public void TextAreaUsesTypedResizeBehavior()
    {
        var cut = Render<TextArea>(parameters => parameters
            .Add(p => p.Id, "textarea")
            .Add(p => p.ResizeBehavior, TextAreaResizeBehavior.Vertical));

        Assert.Equal("vertical", cut.Find("ix-textarea").GetAttribute("resize-behavior"));
    }

    [Fact]
    public async Task TextAreaNormalizesNullValuesAndForwardsEvents()
    {
        string? valueChange = null;
        string? change = null;
        var blurred = false;
        var cut = Render<TextArea>(parameters => parameters
            .Add(p => p.Id, "textarea")
            .Add(p => p.ValueChangeEvent, EventCallback.Factory.Create<string>(this, value => valueChange = value))
            .Add(p => p.IxChangeEvent, EventCallback.Factory.Create<string>(this, value => change = value))
            .Add(p => p.IxBlurEvent, EventCallback.Factory.Create(this, () => blurred = true)));

        await cut.InvokeAsync(() => cut.Instance.ValueChange(JsonDocument.Parse("null").RootElement));
        await cut.InvokeAsync(() => cut.Instance.IxChange(JsonDocument.Parse("null").RootElement));
        await cut.InvokeAsync(() => cut.Instance.IxBlur());

        Assert.Equal(string.Empty, valueChange);
        Assert.Equal(string.Empty, cut.Instance.Value);
        Assert.Equal(string.Empty, change);
        Assert.True(blurred);
    }

    [Fact]
    public async Task InputValidityStateUsesTypedCallback()
    {
        ValidityState? received = null;
        var cut = Render<Input>(parameters => parameters
            .Add(p => p.Id, "input")
            .Add(p => p.ValidityStateChangeEvent,
                EventCallback.Factory.Create<ValidityState>(this, value => received = value)));

        await cut.Instance.ValidityStateChange(new ValidityState { Valid = true });

        Assert.NotNull(received);
        Assert.True(received!.Valid);
    }

    [Fact]
    public async Task DateAndTimeInputsNormalizeNullValuesAndDeserializeValidity()
    {
        string? dateValue = "initial";
        string? dateChange = null;
        DateInputValidityState? dateValidity = null;
        string timeValue = "initial";
        string? timeChange = null;
        TimeInputValidityState? timeValidity = null;

        var date = Render<SiemensIXBlazor.Components.DateInput.DateInput>(parameters => parameters
            .Add(p => p.Id, "date")
            .Add(p => p.ValueChangeEvent, EventCallback.Factory.Create<string?>(this, value => dateValue = value))
            .Add(p => p.ChangeEvent, EventCallback.Factory.Create<string?>(this, value => dateChange = value))
            .Add(p => p.ValidityStateChangeEvent, EventCallback.Factory.Create<DateInputValidityState>(this, value => dateValidity = value)));
        var time = Render<SiemensIXBlazor.Components.TimeInput.TimeInput>(parameters => parameters
            .Add(p => p.Id, "time")
            .Add(p => p.ValueChangeEvent, EventCallback.Factory.Create<string>(this, value => timeValue = value))
            .Add(p => p.ChangeEvent, EventCallback.Factory.Create<string>(this, value => timeChange = value))
            .Add(p => p.ValidityStateChangeEvent, EventCallback.Factory.Create<TimeInputValidityState>(this, value => timeValidity = value)));

        await date.InvokeAsync(() => date.Instance.ValueChange(JsonDocument.Parse("null").RootElement));
        await date.InvokeAsync(() => date.Instance.Change(JsonDocument.Parse("\"2026/08/27\"").RootElement));
        await date.InvokeAsync(() => date.Instance.ValidityStateChange(JsonDocument.Parse("{\"valueMissing\":true,\"invalidReason\":\"required\"}").RootElement));
        await time.InvokeAsync(() => time.Instance.ValueChange(JsonDocument.Parse("null").RootElement));
        await time.InvokeAsync(() => time.Instance.Change(JsonDocument.Parse("\"12:30\"").RootElement));
        await time.InvokeAsync(() => time.Instance.ValidityStateChange(JsonDocument.Parse("{\"patternMismatch\":true,\"invalidReason\":\"format\"}").RootElement));

        Assert.Null(dateValue);
        Assert.Equal("2026/08/27", dateChange);
        Assert.True(dateValidity!.ValueMissing);
        Assert.Equal("required", dateValidity.InvalidReason);
        Assert.Equal(string.Empty, timeValue);
        Assert.Equal("12:30", timeChange);
        Assert.True(timeValidity!.PatternMismatch);
        Assert.Equal("format", timeValidity.InvalidReason);
    }

    [Fact]
    public void CustomFieldOnlyRendersItsDefaultSlot()
    {
        var cut = Render<CustomField>(parameters => parameters
            .Add(p => p.Id, "custom")
            .AddChildContent("control"));

        Assert.Contains("control", cut.Find("ix-custom-field").InnerHtml);
        Assert.DoesNotContain("file-upload", cut.Markup);
    }

    [Fact]
    public void FieldLabelAndHelperTextMapTheirPublicAttributes()
    {
        var label = Render<FieldLabel>(parameters => parameters
            .Add(p => p.HtmlFor, "input")
            .Add(p => p.Required, true)
            .AddChildContent("Label"));
        var helper = Render<HelperText>(parameters => parameters
            .Add(p => p.HtmlFor, "input")
            .Add(p => p.HelperText, "Help")
            .Add(p => p.InvalidText, "Error"));

        Assert.Equal("input", label.Find("ix-field-label").GetAttribute("html-for"));
        Assert.Equal("true", label.Find("ix-field-label").GetAttribute("required"));
        Assert.Equal("input", helper.Find("ix-helper-text").GetAttribute("html-for"));
        Assert.Equal("Help", helper.Find("ix-helper-text").GetAttribute("helper-text"));
        Assert.Equal("Error", helper.Find("ix-helper-text").GetAttribute("invalid-text"));
    }
}
