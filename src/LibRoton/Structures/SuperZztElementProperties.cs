namespace LibRoton.Structures;

public partial class SuperZztElementProperties
{
    public bool IsDestructible => DestructibleBit != 0;
    public bool IsPushable => PushableBit != 0;
    public bool IsAlwaysVisible { get; }
    public bool IsEditorFloor => EditorFloorBit != 0;
    public bool IsFloor => FloorBit != 0;
    public bool HasDrawFunc => DrawFuncBit != 0;
}