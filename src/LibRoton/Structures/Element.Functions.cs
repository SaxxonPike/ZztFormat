namespace LibRoton.Structures;

public partial class Element
{
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
            IsDestructible = element.DestructibleBit,
            IsPushable = element.PushableBit,
            IsAlwaysVisible = element.AlwaysVisibleBit,
            IsEditorFloor = element.EditorFloorBit,
            IsFloor = element.FloorBit,
            HasDrawFunc = element.DrawFuncBit,
            DrawFunc = element.DrawFunc,
            Cycle = element.Cycle,
            ActFunc = element.ActFunc,
            InteractFunc = element.InteractFunc,
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
            IsDestructible = element.DestructibleBit,
            IsPushable = element.PushableBit,
            IsEditorFloor = element.EditorFloorBit,
            IsFloor = element.FloorBit,
            HasDrawFunc = element.DrawFuncBit,
            DrawFunc = element.DrawFunc,
            Cycle = element.Cycle,
            ActFunc = element.ActFunc,
            InteractFunc = element.InteractFunc,
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