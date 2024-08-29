using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 33, Pack = 1)]
public struct RawZztActor
{
    [FieldOffset(0)] public RawZztPosition Position;
    [FieldOffset(2)] public RawZztVector Step;
    [FieldOffset(6)] public short Cycle;
    [FieldOffset(8)] public RawZztActorParameters Parameters;
    [FieldOffset(11)] public short Follower;
    [FieldOffset(13)] public short Leader;
    [FieldOffset(15)] public RawZztTile Under;
    [FieldOffset(17)] public RawZztPointer Pointer;
    [FieldOffset(21)] public short Instruction;
    [FieldOffset(23)] public short Length;
    [FieldOffset(25)] public RawZztActorExtra Extra;
}