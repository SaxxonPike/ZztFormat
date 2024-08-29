using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct RawZztBoardHeader
{
    public const int Size = 53;
    
    [FieldOffset(0)] public short Length;
    [FieldOffset(2)] public RawZztBoardName Name;
}