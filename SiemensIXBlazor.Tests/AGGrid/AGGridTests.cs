using System.Diagnostics;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using SiemensIXBlazor.Components.AGGrid;

namespace SiemensIXBlazor.Tests
{
    public class AGGridTests : TestContextBase
    {
        [Fact]
        public void ComponentRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<AGGrid>();

            // Assert
            cut.MarkupMatches("<div id=''></div>");
        }

        [Fact]
        public void OnCellClickedPropertyIsSetCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<AGGrid>(parameters => parameters.Add(p => p.OnCellClicked, EventCallback.Factory.Create(this, () => eventTriggered = true)));

            // Act
            cut.Instance.OnCellClicked.InvokeAsync(true);

            //Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void IdPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AGGrid>(parameters => parameters.Add(p => p.Id, "testId"));

            // Assert
            Assert.Equal("testId", cut.Instance.Id);
        }

        [Fact]
        public async Task CreateGrid_ReturnsNull_WhenIdIsEmpty()
        {
            // Arrange
            var grid = new AGGrid();

            // Act
            var result = await grid.CreateGrid(new GridOptions());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateGrid_ReturnsJSObjectReference_WhenIdIsNotEmpty()
        {
            // Arrange
            var gridOptions = new GridOptions();
            Mock<IJSRuntime> jsRuntimeMock = new();
            Mock<IJSObjectReference> jsObjectReferenceMock = new();

            // Mock of module import for JSRuntime
            jsRuntimeMock.Setup(x => x.InvokeAsync<IJSObjectReference>("agGridInterop.createGrid", It.IsAny<object[]>()))
                .Returns(new ValueTask<IJSObjectReference>(jsObjectReferenceMock.Object));
            Services.AddSingleton(jsRuntimeMock.Object);

            var cut = RenderComponent<AGGrid>(parameters => parameters.Add(p => p.Id, "testId"));

            // Act
            var result = await cut.Instance.CreateGrid(gridOptions);

            // Assert
            Assert.NotNull(result);
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("agGridInterop.createGrid", It.IsAny<object[]>()), Times.Once);
        }

        [Fact]
        public async Task GetSelectedRows_ReturnsObject()
        {
            // Arrange
            var jsRuntimeMock = new Mock<IJSRuntime>();
            Services.AddSingleton(jsRuntimeMock.Object);
            var cut = RenderComponent<AGGrid>(parameters => parameters.Add(p => p.Id, "testId"));
            var jsObjectReferenceMock = new Mock<IJSObjectReference>();
            jsRuntimeMock.Setup(x => x.InvokeAsync<object>("agGridInterop.getSelectedRows", It.IsAny<object[]>()))
                .ReturnsAsync(new object());
            

            // Act
            var result = await cut.Instance.GetSelectedRows(jsObjectReferenceMock.Object);

            // Assert
            Assert.NotNull(result);
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("agGridInterop.getSelectedRows", It.IsAny<object[]>()), Times.Once);
        }
    }
}