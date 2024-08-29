using System.Runtime.InteropServices;

namespace LibRoton.Zzt.Structures.LowLevel;

[StructLayout(LayoutKind.Explicit, Size = 88, Pack = 1)]
public struct RawZztBoardInfo
{
    [FieldOffset(0)] public byte MaxShots;
    [FieldOffset(1)] public byte IsDark;
    [FieldOffset(2)] public RawZztBoardExitArray Exits;
    [FieldOffset(6)] public byte RestartOnZap;
    [FieldOffset(7)] public RawZztBoardMessage Message;
    [FieldOffset(66)] public RawZztPosition EnterPosition;
    [FieldOffset(68)] public short TimeLimit;
    [FieldOffset(70)] public RawZztBoardExtra Extra;
    [FieldOffset(86)] public short ActorCount;
}