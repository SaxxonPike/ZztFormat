namespace LibRoton.Structures;

public interface IElementProperties
{
    byte Character { get; }
    byte Color { get; }
    bool IsDestructible { get; }
    bool IsPushable { get; }
    bool IsAlwaysVisible { get; }
    bool IsEditorFloor { get; }
    bool IsFloor { get; }
    bool HasDrawFunc { get; }
    int DrawFunc { get; }
    short Cycle { get; }
    int ActFunc { get; }
    int InteractFunc { get; }
    short Menu { get; }
    byte MenuKey { get; }
    string Name { get; }
    string EditorCategoryText { get; }
    string EditorP1Text { get; }
    string EditorP2Text { get; }
    string EditorP3Text { get; }
    string EditorBoardText { get; }
    string EditorStepText { get; }
    string EditorCodeText { get; }
    short Score { get; }
}