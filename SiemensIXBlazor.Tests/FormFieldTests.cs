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
        var cut = RenderComponent<Input>(parameters => parameters
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
    public void NumberInputSupportsNullableValueAndOfficialProperties()
    {
        var cut = RenderComponent<NumberInput>(parameters => parameters
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
        var cut = RenderComponent<NumberInput>(parameters => parameters
            .Add(p => p.Id, "number")
            .Add(p => p.ValueChangeEvent, EventCallback.Factory.Create<double?>(this, value => received = value)));

        await cut.Instance.ValueChange(JsonDocument.Parse("null").RootElement);

        Assert.Null(received);
        Assert.Null(cut.Instance.Value);
    }

    [Fact]
    public void TextAreaUsesTypedResizeBehavior()
    {
        var cut = RenderComponent<TextArea>(parameters => parameters
            .Add(p => p.Id, "textarea")
            .Add(p => p.ResizeBehavior, TextAreaResizeBehavior.Vertical));

        Assert.Equal("vertical", cut.Find("ix-textarea").GetAttribute("resize-behavior"));
    }

    [Fact]
    public async Task InputValidityStateUsesTypedCallback()
    {
        ValidityState? received = null;
        var cut = RenderComponent<Input>(parameters => parameters
            .Add(p => p.Id, "input")
            .Add(p => p.ValidityStateChangeEvent,
                EventCallback.Factory.Create<ValidityState>(this, value => received = value)));

        await cut.Instance.ValidityStateChange(new ValidityState { Valid = true });

        Assert.NotNull(received);
        Assert.True(received!.Valid);
    }

    [Fact]
    public void CustomFieldOnlyRendersItsDefaultSlot()
    {
        var cut = RenderComponent<CustomField>(parameters => parameters
            .Add(p => p.Id, "custom")
            .AddChildContent("control"));

        Assert.Contains("control", cut.Find("ix-custom-field").InnerHtml);
        Assert.DoesNotContain("file-upload", cut.Markup);
    }

    [Fact]
    public void FieldLabelAndHelperTextMapTheirPublicAttributes()
    {
        var label = RenderComponent<FieldLabel>(parameters => parameters
            .Add(p => p.HtmlFor, "input")
            .Add(p => p.Required, true)
            .AddChildContent("Label"));
        var helper = RenderComponent<HelperText>(parameters => parameters
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
