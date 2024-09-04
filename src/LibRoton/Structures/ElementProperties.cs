namespace LibRoton.Structures;

public class ElementProperties
{
    public required Element Element { get; init; }
    public int Character { get; set; }
    public DosColor ForegroundColor { get; set; }
    public DosColor BackgroundColor { get; set; }
    public bool Blink { get; set; }
    public bool EditorForceExactColor { get; set; }
    public bool EditorForceWhite { get; set; }
    public bool IsDestructible { get; set; }
    public bool IsPushable { get; set; }
    public bool IsAlwaysVisible { get; set; }
    public bool IsEditorFloor { get; set; }
    public bool IsFloor { get; set; }
    public bool HasDrawFunc { get; set; }
    public DrawBehavior DrawFunc { get; set; }
    public int Cycle { get; set; }
    public ActBehavior ActFunc { get; set; }
    public InteractBehavior InteractFunc { get; set; }
    public int Menu { get; set; }
    public char MenuKey { get; set; }
    public int Score { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EditorCategory { get; set; } = string.Empty;
    public string EditorP1Text { get; set; } = string.Empty;
    public string EditorP2Text { get; set; } = string.Empty;
    public string EditorP3Text { get; set; } = string.Empty;
    public string EditorBoardText { get; set; } = string.Empty;
    public string EditorStepText { get; set; } = string.Empty;
    public string EditorCodeText { get; set; } = string.Empty;
}