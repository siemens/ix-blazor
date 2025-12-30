// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Select;
using System.Text.Json;

namespace SiemensIXBlazor.Tests.Select;

public class SelectTests : TestContextBase
{
    [Fact]
    public void ComponentRendersWithoutCrashing()
    {
        // Arrange
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "test-select")
            .Add(p => p.Value, "1"));

        // Assert
        cut.MarkupMatches("<ix-select id='test-select' value='1'" +
                          "i18n-placeholder='Select an option' i18n-placeholder-editable='Type of select option' " +
                          "i18n-select-list-header='Please select an option' mode='single' " +
                          "i18n-no-matches='No matches'></ix-select>");
    }

    [Fact]
    public void IdPropertyIsSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "custom-id")
            .Add(p => p.Value, "1"));

        // Assert
        Assert.Equal("custom-id", cut.Instance.Id);
        cut.MarkupMatches("<ix-select id='custom-id' value='1'" +
                          "i18n-placeholder='Select an option' i18n-placeholder-editable='Type of select option' " +
                          "i18n-select-list-header='Please select an option' mode='single' " +
                          "i18n-no-matches='No matches'></ix-select>");
    }

    [Fact]
    public void AllowClearPropertyIsSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "test-select")
            .Add(p => p.AllowClear, true));

        // Assert
        Assert.True(cut.Instance.AllowClear);
        cut.MarkupMatches("<ix-select id='test-select' allow-clear " +
                          "i18n-placeholder='Select an option' i18n-placeholder-editable='Type of select option' " +
                          "i18n-select-list-header='Please select an option' mode='single'" +
                          "i18n-no-matches='No matches'></ix-select>");
    }

    [Fact]
    public void ModePropertyIsSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "test-select")
            .Add(p => p.Value, "1")
            .Add(p => p.Mode, SelectMode.Multiple));

        // Assert
        Assert.Equal(SelectMode.Multiple, cut.Instance.Mode);
        cut.MarkupMatches("<ix-select id='test-select' value='1'" +
                          "i18n-placeholder='Select an option' i18n-placeholder-editable='Type of select option' " +
                          "i18n-select-list-header='Please select an option' mode='multiple' " +
                          "i18n-no-matches='No matches'></ix-select>");
    }

    [Fact]
    public void ValuePropertyIsSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "test-select")
            .Add(p => p.Value, "testValue"));

        // Assert
        Assert.Equal("testValue", cut.Instance.Value);
        cut.MarkupMatches("<ix-select id='test-select' value='testValue'" +
                          "i18n-placeholder='Select an option' i18n-placeholder-editable='Type of select option' " +
                          "i18n-select-list-header='Please select an option' mode='single'" +
                          "i18n-no-matches='No matches'></ix-select>");
    }

    [Fact]
    public void ClassPropertyIsSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "test-select")
            .Add(p => p.Value, "1")
            .Add(p => p.Class, "ix-warning"));

        // Assert
        Assert.Equal("ix-warning", cut.Instance.Class);
        cut.MarkupMatches("<ix-select id='test-select' value='1'" +
                          "i18n-placeholder='Select an option' i18n-placeholder-editable='Type of select option' " +
                          "i18n-select-list-header='Please select an option' mode='single' class='ix-warning' " +
                          "i18n-no-matches='No matches'></ix-select>");
    }

    [Fact]
    public void WarningTextPropertyIsSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "test-select")
            .Add(p => p.Value, "1")
            .Add(p => p.WarningText, "This is a warning text"));

        // Assert
        Assert.Equal("This is a warning text", cut.Instance.WarningText);
        cut.MarkupMatches("<ix-select id='test-select' value='1'" +
                          "i18n-placeholder='Select an option' i18n-placeholder-editable='Type of select option' " +
                          "i18n-select-list-header='Please select an option' mode='single' " +
                          "i18n-no-matches='No matches' " +
                          "warning-text='This is a warning text'></ix-select>");
    }

    [Fact]
    public void ChildContentIsRenderedCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "test-select")
            .Add(p => p.Value, "1")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<SelectItem>(0);
                builder.AddAttribute(1, "Id", "selectItem1");
                builder.AddAttribute(2, "Label", "Item 1");
                builder.AddAttribute(3, "Value", "1");
                builder.CloseComponent();
            }));

        // Assert
        cut.MarkupMatches("<ix-select id='test-select' value='1'" +
                          "i18n-placeholder='Select an option' i18n-placeholder-editable='Type of select option' " +
                          "i18n-select-list-header='Please select an option' mode='single' " +
                          "i18n-no-matches='No matches'>" +
                          "<ix-select-item id='selectItem1' label='Item 1' value='1'></ix-select-item>" +
                          "</ix-select>");
    }

    [Fact]
    public async Task AddItemEventIsTriggeredCorrectly()
    {
        // Arrange
        string? addedItem = null;
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "test-select")
            .Add(p => p.AddItemEvent, EventCallback.Factory.Create<string>(this, (item) => addedItem = item)));

        // Act
        await cut.Instance.AddItemChanged("New Item");

        // Assert
        Assert.Equal("New Item", addedItem);
    }

    [Fact]
    public async Task ValueChangeEventIsTriggeredCorrectlyWithString()
    {
        // Arrange
        string? changedValue = null;
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "test-select")
            .Add(p => p.ValueChangeEvent, EventCallback.Factory.Create<dynamic>(this, (value) => changedValue = value)));

        // Act
        var jsonElement = JsonDocument.Parse("\"Option 1\"").RootElement;
        await cut.Instance.ValueChanged(jsonElement);

        // Assert
        Assert.Equal("Option 1", changedValue);
    }

    [Fact]
    public async Task ValueChangeEventIsTriggeredCorrectlyWithArray()
    {
        // Arrange
        string[]? changedValues = null;
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "test-select")
            .Add(p => p.ValueChangeEvent, EventCallback.Factory.Create<dynamic>(this, (value) => changedValues = value)));

        // Act
        var jsonElement = JsonDocument.Parse("[\"Option 1\", \"Option 2\"]").RootElement;
        await cut.Instance.ValueChanged(jsonElement);

        // Assert
        Assert.NotNull(changedValues);
        Assert.Equal(2, changedValues.Length);
        Assert.Equal("Option 1", changedValues[0]);
        Assert.Equal("Option 2", changedValues[1]);
    }

    [Fact]
    public async Task InputChangeEventIsTriggeredCorrectly()
    {
        // Arrange
        string? changedInput = null;
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "test-select")
            .Add(p => p.InputChangeEvent, EventCallback.Factory.Create<string>(this, (input) => changedInput = input)));

        // Act
        await cut.Instance.InputChanged("New Input");

        // Assert
        Assert.Equal("New Input", changedInput);
    }

    [Fact]
    public async Task BlurEventIsTriggeredCorrectly()
    {
        // Arrange
        bool blurTriggered = false;
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "test-select")
            .Add(p => p.BlurEvent, EventCallback.Factory.Create<object>(this, (_) => blurTriggered = true)));

        // Act
        await cut.Instance.Blurred();

        // Assert
        Assert.True(blurTriggered);
    }

    [Fact]
    public void ComplexSelectComponentRendersCorrectly()
    {
        // Arrange & Act
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "valid-select")
            .Add(p => p.Class, "ix-warning")
            .Add(p => p.Mode, SelectMode.Single)
            .Add(p => p.Value, "2")
            .Add(p => p.ValidText, "Your selection is correct!")
            .Add(p => p.InfoText, "This is an info text")
            .Add(p => p.WarningText, "This is a warning text")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<SelectItem>(0);
                builder.AddAttribute(1, "Id", "selectItem1");
                builder.AddAttribute(2, "Label", "Item 1");
                builder.AddAttribute(3, "Value", "1");
                builder.CloseComponent();

                builder.OpenComponent<SelectItem>(4);
                builder.AddAttribute(5, "Id", "selectItem2");
                builder.AddAttribute(6, "Label", "Item 2");
                builder.AddAttribute(7, "Value", "2");
                builder.CloseComponent();
            }));

        // Assert
        cut.MarkupMatches("<ix-select id='valid-select' value='2'" +
                          "i18n-placeholder='Select an option' i18n-placeholder-editable='Type of select option' " +
                          "i18n-select-list-header='Please select an option' mode='single' " +
                          "class='ix-warning' i18n-no-matches='No matches' " +
                          " info-text='This is an info text' valid-text='Your selection is correct!' " +
                          "warning-text='This is a warning text'>" +
                          "<ix-select-item id='selectItem1' label='Item 1' value='1'></ix-select-item>" +
                          "<ix-select-item id='selectItem2' label='Item 2' value='2'></ix-select-item>" +
                          "</ix-select>");
    }
}

