using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Button;

namespace SiemensIXBlazor.Tests
{
    public class IconButtonTests: TestContextBase
    {
        [Fact]
        public void IconButtonRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<IconButton>(parameters => {
                parameters.Add(p => p.IconColor, "testIconColor");
                parameters.Add(p => p.Disabled, true);
                parameters.Add(p => p.Ghost, true);
                parameters.Add(p => p.Icon, "testIcon");
                parameters.Add(p => p.Loading, true);
                parameters.Add(p => p.Outline, true);
                parameters.Add(p => p.Oval, true);
                parameters.Add(p => p.Size, IconButtonSize._12);
                parameters.Add(p => p.Selected, true);
                parameters.Add(p => p.DataTooltip, "testDataTooltip");
                parameters.Add(p => p.Type, ButtonType.Button);
                parameters.Add(p => p.A11yLabel, "testA11Label");
                parameters.Add(p => p.Variant, ButtonVariant.primary);
            });

            // Assert
            cut.MarkupMatches("<ix-icon-button  disabled=\"\" ghost=\"\" outline=\"\" selected=\"\" type=\"button\" variant=\"primary\" icon-color=\"testIconColor\" icon=\"testIcon\" oval=\"\" loading=\"\" data-tooltip=\"testDataTooltip\" size=\"12\" a11y-label=\"testA11Label\"></ix-icon-button>");
        }

        [Fact]
        public void IconButtonComponentHandlesClickEvent()
        {
            // Arrange
            var clickInvoked = false;
            var cut = RenderComponent<IconButton>(parameters => parameters
                .Add(p => p.ClickEvent, EventCallback.Factory.Create(this, () => clickInvoked = true)));

            // Act
            cut.Find("ix-icon-button").Click();

            // Assert
            Assert.True(clickInvoked);
        }
    }
}
