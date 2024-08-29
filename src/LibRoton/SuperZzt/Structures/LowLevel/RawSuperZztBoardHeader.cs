using System.Runtime.InteropServices;

namespace LibRoton.SuperZzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct RawSuperZztBoardHeader
{
    public const int Size = 63;
    
    [FieldOffset(0)] public short Length;
    [FieldOffset(2)] public RawSuperZztBoardName Name;
}