// Automatically generated from Structures.txt and Structures.tt

using JetBrains.Annotations;
using System.Text;
using static System.Buffers.Binary.BinaryPrimitives;

namespace ZztFormat;

#region Utilities

internal static class CodePage437
{
    private static readonly Lazy<Encoding> _encoding = new(() =>
    {
        CodePagesEncodingProvider.Instance.GetEncoding(437);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        return Encoding.GetEncoding(437);
    });

    public static Encoding Encoding => _encoding.Value;
}

#endregion

#region Data Types

[PublicAPI]
internal partial class ZztActor
{
    public const int Size = 33;

    public RawPosition Position { get; set; }
    public RawVector Step { get; set; }
    public short Cycle { get; set; }
    public byte[] Parameters { get; init; } = new byte[3];
    public short Follower { get; set; }
    public short Leader { get; set; }
    public RawTile Under { get; set; }
    public int Pointer { get; set; }
    public short Instruction { get; set; }
    public short Length { get; set; }
    public byte[] Extra { get; init; } = new byte[8];

    public static ZztActor Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static ZztActor Read(ReadOnlySpan<byte> bytes)
    {
        var result = new ZztActor();
        result.Position = RawPosition.Read(bytes[0..]);
        result.Step = RawVector.Read(bytes[2..]);
        result.Cycle = ReadInt16LittleEndian(bytes[6..]);
        bytes[8..11].CopyTo(result.Parameters);
        result.Follower = ReadInt16LittleEndian(bytes[11..]);
        result.Leader = ReadInt16LittleEndian(bytes[13..]);
        result.Under = RawTile.Read(bytes[15..]);
        result.Pointer = ReadInt32LittleEndian(bytes[17..]);
        result.Instruction = ReadInt16LittleEndian(bytes[21..]);
        result.Length = ReadInt16LittleEndian(bytes[23..]);
        bytes[25..33].CopyTo(result.Extra);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        Position.Write(bytes[0..]);
        Step.Write(bytes[2..]);
        WriteInt16LittleEndian(bytes[6..], Cycle);
        Parameters.CopyTo(bytes[8..]);
        WriteInt16LittleEndian(bytes[11..], Follower);
        WriteInt16LittleEndian(bytes[13..], Leader);
        Under.Write(bytes[15..]);
        WriteInt32LittleEndian(bytes[17..], Pointer);
        WriteInt16LittleEndian(bytes[21..], Instruction);
        WriteInt16LittleEndian(bytes[23..], Length);
        Extra.CopyTo(bytes[25..]);
    }
}

[PublicAPI]
internal partial class SuperZztActor
{
    public const int Size = 25;

    public RawPosition Position { get; set; }
    public RawVector Step { get; set; }
    public short Cycle { get; set; }
    public byte[] Parameters { get; init; } = new byte[3];
    public short Follower { get; set; }
    public short Leader { get; set; }
    public RawTile Under { get; set; }
    public int Pointer { get; set; }
    public short Instruction { get; set; }
    public short Length { get; set; }

    public static SuperZztActor Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static SuperZztActor Read(ReadOnlySpan<byte> bytes)
    {
        var result = new SuperZztActor();
        result.Position = RawPosition.Read(bytes[0..]);
        result.Step = RawVector.Read(bytes[2..]);
        result.Cycle = ReadInt16LittleEndian(bytes[6..]);
        bytes[8..11].CopyTo(result.Parameters);
        result.Follower = ReadInt16LittleEndian(bytes[11..]);
        result.Leader = ReadInt16LittleEndian(bytes[13..]);
        result.Under = RawTile.Read(bytes[15..]);
        result.Pointer = ReadInt32LittleEndian(bytes[17..]);
        result.Instruction = ReadInt16LittleEndian(bytes[21..]);
        result.Length = ReadInt16LittleEndian(bytes[23..]);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        Position.Write(bytes[0..]);
        Step.Write(bytes[2..]);
        WriteInt16LittleEndian(bytes[6..], Cycle);
        Parameters.CopyTo(bytes[8..]);
        WriteInt16LittleEndian(bytes[11..], Follower);
        WriteInt16LittleEndian(bytes[13..], Leader);
        Under.Write(bytes[15..]);
        WriteInt32LittleEndian(bytes[17..], Pointer);
        WriteInt16LittleEndian(bytes[21..], Instruction);
        WriteInt16LittleEndian(bytes[23..], Length);
    }
}

[PublicAPI]
internal partial class ZztBoardHeader
{
    public const int Size = 51;

    public byte NameLength { get; set; }
    public byte[] NameBytes { get; init; } = new byte[50];

    public string Name
    {
        get => CodePage437.Encoding.GetString(NameBytes[..Math.Min(NameLength, (byte)50)]);
        set => NameLength = (byte)CodePage437.Encoding.GetBytes(value, NameBytes);
    }

    public static ZztBoardHeader Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static ZztBoardHeader Read(ReadOnlySpan<byte> bytes)
    {
        var result = new ZztBoardHeader();
        result.NameLength = bytes[0];
        bytes[1..51].CopyTo(result.NameBytes);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = NameLength;
        NameBytes.CopyTo(bytes[1..]);
    }
}

[PublicAPI]
internal partial class SuperZztBoardHeader
{
    public const int Size = 61;

    public byte NameLength { get; set; }
    public byte[] NameBytes { get; init; } = new byte[60];

    public string Name
    {
        get => CodePage437.Encoding.GetString(NameBytes[..Math.Min(NameLength, (byte)60)]);
        set => NameLength = (byte)CodePage437.Encoding.GetBytes(value, NameBytes);
    }

    public static SuperZztBoardHeader Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static SuperZztBoardHeader Read(ReadOnlySpan<byte> bytes)
    {
        var result = new SuperZztBoardHeader();
        result.NameLength = bytes[0];
        bytes[1..61].CopyTo(result.NameBytes);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = NameLength;
        NameBytes.CopyTo(bytes[1..]);
    }
}

[PublicAPI]
internal partial class ZztBoardInfo
{
    public const int Size = 88;

    public byte MaxShots { get; set; }
    public byte DarkBit { get; set; }
    public byte[] Exits { get; init; } = new byte[4];
    public byte RestartOnZapBit { get; set; }
    public byte MessageLength { get; set; }
    public byte[] MessageBytes { get; init; } = new byte[58];
    public RawPosition Enter { get; set; }
    public short TimeLimit { get; set; }
    public byte[] Extra { get; init; } = new byte[16];
    public short ActorCount { get; set; }

    public string Message
    {
        get => CodePage437.Encoding.GetString(MessageBytes[..Math.Min(MessageLength, (byte)58)]);
        set => MessageLength = (byte)CodePage437.Encoding.GetBytes(value, MessageBytes);
    }

    public static ZztBoardInfo Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static ZztBoardInfo Read(ReadOnlySpan<byte> bytes)
    {
        var result = new ZztBoardInfo();
        result.MaxShots = bytes[0];
        result.DarkBit = bytes[1];
        bytes[2..6].CopyTo(result.Exits);
        result.RestartOnZapBit = bytes[6];
        result.MessageLength = bytes[7];
        bytes[8..66].CopyTo(result.MessageBytes);
        result.Enter = RawPosition.Read(bytes[66..]);
        result.TimeLimit = ReadInt16LittleEndian(bytes[68..]);
        bytes[70..86].CopyTo(result.Extra);
        result.ActorCount = ReadInt16LittleEndian(bytes[86..]);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = MaxShots;
        bytes[1] = DarkBit;
        Exits.CopyTo(bytes[2..]);
        bytes[6] = RestartOnZapBit;
        bytes[7] = MessageLength;
        MessageBytes.CopyTo(bytes[8..]);
        Enter.Write(bytes[66..]);
        WriteInt16LittleEndian(bytes[68..], TimeLimit);
        Extra.CopyTo(bytes[70..]);
        WriteInt16LittleEndian(bytes[86..], ActorCount);
    }
}

[PublicAPI]
internal partial class SuperZztBoardInfo
{
    public const int Size = 30;

    public byte MaxShots { get; set; }
    public byte[] Exits { get; init; } = new byte[4];
    public byte RestartOnZapBit { get; set; }
    public RawPosition Enter { get; set; }
    public RawVector Camera { get; set; }
    public short TimeLimit { get; set; }
    public byte[] Extra { get; init; } = new byte[14];
    public short ActorCount { get; set; }

    public static SuperZztBoardInfo Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static SuperZztBoardInfo Read(ReadOnlySpan<byte> bytes)
    {
        var result = new SuperZztBoardInfo();
        result.MaxShots = bytes[0];
        bytes[1..5].CopyTo(result.Exits);
        result.RestartOnZapBit = bytes[5];
        result.Enter = RawPosition.Read(bytes[6..]);
        result.Camera = RawVector.Read(bytes[8..]);
        result.TimeLimit = ReadInt16LittleEndian(bytes[12..]);
        bytes[14..28].CopyTo(result.Extra);
        result.ActorCount = ReadInt16LittleEndian(bytes[28..]);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = MaxShots;
        Exits.CopyTo(bytes[1..]);
        bytes[5] = RestartOnZapBit;
        Enter.Write(bytes[6..]);
        Camera.Write(bytes[8..]);
        WriteInt16LittleEndian(bytes[12..], TimeLimit);
        Extra.CopyTo(bytes[14..]);
        WriteInt16LittleEndian(bytes[28..], ActorCount);
    }
}

[PublicAPI]
internal partial class ZztElementProperties
{
    public const int Size = 195;

    public byte Character { get; set; }
    public byte Color { get; set; }
    public byte DestructibleBit { get; set; }
    public byte PushableBit { get; set; }
    public byte AlwaysVisibleBit { get; set; }
    public byte EditorFloorBit { get; set; }
    public byte FloorBit { get; set; }
    public byte DrawFuncBit { get; set; }
    public int DrawFunc { get; set; }
    public short Cycle { get; set; }
    public int ActFunc { get; set; }
    public int InteractFunc { get; set; }
    public short Menu { get; set; }
    public byte MenuKey { get; set; }
    public byte NameLength { get; set; }
    public byte[] NameBytes { get; init; } = new byte[20];
    public byte EditorCategoryTextLength { get; set; }
    public byte[] EditorCategoryTextBytes { get; init; } = new byte[20];
    public byte EditorP1TextLength { get; set; }
    public byte[] EditorP1TextBytes { get; init; } = new byte[20];
    public byte EditorP2TextLength { get; set; }
    public byte[] EditorP2TextBytes { get; init; } = new byte[20];
    public byte EditorP3TextLength { get; set; }
    public byte[] EditorP3TextBytes { get; init; } = new byte[20];
    public byte EditorBoardTextLength { get; set; }
    public byte[] EditorBoardTextBytes { get; init; } = new byte[20];
    public byte EditorStepTextLength { get; set; }
    public byte[] EditorStepTextBytes { get; init; } = new byte[20];
    public byte EditorCodeTextLength { get; set; }
    public byte[] EditorCodeTextBytes { get; init; } = new byte[20];
    public short Score { get; set; }

    public string Name
    {
        get => CodePage437.Encoding.GetString(NameBytes[..Math.Min(NameLength, (byte)20)]);
        set => NameLength = (byte)CodePage437.Encoding.GetBytes(value, NameBytes);
    }

    public string EditorCategoryText
    {
        get => CodePage437.Encoding.GetString(EditorCategoryTextBytes[..Math.Min(EditorCategoryTextLength, (byte)20)]);
        set => EditorCategoryTextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorCategoryTextBytes);
    }

    public string EditorP1Text
    {
        get => CodePage437.Encoding.GetString(EditorP1TextBytes[..Math.Min(EditorP1TextLength, (byte)20)]);
        set => EditorP1TextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorP1TextBytes);
    }

    public string EditorP2Text
    {
        get => CodePage437.Encoding.GetString(EditorP2TextBytes[..Math.Min(EditorP2TextLength, (byte)20)]);
        set => EditorP2TextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorP2TextBytes);
    }

    public string EditorP3Text
    {
        get => CodePage437.Encoding.GetString(EditorP3TextBytes[..Math.Min(EditorP3TextLength, (byte)20)]);
        set => EditorP3TextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorP3TextBytes);
    }

    public string EditorBoardText
    {
        get => CodePage437.Encoding.GetString(EditorBoardTextBytes[..Math.Min(EditorBoardTextLength, (byte)20)]);
        set => EditorBoardTextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorBoardTextBytes);
    }

    public string EditorStepText
    {
        get => CodePage437.Encoding.GetString(EditorStepTextBytes[..Math.Min(EditorStepTextLength, (byte)20)]);
        set => EditorStepTextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorStepTextBytes);
    }

    public string EditorCodeText
    {
        get => CodePage437.Encoding.GetString(EditorCodeTextBytes[..Math.Min(EditorCodeTextLength, (byte)20)]);
        set => EditorCodeTextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorCodeTextBytes);
    }

    public static ZztElementProperties Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static ZztElementProperties Read(ReadOnlySpan<byte> bytes)
    {
        var result = new ZztElementProperties();
        result.Character = bytes[0];
        result.Color = bytes[1];
        result.DestructibleBit = bytes[2];
        result.PushableBit = bytes[3];
        result.AlwaysVisibleBit = bytes[4];
        result.EditorFloorBit = bytes[5];
        result.FloorBit = bytes[6];
        result.DrawFuncBit = bytes[7];
        result.DrawFunc = ReadInt32LittleEndian(bytes[8..]);
        result.Cycle = ReadInt16LittleEndian(bytes[12..]);
        result.ActFunc = ReadInt32LittleEndian(bytes[14..]);
        result.InteractFunc = ReadInt32LittleEndian(bytes[18..]);
        result.Menu = ReadInt16LittleEndian(bytes[22..]);
        result.MenuKey = bytes[24];
        result.NameLength = bytes[25];
        bytes[26..46].CopyTo(result.NameBytes);
        result.EditorCategoryTextLength = bytes[46];
        bytes[47..67].CopyTo(result.EditorCategoryTextBytes);
        result.EditorP1TextLength = bytes[67];
        bytes[68..88].CopyTo(result.EditorP1TextBytes);
        result.EditorP2TextLength = bytes[88];
        bytes[89..109].CopyTo(result.EditorP2TextBytes);
        result.EditorP3TextLength = bytes[109];
        bytes[110..130].CopyTo(result.EditorP3TextBytes);
        result.EditorBoardTextLength = bytes[130];
        bytes[131..151].CopyTo(result.EditorBoardTextBytes);
        result.EditorStepTextLength = bytes[151];
        bytes[152..172].CopyTo(result.EditorStepTextBytes);
        result.EditorCodeTextLength = bytes[172];
        bytes[173..193].CopyTo(result.EditorCodeTextBytes);
        result.Score = ReadInt16LittleEndian(bytes[193..]);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = Character;
        bytes[1] = Color;
        bytes[2] = DestructibleBit;
        bytes[3] = PushableBit;
        bytes[4] = AlwaysVisibleBit;
        bytes[5] = EditorFloorBit;
        bytes[6] = FloorBit;
        bytes[7] = DrawFuncBit;
        WriteInt32LittleEndian(bytes[8..], DrawFunc);
        WriteInt16LittleEndian(bytes[12..], Cycle);
        WriteInt32LittleEndian(bytes[14..], ActFunc);
        WriteInt32LittleEndian(bytes[18..], InteractFunc);
        WriteInt16LittleEndian(bytes[22..], Menu);
        bytes[24] = MenuKey;
        bytes[25] = NameLength;
        NameBytes.CopyTo(bytes[26..]);
        bytes[46] = EditorCategoryTextLength;
        EditorCategoryTextBytes.CopyTo(bytes[47..]);
        bytes[67] = EditorP1TextLength;
        EditorP1TextBytes.CopyTo(bytes[68..]);
        bytes[88] = EditorP2TextLength;
        EditorP2TextBytes.CopyTo(bytes[89..]);
        bytes[109] = EditorP3TextLength;
        EditorP3TextBytes.CopyTo(bytes[110..]);
        bytes[130] = EditorBoardTextLength;
        EditorBoardTextBytes.CopyTo(bytes[131..]);
        bytes[151] = EditorStepTextLength;
        EditorStepTextBytes.CopyTo(bytes[152..]);
        bytes[172] = EditorCodeTextLength;
        EditorCodeTextBytes.CopyTo(bytes[173..]);
        WriteInt16LittleEndian(bytes[193..], Score);
    }
}

[PublicAPI]
internal partial class SuperZztElementProperties
{
    public const int Size = 194;

    public byte Character { get; set; }
    public byte Color { get; set; }
    public byte DestructibleBit { get; set; }
    public byte PushableBit { get; set; }
    public byte EditorFloorBit { get; set; }
    public byte FloorBit { get; set; }
    public byte DrawFuncBit { get; set; }
    public int DrawFunc { get; set; }
    public short Cycle { get; set; }
    public int ActFunc { get; set; }
    public int InteractFunc { get; set; }
    public short Menu { get; set; }
    public byte MenuKey { get; set; }
    public byte NameLength { get; set; }
    public byte[] NameBytes { get; init; } = new byte[20];
    public byte EditorCategoryTextLength { get; set; }
    public byte[] EditorCategoryTextBytes { get; init; } = new byte[20];
    public byte EditorP1TextLength { get; set; }
    public byte[] EditorP1TextBytes { get; init; } = new byte[20];
    public byte EditorP2TextLength { get; set; }
    public byte[] EditorP2TextBytes { get; init; } = new byte[20];
    public byte EditorP3TextLength { get; set; }
    public byte[] EditorP3TextBytes { get; init; } = new byte[20];
    public byte EditorBoardTextLength { get; set; }
    public byte[] EditorBoardTextBytes { get; init; } = new byte[20];
    public byte EditorStepTextLength { get; set; }
    public byte[] EditorStepTextBytes { get; init; } = new byte[20];
    public byte EditorCodeTextLength { get; set; }
    public byte[] EditorCodeTextBytes { get; init; } = new byte[20];
    public short Score { get; set; }

    public string Name
    {
        get => CodePage437.Encoding.GetString(NameBytes[..Math.Min(NameLength, (byte)20)]);
        set => NameLength = (byte)CodePage437.Encoding.GetBytes(value, NameBytes);
    }

    public string EditorCategoryText
    {
        get => CodePage437.Encoding.GetString(EditorCategoryTextBytes[..Math.Min(EditorCategoryTextLength, (byte)20)]);
        set => EditorCategoryTextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorCategoryTextBytes);
    }

    public string EditorP1Text
    {
        get => CodePage437.Encoding.GetString(EditorP1TextBytes[..Math.Min(EditorP1TextLength, (byte)20)]);
        set => EditorP1TextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorP1TextBytes);
    }

    public string EditorP2Text
    {
        get => CodePage437.Encoding.GetString(EditorP2TextBytes[..Math.Min(EditorP2TextLength, (byte)20)]);
        set => EditorP2TextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorP2TextBytes);
    }

    public string EditorP3Text
    {
        get => CodePage437.Encoding.GetString(EditorP3TextBytes[..Math.Min(EditorP3TextLength, (byte)20)]);
        set => EditorP3TextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorP3TextBytes);
    }

    public string EditorBoardText
    {
        get => CodePage437.Encoding.GetString(EditorBoardTextBytes[..Math.Min(EditorBoardTextLength, (byte)20)]);
        set => EditorBoardTextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorBoardTextBytes);
    }

    public string EditorStepText
    {
        get => CodePage437.Encoding.GetString(EditorStepTextBytes[..Math.Min(EditorStepTextLength, (byte)20)]);
        set => EditorStepTextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorStepTextBytes);
    }

    public string EditorCodeText
    {
        get => CodePage437.Encoding.GetString(EditorCodeTextBytes[..Math.Min(EditorCodeTextLength, (byte)20)]);
        set => EditorCodeTextLength = (byte)CodePage437.Encoding.GetBytes(value, EditorCodeTextBytes);
    }

    public static SuperZztElementProperties Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static SuperZztElementProperties Read(ReadOnlySpan<byte> bytes)
    {
        var result = new SuperZztElementProperties();
        result.Character = bytes[0];
        result.Color = bytes[1];
        result.DestructibleBit = bytes[2];
        result.PushableBit = bytes[3];
        result.EditorFloorBit = bytes[4];
        result.FloorBit = bytes[5];
        result.DrawFuncBit = bytes[6];
        result.DrawFunc = ReadInt32LittleEndian(bytes[7..]);
        result.Cycle = ReadInt16LittleEndian(bytes[11..]);
        result.ActFunc = ReadInt32LittleEndian(bytes[13..]);
        result.InteractFunc = ReadInt32LittleEndian(bytes[17..]);
        result.Menu = ReadInt16LittleEndian(bytes[21..]);
        result.MenuKey = bytes[23];
        result.NameLength = bytes[24];
        bytes[25..45].CopyTo(result.NameBytes);
        result.EditorCategoryTextLength = bytes[45];
        bytes[46..66].CopyTo(result.EditorCategoryTextBytes);
        result.EditorP1TextLength = bytes[66];
        bytes[67..87].CopyTo(result.EditorP1TextBytes);
        result.EditorP2TextLength = bytes[87];
        bytes[88..108].CopyTo(result.EditorP2TextBytes);
        result.EditorP3TextLength = bytes[108];
        bytes[109..129].CopyTo(result.EditorP3TextBytes);
        result.EditorBoardTextLength = bytes[129];
        bytes[130..150].CopyTo(result.EditorBoardTextBytes);
        result.EditorStepTextLength = bytes[150];
        bytes[151..171].CopyTo(result.EditorStepTextBytes);
        result.EditorCodeTextLength = bytes[171];
        bytes[172..192].CopyTo(result.EditorCodeTextBytes);
        result.Score = ReadInt16LittleEndian(bytes[192..]);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = Character;
        bytes[1] = Color;
        bytes[2] = DestructibleBit;
        bytes[3] = PushableBit;
        bytes[4] = EditorFloorBit;
        bytes[5] = FloorBit;
        bytes[6] = DrawFuncBit;
        WriteInt32LittleEndian(bytes[7..], DrawFunc);
        WriteInt16LittleEndian(bytes[11..], Cycle);
        WriteInt32LittleEndian(bytes[13..], ActFunc);
        WriteInt32LittleEndian(bytes[17..], InteractFunc);
        WriteInt16LittleEndian(bytes[21..], Menu);
        bytes[23] = MenuKey;
        bytes[24] = NameLength;
        NameBytes.CopyTo(bytes[25..]);
        bytes[45] = EditorCategoryTextLength;
        EditorCategoryTextBytes.CopyTo(bytes[46..]);
        bytes[66] = EditorP1TextLength;
        EditorP1TextBytes.CopyTo(bytes[67..]);
        bytes[87] = EditorP2TextLength;
        EditorP2TextBytes.CopyTo(bytes[88..]);
        bytes[108] = EditorP3TextLength;
        EditorP3TextBytes.CopyTo(bytes[109..]);
        bytes[129] = EditorBoardTextLength;
        EditorBoardTextBytes.CopyTo(bytes[130..]);
        bytes[150] = EditorStepTextLength;
        EditorStepTextBytes.CopyTo(bytes[151..]);
        bytes[171] = EditorCodeTextLength;
        EditorCodeTextBytes.CopyTo(bytes[172..]);
        WriteInt16LittleEndian(bytes[192..], Score);
    }
}

[PublicAPI]
internal partial class ZztHighScoreList
{
    public const int Size = 1590;

    public ZztHighScore[] Scores { get; init; } = new ZztHighScore[30];

    public static ZztHighScoreList Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static ZztHighScoreList Read(ReadOnlySpan<byte> bytes)
    {
        var result = new ZztHighScoreList();
        for (int i = 0, j = 0; i < 30; i++, j += 53)
            result.Scores[i] = ZztHighScore.Read(bytes[(0 + j)..]);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        for (int i = 0, j = 0; i < 30; i++, j += 53)
            Scores[i].Write(bytes[(0 + j)..]);
    }
}

[PublicAPI]
internal partial struct ZztHighScore
{
    public const int Size = 53;

    public ZztHighScore()
    {
    }

    public byte NameLength { get; set; }
    public byte[] NameBytes { get; init; } = new byte[50];
    public short Score { get; set; }

    public string Name
    {
        get => CodePage437.Encoding.GetString(NameBytes[..Math.Min(NameLength, (byte)50)]);
        set => NameLength = (byte)CodePage437.Encoding.GetBytes(value, NameBytes);
    }

    public static ZztHighScore Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static ZztHighScore Read(ReadOnlySpan<byte> bytes)
    {
        var result = new ZztHighScore();
        result.NameLength = bytes[0];
        bytes[1..51].CopyTo(result.NameBytes);
        result.Score = ReadInt16LittleEndian(bytes[51..]);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = NameLength;
        NameBytes.CopyTo(bytes[1..]);
        WriteInt16LittleEndian(bytes[51..], Score);
    }
}

[PublicAPI]
internal partial class SuperZztHighScoreList
{
    public const int Size = 1890;

    public SuperZztHighScore[] Scores { get; init; } = new SuperZztHighScore[30];

    public static SuperZztHighScoreList Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static SuperZztHighScoreList Read(ReadOnlySpan<byte> bytes)
    {
        var result = new SuperZztHighScoreList();
        for (int i = 0, j = 0; i < 30; i++, j += 63)
            result.Scores[i] = SuperZztHighScore.Read(bytes[(0 + j)..]);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        for (int i = 0, j = 0; i < 30; i++, j += 63)
            Scores[i].Write(bytes[(0 + j)..]);
    }
}

[PublicAPI]
internal partial struct SuperZztHighScore
{
    public const int Size = 63;

    public SuperZztHighScore()
    {
    }

    public byte NameLength { get; set; }
    public byte[] NameBytes { get; init; } = new byte[60];
    public short Score { get; set; }

    public string Name
    {
        get => CodePage437.Encoding.GetString(NameBytes[..Math.Min(NameLength, (byte)60)]);
        set => NameLength = (byte)CodePage437.Encoding.GetBytes(value, NameBytes);
    }

    public static SuperZztHighScore Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static SuperZztHighScore Read(ReadOnlySpan<byte> bytes)
    {
        var result = new SuperZztHighScore();
        result.NameLength = bytes[0];
        bytes[1..61].CopyTo(result.NameBytes);
        result.Score = ReadInt16LittleEndian(bytes[61..]);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = NameLength;
        NameBytes.CopyTo(bytes[1..]);
        WriteInt16LittleEndian(bytes[61..], Score);
    }
}

[PublicAPI]
internal partial class ZztWorldHeader
{
    public const int Size = 510;

    public short BoardCount { get; set; }
    public short Ammo { get; set; }
    public short Gems { get; set; }
    public byte[] Keys { get; init; } = new byte[7];
    public short Health { get; set; }
    public short Board { get; set; }
    public short Torches { get; set; }
    public short TorchCycles { get; set; }
    public short EnergyCycles { get; set; }
    public short Unused25 { get; set; }
    public short Score { get; set; }
    public byte NameLength { get; set; }
    public byte[] NameBytes { get; init; } = new byte[20];
    public Flag[] Flags { get; init; } = new Flag[10];
    public Time TimePassed { get; set; }
    public byte Locked { get; set; }
    public byte[] Extra { get; init; } = new byte[247];

    public string Name
    {
        get => CodePage437.Encoding.GetString(NameBytes[..Math.Min(NameLength, (byte)20)]);
        set => NameLength = (byte)CodePage437.Encoding.GetBytes(value, NameBytes);
    }

    public static ZztWorldHeader Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static ZztWorldHeader Read(ReadOnlySpan<byte> bytes)
    {
        var result = new ZztWorldHeader();
        result.BoardCount = ReadInt16LittleEndian(bytes[0..]);
        result.Ammo = ReadInt16LittleEndian(bytes[2..]);
        result.Gems = ReadInt16LittleEndian(bytes[4..]);
        bytes[6..13].CopyTo(result.Keys);
        result.Health = ReadInt16LittleEndian(bytes[13..]);
        result.Board = ReadInt16LittleEndian(bytes[15..]);
        result.Torches = ReadInt16LittleEndian(bytes[17..]);
        result.TorchCycles = ReadInt16LittleEndian(bytes[19..]);
        result.EnergyCycles = ReadInt16LittleEndian(bytes[21..]);
        result.Unused25 = ReadInt16LittleEndian(bytes[23..]);
        result.Score = ReadInt16LittleEndian(bytes[25..]);
        result.NameLength = bytes[27];
        bytes[28..48].CopyTo(result.NameBytes);
        for (int i = 0, j = 0; i < 10; i++, j += 21)
            result.Flags[i] = Flag.Read(bytes[(48 + j)..]);
        result.TimePassed = Time.Read(bytes[258..]);
        result.Locked = bytes[262];
        bytes[263..510].CopyTo(result.Extra);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        WriteInt16LittleEndian(bytes[0..], BoardCount);
        WriteInt16LittleEndian(bytes[2..], Ammo);
        WriteInt16LittleEndian(bytes[4..], Gems);
        Keys.CopyTo(bytes[6..]);
        WriteInt16LittleEndian(bytes[13..], Health);
        WriteInt16LittleEndian(bytes[15..], Board);
        WriteInt16LittleEndian(bytes[17..], Torches);
        WriteInt16LittleEndian(bytes[19..], TorchCycles);
        WriteInt16LittleEndian(bytes[21..], EnergyCycles);
        WriteInt16LittleEndian(bytes[23..], Unused25);
        WriteInt16LittleEndian(bytes[25..], Score);
        bytes[27] = NameLength;
        NameBytes.CopyTo(bytes[28..]);
        for (int i = 0, j = 0; i < 10; i++, j += 21)
            Flags[i].Write(bytes[(48 + j)..]);
        TimePassed.Write(bytes[258..]);
        bytes[262] = Locked;
        Extra.CopyTo(bytes[263..]);
    }
}

[PublicAPI]
internal partial class SuperZztWorldHeader
{
    public const int Size = 1022;

    public short BoardCount { get; set; }
    public short Ammo { get; set; }
    public short Gems { get; set; }
    public byte[] Keys { get; init; } = new byte[7];
    public short Health { get; set; }
    public short Board { get; set; }
    public short Unused19 { get; set; }
    public short Score { get; set; }
    public short Unused23 { get; set; }
    public short EnergyCycles { get; set; }
    public byte NameLength { get; set; }
    public byte[] NameBytes { get; init; } = new byte[20];
    public Flag[] Flags { get; init; } = new Flag[16];
    public Time TimePassed { get; set; }
    public byte Locked { get; set; }
    public short Stones { get; set; }
    public byte[] Extra { get; init; } = new byte[633];

    public string Name
    {
        get => CodePage437.Encoding.GetString(NameBytes[..Math.Min(NameLength, (byte)20)]);
        set => NameLength = (byte)CodePage437.Encoding.GetBytes(value, NameBytes);
    }

    public static SuperZztWorldHeader Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static SuperZztWorldHeader Read(ReadOnlySpan<byte> bytes)
    {
        var result = new SuperZztWorldHeader();
        result.BoardCount = ReadInt16LittleEndian(bytes[0..]);
        result.Ammo = ReadInt16LittleEndian(bytes[2..]);
        result.Gems = ReadInt16LittleEndian(bytes[4..]);
        bytes[6..13].CopyTo(result.Keys);
        result.Health = ReadInt16LittleEndian(bytes[13..]);
        result.Board = ReadInt16LittleEndian(bytes[15..]);
        result.Unused19 = ReadInt16LittleEndian(bytes[17..]);
        result.Score = ReadInt16LittleEndian(bytes[19..]);
        result.Unused23 = ReadInt16LittleEndian(bytes[21..]);
        result.EnergyCycles = ReadInt16LittleEndian(bytes[23..]);
        result.NameLength = bytes[25];
        bytes[26..46].CopyTo(result.NameBytes);
        for (int i = 0, j = 0; i < 16; i++, j += 21)
            result.Flags[i] = Flag.Read(bytes[(46 + j)..]);
        result.TimePassed = Time.Read(bytes[382..]);
        result.Locked = bytes[386];
        result.Stones = ReadInt16LittleEndian(bytes[387..]);
        bytes[389..1022].CopyTo(result.Extra);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        WriteInt16LittleEndian(bytes[0..], BoardCount);
        WriteInt16LittleEndian(bytes[2..], Ammo);
        WriteInt16LittleEndian(bytes[4..], Gems);
        Keys.CopyTo(bytes[6..]);
        WriteInt16LittleEndian(bytes[13..], Health);
        WriteInt16LittleEndian(bytes[15..], Board);
        WriteInt16LittleEndian(bytes[17..], Unused19);
        WriteInt16LittleEndian(bytes[19..], Score);
        WriteInt16LittleEndian(bytes[21..], Unused23);
        WriteInt16LittleEndian(bytes[23..], EnergyCycles);
        bytes[25] = NameLength;
        NameBytes.CopyTo(bytes[26..]);
        for (int i = 0, j = 0; i < 16; i++, j += 21)
            Flags[i].Write(bytes[(46 + j)..]);
        TimePassed.Write(bytes[382..]);
        bytes[386] = Locked;
        WriteInt16LittleEndian(bytes[387..], Stones);
        Extra.CopyTo(bytes[389..]);
    }
}

[PublicAPI]
internal partial class ZztDatHeader
{
    public const int Size = 1322;

    public short Count { get; set; }
    public ZztDatEntry[] Entries { get; init; } = new ZztDatEntry[24];
    public int[] Offsets { get; init; } = new int[24];

    public static ZztDatHeader Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static ZztDatHeader Read(ReadOnlySpan<byte> bytes)
    {
        var result = new ZztDatHeader();
        result.Count = ReadInt16LittleEndian(bytes[0..]);
        for (int i = 0, j = 0; i < 24; i++, j += 51)
            result.Entries[i] = ZztDatEntry.Read(bytes[(2 + j)..]);
        for (int i = 0, j = 0; i < 24; i++, j += 4)
            result.Offsets[i] = ReadInt32LittleEndian(bytes[(1226 + j)..]);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        WriteInt16LittleEndian(bytes[0..], Count);
        for (int i = 0, j = 0; i < 24; i++, j += 51)
            Entries[i].Write(bytes[(2 + j)..]);
        for (int i = 0, j = 0; i < 24; i++, j += 4)
            WriteInt32LittleEndian(bytes[(1226 + j)..], Offsets[i]);
    }
}

[PublicAPI]
internal partial struct ZztDatEntry
{
    public const int Size = 51;

    public ZztDatEntry()
    {
    }

    public byte Length { get; set; }
    public byte[] Bytes { get; init; } = new byte[50];

    public string Name
    {
        get => CodePage437.Encoding.GetString(Bytes[..Math.Min(Length, (byte)50)]);
        set => Length = (byte)CodePage437.Encoding.GetBytes(value, Bytes);
    }

    public static ZztDatEntry Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static ZztDatEntry Read(ReadOnlySpan<byte> bytes)
    {
        var result = new ZztDatEntry();
        result.Length = bytes[0];
        bytes[1..51].CopyTo(result.Bytes);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = Length;
        Bytes.CopyTo(bytes[1..]);
    }
}

[PublicAPI]
internal partial struct RawPosition
{
    public const int Size = 2;

    public RawPosition()
    {
    }

    public byte X { get; set; }
    public byte Y { get; set; }

    public static RawPosition Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static RawPosition Read(ReadOnlySpan<byte> bytes)
    {
        var result = new RawPosition();
        result.X = bytes[0];
        result.Y = bytes[1];
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = X;
        bytes[1] = Y;
    }
}

[PublicAPI]
internal partial struct RawTile
{
    public const int Size = 2;

    public RawTile()
    {
    }

    public byte ElementId { get; set; }
    public byte Color { get; set; }

    public static RawTile Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static RawTile Read(ReadOnlySpan<byte> bytes)
    {
        var result = new RawTile();
        result.ElementId = bytes[0];
        result.Color = bytes[1];
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = ElementId;
        bytes[1] = Color;
    }
}

[PublicAPI]
internal partial struct TileRle
{
    public const int Size = 3;

    public TileRle()
    {
    }

    public byte Count { get; set; }
    public byte ElementId { get; set; }
    public byte Color { get; set; }

    public static TileRle Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static TileRle Read(ReadOnlySpan<byte> bytes)
    {
        var result = new TileRle();
        result.Count = bytes[0];
        result.ElementId = bytes[1];
        result.Color = bytes[2];
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = Count;
        bytes[1] = ElementId;
        bytes[2] = Color;
    }
}

[PublicAPI]
internal partial struct Time
{
    public const int Size = 4;

    public Time()
    {
    }

    public short Seconds { get; set; }
    public short Hundredths { get; set; }

    public static Time Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static Time Read(ReadOnlySpan<byte> bytes)
    {
        var result = new Time();
        result.Seconds = ReadInt16LittleEndian(bytes[0..]);
        result.Hundredths = ReadInt16LittleEndian(bytes[2..]);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        WriteInt16LittleEndian(bytes[0..], Seconds);
        WriteInt16LittleEndian(bytes[2..], Hundredths);
    }
}

[PublicAPI]
internal partial struct RawVector
{
    public const int Size = 4;

    public RawVector()
    {
    }

    public short X { get; set; }
    public short Y { get; set; }

    public static RawVector Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static RawVector Read(ReadOnlySpan<byte> bytes)
    {
        var result = new RawVector();
        result.X = ReadInt16LittleEndian(bytes[0..]);
        result.Y = ReadInt16LittleEndian(bytes[2..]);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        WriteInt16LittleEndian(bytes[0..], X);
        WriteInt16LittleEndian(bytes[2..], Y);
    }
}

[PublicAPI]
internal partial struct Flag
{
    public const int Size = 21;

    public Flag()
    {
    }

    public byte Length { get; set; }
    public byte[] Bytes { get; init; } = new byte[20];

    public string Text
    {
        get => CodePage437.Encoding.GetString(Bytes[..Math.Min(Length, (byte)20)]);
        set => Length = (byte)CodePage437.Encoding.GetBytes(value, Bytes);
    }

    public static Flag Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        stream.ReadExactly(span);
        return Read(span);
    }

    public void Write(Stream stream)
    {
        Span<byte> span = stackalloc byte[Size];
        Write(span);
        stream.Write(span);
    }

    public static Flag Read(ReadOnlySpan<byte> bytes)
    {
        var result = new Flag();
        result.Length = bytes[0];
        bytes[1..21].CopyTo(result.Bytes);
        return result;
    }

    public void Write(Span<byte> bytes)
    {
        bytes[0] = Length;
        Bytes.CopyTo(bytes[1..]);
    }
}

#endregion