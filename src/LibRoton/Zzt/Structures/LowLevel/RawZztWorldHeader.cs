using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 512, Pack = 1)]
public struct RawZztWorldHeader
{
    [FieldOffset(0)] public short Type;
    [FieldOffset(2)] public short BoardCount;
    [FieldOffset(4)] public short Ammo;
    [FieldOffset(6)] public short Gems;
    [FieldOffset(8)] public RawZztWorldKeyArray Keys;
    [FieldOffset(15)] public short Health;
    [FieldOffset(17)] public short Board;
    [FieldOffset(19)] public short Torches;
    [FieldOffset(21)] public short TorchCycles;
    [FieldOffset(23)] public short EnergyCycles;
    [FieldOffset(25)] public short Unused25;
    [FieldOffset(27)] public short Score;
    [FieldOffset(29)] public RawZztWorldString WorldName;
    [FieldOffset(50)] public RawZztWorldFlagArray Flags;
    [FieldOffset(260)] public RawZztTime TimePassed;
    [FieldOffset(264)] public byte Locked;
    [FieldOffset(265)] public RawZztWorldExtra Extra;
}