using LibRoton.Structures;

namespace LibRoton.Resources;

public static class Elements
{
    public static List<Element> Load(int worldType)
    {
        return worldType switch
        {
            -1 => LoadZztElements(),
            -2 => LoadSuperZztElements(),
            _ => throw new Exception($"Unknown world type {worldType}.")
        };
    }

    private static List<Element> LoadZztElements()
    {
        var bytes = Resources.ZztElements;
        var stream = new MemoryStream(bytes);
        var count = bytes.Length / ZztElementProperties.Size;
        var result = new List<Element>();

        for (var i = 0; i < count; i++)
            result.Add(Element.Read(stream, -1));

        return result;
    }
    
    private static List<Element> LoadSuperZztElements()
    {
        var bytes = Resources.SuperZztElements;
        var stream = new MemoryStream(bytes);
        var count = bytes.Length / SuperZztElementProperties.Size;
        var result = new List<Element>();

        for (var i = 0; i < count; i++)
            result.Add(Element.Read(stream, -2));

        return result;
    }
}