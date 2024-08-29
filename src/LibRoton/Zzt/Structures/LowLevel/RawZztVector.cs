using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 4, Pack = 1)]
public struct RawZztVector
{
    [FieldOffset(0)] public short X;
    [FieldOffset(2)] public short Y;
}