using System.Runtime.InteropServices;
using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.SuperZzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 1024, Pack = 1)]
public struct RawSuperZztWorldHeader
{
    [FieldOffset(0)] public short Type;
    [FieldOffset(2)] public short BoardCount;
    [FieldOffset(4)] public short Ammo;
    [FieldOffset(6)] public short Gems;
    [FieldOffset(8)] public RawZztWorldKeyArray Keys;
    [FieldOffset(15)] public short Health;
    [FieldOffset(17)] public short Board;
    [FieldOffset(19)] public short Unused19;
    [FieldOffset(21)] public short Score;
    [FieldOffset(23)] public short Unused23;
    [FieldOffset(25)] public short EnergyCycles;
    [FieldOffset(27)] public RawZztWorldString WorldName;
    [FieldOffset(48)] public RawSuperZztWorldFlagArray Flags;
    [FieldOffset(384)] public RawZztTime TimePassed;
    [FieldOffset(388)] public byte Locked;
    [FieldOffset(389)] public short Stones;
    [FieldOffset(391)] public RawSuperZztWorldExtra Extra;
}