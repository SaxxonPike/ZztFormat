using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 59, Pack = 1)]
public struct RawZztBoardMessage
{
    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public RawZztBoardMessageText Text;
}