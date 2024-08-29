using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 4, Pack = 1)]
public struct RawZztPointer
{
    [NonSerialized] [FieldOffset(0)] public uint Value;
    [FieldOffset(0)] public ushort Segment;
    [FieldOffset(2)] public ushort Offset;
}