namespace SiemensIXBlazor.Objects
{
    public class IXFile
    {
        public string Name { get; }
        public long Size { get; }
        public string Type { get; }
        public string Base64Data { get; }

        public IXFile(string name, long size, string type, string base64Data)
        {
            Name = name;
            Size = size;
            Type = type;
            Base64Data = base64Data;
        }
    }
}
