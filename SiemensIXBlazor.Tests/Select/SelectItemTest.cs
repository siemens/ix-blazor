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

namespace SiemensIXBlazor.Tests.Select;

public class SelectItemTests : TestContextBase
{
    [Fact]
    public void ComponentRendersWithoutCrashing()
    {
        // Arrange
        var cut = RenderComponent<SelectItem>(parameters => parameters
            .Add(p => p.Id, "test-select-item"));

        // Assert
        cut.MarkupMatches("<ix-select-item id='test-select-item'></ix-select-item>");
    }

    [Fact]
    public void IdPropertyIsSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<SelectItem>(parameters => parameters
            .Add(p => p.Id, "custom-item-id"));

        // Assert
        Assert.Equal("custom-item-id", cut.Instance.Id);
        cut.MarkupMatches("<ix-select-item id='custom-item-id'></ix-select-item>");
    }

    [Fact]
    public void LabelPropertyIsSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<SelectItem>(parameters => parameters
            .Add(p => p.Id, "test-select-item")
            .Add(p => p.Label, "Test Label"));

        // Assert
        Assert.Equal("Test Label", cut.Instance.Label);
        cut.MarkupMatches("<ix-select-item id='test-select-item' label='Test Label'></ix-select-item>");
    }

    [Fact]
    public void ValuePropertyIsSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<SelectItem>(parameters => parameters
            .Add(p => p.Id, "test-select-item")
            .Add(p => p.Value, "item-value"));

        // Assert
        Assert.Equal("item-value", cut.Instance.Value);
        cut.MarkupMatches("<ix-select-item id='test-select-item' value='item-value'></ix-select-item>");
    }

    [Fact]
    public void SelectedPropertyIsSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<SelectItem>(parameters => parameters
            .Add(p => p.Id, "test-select-item")
            .Add(p => p.Selected, true));

        // Assert
        Assert.True(cut.Instance.Selected);
        cut.MarkupMatches("<ix-select-item id='test-select-item' selected></ix-select-item>");
    }

    [Fact]
    public void ClassPropertyIsSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<SelectItem>(parameters => parameters
            .Add(p => p.Id, "test-select-item")
            .Add(p => p.Class, "custom-class"));

        // Assert
        Assert.Equal("custom-class", cut.Instance.Class);
        cut.MarkupMatches("<ix-select-item id='test-select-item' class='custom-class'></ix-select-item>");
    }

    [Fact]
    public void StylePropertyIsSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<SelectItem>(parameters => parameters
            .Add(p => p.Id, "test-select-item")
            .Add(p => p.Style, "color: red;"));

        // Assert
        Assert.Equal("color: red;", cut.Instance.Style);
        cut.MarkupMatches("<ix-select-item id='test-select-item' style='color: red;'></ix-select-item>");
    }

    [Fact]
    public async Task ItemClickEventIsTriggeredCorrectly()
    {
        // Arrange
        string? clickedItem = null;
        var cut = RenderComponent<SelectItem>(parameters => parameters
            .Add(p => p.Id, "test-select-item")
            .Add(p => p.ItemClickEvent, EventCallback.Factory.Create<string>(this, (item) => clickedItem = item)));

        // Act
        cut.Instance.ItemClicked("Item Label");

        // Assert
        Assert.Equal("Item Label", clickedItem);
    }

    [Fact]
    public void CompleteSelectItemRendersCorrectly()
    {
        // Arrange & Act
        var cut = RenderComponent<SelectItem>(parameters => parameters
            .Add(p => p.Id, "selectItem1")
            .Add(p => p.Label, "Item 1")
            .Add(p => p.Value, "1")
            .Add(p => p.Selected, true)
            .Add(p => p.Class, "custom-class")
            .Add(p => p.Style, "margin-top: 5px;"));

        // Assert
        cut.MarkupMatches("<ix-select-item id='selectItem1' label='Item 1' value='1' selected " +
                         "style='margin-top: 5px;' class='custom-class'></ix-select-item>");
    }

    [Fact]
    public void SelectItemRendersWithinSelect()
    {
        // Arrange & Act
        var cut = RenderComponent<Components.Select>(parameters => parameters
            .Add(p => p.Id, "parent-select")
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
        cut.MarkupMatches("<ix-select id='parent-select' value='1' " +
                          "i18n-placeholder='Select an option' i18n-placeholder-editable='Type of select option' " +
                          "i18n-select-list-header='Please select an option' mode='single'" +
                          "i18n-no-matches='No matches'>" +
                          "<ix-select-item id='selectItem1' label='Item 1' value='1'></ix-select-item>" +
                          "</ix-select>");
    }
}
