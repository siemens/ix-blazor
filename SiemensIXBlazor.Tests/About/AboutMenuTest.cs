using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using SiemensIXBlazor.Components.About;

namespace SiemensIXBlazor.Tests.About
{
    public class AboutMenuTest: TestContext
    {
        private readonly Mock<IJSRuntime> _jsRuntimeMock;
        private readonly Mock<IJSObjectReference> _jsObjectReferenceMock;

        public AboutMenuTest()
        {
            _jsRuntimeMock = new Mock<IJSRuntime>();
            _jsObjectReferenceMock = new Mock<IJSObjectReference>();

            // Mock of module import for JSRuntime
            _jsRuntimeMock.Setup(x => x.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]>()))
                          .Returns(new ValueTask<IJSObjectReference>(_jsObjectReferenceMock.Object));
            Services.AddSingleton(_jsRuntimeMock.Object);
        }


        [Fact]
        public void ComponentRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<AboutMenu>();

            // Assert
            cut.MarkupMatches("<ix-menu-about label=\"About &amp; legal information\" id=\"\"></ix-menu-about>");
        }

        [Fact]
        public void ChildContentPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenu>(parameters => parameters.Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

            // Assert
            Assert.NotNull(cut.Instance.ChildContent);
        }

        [Fact]
        public void IdPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenu>(parameters => parameters.Add(p => p.Id, "testId"));

            // Assert
            Assert.Equal("testId", cut.Instance.Id);
        }

        [Fact]
        public void ClosedEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<AboutMenu>(parameters => parameters.Add(p => p.ClosedEvent, EventCallback.Factory.Create<MouseEventArgs>(this, () => eventTriggered = true)));

            // Act
            cut.Instance.ClosedEvent.InvokeAsync(new MouseEventArgs());

            // Assert
            Assert.True(eventTriggered);
        }
    }
}
