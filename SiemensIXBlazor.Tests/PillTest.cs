using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Pill;

namespace SiemensIXBlazor.Tests
{
    public class PillTests : TestContextBase
    {
        [Fact]
        public void PillRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Pill>(
                ("AlignLeft", true),
                ("Background", "red"),
                ("Color", "white"),
                ("Icon", "testIcon"),
                ("Outline", true),
                ("Variant", PillVariant.primary),
                ("ChildContent", (RenderFragment)(builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.AddContent(1, "Test child content");
                    builder.CloseElement();
                }))
            );

            // Assert
       
            cut.MarkupMatches("<ix-pill align-left=\"\" background=\"red\" color=\"white\" icon=\"testIcon\" outline=\"\" variant=\"primary\"><div>Test child content</div></ix-pill>");
        }
    }
}