// Automatically generated from Structures.txt

using JetBrains.Annotations;
using System.Buffers.Binary;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
// ReSharper disable UseObjectOrCollectionInitializer
namespace LibRoton.Structures;

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

// Line 0
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztActor
{
    public const int Size = 33;

    [FieldOffset(0)] public ZztPosition Position;
    [FieldOffset(2)] public ZztVector Step;
    [FieldOffset(6)] public short Cycle;
    [FieldOffset(8)] public ZztActorParameters Parameters;
    [FieldOffset(11)] public short Follower;
    [FieldOffset(13)] public short Leader;
    [FieldOffset(15)] public ZztTile Under;
    [FieldOffset(17)] public int Pointer;
    [FieldOffset(21)] public short Instruction;
    [FieldOffset(23)] public short Length;
    [FieldOffset(25)] public ZztActorExtra Extra;

    public static ZztActor Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztActor();
        result.Position = ZztPosition.Read(span[0..]);
        result.Step = ZztVector.Read(span[2..]);
        result.Cycle = BinaryPrimitives.ReadInt16LittleEndian(span[6..]);
        result.Parameters = ZztActorParameters.Read(span[8..]);
        result.Follower = BinaryPrimitives.ReadInt16LittleEndian(span[11..]);
        result.Leader = BinaryPrimitives.ReadInt16LittleEndian(span[13..]);
        result.Under = ZztTile.Read(span[15..]);
        result.Pointer = BinaryPrimitives.ReadInt32LittleEndian(span[17..]);
        result.Instruction = BinaryPrimitives.ReadInt16LittleEndian(span[21..]);
        result.Length = BinaryPrimitives.ReadInt16LittleEndian(span[23..]);
        result.Extra = ZztActorExtra.Read(span[25..]);
        return result;
    }

    public static ZztActor Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[33];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztActor value)
    {
        ZztPosition.Write(span[0..], value.Position);
        ZztVector.Write(span[2..], value.Step);
        BinaryPrimitives.WriteInt16LittleEndian(span[6..], value.Cycle);
        ZztActorParameters.Write(span[8..], value.Parameters);
        BinaryPrimitives.WriteInt16LittleEndian(span[11..], value.Follower);
        BinaryPrimitives.WriteInt16LittleEndian(span[13..], value.Leader);
        ZztTile.Write(span[15..], value.Under);
        BinaryPrimitives.WriteInt32LittleEndian(span[17..], value.Pointer);
        BinaryPrimitives.WriteInt16LittleEndian(span[21..], value.Instruction);
        BinaryPrimitives.WriteInt16LittleEndian(span[23..], value.Length);
        ZztActorExtra.Write(span[25..], value.Extra);
    }

    public static void Write(Stream stream, ZztActor value)
    {
        Span<byte> span = stackalloc byte[33];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 14
[PublicAPI]
[InlineArray(Length)]
public struct ZztActorExtra : IEnumerable<byte>
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

    public static ZztActorExtra Read(Stream stream)
    {
        Span<ZztActorExtra> values = stackalloc ZztActorExtra[1];
        stream.ReadExactly(MemoryMarshal.Cast<ZztActorExtra, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, ZztActorExtra value) =>
        MemoryMarshal.Cast<byte, ZztActorExtra>(span)[0] = value;

    public static void Write(Stream stream, ZztActorExtra value)
    {
        Span<ZztActorExtra> values = stackalloc ZztActorExtra[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<ZztActorExtra, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 17
[PublicAPI]
[InlineArray(Length)]
public struct ZztActorParameters : IEnumerable<byte>
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

    public static ZztActorParameters Read(Stream stream)
    {
        Span<ZztActorParameters> values = stackalloc ZztActorParameters[1];
        stream.ReadExactly(MemoryMarshal.Cast<ZztActorParameters, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, ZztActorParameters value) =>
        MemoryMarshal.Cast<byte, ZztActorParameters>(span)[0] = value;

    public static void Write(Stream stream, ZztActorParameters value)
    {
        Span<ZztActorParameters> values = stackalloc ZztActorParameters[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<ZztActorParameters, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 20
[PublicAPI]
[InlineArray(Length)]
public struct ZztBoardExits : IEnumerable<byte>
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

    public static ZztBoardExits Read(Stream stream)
    {
        Span<ZztBoardExits> values = stackalloc ZztBoardExits[1];
        stream.ReadExactly(MemoryMarshal.Cast<ZztBoardExits, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, ZztBoardExits value) =>
        MemoryMarshal.Cast<byte, ZztBoardExits>(span)[0] = value;

    public static void Write(Stream stream, ZztBoardExits value)
    {
        Span<ZztBoardExits> values = stackalloc ZztBoardExits[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<ZztBoardExits, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 23
[PublicAPI]
[InlineArray(Length)]
public struct ZztBoardExtra : IEnumerable<byte>
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

    public static ZztBoardExtra Read(Stream stream)
    {
        Span<ZztBoardExtra> values = stackalloc ZztBoardExtra[1];
        stream.ReadExactly(MemoryMarshal.Cast<ZztBoardExtra, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, ZztBoardExtra value) =>
        MemoryMarshal.Cast<byte, ZztBoardExtra>(span)[0] = value;

    public static void Write(Stream stream, ZztBoardExtra value)
    {
        Span<ZztBoardExtra> values = stackalloc ZztBoardExtra[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<ZztBoardExtra, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 26
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztBoardHeader
{
    public const int Size = 53;

    [FieldOffset(0)] public short Length;
    [FieldOffset(2)] public ZztBoardName Name;

    public static ZztBoardHeader Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztBoardHeader();
        result.Length = BinaryPrimitives.ReadInt16LittleEndian(span[0..]);
        result.Name = ZztBoardName.Read(span[2..]);
        return result;
    }

    public static ZztBoardHeader Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[53];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztBoardHeader value)
    {
        BinaryPrimitives.WriteInt16LittleEndian(span[0..], value.Length);
        ZztBoardName.Write(span[2..], value.Name);
    }

    public static void Write(Stream stream, ZztBoardHeader value)
    {
        Span<byte> span = stackalloc byte[53];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 31
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztBoardInfo
{
    public const int Size = 88;

    [FieldOffset(0)] public byte MaxShots;
    [FieldOffset(1)] public byte IsDark;
    [FieldOffset(2)] public ZztBoardExits Exits;
    [FieldOffset(6)] public byte RestartOnZap;
    [FieldOffset(7)] public ZztBoardMessage Message;
    [FieldOffset(66)] public ZztPosition Enter;
    [FieldOffset(68)] public short TimeLimit;
    [FieldOffset(70)] public ZztBoardExtra Extra;
    [FieldOffset(86)] public short ActorCount;

    public static ZztBoardInfo Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztBoardInfo();
        result.MaxShots = span[0];
        result.IsDark = span[1];
        result.Exits = ZztBoardExits.Read(span[2..]);
        result.RestartOnZap = span[6];
        result.Message = ZztBoardMessage.Read(span[7..]);
        result.Enter = ZztPosition.Read(span[66..]);
        result.TimeLimit = BinaryPrimitives.ReadInt16LittleEndian(span[68..]);
        result.Extra = ZztBoardExtra.Read(span[70..]);
        result.ActorCount = BinaryPrimitives.ReadInt16LittleEndian(span[86..]);
        return result;
    }

    public static ZztBoardInfo Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[88];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztBoardInfo value)
    {
        span[0] = value.MaxShots;
        span[1] = value.IsDark;
        ZztBoardExits.Write(span[2..], value.Exits);
        span[6] = value.RestartOnZap;
        ZztBoardMessage.Write(span[7..], value.Message);
        ZztPosition.Write(span[66..], value.Enter);
        BinaryPrimitives.WriteInt16LittleEndian(span[68..], value.TimeLimit);
        ZztBoardExtra.Write(span[70..], value.Extra);
        BinaryPrimitives.WriteInt16LittleEndian(span[86..], value.ActorCount);
    }

    public static void Write(Stream stream, ZztBoardInfo value)
    {
        Span<byte> span = stackalloc byte[88];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 43
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztBoardMessage
{
    public const int Size = 59;

    public string Text
    {
        get
        {
            var source = Bytes.AsSpan();
            var length = Math.Min(Length, source.Length);
            return CodePage437.Encoding.GetString(source[..length]);
        }
        set
        {
            var target = Bytes.AsSpan();
            var length = CodePage437.Encoding.GetBytes(value, target);
            Length = (byte)length;
        }
    }

    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public ZztBoardMessageBytes Bytes;

    public static ZztBoardMessage Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztBoardMessage();
        result.Length = span[0];
        result.Bytes = ZztBoardMessageBytes.Read(span[1..]);
        return result;
    }

    public static ZztBoardMessage Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[59];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztBoardMessage value)
    {
        span[0] = value.Length;
        ZztBoardMessageBytes.Write(span[1..], value.Bytes);
    }

    public static void Write(Stream stream, ZztBoardMessage value)
    {
        Span<byte> span = stackalloc byte[59];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 49
[PublicAPI]
[InlineArray(Length)]
public struct ZztBoardMessageBytes : IEnumerable<byte>
{
    public const int Length = 58;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztBoardMessageBytes Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztBoardMessageBytes>(span)[0];

    public static ZztBoardMessageBytes Read(Stream stream)
    {
        Span<ZztBoardMessageBytes> values = stackalloc ZztBoardMessageBytes[1];
        stream.ReadExactly(MemoryMarshal.Cast<ZztBoardMessageBytes, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, ZztBoardMessageBytes value) =>
        MemoryMarshal.Cast<byte, ZztBoardMessageBytes>(span)[0] = value;

    public static void Write(Stream stream, ZztBoardMessageBytes value)
    {
        Span<ZztBoardMessageBytes> values = stackalloc ZztBoardMessageBytes[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<ZztBoardMessageBytes, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 52
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztBoardName
{
    public const int Size = 51;

    public string Text
    {
        get
        {
            var source = Bytes.AsSpan();
            var length = Math.Min(Length, source.Length);
            return CodePage437.Encoding.GetString(source[..length]);
        }
        set
        {
            var target = Bytes.AsSpan();
            var length = CodePage437.Encoding.GetBytes(value, target);
            Length = (byte)length;
        }
    }

    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public ZztBoardNameBytes Bytes;

    public static ZztBoardName Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztBoardName();
        result.Length = span[0];
        result.Bytes = ZztBoardNameBytes.Read(span[1..]);
        return result;
    }

    public static ZztBoardName Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[51];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztBoardName value)
    {
        span[0] = value.Length;
        ZztBoardNameBytes.Write(span[1..], value.Bytes);
    }

    public static void Write(Stream stream, ZztBoardName value)
    {
        Span<byte> span = stackalloc byte[51];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 58
[PublicAPI]
[InlineArray(Length)]
public struct ZztBoardNameBytes : IEnumerable<byte>
{
    public const int Length = 50;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztBoardNameBytes Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztBoardNameBytes>(span)[0];

    public static ZztBoardNameBytes Read(Stream stream)
    {
        Span<ZztBoardNameBytes> values = stackalloc ZztBoardNameBytes[1];
        stream.ReadExactly(MemoryMarshal.Cast<ZztBoardNameBytes, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, ZztBoardNameBytes value) =>
        MemoryMarshal.Cast<byte, ZztBoardNameBytes>(span)[0] = value;

    public static void Write(Stream stream, ZztBoardNameBytes value)
    {
        Span<ZztBoardNameBytes> values = stackalloc ZztBoardNameBytes[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<ZztBoardNameBytes, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 61
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztElement
{
    public const int Size = 195;

    [FieldOffset(0)] public byte Character;
    [FieldOffset(1)] public byte Color;
    [FieldOffset(2)] public byte IsDestructible;
    [FieldOffset(3)] public byte IsPushable;
    [FieldOffset(4)] public byte IsAlwaysVisible;
    [FieldOffset(5)] public byte IsEditorFloor;
    [FieldOffset(6)] public byte IsFloor;
    [FieldOffset(7)] public byte HasDrawFunc;
    [FieldOffset(8)] public int DrawFunc;
    [FieldOffset(12)] public short Cycle;
    [FieldOffset(14)] public int ActFunc;
    [FieldOffset(18)] public int InteractFunc;
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

    public static ZztElement Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztElement();
        result.Character = span[0];
        result.Color = span[1];
        result.IsDestructible = span[2];
        result.IsPushable = span[3];
        result.IsAlwaysVisible = span[4];
        result.IsEditorFloor = span[5];
        result.IsFloor = span[6];
        result.HasDrawFunc = span[7];
        result.DrawFunc = BinaryPrimitives.ReadInt32LittleEndian(span[8..]);
        result.Cycle = BinaryPrimitives.ReadInt16LittleEndian(span[12..]);
        result.ActFunc = BinaryPrimitives.ReadInt32LittleEndian(span[14..]);
        result.InteractFunc = BinaryPrimitives.ReadInt32LittleEndian(span[18..]);
        result.Menu = BinaryPrimitives.ReadInt16LittleEndian(span[22..]);
        result.MenuKey = span[24];
        result.Name = ZztElementString.Read(span[25..]);
        result.EditorCategoryText = ZztElementString.Read(span[46..]);
        result.EditorP1Text = ZztElementString.Read(span[67..]);
        result.EditorP2Text = ZztElementString.Read(span[88..]);
        result.EditorP3Text = ZztElementString.Read(span[109..]);
        result.EditorBoardText = ZztElementString.Read(span[130..]);
        result.EditorStepText = ZztElementString.Read(span[151..]);
        result.EditorCodeText = ZztElementString.Read(span[172..]);
        result.Score = BinaryPrimitives.ReadInt16LittleEndian(span[193..]);
        return result;
    }

    public static ZztElement Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[195];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztElement value)
    {
        span[0] = value.Character;
        span[1] = value.Color;
        span[2] = value.IsDestructible;
        span[3] = value.IsPushable;
        span[4] = value.IsAlwaysVisible;
        span[5] = value.IsEditorFloor;
        span[6] = value.IsFloor;
        span[7] = value.HasDrawFunc;
        BinaryPrimitives.WriteInt32LittleEndian(span[8..], value.DrawFunc);
        BinaryPrimitives.WriteInt16LittleEndian(span[12..], value.Cycle);
        BinaryPrimitives.WriteInt32LittleEndian(span[14..], value.ActFunc);
        BinaryPrimitives.WriteInt32LittleEndian(span[18..], value.InteractFunc);
        BinaryPrimitives.WriteInt16LittleEndian(span[22..], value.Menu);
        span[24] = value.MenuKey;
        ZztElementString.Write(span[25..], value.Name);
        ZztElementString.Write(span[46..], value.EditorCategoryText);
        ZztElementString.Write(span[67..], value.EditorP1Text);
        ZztElementString.Write(span[88..], value.EditorP2Text);
        ZztElementString.Write(span[109..], value.EditorP3Text);
        ZztElementString.Write(span[130..], value.EditorBoardText);
        ZztElementString.Write(span[151..], value.EditorStepText);
        ZztElementString.Write(span[172..], value.EditorCodeText);
        BinaryPrimitives.WriteInt16LittleEndian(span[193..], value.Score);
    }

    public static void Write(Stream stream, ZztElement value)
    {
        Span<byte> span = stackalloc byte[195];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 87
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztElementString
{
    public const int Size = 21;

    public string Text
    {
        get
        {
            var source = Bytes.AsSpan();
            var length = Math.Min(Length, source.Length);
            return CodePage437.Encoding.GetString(source[..length]);
        }
        set
        {
            var target = Bytes.AsSpan();
            var length = CodePage437.Encoding.GetBytes(value, target);
            Length = (byte)length;
        }
    }

    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public ZztElementStringBytes Bytes;

    public static ZztElementString Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztElementString();
        result.Length = span[0];
        result.Bytes = ZztElementStringBytes.Read(span[1..]);
        return result;
    }

    public static ZztElementString Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[21];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztElementString value)
    {
        span[0] = value.Length;
        ZztElementStringBytes.Write(span[1..], value.Bytes);
    }

    public static void Write(Stream stream, ZztElementString value)
    {
        Span<byte> span = stackalloc byte[21];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 93
[PublicAPI]
[InlineArray(Length)]
public struct ZztElementStringBytes : IEnumerable<byte>
{
    public const int Length = 20;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztElementStringBytes Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztElementStringBytes>(span)[0];

    public static ZztElementStringBytes Read(Stream stream)
    {
        Span<ZztElementStringBytes> values = stackalloc ZztElementStringBytes[1];
        stream.ReadExactly(MemoryMarshal.Cast<ZztElementStringBytes, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, ZztElementStringBytes value) =>
        MemoryMarshal.Cast<byte, ZztElementStringBytes>(span)[0] = value;

    public static void Write(Stream stream, ZztElementStringBytes value)
    {
        Span<ZztElementStringBytes> values = stackalloc ZztElementStringBytes[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<ZztElementStringBytes, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 96
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztPosition
{
    public const int Size = 2;

    [FieldOffset(0)] public byte X;
    [FieldOffset(1)] public byte Y;

    public static ZztPosition Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztPosition();
        result.X = span[0];
        result.Y = span[1];
        return result;
    }

    public static ZztPosition Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[2];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztPosition value)
    {
        span[0] = value.X;
        span[1] = value.Y;
    }

    public static void Write(Stream stream, ZztPosition value)
    {
        Span<byte> span = stackalloc byte[2];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 101
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztTile
{
    public const int Size = 2;

    [FieldOffset(0)] public byte Element;
    [FieldOffset(1)] public byte Color;

    public static ZztTile Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztTile();
        result.Element = span[0];
        result.Color = span[1];
        return result;
    }

    public static ZztTile Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[2];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztTile value)
    {
        span[0] = value.Element;
        span[1] = value.Color;
    }

    public static void Write(Stream stream, ZztTile value)
    {
        Span<byte> span = stackalloc byte[2];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 106
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztTileRle
{
    public const int Size = 3;

    [FieldOffset(0)] public byte Count;
    [FieldOffset(1)] public byte Element;
    [FieldOffset(2)] public byte Color;

    public static ZztTileRle Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztTileRle();
        result.Count = span[0];
        result.Element = span[1];
        result.Color = span[2];
        return result;
    }

    public static ZztTileRle Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[3];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztTileRle value)
    {
        span[0] = value.Count;
        span[1] = value.Element;
        span[2] = value.Color;
    }

    public static void Write(Stream stream, ZztTileRle value)
    {
        Span<byte> span = stackalloc byte[3];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 112
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztTime
{
    public const int Size = 4;

    [FieldOffset(0)] public short Seconds;
    [FieldOffset(2)] public short Fraction;

    public static ZztTime Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztTime();
        result.Seconds = BinaryPrimitives.ReadInt16LittleEndian(span[0..]);
        result.Fraction = BinaryPrimitives.ReadInt16LittleEndian(span[2..]);
        return result;
    }

    public static ZztTime Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[4];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztTime value)
    {
        BinaryPrimitives.WriteInt16LittleEndian(span[0..], value.Seconds);
        BinaryPrimitives.WriteInt16LittleEndian(span[2..], value.Fraction);
    }

    public static void Write(Stream stream, ZztTime value)
    {
        Span<byte> span = stackalloc byte[4];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 117
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztVector
{
    public const int Size = 4;

    [FieldOffset(0)] public short X;
    [FieldOffset(2)] public short Y;

    public static ZztVector Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztVector();
        result.X = BinaryPrimitives.ReadInt16LittleEndian(span[0..]);
        result.Y = BinaryPrimitives.ReadInt16LittleEndian(span[2..]);
        return result;
    }

    public static ZztVector Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[4];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztVector value)
    {
        BinaryPrimitives.WriteInt16LittleEndian(span[0..], value.X);
        BinaryPrimitives.WriteInt16LittleEndian(span[2..], value.Y);
    }

    public static void Write(Stream stream, ZztVector value)
    {
        Span<byte> span = stackalloc byte[4];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 122
[PublicAPI]
[InlineArray(Length)]
public struct ZztWorldExtra : IEnumerable<byte>
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

    public static ZztWorldExtra Read(Stream stream)
    {
        Span<ZztWorldExtra> values = stackalloc ZztWorldExtra[1];
        stream.ReadExactly(MemoryMarshal.Cast<ZztWorldExtra, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, ZztWorldExtra value) =>
        MemoryMarshal.Cast<byte, ZztWorldExtra>(span)[0] = value;

    public static void Write(Stream stream, ZztWorldExtra value)
    {
        Span<ZztWorldExtra> values = stackalloc ZztWorldExtra[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<ZztWorldExtra, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 125
[PublicAPI]
[InlineArray(Length)]
public struct ZztWorldFlags : IEnumerable<ZztWorldString>
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

    public static ZztWorldFlags Read(Stream stream)
    {
        Span<ZztWorldFlags> values = stackalloc ZztWorldFlags[1];
        stream.ReadExactly(MemoryMarshal.Cast<ZztWorldFlags, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, ZztWorldFlags value) =>
        MemoryMarshal.Cast<byte, ZztWorldFlags>(span)[0] = value;

    public static void Write(Stream stream, ZztWorldFlags value)
    {
        Span<ZztWorldFlags> values = stackalloc ZztWorldFlags[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<ZztWorldFlags, byte>(values));
    }

    public IEnumerable<ZztWorldString> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<ZztWorldString> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public ZztWorldString[] ToArray() =>
        AsSpan().ToArray();
}

// Line 128
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztWorldHeader
{
    public const int Size = 512;

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
    [FieldOffset(29)] public ZztWorldString Name;
    [FieldOffset(50)] public ZztWorldFlags Flags;
    [FieldOffset(260)] public ZztTime TimePassed;
    [FieldOffset(264)] public byte Locked;
    [FieldOffset(265)] public ZztWorldExtra Extra;

    public static ZztWorldHeader Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztWorldHeader();
        result.Type = BinaryPrimitives.ReadInt16LittleEndian(span[0..]);
        result.BoardCount = BinaryPrimitives.ReadInt16LittleEndian(span[2..]);
        result.Ammo = BinaryPrimitives.ReadInt16LittleEndian(span[4..]);
        result.Gems = BinaryPrimitives.ReadInt16LittleEndian(span[6..]);
        result.Keys = ZztWorldKeys.Read(span[8..]);
        result.Health = BinaryPrimitives.ReadInt16LittleEndian(span[15..]);
        result.Board = BinaryPrimitives.ReadInt16LittleEndian(span[17..]);
        result.Torches = BinaryPrimitives.ReadInt16LittleEndian(span[19..]);
        result.TorchCycles = BinaryPrimitives.ReadInt16LittleEndian(span[21..]);
        result.EnergyCycles = BinaryPrimitives.ReadInt16LittleEndian(span[23..]);
        result.Unused25 = BinaryPrimitives.ReadInt16LittleEndian(span[25..]);
        result.Score = BinaryPrimitives.ReadInt16LittleEndian(span[27..]);
        result.Name = ZztWorldString.Read(span[29..]);
        result.Flags = ZztWorldFlags.Read(span[50..]);
        result.TimePassed = ZztTime.Read(span[260..]);
        result.Locked = span[264];
        result.Extra = ZztWorldExtra.Read(span[265..]);
        return result;
    }

    public static ZztWorldHeader Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[512];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztWorldHeader value)
    {
        BinaryPrimitives.WriteInt16LittleEndian(span[0..], value.Type);
        BinaryPrimitives.WriteInt16LittleEndian(span[2..], value.BoardCount);
        BinaryPrimitives.WriteInt16LittleEndian(span[4..], value.Ammo);
        BinaryPrimitives.WriteInt16LittleEndian(span[6..], value.Gems);
        ZztWorldKeys.Write(span[8..], value.Keys);
        BinaryPrimitives.WriteInt16LittleEndian(span[15..], value.Health);
        BinaryPrimitives.WriteInt16LittleEndian(span[17..], value.Board);
        BinaryPrimitives.WriteInt16LittleEndian(span[19..], value.Torches);
        BinaryPrimitives.WriteInt16LittleEndian(span[21..], value.TorchCycles);
        BinaryPrimitives.WriteInt16LittleEndian(span[23..], value.EnergyCycles);
        BinaryPrimitives.WriteInt16LittleEndian(span[25..], value.Unused25);
        BinaryPrimitives.WriteInt16LittleEndian(span[27..], value.Score);
        ZztWorldString.Write(span[29..], value.Name);
        ZztWorldFlags.Write(span[50..], value.Flags);
        ZztTime.Write(span[260..], value.TimePassed);
        span[264] = value.Locked;
        ZztWorldExtra.Write(span[265..], value.Extra);
    }

    public static void Write(Stream stream, ZztWorldHeader value)
    {
        Span<byte> span = stackalloc byte[512];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 148
[PublicAPI]
[InlineArray(Length)]
public struct ZztWorldKeys : IEnumerable<byte>
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

    public static ZztWorldKeys Read(Stream stream)
    {
        Span<ZztWorldKeys> values = stackalloc ZztWorldKeys[1];
        stream.ReadExactly(MemoryMarshal.Cast<ZztWorldKeys, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, ZztWorldKeys value) =>
        MemoryMarshal.Cast<byte, ZztWorldKeys>(span)[0] = value;

    public static void Write(Stream stream, ZztWorldKeys value)
    {
        Span<ZztWorldKeys> values = stackalloc ZztWorldKeys[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<ZztWorldKeys, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 151
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct ZztWorldString
{
    public const int Size = 21;

    public string Text
    {
        get
        {
            var source = Bytes.AsSpan();
            var length = Math.Min(Length, source.Length);
            return CodePage437.Encoding.GetString(source[..length]);
        }
        set
        {
            var target = Bytes.AsSpan();
            var length = CodePage437.Encoding.GetBytes(value, target);
            Length = (byte)length;
        }
    }

    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public ZztWorldStringBytes Bytes;

    public static ZztWorldString Read(ReadOnlySpan<byte> span)
    {
        var result = new ZztWorldString();
        result.Length = span[0];
        result.Bytes = ZztWorldStringBytes.Read(span[1..]);
        return result;
    }

    public static ZztWorldString Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[21];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, ZztWorldString value)
    {
        span[0] = value.Length;
        ZztWorldStringBytes.Write(span[1..], value.Bytes);
    }

    public static void Write(Stream stream, ZztWorldString value)
    {
        Span<byte> span = stackalloc byte[21];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 157
[PublicAPI]
[InlineArray(Length)]
public struct ZztWorldStringBytes : IEnumerable<byte>
{
    public const int Length = 20;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static ZztWorldStringBytes Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, ZztWorldStringBytes>(span)[0];

    public static ZztWorldStringBytes Read(Stream stream)
    {
        Span<ZztWorldStringBytes> values = stackalloc ZztWorldStringBytes[1];
        stream.ReadExactly(MemoryMarshal.Cast<ZztWorldStringBytes, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, ZztWorldStringBytes value) =>
        MemoryMarshal.Cast<byte, ZztWorldStringBytes>(span)[0] = value;

    public static void Write(Stream stream, ZztWorldStringBytes value)
    {
        Span<ZztWorldStringBytes> values = stackalloc ZztWorldStringBytes[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<ZztWorldStringBytes, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 160
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct SuperZztActor
{
    public const int Size = 25;

    [FieldOffset(0)] public ZztPosition Position;
    [FieldOffset(2)] public ZztVector Step;
    [FieldOffset(6)] public short Cycle;
    [FieldOffset(8)] public ZztActorParameters Parameters;
    [FieldOffset(11)] public short Follower;
    [FieldOffset(13)] public short Leader;
    [FieldOffset(15)] public ZztTile Under;
    [FieldOffset(17)] public int Pointer;
    [FieldOffset(21)] public short Instruction;
    [FieldOffset(23)] public short Length;

    public static SuperZztActor Read(ReadOnlySpan<byte> span)
    {
        var result = new SuperZztActor();
        result.Position = ZztPosition.Read(span[0..]);
        result.Step = ZztVector.Read(span[2..]);
        result.Cycle = BinaryPrimitives.ReadInt16LittleEndian(span[6..]);
        result.Parameters = ZztActorParameters.Read(span[8..]);
        result.Follower = BinaryPrimitives.ReadInt16LittleEndian(span[11..]);
        result.Leader = BinaryPrimitives.ReadInt16LittleEndian(span[13..]);
        result.Under = ZztTile.Read(span[15..]);
        result.Pointer = BinaryPrimitives.ReadInt32LittleEndian(span[17..]);
        result.Instruction = BinaryPrimitives.ReadInt16LittleEndian(span[21..]);
        result.Length = BinaryPrimitives.ReadInt16LittleEndian(span[23..]);
        return result;
    }

    public static SuperZztActor Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[25];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, SuperZztActor value)
    {
        ZztPosition.Write(span[0..], value.Position);
        ZztVector.Write(span[2..], value.Step);
        BinaryPrimitives.WriteInt16LittleEndian(span[6..], value.Cycle);
        ZztActorParameters.Write(span[8..], value.Parameters);
        BinaryPrimitives.WriteInt16LittleEndian(span[11..], value.Follower);
        BinaryPrimitives.WriteInt16LittleEndian(span[13..], value.Leader);
        ZztTile.Write(span[15..], value.Under);
        BinaryPrimitives.WriteInt32LittleEndian(span[17..], value.Pointer);
        BinaryPrimitives.WriteInt16LittleEndian(span[21..], value.Instruction);
        BinaryPrimitives.WriteInt16LittleEndian(span[23..], value.Length);
    }

    public static void Write(Stream stream, SuperZztActor value)
    {
        Span<byte> span = stackalloc byte[25];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 173
[PublicAPI]
[InlineArray(Length)]
public struct SuperZztBoardExtra : IEnumerable<byte>
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

    public static SuperZztBoardExtra Read(Stream stream)
    {
        Span<SuperZztBoardExtra> values = stackalloc SuperZztBoardExtra[1];
        stream.ReadExactly(MemoryMarshal.Cast<SuperZztBoardExtra, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, SuperZztBoardExtra value) =>
        MemoryMarshal.Cast<byte, SuperZztBoardExtra>(span)[0] = value;

    public static void Write(Stream stream, SuperZztBoardExtra value)
    {
        Span<SuperZztBoardExtra> values = stackalloc SuperZztBoardExtra[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<SuperZztBoardExtra, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 176
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct SuperZztBoardHeader
{
    public const int Size = 63;

    [FieldOffset(0)] public short Length;
    [FieldOffset(2)] public SuperZztBoardName Name;

    public static SuperZztBoardHeader Read(ReadOnlySpan<byte> span)
    {
        var result = new SuperZztBoardHeader();
        result.Length = BinaryPrimitives.ReadInt16LittleEndian(span[0..]);
        result.Name = SuperZztBoardName.Read(span[2..]);
        return result;
    }

    public static SuperZztBoardHeader Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[63];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, SuperZztBoardHeader value)
    {
        BinaryPrimitives.WriteInt16LittleEndian(span[0..], value.Length);
        SuperZztBoardName.Write(span[2..], value.Name);
    }

    public static void Write(Stream stream, SuperZztBoardHeader value)
    {
        Span<byte> span = stackalloc byte[63];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 181
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct SuperZztBoardInfo
{
    public const int Size = 30;

    [FieldOffset(0)] public byte MaxShots;
    [FieldOffset(1)] public ZztBoardExits Exits;
    [FieldOffset(5)] public byte RestartOnZap;
    [FieldOffset(6)] public ZztPosition Enter;
    [FieldOffset(8)] public ZztVector Camera;
    [FieldOffset(12)] public short TimeLimit;
    [FieldOffset(14)] public SuperZztBoardExtra Extra;
    [FieldOffset(28)] public short ActorCount;

    public static SuperZztBoardInfo Read(ReadOnlySpan<byte> span)
    {
        var result = new SuperZztBoardInfo();
        result.MaxShots = span[0];
        result.Exits = ZztBoardExits.Read(span[1..]);
        result.RestartOnZap = span[5];
        result.Enter = ZztPosition.Read(span[6..]);
        result.Camera = ZztVector.Read(span[8..]);
        result.TimeLimit = BinaryPrimitives.ReadInt16LittleEndian(span[12..]);
        result.Extra = SuperZztBoardExtra.Read(span[14..]);
        result.ActorCount = BinaryPrimitives.ReadInt16LittleEndian(span[28..]);
        return result;
    }

    public static SuperZztBoardInfo Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[30];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, SuperZztBoardInfo value)
    {
        span[0] = value.MaxShots;
        ZztBoardExits.Write(span[1..], value.Exits);
        span[5] = value.RestartOnZap;
        ZztPosition.Write(span[6..], value.Enter);
        ZztVector.Write(span[8..], value.Camera);
        BinaryPrimitives.WriteInt16LittleEndian(span[12..], value.TimeLimit);
        SuperZztBoardExtra.Write(span[14..], value.Extra);
        BinaryPrimitives.WriteInt16LittleEndian(span[28..], value.ActorCount);
    }

    public static void Write(Stream stream, SuperZztBoardInfo value)
    {
        Span<byte> span = stackalloc byte[30];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 192
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct SuperZztBoardName
{
    public const int Size = 61;

    public string Text
    {
        get
        {
            var source = Bytes.AsSpan();
            var length = Math.Min(Length, source.Length);
            return CodePage437.Encoding.GetString(source[..length]);
        }
        set
        {
            var target = Bytes.AsSpan();
            var length = CodePage437.Encoding.GetBytes(value, target);
            Length = (byte)length;
        }
    }

    [FieldOffset(0)] public byte Length;
    [FieldOffset(1)] public SuperZztBoardNameBytes Bytes;

    public static SuperZztBoardName Read(ReadOnlySpan<byte> span)
    {
        var result = new SuperZztBoardName();
        result.Length = span[0];
        result.Bytes = SuperZztBoardNameBytes.Read(span[1..]);
        return result;
    }

    public static SuperZztBoardName Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[61];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, SuperZztBoardName value)
    {
        span[0] = value.Length;
        SuperZztBoardNameBytes.Write(span[1..], value.Bytes);
    }

    public static void Write(Stream stream, SuperZztBoardName value)
    {
        Span<byte> span = stackalloc byte[61];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 198
[PublicAPI]
[InlineArray(Length)]
public struct SuperZztBoardNameBytes : IEnumerable<byte>
{
    public const int Length = 60;

    private byte _element0;

    public static int Size => Length * Marshal.SizeOf<byte>();

    public Span<byte> AsSpan() =>
        MemoryMarshal.CreateSpan(ref _element0, Length);

    public void CopyTo(Span<byte> span) =>
        AsSpan().CopyTo(span);

    public static SuperZztBoardNameBytes Read(ReadOnlySpan<byte> span) =>
        MemoryMarshal.Cast<byte, SuperZztBoardNameBytes>(span)[0];

    public static SuperZztBoardNameBytes Read(Stream stream)
    {
        Span<SuperZztBoardNameBytes> values = stackalloc SuperZztBoardNameBytes[1];
        stream.ReadExactly(MemoryMarshal.Cast<SuperZztBoardNameBytes, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, SuperZztBoardNameBytes value) =>
        MemoryMarshal.Cast<byte, SuperZztBoardNameBytes>(span)[0] = value;

    public static void Write(Stream stream, SuperZztBoardNameBytes value)
    {
        Span<SuperZztBoardNameBytes> values = stackalloc SuperZztBoardNameBytes[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<SuperZztBoardNameBytes, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 201
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct SuperZztElement
{
    public const int Size = 194;

    [FieldOffset(0)] public byte Character;
    [FieldOffset(1)] public byte Color;
    [FieldOffset(2)] public byte IsDestructible;
    [FieldOffset(3)] public byte IsPushable;
    [FieldOffset(4)] public byte IsEditorFloor;
    [FieldOffset(5)] public byte IsFloor;
    [FieldOffset(6)] public byte HasDrawFunc;
    [FieldOffset(7)] public int DrawFunc;
    [FieldOffset(11)] public short Cycle;
    [FieldOffset(13)] public int ActFunc;
    [FieldOffset(17)] public int InteractFunc;
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

    public static SuperZztElement Read(ReadOnlySpan<byte> span)
    {
        var result = new SuperZztElement();
        result.Character = span[0];
        result.Color = span[1];
        result.IsDestructible = span[2];
        result.IsPushable = span[3];
        result.IsEditorFloor = span[4];
        result.IsFloor = span[5];
        result.HasDrawFunc = span[6];
        result.DrawFunc = BinaryPrimitives.ReadInt32LittleEndian(span[7..]);
        result.Cycle = BinaryPrimitives.ReadInt16LittleEndian(span[11..]);
        result.ActFunc = BinaryPrimitives.ReadInt32LittleEndian(span[13..]);
        result.InteractFunc = BinaryPrimitives.ReadInt32LittleEndian(span[17..]);
        result.Menu = BinaryPrimitives.ReadInt16LittleEndian(span[21..]);
        result.MenuKey = span[23];
        result.Name = ZztElementString.Read(span[24..]);
        result.EditorCategoryText = ZztElementString.Read(span[45..]);
        result.EditorP1Text = ZztElementString.Read(span[66..]);
        result.EditorP2Text = ZztElementString.Read(span[87..]);
        result.EditorP3Text = ZztElementString.Read(span[108..]);
        result.EditorBoardText = ZztElementString.Read(span[129..]);
        result.EditorStepText = ZztElementString.Read(span[150..]);
        result.EditorCodeText = ZztElementString.Read(span[171..]);
        result.Score = BinaryPrimitives.ReadInt16LittleEndian(span[192..]);
        return result;
    }

    public static SuperZztElement Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[194];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, SuperZztElement value)
    {
        span[0] = value.Character;
        span[1] = value.Color;
        span[2] = value.IsDestructible;
        span[3] = value.IsPushable;
        span[4] = value.IsEditorFloor;
        span[5] = value.IsFloor;
        span[6] = value.HasDrawFunc;
        BinaryPrimitives.WriteInt32LittleEndian(span[7..], value.DrawFunc);
        BinaryPrimitives.WriteInt16LittleEndian(span[11..], value.Cycle);
        BinaryPrimitives.WriteInt32LittleEndian(span[13..], value.ActFunc);
        BinaryPrimitives.WriteInt32LittleEndian(span[17..], value.InteractFunc);
        BinaryPrimitives.WriteInt16LittleEndian(span[21..], value.Menu);
        span[23] = value.MenuKey;
        ZztElementString.Write(span[24..], value.Name);
        ZztElementString.Write(span[45..], value.EditorCategoryText);
        ZztElementString.Write(span[66..], value.EditorP1Text);
        ZztElementString.Write(span[87..], value.EditorP2Text);
        ZztElementString.Write(span[108..], value.EditorP3Text);
        ZztElementString.Write(span[129..], value.EditorBoardText);
        ZztElementString.Write(span[150..], value.EditorStepText);
        ZztElementString.Write(span[171..], value.EditorCodeText);
        BinaryPrimitives.WriteInt16LittleEndian(span[192..], value.Score);
    }

    public static void Write(Stream stream, SuperZztElement value)
    {
        Span<byte> span = stackalloc byte[194];
        Write(span, value);
        stream.Write(span);
    }
}

// Line 226
[PublicAPI]
[InlineArray(Length)]
public struct SuperZztWorldExtra : IEnumerable<byte>
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

    public static SuperZztWorldExtra Read(Stream stream)
    {
        Span<SuperZztWorldExtra> values = stackalloc SuperZztWorldExtra[1];
        stream.ReadExactly(MemoryMarshal.Cast<SuperZztWorldExtra, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, SuperZztWorldExtra value) =>
        MemoryMarshal.Cast<byte, SuperZztWorldExtra>(span)[0] = value;

    public static void Write(Stream stream, SuperZztWorldExtra value)
    {
        Span<SuperZztWorldExtra> values = stackalloc SuperZztWorldExtra[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<SuperZztWorldExtra, byte>(values));
    }

    public IEnumerable<byte> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<byte> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public byte[] ToArray() =>
        AsSpan().ToArray();
}

// Line 229
[PublicAPI]
[InlineArray(Length)]
public struct SuperZztWorldFlags : IEnumerable<ZztWorldString>
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

    public static SuperZztWorldFlags Read(Stream stream)
    {
        Span<SuperZztWorldFlags> values = stackalloc SuperZztWorldFlags[1];
        stream.ReadExactly(MemoryMarshal.Cast<SuperZztWorldFlags, byte>(values));
        return values[0];
    }

    public static void Write(Span<byte> span, SuperZztWorldFlags value) =>
        MemoryMarshal.Cast<byte, SuperZztWorldFlags>(span)[0] = value;

    public static void Write(Stream stream, SuperZztWorldFlags value)
    {
        Span<SuperZztWorldFlags> values = stackalloc SuperZztWorldFlags[1];
        values[0] = value;
        stream.Write(MemoryMarshal.Cast<SuperZztWorldFlags, byte>(values));
    }

    public IEnumerable<ZztWorldString> AsEnumerable()
    {
        for (var i = 0; i < Length; i++)
            yield return this[i];
    }

    public IEnumerator<ZztWorldString> GetEnumerator() =>
        AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();

    public ZztWorldString[] ToArray() =>
        AsSpan().ToArray();
}

// Line 232
[PublicAPI]
[StructLayout(LayoutKind.Explicit, Size = Size, Pack = 1)]
public struct SuperZztWorldHeader
{
    public const int Size = 1024;

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
    [FieldOffset(27)] public ZztWorldString Name;
    [FieldOffset(48)] public SuperZztWorldFlags Flags;
    [FieldOffset(384)] public ZztTime TimePassed;
    [FieldOffset(388)] public byte Locked;
    [FieldOffset(389)] public short Stones;
    [FieldOffset(391)] public SuperZztWorldExtra Extra;

    public static SuperZztWorldHeader Read(ReadOnlySpan<byte> span)
    {
        var result = new SuperZztWorldHeader();
        result.Type = BinaryPrimitives.ReadInt16LittleEndian(span[0..]);
        result.BoardCount = BinaryPrimitives.ReadInt16LittleEndian(span[2..]);
        result.Ammo = BinaryPrimitives.ReadInt16LittleEndian(span[4..]);
        result.Gems = BinaryPrimitives.ReadInt16LittleEndian(span[6..]);
        result.Keys = ZztWorldKeys.Read(span[8..]);
        result.Health = BinaryPrimitives.ReadInt16LittleEndian(span[15..]);
        result.Board = BinaryPrimitives.ReadInt16LittleEndian(span[17..]);
        result.Unused19 = BinaryPrimitives.ReadInt16LittleEndian(span[19..]);
        result.Score = BinaryPrimitives.ReadInt16LittleEndian(span[21..]);
        result.Unused23 = BinaryPrimitives.ReadInt16LittleEndian(span[23..]);
        result.EnergyCycles = BinaryPrimitives.ReadInt16LittleEndian(span[25..]);
        result.Name = ZztWorldString.Read(span[27..]);
        result.Flags = SuperZztWorldFlags.Read(span[48..]);
        result.TimePassed = ZztTime.Read(span[384..]);
        result.Locked = span[388];
        result.Stones = BinaryPrimitives.ReadInt16LittleEndian(span[389..]);
        result.Extra = SuperZztWorldExtra.Read(span[391..]);
        return result;
    }

    public static SuperZztWorldHeader Read(Stream stream)
    {
        Span<byte> span = stackalloc byte[1024];
        stream.ReadExactly(span);
        return Read(span);
    }

    public static void Write(Span<byte> span, SuperZztWorldHeader value)
    {
        BinaryPrimitives.WriteInt16LittleEndian(span[0..], value.Type);
        BinaryPrimitives.WriteInt16LittleEndian(span[2..], value.BoardCount);
        BinaryPrimitives.WriteInt16LittleEndian(span[4..], value.Ammo);
        BinaryPrimitives.WriteInt16LittleEndian(span[6..], value.Gems);
        ZztWorldKeys.Write(span[8..], value.Keys);
        BinaryPrimitives.WriteInt16LittleEndian(span[15..], value.Health);
        BinaryPrimitives.WriteInt16LittleEndian(span[17..], value.Board);
        BinaryPrimitives.WriteInt16LittleEndian(span[19..], value.Unused19);
        BinaryPrimitives.WriteInt16LittleEndian(span[21..], value.Score);
        BinaryPrimitives.WriteInt16LittleEndian(span[23..], value.Unused23);
        BinaryPrimitives.WriteInt16LittleEndian(span[25..], value.EnergyCycles);
        ZztWorldString.Write(span[27..], value.Name);
        SuperZztWorldFlags.Write(span[48..], value.Flags);
        ZztTime.Write(span[384..], value.TimePassed);
        span[388] = value.Locked;
        BinaryPrimitives.WriteInt16LittleEndian(span[389..], value.Stones);
        SuperZztWorldExtra.Write(span[391..], value.Extra);
    }

    public static void Write(Stream stream, SuperZztWorldHeader value)
    {
        Span<byte> span = stackalloc byte[1024];
        Write(span, value);
        stream.Write(span);
    }
}
