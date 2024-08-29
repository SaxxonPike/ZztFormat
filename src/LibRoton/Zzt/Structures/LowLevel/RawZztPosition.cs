using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 2, Pack = 1)]
public struct RawZztPosition
{
    [FieldOffset(0)] public byte X;
    [FieldOffset(1)] public byte Y;
}