// Automatically generated from Structures.txt

using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace LibRoton.Structures;

// Line 0
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 33, Pack = 1)]
public struct ZztActor
{
    [FieldOffset(0)] public ZztPosition Position;
    [FieldOffset(2)] public ZztVector Step;
    [FieldOffset(6)] public short Cycle;
    [FieldOffset(8)] public ZztActorParameters Parameters;
    [FieldOffset(11)] public short Follower;
    [FieldOffset(13)] public short Leader;
    [FieldOffset(15)] public ZztTile Under;
    [FieldOffset(17)] public ZztPointer Pointer;
    [FieldOffset(21)] public short Instruction;
    [FieldOffset(23)] public short Length;
    [FieldOffset(25)] public ZztActorExtra Extra;
}

// Line 14
[PublicAPI]
[InlineArray(Length)]
public struct ZztActorExtra
{
    public const int Length = 8;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztActorExtra Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztActorExtra>(span)[0];

    public static void Write(Span<byte> span, ZztActorExtra value) =>
        MemoryMarshal.Cast<byte, ZztActorExtra>(span)[0] = value;
}

// Line 17
[PublicAPI]
[InlineArray(Length)]
public struct ZztActorParameters
{
    public const int Length = 3;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztActorParameters Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztActorParameters>(span)[0];

    public static void Write(Span<byte> span, ZztActorParameters value) =>
        MemoryMarshal.Cast<byte, ZztActorParameters>(span)[0] = value;
}

// Line 20
[PublicAPI]
[InlineArray(Length)]
public struct ZztBoardExits
{
    public const int Length = 4;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztBoardExits Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztBoardExits>(span)[0];

    public static void Write(Span<byte> span, ZztBoardExits value) =>
        MemoryMarshal.Cast<byte, ZztBoardExits>(span)[0] = value;
}

// Line 23
[PublicAPI]
[InlineArray(Length)]
public struct ZztBoardExtra
{
    public const int Length = 16;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztBoardExtra Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztBoardExtra>(span)[0];

    public static void Write(Span<byte> span, ZztBoardExtra value) =>
        MemoryMarshal.Cast<byte, ZztBoardExtra>(span)[0] = value;
}

// Line 26
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 53, Pack = 1)]
public struct ZztBoardHeader
{
    [FieldOffset(0)] public short Length;
    [FieldOffset(2)] public ZztBoardName Name;
}

// Line 31
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 88, Pack = 1)]
public struct ZztBoardInfo
{
    [FieldOffset(0)] public byte MaxShots;
    [FieldOffset(1)] public byte IsDark;
    [FieldOffset(2)] public ZztBoardExits Exits;
    [FieldOffset(6)] public byte RestartOnZap;
    [FieldOffset(7)] public ZztBoardMessage Message;
    [FieldOffset(66)] public ZztPosition Enter;
    [FieldOffset(68)] public short TimeLimit;
    [FieldOffset(70)] public ZztBoardExtra Extra;
    [FieldOffset(86)] public short ActorCount;
}

// Line 43
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 59, Pack = 1)]
public struct ZztBoardMessage
{
    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public ZztBoardMessageText Text;
}

// Line 48
[PublicAPI]
[InlineArray(Length)]
public struct ZztBoardMessageText
{
    public const int Length = 58;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztBoardMessageText Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztBoardMessageText>(span)[0];

    public static void Write(Span<byte> span, ZztBoardMessageText value) =>
        MemoryMarshal.Cast<byte, ZztBoardMessageText>(span)[0] = value;
}

// Line 51
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 51, Pack = 1)]
public struct ZztBoardName
{
    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public ZztBoardNameText Text;
}

// Line 56
[PublicAPI]
[InlineArray(Length)]
public struct ZztBoardNameText
{
    public const int Length = 50;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztBoardNameText Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztBoardNameText>(span)[0];

    public static void Write(Span<byte> span, ZztBoardNameText value) =>
        MemoryMarshal.Cast<byte, ZztBoardNameText>(span)[0] = value;
}

// Line 59
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 195, Pack = 1)]
public struct ZztElement
{
    [FieldOffset(0)] public byte Character;
    [FieldOffset(1)] public byte Color;
    [FieldOffset(2)] public byte IsDestructible;
    [FieldOffset(3)] public byte IsPushable;
    [FieldOffset(4)] public byte IsAlwaysVisible;
    [FieldOffset(5)] public byte IsEditorFloor;
    [FieldOffset(6)] public byte IsFloor;
    [FieldOffset(7)] public byte HasDrawFunc;
    [FieldOffset(8)] public ZztPointer DrawFunc;
    [FieldOffset(12)] public short Cycle;
    [FieldOffset(14)] public ZztPointer ActFunc;
    [FieldOffset(18)] public ZztPointer InteractFunc;
    [FieldOffset(22)] public short Menu;
    [FieldOffset(24)] public byte MenuKey;
    [FieldOffset(25)] public ZztElementString Name;
    [FieldOffset(46)] public ZztElementString EditorCategoryText;
    [FieldOffset(67)] public ZztElementString EditorP1Text;
    [FieldOffset(88)] public ZztElementString EditorP2Text;
    [FieldOffset(109)] public ZztElementString EditorP3Text;
    [FieldOffset(130)] public ZztElementString EditorBoardText;
    [FieldOffset(151)] public ZztElementString EditorStepText;
    [FieldOffset(172)] public ZztElementString EditorCodeText;
    [FieldOffset(193)] public short Score;
}

// Line 85
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 21, Pack = 1)]
public struct ZztElementString
{
    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public ZztElementStringText Text;
}

// Line 90
[PublicAPI]
[InlineArray(Length)]
public struct ZztElementStringText
{
    public const int Length = 20;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztElementStringText Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztElementStringText>(span)[0];

    public static void Write(Span<byte> span, ZztElementStringText value) =>
        MemoryMarshal.Cast<byte, ZztElementStringText>(span)[0] = value;
}

// Line 93
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 4, Pack = 1)]
public struct ZztPointer
{
    [FieldOffset(0)] public ushort Segment;
    [FieldOffset(2)] public ushort Offset;
}

// Line 98
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 2, Pack = 1)]
public struct ZztPosition
{
    [FieldOffset(0)] public byte X;
    [FieldOffset(1)] public byte Y;
}

// Line 103
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 2, Pack = 1)]
public struct ZztTile
{
    [FieldOffset(0)] public byte Element;
    [FieldOffset(1)] public byte Color;
}

// Line 108
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 3, Pack = 1)]
public struct ZztTileRle
{
    [FieldOffset(0)] public byte Count;
    [FieldOffset(1)] public byte Element;
    [FieldOffset(2)] public byte Color;
}

// Line 114
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 4, Pack = 1)]
public struct ZztTime
{
    [FieldOffset(0)] public short Seconds;
    [FieldOffset(2)] public short Fraction;
}

// Line 119
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 4, Pack = 1)]
public struct ZztVector
{
    [FieldOffset(0)] public short X;
    [FieldOffset(2)] public short Y;
}

// Line 124
[PublicAPI]
[InlineArray(Length)]
public struct ZztWorldExtra
{
    public const int Length = 247;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztWorldExtra Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztWorldExtra>(span)[0];

    public static void Write(Span<byte> span, ZztWorldExtra value) =>
        MemoryMarshal.Cast<byte, ZztWorldExtra>(span)[0] = value;
}

// Line 127
[PublicAPI]
[InlineArray(Length)]
public struct ZztWorldFlags
{
    public const int Length = 10;

    private ZztWorldString _element0;

    public static int Size => Length * Marshal.SizeOf<ZztWorldString>();

    public Span<ZztWorldString> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<ZztWorldString> span) =>
        AsSpan().CopyTo(span);

    public static ZztWorldFlags Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztWorldFlags>(span)[0];

    public static void Write(Span<byte> span, ZztWorldFlags value) =>
        MemoryMarshal.Cast<byte, ZztWorldFlags>(span)[0] = value;
}

// Line 130
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 512, Pack = 1)]
public struct ZztWorldHeader
{
    [FieldOffset(0)] public short Type;
    [FieldOffset(2)] public short BoardCount;
    [FieldOffset(4)] public short Ammo;
    [FieldOffset(6)] public short Gems;
    [FieldOffset(8)] public ZztWorldKeys Keys;
    [FieldOffset(15)] public short Health;
    [FieldOffset(17)] public short Board;
    [FieldOffset(19)] public short Torches;
    [FieldOffset(21)] public short TorchCycles;
    [FieldOffset(23)] public short EnergyCycles;
    [FieldOffset(25)] public short Unused25;
    [FieldOffset(27)] public short Score;
    [FieldOffset(29)] public ZztWorldString WorldName;
    [FieldOffset(50)] public ZztWorldFlags Flags;
    [FieldOffset(260)] public ZztTime TimePassed;
    [FieldOffset(264)] public byte Locked;
    [FieldOffset(265)] public ZztWorldExtra Extra;
}

// Line 150
[PublicAPI]
[InlineArray(Length)]
public struct ZztWorldKeys
{
    public const int Length = 7;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztWorldKeys Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztWorldKeys>(span)[0];

    public static void Write(Span<byte> span, ZztWorldKeys value) =>
        MemoryMarshal.Cast<byte, ZztWorldKeys>(span)[0] = value;
}

// Line 153
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 21, Pack = 1)]
public struct ZztWorldString
{
    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public ZztWorldStringText Text;
}

// Line 158
[PublicAPI]
[InlineArray(Length)]
public struct ZztWorldStringText
{
    public const int Length = 20;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztWorldStringText Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztWorldStringText>(span)[0];

    public static void Write(Span<byte> span, ZztWorldStringText value) =>
        MemoryMarshal.Cast<byte, ZztWorldStringText>(span)[0] = value;
}

// Line 161
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 25, Pack = 1)]
public struct SuperZztActor
{
    [FieldOffset(0)] public ZztPosition Position;
    [FieldOffset(2)] public ZztVector Step;
    [FieldOffset(6)] public short Cycle;
    [FieldOffset(8)] public ZztActorParameters Parameters;
    [FieldOffset(11)] public short Follower;
    [FieldOffset(13)] public short Leader;
    [FieldOffset(15)] public ZztTile Under;
    [FieldOffset(17)] public ZztPointer Pointer;
    [FieldOffset(21)] public short Instruction;
    [FieldOffset(23)] public short Length;
}

// Line 174
[PublicAPI]
[InlineArray(Length)]
public struct SuperZztBoardExtra
{
    public const int Length = 14;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static SuperZztBoardExtra Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, SuperZztBoardExtra>(span)[0];

    public static void Write(Span<byte> span, SuperZztBoardExtra value) =>
        MemoryMarshal.Cast<byte, SuperZztBoardExtra>(span)[0] = value;
}

// Line 177
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 63, Pack = 1)]
public struct SuperZztBoardHeader
{
    [FieldOffset(0)] public short Length;
    [FieldOffset(2)] public SuperZztBoardName Name;
}

// Line 182
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 30, Pack = 1)]
public struct SuperZztBoardInfo
{
    [FieldOffset(0)] public byte MaxShots;
    [FieldOffset(1)] public ZztBoardExits Exits;
    [FieldOffset(5)] public byte RestartOnZap;
    [FieldOffset(6)] public ZztPosition Enter;
    [FieldOffset(8)] public ZztVector Camera;
    [FieldOffset(12)] public short TimeLimit;
    [FieldOffset(14)] public SuperZztBoardExtra Extra;
    [FieldOffset(28)] public short ActorCount;
}

// Line 193
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 61, Pack = 1)]
public struct SuperZztBoardName
{
    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public SuperZztBoardNameText Text;
}

// Line 198
[PublicAPI]
[InlineArray(Length)]
public struct SuperZztBoardNameText
{
    public const int Length = 60;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static SuperZztBoardNameText Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, SuperZztBoardNameText>(span)[0];

    public static void Write(Span<byte> span, SuperZztBoardNameText value) =>
        MemoryMarshal.Cast<byte, SuperZztBoardNameText>(span)[0] = value;
}

// Line 201
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 194, Pack = 1)]
public struct SuperZztElement
{
    [FieldOffset(0)] public byte Character;
    [FieldOffset(1)] public byte Color;
    [FieldOffset(2)] public byte IsDestructible;
    [FieldOffset(3)] public byte IsPushable;
    [FieldOffset(4)] public byte IsEditorFloor;
    [FieldOffset(5)] public byte IsFloor;
    [FieldOffset(6)] public byte HasDrawFunc;
    [FieldOffset(7)] public ZztPointer DrawFunc;
    [FieldOffset(11)] public short Cycle;
    [FieldOffset(13)] public ZztPointer ActFunc;
    [FieldOffset(17)] public ZztPointer InteractFunc;
    [FieldOffset(21)] public short Menu;
    [FieldOffset(23)] public byte MenuKey;
    [FieldOffset(24)] public ZztElementString Name;
    [FieldOffset(45)] public ZztElementString EditorCategoryText;
    [FieldOffset(66)] public ZztElementString EditorP1Text;
    [FieldOffset(87)] public ZztElementString EditorP2Text;
    [FieldOffset(108)] public ZztElementString EditorP3Text;
    [FieldOffset(129)] public ZztElementString EditorBoardText;
    [FieldOffset(150)] public ZztElementString EditorStepText;
    [FieldOffset(171)] public ZztElementString EditorCodeText;
    [FieldOffset(192)] public short Score;
}

// Line 226
[PublicAPI]
[InlineArray(Length)]
public struct SuperZztWorldExtra
{
    public const int Length = 633;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static SuperZztWorldExtra Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, SuperZztWorldExtra>(span)[0];

    public static void Write(Span<byte> span, SuperZztWorldExtra value) =>
        MemoryMarshal.Cast<byte, SuperZztWorldExtra>(span)[0] = value;
}

// Line 229
[PublicAPI]
[InlineArray(Length)]
public struct SuperZztWorldFlags
{
    public const int Length = 16;

    private ZztWorldString _element0;

    public static int Size => Length * Marshal.SizeOf<ZztWorldString>();

    public Span<ZztWorldString> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<ZztWorldString> span) =>
        AsSpan().CopyTo(span);

    public static SuperZztWorldFlags Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, SuperZztWorldFlags>(span)[0];

    public static void Write(Span<byte> span, SuperZztWorldFlags value) =>
        MemoryMarshal.Cast<byte, SuperZztWorldFlags>(span)[0] = value;
}

// Line 232
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = 1024, Pack = 1)]
public struct SuperZztWorldHeader
{
    [FieldOffset(0)] public short Type;
    [FieldOffset(2)] public short BoardCount;
    [FieldOffset(4)] public short Ammo;
    [FieldOffset(6)] public short Gems;
    [FieldOffset(8)] public ZztWorldKeys Keys;
    [FieldOffset(15)] public short Health;
    [FieldOffset(17)] public short Board;
    [FieldOffset(19)] public short Unused19;
    [FieldOffset(21)] public short Score;
    [FieldOffset(23)] public short Unused23;
    [FieldOffset(25)] public short EnergyCycles;
    [FieldOffset(27)] public ZztWorldString WorldName;
    [FieldOffset(48)] public SuperZztWorldFlags Flags;
    [FieldOffset(384)] public ZztTime TimePassed;
    [FieldOffset(388)] public byte Locked;
    [FieldOffset(389)] public short Stones;
    [FieldOffset(391)] public SuperZztWorldExtra Extra;
}
