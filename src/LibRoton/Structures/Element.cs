using JetBrains.Annotations;

namespace LibRoton.Structures;

[PublicAPI]
public partial class Element
{
    public byte Character { get; set; }
    public byte Color { get; set; }
    public byte IsDestructible { get; set; }
    public byte IsPushable { get; set; }
    public byte IsAlwaysVisible { get; set; }
    public byte IsEditorFloor { get; set; }
    public byte IsFloor { get; set; }
    public byte HasDrawFunc { get; set; }
    public int DrawFunc { get; set; }
    public short Cycle { get; set; }
    public int ActFunc { get; set; }
    public int InteractFunc { get; set; }
    public short Menu { get; set; }
    public char MenuKey { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EditorCategoryText { get; set; } = string.Empty;
    public string EditorP1Text { get; set; } = string.Empty;
    public string EditorP2Text { get; set; } = string.Empty;
    public string EditorP3Text { get; set; } = string.Empty;
    public string EditorBoardText { get; set; } = string.Empty;
    public string EditorStepText { get; set; } = string.Empty;
    public string EditorCodeText { get; set; } = string.Empty;
    public short Score { get; set; }
}