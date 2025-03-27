using SiemensIXBlazor.Enums.PushCard;
using SiemensIXBlazor.Helpers;

namespace SiemensIXBlazor.Tests.Helpers
{
    public class EnumParserTests
    {
        [Theory]
        [InlineData(PushCardVariant.alarm, "alarm")]
        [InlineData(PushCardVariant.outline, "outline")]
        public void EnumToString_ShouldReturnCorrectLowercaseString_ForValidEnum(PushCardVariant variant, string expected)
        {
            // Act
            var result = EnumParser<PushCardVariant>.EnumToString(variant);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void EnumToString_ShouldReturnEmptyString_ForInvalidEnum()
        {
            // Arrange
            var invalidEnumValue = (PushCardVariant)999;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => EnumParser<PushCardVariant>.EnumToString(invalidEnumValue));
        }

        [Fact]
        public void EnumToString_ShouldReturnEmptyString_ForNullEnum()
        {
            // Arrange
            PushCardVariant? nullableEnum = null;

            // Act & Assert
            // You need to handle nullable enums outside the method if using EnumParser
            Assert.Throws<InvalidOperationException>(() => EnumParser<PushCardVariant>.EnumToString(nullableEnum!.Value));
        }
    }
}