using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 195, Pack = 1)]
public struct RawZztElement
{
    [FieldOffset(0)] public byte Character;
    [FieldOffset(1)] public byte Color;
    [FieldOffset(2)] public byte IsDestructible;
    [FieldOffset(3)] public byte IsPushable;
    [FieldOffset(4)] public byte IsAlwaysVisible;
    [FieldOffset(5)] public byte IsEditorFloor;
    [FieldOffset(6)] public byte IsFloor;
    [FieldOffset(7)] public byte HasDrawFunc;
    [FieldOffset(8)] public RawZztPointer DrawFunc;
    [FieldOffset(12)] public short Cycle;
    [FieldOffset(14)] public RawZztPointer ActFunc;
    [FieldOffset(18)] public RawZztPointer InteractFunc;
    [FieldOffset(22)] public short Menu;
    [FieldOffset(24)] public byte MenuKey;
    [FieldOffset(25)] public RawZztElementString Name;
    [FieldOffset(46)] public RawZztElementString EditorCategoryText;
    [FieldOffset(67)] public RawZztElementString EditorP1Text;
    [FieldOffset(88)] public RawZztElementString EditorP2Text;
    [FieldOffset(109)] public RawZztElementString EditorP3Text;
    [FieldOffset(130)] public RawZztElementString EditorBoardText;
    [FieldOffset(151)] public RawZztElementString EditorStepText;
    [FieldOffset(172)] public RawZztElementString EditorCodeText;
    [FieldOffset(193)] public short Score;
}