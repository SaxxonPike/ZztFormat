using System.Buffers;
using System.Buffers.Binary;
using JetBrains.Annotations;

namespace LibRoton.Structures;

[PublicAPI]
public class Board
{
    public string Name { get; set; } = string.Empty;
    public Tile[] Tiles { get; set; } = [];
    public int MaxShots { get; set; }
    public bool IsDark { get; set; }
    public int[] Exits { get; set; } = [];
    public bool RestartOnZap { get; set; }
    public string Message { get; set; } = string.Empty;
    public Vec2 Enter { get; set; }
    public TimeSpan TimeLimit { get; set; }
    public Vec2 Camera { get; set; }
    public List<Actor> Actors { get; set; } = [];

    public Vec2 Size { get; set; }
    public bool IsCorrupt { get; set; }

    public Tile this[int x, int y]
    {
        get => Tiles[y * Width + x];
        set => Tiles[y * Width + x] = value;
    }

    public int Width
    {
        get => Size.X;
        set => Size = Size with { X = value };
    }

    public int Height
    {
        get => Size.Y;
        set => Size = Size with { Y = value };
    }

    public static Board Read(Stream stream, short worldType)
    {
        Span<byte> sizeBuf = stackalloc byte[2];
        stream.ReadExactly(sizeBuf);
        var size = BinaryPrimitives.ReadInt16LittleEndian(sizeBuf);

        byte[]? boardBuf = default;

        try
        {
            boardBuf = ArrayPool<byte>.Shared.Rent(size);
            stream.ReadExactly(boardBuf.AsSpan(0, size));
            using var dataStream = new MemoryStream(boardBuf);

            return worldType switch
            {
                -1 => ReadZztBoard(dataStream),
                -2 => ReadSuperZztBoard(dataStream),
                _ => throw new Exception($"Unknown world type {size}.")
            };
        }
        finally
        {
            if (boardBuf != null)
                ArrayPool<byte>.Shared.Return(boardBuf);
        }
    }

    private static int[] ReadExits(byte[] exits)
    {
        var result = new int[exits.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = exits[i];
        return result;
    }

    private static Tile[] ReadTiles(RawTile[] tiles)
    {
        var result = new Tile[tiles.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = tiles[i].ToTile();
        return result;
    }

    private static Board ReadZztBoard(Stream stream)
    {
        var header = ZztBoardHeader.Read(stream);

        try
        {
            var tiles = new RawTile[60 * 25];
            Rle.Unpack(stream, tiles);

            var info = ZztBoardInfo.Read(stream);

            var actors = new List<Actor>();
            for (var i = 0; i <= info.ActorCount; i++)
                actors.Add(ReadZztActor(stream));

            return new Board
            {
                Width = 60,
                Height = 25,
                Name = header.Name,
                Tiles = ReadTiles(tiles),
                MaxShots = info.MaxShots,
                IsDark = info.DarkBit != 0,
                Exits = ReadExits(info.Exits),
                RestartOnZap = info.RestartOnZapBit != 0,
                Message = info.Message,
                Enter = Vec2.FromRawPosition(info.Enter),
                TimeLimit = TimeSpan.FromSeconds(info.TimeLimit),
                Actors = actors
            };
        }
        catch (EndOfStreamException)
        {
            return new Board
            {
                IsCorrupt = true,
                Name = header.Name
            };
        }
    }

    private static Board ReadSuperZztBoard(Stream stream)
    {
        var header = SuperZztBoardHeader.Read(stream);

        try
        {
            var tiles = new RawTile[96 * 80];
            Rle.Unpack(stream, tiles);

            var info = SuperZztBoardInfo.Read(stream);

            var actors = new List<Actor>();
            for (var i = 0; i <= info.ActorCount; i++)
                actors.Add(ReadSuperZztActor(stream));

            return new Board
            {
                Width = 96,
                Height = 80,
                Name = header.Name,
                Tiles = ReadTiles(tiles),
                MaxShots = info.MaxShots,
                Exits = ReadExits(info.Exits),
                RestartOnZap = info.RestartOnZapBit != 0,
                Enter = Vec2.FromRawPosition(info.Enter),
                TimeLimit = TimeSpan.FromSeconds(info.TimeLimit),
                Camera = Vec2.FromRawVector(info.Camera),
                Actors = actors
            };
        }
        catch
        {
            return new Board
            {
                IsCorrupt = true,
                Name = header.Name
            };
        }
    }

    private static char[] ReadScript(Stream stream, int length)
    {
        if (length < 1)
            return [];

        Span<byte> bytes = stackalloc byte[length];
        stream.ReadExactly(bytes);

        var charCount = CodePage437.Encoding.GetCharCount(bytes);
        var chars = new char[charCount];
        CodePage437.Encoding.GetChars(bytes, chars);

        return chars;
    }

    private static int[] ReadParameters(byte[] parameters)
    {
        var result = new int[parameters.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = parameters[i];
        return result;
    }

    private static Actor ReadZztActor(Stream stream)
    {
        var info = ZztActor.Read(stream);
        var script = ReadScript(stream, info.Length);

        return new Actor
        {
            Position = Vec2.FromRawPosition(info.Position),
            Step = Vec2.FromRawVector(info.Step),
            Cycle = info.Cycle,
            Parameters = ReadParameters(info.Parameters),
            Follower = info.Follower,
            Leader = info.Leader,
            Under = info.Under.ToTile(),
            Instruction = info.Instruction,
            Length = info.Length > 0 ? script.Length : info.Length,
            Script = script
        };
    }

    private static Actor ReadSuperZztActor(Stream stream)
    {
        var info = SuperZztActor.Read(stream);
        var script = ReadScript(stream, info.Length);

        return new Actor
        {
            Position = Vec2.FromRawPosition(info.Position),
            Step = Vec2.FromRawVector(info.Step),
            Cycle = info.Cycle,
            Parameters = ReadParameters(info.Parameters),
            Follower = info.Follower,
            Leader = info.Leader,
            Under = info.Under.ToTile(),
            Instruction = info.Instruction,
            Length = info.Length > 0 ? script.Length : info.Length,
            Script = script
        };
    }

    public static void Write(Stream stream, short worldType, Board board)
    {
        if (board.Actors.Count < 1)
            throw new Exception("At least 1 actor is required on a board.");

        switch (worldType)
        {
            case -1:
                WriteZztBoard(stream, board);
                break;
            case -2:
                WriteSuperZztBoard(stream, board);
                break;
            default:
                throw new Exception($"Unknown world type {worldType}.");
        }
    }

    private static byte[] WriteExits(int[] exits)
    {
        var result = new byte[exits.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = (byte)exits[i];
        return result;
    }

    private static RawTile[] WriteTiles(Tile[] tiles)
    {
        var result = new RawTile[tiles.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = RawTile.FromTile(tiles[i]);
        return result;
    }

    private static void WriteZztBoard(Stream stream, Board board)
    {
        var dataStream = new MemoryStream();

        new ZztBoardHeader
        {
            Name = board.Name
        }.Write(dataStream);

        Rle.Pack(dataStream, WriteTiles(board.Tiles));

        new ZztBoardInfo
        {
            MaxShots = (byte)board.MaxShots,
            DarkBit = board.IsDark ? (byte)1 : (byte)0,
            Exits = WriteExits(board.Exits),
            RestartOnZapBit = board.RestartOnZap ? (byte)1 : (byte)0,
            Message = board.Message,
            Enter = board.Enter.ToRawPosition(),
            TimeLimit = (short)board.TimeLimit.TotalSeconds,
            ActorCount = (short)(board.Actors.Count - 1)
        }.Write(dataStream);

        foreach (var actor in board.Actors)
            WriteZztActor(dataStream, actor);

        Span<byte> sizeBuf = stackalloc byte[2];
        BinaryPrimitives.WriteInt16LittleEndian(sizeBuf, (short)dataStream.Length);
        stream.Write(sizeBuf);
        dataStream.Position = 0;
        dataStream.CopyTo(stream);
    }

    private static void WriteSuperZztBoard(Stream stream, Board board)
    {
        var dataStream = new MemoryStream();

        new SuperZztBoardHeader
        {
            Name = board.Name
        }.Write(dataStream);

        Rle.Pack(dataStream, WriteTiles(board.Tiles));

        new SuperZztBoardInfo
        {
            MaxShots = (byte)board.MaxShots,
            Exits = WriteExits(board.Exits),
            RestartOnZapBit = board.RestartOnZap ? (byte)1 : (byte)0,
            Enter = board.Enter.ToRawPosition(),
            TimeLimit = (short)board.TimeLimit.TotalSeconds,
            ActorCount = (short)(board.Actors.Count - 1),
            Camera = board.Camera.ToRawVector()
        }.Write(dataStream);

        foreach (var actor in board.Actors)
            WriteZztActor(dataStream, actor);

        Span<byte> sizeBuf = stackalloc byte[2];
        BinaryPrimitives.WriteInt16LittleEndian(sizeBuf, (short)dataStream.Length);
        stream.Write(sizeBuf);
        dataStream.Position = 0;
        dataStream.CopyTo(stream);
    }

    private static byte[] WriteParameters(int[] parameters)
    {
        var result = new byte[parameters.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = (byte)parameters[i];
        return result;
    }

    private static void WriteScript(Stream stream, int length, ReadOnlySpan<char> script)
    {
        if (length < 1 || script.Length < 1)
            return;

        length = CodePage437.Encoding.GetByteCount(script);
        Span<byte> buf = stackalloc byte[length];
        CodePage437.Encoding.GetBytes(script, buf);
        stream.Write(buf);
    }

    private static void WriteZztActor(Stream stream, Actor actor)
    {
        var info = new ZztActor
        {
            Position = actor.Position.ToRawPosition(),
            Step = actor.Step.ToRawVector(),
            Cycle = (short)actor.Cycle,
            Parameters = WriteParameters(actor.Parameters),
            Follower = (short)actor.Follower,
            Leader = (short)actor.Leader,
            Under = RawTile.FromTile(actor.Under),
            Instruction = (short)actor.Instruction,
            Length = (short)actor.Length
        };

        info.Write(stream);
        WriteScript(stream, info.Length, actor.Script);
    }

    private static void WriteSuperZztActor(Stream stream, Actor actor)
    {
        var info = new ZztActor
        {
            Position = actor.Position.ToRawPosition(),
            Step = actor.Step.ToRawVector(),
            Cycle = (short)actor.Cycle,
            Parameters = WriteParameters(actor.Parameters),
            Follower = (short)actor.Follower,
            Leader = (short)actor.Leader,
            Under = RawTile.FromTile(actor.Under),
            Instruction = (short)actor.Instruction,
            Length = (short)actor.Length
        };

        info.Write(stream);
        WriteScript(stream, info.Length, actor.Script);
    }
}