using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 2, Pack = 1)]
public struct RawZztTile
{
    [NonSerialized] [FieldOffset(0)] public ushort Value;
    [FieldOffset(0)] public byte Element;
    [FieldOffset(1)] public byte Color;
}