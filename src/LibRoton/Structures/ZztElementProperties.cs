namespace LibRoton.Structures;

public partial class ZztElementProperties
{
    public bool IsDestructible => DestructibleBit != 0;
    public bool IsPushable => PushableBit != 0;
    public bool IsAlwaysVisible => AlwaysVisibleBit != 0;
    public bool IsEditorFloor => EditorFloorBit != 0;
    public bool IsFloor => FloorBit != 0;
    public bool HasDrawFunc => DrawFuncBit != 0;
}