using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components.MapNavigationOverlay;
using System.ComponentModel;

namespace SiemensIXBlazor.Tests.MapNavigation
{
    public class MapNavigationOverlayTest : TestContextBase
    {
        [Fact]
        public void ComponentRendersWithParametersSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MapNavigationOverlay>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.Name, "testApp")
                .Add(p => p.Icon, "testIcon")
                .Add(p => p.IconColor, "testColor"));

            // Assert
            cut.MarkupMatches(
            "<ix-map-navigation-overlay id=\"testId\" name=\"testApp\" icon=\"testIcon\" icon-color=\"testColor\" ></ix-map-navigation-overlay>");

        }
        [Fact]
        public void EventCallbacksAreTriggeredCorrectly()
        {
            // Arrange
            var isCloseButtonClicked = false;

            var cut = RenderComponent<MapNavigationOverlay>(parameters => parameters
                  .Add(p => p.Id, "test-id")
                  .Add(p => p.CloseButtonClickedEvent, EventCallback.Factory.Create(this, () => isCloseButtonClicked = true))
              );


            // Act
            cut.InvokeAsync(() => cut.Instance.CloseClick());

            // Assert
            Assert.True(isCloseButtonClicked);
        }
    }
}