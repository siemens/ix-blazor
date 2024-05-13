using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using SiemensIXBlazor.Components.About;

namespace SiemensIXBlazor.Tests.About
{
    public class AboutMenuItemTest: TestContext
    {
        private readonly Mock<IJSRuntime> _jsRuntimeMock;
        private readonly Mock<IJSObjectReference> _jsObjectReferenceMock;

        public AboutMenuItemTest()
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
            var cut = RenderComponent<AboutMenuItem>();

            // Assert
            cut.MarkupMatches("<ix-menu-about-item></ix-menu-about-item>");
        }

        [Fact]
        public void ChildContentPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuItem>(parameters => parameters.Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

            // Assert
            Assert.NotNull(cut.Instance.ChildContent);
        }

        [Fact]
        public void LablePropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuItem>(parameters => parameters.Add(p => p.Label, "testLabel"));

            // Assert
            Assert.Equal("testLabel", cut.Instance.Label);
        }
    }
}
