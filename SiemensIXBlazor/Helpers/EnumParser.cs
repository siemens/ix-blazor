namespace SiemensIXBlazor.Helpers
{
    public static class EnumParser<TEnum> where TEnum : Enum
    {
        public static string ParseEnumToString(object enumValue) => Enum.GetName(typeof(TEnum), enumValue)!.ToLower();
    }
}
