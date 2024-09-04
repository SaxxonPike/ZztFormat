namespace LibRoton.Structures;

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

    public static Element Read(Stream stream, int worldType)
    {
        return worldType switch
        {
            -1 => ReadZztElement(stream),
            -2 => ReadSuperZztElement(stream),
            _ => throw new Exception($"Unknown world type {worldType}.")
        };
    }

    private static Element ReadZztElement(Stream stream)
    {
        var element = ZztElementProperties.Read(stream);
        return new Element
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
    }

    private static Element ReadSuperZztElement(Stream stream)
    {
        var element = SuperZztElementProperties.Read(stream);
        return new Element
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
    }
}