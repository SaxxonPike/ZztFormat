using System.Runtime.InteropServices;

namespace LibRoton.SuperZzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 61, Pack = 1)]
public struct RawSuperZztBoardName
{
    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public RawSuperZztBoardNameText Text;
}