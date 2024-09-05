using JetBrains.Annotations;

namespace LibRoton;

[PublicAPI]
public partial class Element
{
    public int Id { get; set; }
    public ElementType Type { get; set; }
    public int Character { get; set; }
    public int Color { get; set; }
    public bool IsDestructible { get; set; }
    public bool IsPushable { get; set; }
    public bool IsAlwaysVisible { get; set; }
    public bool IsEditorFloor { get; set; }
    public bool IsFloor { get; set; }
    public int Cycle { get; set; }
    public int Menu { get; set; }
    public char MenuKey { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EditorCategoryText { get; set; } = string.Empty;
    public string EditorP1Text { get; set; } = string.Empty;
    public string EditorP2Text { get; set; } = string.Empty;
    public string EditorP3Text { get; set; } = string.Empty;
    public string EditorBoardText { get; set; } = string.Empty;
    public string EditorStepText { get; set; } = string.Empty;
    public string EditorCodeText { get; set; } = string.Empty;
    public int Score { get; set; }
}