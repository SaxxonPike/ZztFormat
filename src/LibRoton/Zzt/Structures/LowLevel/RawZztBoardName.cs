using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 51, Pack = 1)]
public struct RawZztBoardName
{
    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public RawZztBoardNameText Text;
}