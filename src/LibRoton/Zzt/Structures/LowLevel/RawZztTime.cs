using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 4, Pack = 1)]
public struct RawZztTime
{
    [NonSerialized] [FieldOffset(0)] public int Value;
    [FieldOffset(0)] public short Seconds;
    [FieldOffset(2)] public short Fraction;
}