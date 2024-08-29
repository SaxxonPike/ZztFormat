using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 21, Pack = 1)]
public struct RawZztWorldString
{
    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public RawZztWorldStringText Text;
}