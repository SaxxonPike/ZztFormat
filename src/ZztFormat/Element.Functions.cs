namespace ZztFormat;

public partial class Element
{
    public override string ToString() =>
        string.IsNullOrEmpty(Name)
            ? Type.ToString()
            : Name;

    public int GetEditorColor(int desiredColor) =>
        Color switch
        {
            0xFE => ((Color & 0x7) << 4) | 0xF,
            0xFF => desiredColor,
            _ => Color,
        };

    public static Element? Get(int worldType, int elementId) =>
        worldType switch
        {
            -1 => GetZztElement(elementId),
            -2 => GetSuperZztElement(elementId),
            _ => throw new LibRotonException($"Unknown world type {worldType}.")
        };

    public static Element? Get(int worldType, ElementType element) =>
        worldType switch
        {
            -1 => Get(-1, ElementList.UnmapZztElement(element)),
            -2 => Get(-2, ElementList.UnmapSuperZztElement(element)),
            _ => throw new LibRotonException($"Unknown world type {worldType}.")
        };

    internal static Element? GetZztElement(int elementId)
    {
        var offset = elementId * ZztElementProperties.Size;
        var data = Resources.ZztElements;
        if (offset >= data.Length || offset < 0)
            return default;
        
        var element = ConvertElement(ZztElementProperties.Read(data[offset..]));
        element.Id = elementId;
        element.Type = ElementList.MapZztElement(elementId);
        return element;
    }

    internal static Element? GetSuperZztElement(int elementId)
    {
        var offset = elementId * SuperZztElementProperties.Size;
        var data = Resources.SuperZztElements;
        if (offset >= data.Length || offset < 0)
            return default;

        var element = ConvertElement(SuperZztElementProperties.Read(data[offset..]));
        element.Id = elementId;
        element.Type = ElementList.MapSuperZztElement(elementId);
        return element;
    }

    public static Element Read(Stream stream, int worldType) =>
        worldType switch
        {
            -1 => ReadZztElement(stream),
            -2 => ReadSuperZztElement(stream),
            _ => throw new LibRotonException($"Unknown world type {worldType}.")
        };

    internal static Element ConvertElement(ZztElementProperties element) =>
        new()
        {
            Character = element.Character,
            Color = element.Color,
            IsDestructible = element.DestructibleBit != 0,
            IsPushable = element.PushableBit != 0,
            IsAlwaysVisible = element.AlwaysVisibleBit != 0,
            IsEditorFloor = element.EditorFloorBit != 0,
            IsFloor = element.FloorBit != 0,
            Cycle = element.Cycle,
            Menu = element.Menu,
            MenuKey = (char)element.MenuKey,
            Name = element.Name,
            EditorCategoryText = element.EditorCategoryText,
            EditorP1Text = element.EditorP1Text,
            EditorP2Text = element.EditorP2Text,
            EditorP3Text = element.EditorP3Text,
            EditorBoardText = element.EditorBoardText,
            EditorStepText = element.EditorStepText,
            EditorCodeText = element.EditorCodeText,
            Score = 0
        };
    
    internal static Element ConvertElement(SuperZztElementProperties element) =>
        new()
        {
            Character = element.Character,
            Color = element.Color,
            IsDestructible = element.DestructibleBit != 0,
            IsPushable = element.PushableBit != 0,
            IsEditorFloor = element.EditorFloorBit != 0,
            IsFloor = element.FloorBit != 0,
            Cycle = element.Cycle,
            Menu = element.Menu,
            MenuKey = (char)element.MenuKey,
            Name = element.Name,
            EditorCategoryText = element.EditorCategoryText,
            EditorP1Text = element.EditorP1Text,
            EditorP2Text = element.EditorP2Text,
            EditorP3Text = element.EditorP3Text,
            EditorBoardText = element.EditorBoardText,
            EditorStepText = element.EditorStepText,
            EditorCodeText = element.EditorCodeText,
            Score = 0
        };

    internal static Element ReadZztElement(Stream stream) => 
        ConvertElement(ZztElementProperties.Read(stream));

    internal static Element ReadZztElement(ReadOnlySpan<byte> bytes) =>
        ConvertElement(ZztElementProperties.Read(bytes));

    internal static Element ReadSuperZztElement(Stream stream) =>
        ConvertElement(SuperZztElementProperties.Read(stream));

    internal static Element ReadSuperZztElement(ReadOnlySpan<byte> bytes) =>
        ConvertElement(SuperZztElementProperties.Read(bytes));
}