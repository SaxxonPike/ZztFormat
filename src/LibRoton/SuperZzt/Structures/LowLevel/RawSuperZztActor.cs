using System.Runtime.InteropServices;
using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.SuperZzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct RawSuperZztActor
{
    public const int Size = 25;
    
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
}