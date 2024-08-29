using System.Runtime.InteropServices;
using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.SuperZzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 194, Pack = 1)]
public struct RawSuperZztElement
{
    [FieldOffset(0)] public byte Character;
    [FieldOffset(1)] public byte Color;
    [FieldOffset(2)] public byte IsDestructible;
    [FieldOffset(3)] public byte IsPushable;
    [FieldOffset(4)] public byte IsEditorFloor;
    [FieldOffset(5)] public byte IsFloor;
    [FieldOffset(6)] public byte HasDrawFunc;
    [FieldOffset(7)] public RawZztPointer DrawFunc;
    [FieldOffset(11)] public short Cycle;
    [FieldOffset(13)] public RawZztPointer ActFunc;
    [FieldOffset(17)] public RawZztPointer InteractFunc;
    [FieldOffset(21)] public short Menu;
    [FieldOffset(23)] public byte MenuKey;
    [FieldOffset(24)] public RawZztElementString Name;
    [FieldOffset(45)] public RawZztElementString EditorCategoryText;
    [FieldOffset(66)] public RawZztElementString EditorP1Text;
    [FieldOffset(87)] public RawZztElementString EditorP2Text;
    [FieldOffset(108)] public RawZztElementString EditorP3Text;
    [FieldOffset(129)] public RawZztElementString EditorBoardText;
    [FieldOffset(150)] public RawZztElementString EditorStepText;
    [FieldOffset(171)] public RawZztElementString EditorCodeText;
    [FieldOffset(192)] public short Score;
}