using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 3, Pack = 1)]
public struct RawZztTileRle
{
    [FieldOffset(0)] public byte Count;
    [FieldOffset(1)] public byte Element;
    [FieldOffset(2)] public byte Color;
}