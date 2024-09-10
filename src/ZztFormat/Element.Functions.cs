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

    public static Element? Get(WorldType worldType, int elementId) =>
        worldType switch
        {
            WorldType.Zzt => GetZztElement(elementId),
            WorldType.SuperZzt => GetSuperZztElement(elementId),
            _ => throw new ZztFormatException($"Unknown world type {worldType}.")
        };

    public static Element? Get(WorldType worldType, ElementType element) =>
        worldType switch
        {
            WorldType.Zzt => Get(WorldType.Zzt, ElementList.UnmapZztElement(element)),
            WorldType.SuperZzt => Get(WorldType.SuperZzt, ElementList.UnmapSuperZztElement(element)),
            _ => throw new ZztFormatException($"Unknown world type {worldType}.")
        };

    internal static Element? GetZztElement(int elementId)
    {
        var offset = elementId * ZztElementProperties.Size;
        var data = Resources.ZztElements;
        if (offset >= data.Length || offset < 0)
            return default;
        
        var element = ConvertElement(
            ZztElementProperties.Read(data[offset..]),
            elementId,
            ElementList.MapZztElement(elementId));

        return element;
    }

    internal static Element? GetSuperZztElement(int elementId)
    {
        var offset = elementId * SuperZztElementProperties.Size;
        var data = Resources.SuperZztElements;
        if (offset >= data.Length || offset < 0)
            return default;

        var element = ConvertElement(
            SuperZztElementProperties.Read(data[offset..]),
            elementId,
            ElementList.MapSuperZztElement(elementId));

        return element;
    }

    public static Element Read(Stream stream, WorldType worldType, int index) =>
        worldType switch
        {
            WorldType.Zzt => ReadZztElement(stream, index),
            WorldType.SuperZzt => ReadSuperZztElement(stream, index),
            _ => throw new ZztFormatException($"Unknown world type {worldType}.")
        };

    internal static Element ConvertElement(
        ZztElementProperties element,
        int index,
        ElementType elementType) =>
        new()
        {
            Id = index,
            Type = elementType,
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
    
    internal static Element ConvertElement(
        SuperZztElementProperties element,
        int index,
        ElementType elementType) =>
        new()
        {
            Id = index,
            Type = elementType,
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

    internal static Element ReadZztElement(Stream stream, int index) => 
        ConvertElement(
            ZztElementProperties.Read(stream),
            index,
            ElementList.MapZztElement(index));

    internal static Element ReadZztElement(ReadOnlySpan<byte> bytes, int index) =>
        ConvertElement(
            ZztElementProperties.Read(bytes),
            index,
            ElementList.MapZztElement(index));

    internal static Element ReadSuperZztElement(Stream stream, int index) =>
        ConvertElement(
            SuperZztElementProperties.Read(stream),
            index,
            ElementList.MapSuperZztElement(index));

    internal static Element ReadSuperZztElement(ReadOnlySpan<byte> bytes, int index) =>
        ConvertElement(
            SuperZztElementProperties.Read(bytes),
            index,
            ElementList.MapSuperZztElement(index));
}