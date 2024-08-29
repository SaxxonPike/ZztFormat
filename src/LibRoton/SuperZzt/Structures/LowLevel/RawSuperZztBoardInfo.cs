using System.Runtime.InteropServices;
using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.SuperZzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 30, Pack = 1)]
public struct RawSuperZztBoardInfo
{
    [FieldOffset(0)] public byte MaxShots;
    [FieldOffset(1)] public RawZztBoardExitArray Exits;
    [FieldOffset(5)] public byte RestartOnZap;
    [FieldOffset(6)] public RawZztPosition EnterPosition;
    [FieldOffset(8)] public RawZztVector Camera;
    [FieldOffset(12)] public short TimeLimit;
    [FieldOffset(14)] public RawSuperZztBoardExtra Extra;
    [FieldOffset(28)] public short ActorCount;
}