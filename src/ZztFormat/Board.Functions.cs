using System.Buffers;
using static System.Buffers.Binary.BinaryPrimitives;

namespace ZztFormat;

public partial class Board
{
    private const int ZztBoardWidth = 60;
    private const int ZztBoardHeight = 25;
    private const int ZztBoardTileCount = ZztBoardWidth * ZztBoardHeight;
    private const int ZztActorCount = 151;
    private const int SuperZztBoardWidth = 96;
    private const int SuperZztBoardHeight = 80;
    private const int SuperZztBoardTileCount = SuperZztBoardWidth * SuperZztBoardHeight;
    private const int SuperZztActorCount = 129;

    public Tile this[int x, int y]
    {
        get => Tiles[(y - 1) * Width + (x - 1)];
        set => Tiles[(y - 1) * Width + (x - 1)] = value;
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

    public static Board Create(WorldType worldType) =>
        worldType switch
        {
            WorldType.Zzt => CreateZztBoard(),
            WorldType.SuperZzt => CreateSuperZztBoard(),
            _ => throw new LibRotonException(
                $"Unknown world type {worldType}.")
        };

    internal static Board CreateZztBoard()
    {
        var center = new Vec2(
            ZztBoardWidth / 2 + 1,
            ZztBoardHeight / 2 + 1);

        var player = new Actor
        {
            Position = center,
            Cycle = 1
        };

        return new Board
        {
            Tiles = new Tile[ZztBoardTileCount],
            Enter = center,
            Size = new Vec2(ZztBoardWidth, ZztBoardHeight),
            Actors = [player],
            [center.X, center.Y] = new()
            {
                Element = 0x04,
                Color = 0x1F
            }
        };
    }

    internal static Board CreateSuperZztBoard()
    {
        var center = new Vec2(
            SuperZztBoardWidth / 2 + 1,
            SuperZztBoardHeight / 2 + 1);

        var player = new Actor
        {
            Position = center,
            Cycle = 1
        };

        return new Board
        {
            Tiles = new Tile[SuperZztBoardTileCount],
            Enter = center,
            Size = new Vec2(SuperZztBoardWidth, SuperZztBoardHeight),
            Actors = [player],
            [center.X, center.Y] = new()
            {
                Element = 0x04,
                Color = 0x1F
            }
        };
    }

    public static Board Read(Stream stream, WorldType worldType, ReadOptions options = default)
    {
        byte[]? boardBuf = default;

        try
        {
            Span<byte> sizeBuf = stackalloc byte[2];
            stream.ReadExactly(sizeBuf);
            var size = ReadInt16LittleEndian(sizeBuf);

            boardBuf = ArrayPool<byte>.Shared.Rent(size);
            stream.ReadExactly(boardBuf.AsSpan(0, size));
            using var dataStream = new MemoryStream(boardBuf);

            var board = worldType switch
            {
                WorldType.Zzt => ReadZztBoard(dataStream),
                WorldType.SuperZzt => ReadSuperZztBoard(dataStream),
                _ => throw new LibRotonException(
                    $"Unknown world type {worldType}.")
            };

            if (board == null)
            {
                if (options.HasFlag(ReadOptions.ThrowOnCorruptBoards))
                    throw new LibRotonException(
                        "A corrupt board was found.");

                if (!options.HasFlag(ReadOptions.KeepCorruptBoards))
                {
                    return Create(worldType);
                }
                
                // The board data needs to be duplicated in order to be
                // able to free the temporary board memory. We still grab
                // the board name when available, because it is a fixed offset
                // from the beginning of the data.

                return new Board
                {
                    Name = worldType switch
                    {
                        WorldType.Zzt => boardBuf.Length >= ZztBoardHeader.Size
                            ? ZztBoardHeader.Read(boardBuf).Name
                            : string.Empty,
                        WorldType.SuperZzt => boardBuf.Length >= SuperZztBoardHeader.Size
                            ? SuperZztBoardHeader.Read(boardBuf).Name
                            : string.Empty,
                        _ => throw new LibRotonException(
                            $"Unknown world type {worldType}.")
                    },
                    Extra = boardBuf.AsSpan(0, size).ToArray()
                };
            }

            return board;
        }
        finally
        {
            if (boardBuf != null)
                ArrayPool<byte>.Shared.Return(boardBuf);
        }
    }

    internal static Dictionary<ExitDirection, int> ConvertExits(ReadOnlySpan<byte> exits)
    {
        var result = new Dictionary<ExitDirection, int>();
        for (var i = 0; i < exits.Length; i++)
            result[(ExitDirection)i] = exits[i];
        return result;
    }

    internal static Tile[] ConvertTiles(ReadOnlySpan<RawTile> tiles)
    {
        var result = new Tile[tiles.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = tiles[i].ToTile();
        return result;
    }

    internal static Board? ReadZztBoard(Stream stream)
    {
        Span<RawTile> tiles = stackalloc RawTile[ZztBoardTileCount];
        var header = ZztBoardHeader.Read(stream);

        if (!Rle.Unpack(stream, tiles))
            return null;

        var info = ZztBoardInfo.Read(stream);

        var actors = new List<Actor>();
        for (var i = 0; i <= info.ActorCount; i++)
            actors.Add(ReadZztActor(stream));

        return new Board
        {
            Size = new Vec2(ZztBoardWidth, ZztBoardHeight),
            Name = header.Name,
            Tiles = ConvertTiles(tiles),
            MaxShots = info.MaxShots,
            IsDark = info.DarkBit != 0,
            Exits = ConvertExits(info.Exits),
            RestartOnZap = info.RestartOnZapBit != 0,
            Message = info.Message,
            Enter = Vec2.FromRawPosition(info.Enter),
            TimeLimit = TimeSpan.FromSeconds(info.TimeLimit),
            Actors = actors
        };
    }

    internal static Board? ReadSuperZztBoard(Stream stream)
    {
        Span<RawTile> tiles = stackalloc RawTile[SuperZztBoardTileCount];
        var header = SuperZztBoardHeader.Read(stream);

        if (!Rle.Unpack(stream, tiles))
            return null;

        var info = SuperZztBoardInfo.Read(stream);

        var actors = new List<Actor>();
        for (var i = 0; i <= info.ActorCount; i++)
            actors.Add(ReadSuperZztActor(stream));

        return new Board
        {
            Size = new Vec2(SuperZztBoardWidth, SuperZztBoardHeight),
            Name = header.Name,
            Tiles = ConvertTiles(tiles),
            MaxShots = info.MaxShots,
            Exits = ConvertExits(info.Exits),
            RestartOnZap = info.RestartOnZapBit != 0,
            Enter = Vec2.FromRawPosition(info.Enter),
            TimeLimit = TimeSpan.FromSeconds(info.TimeLimit),
            Camera = Vec2.FromRawVector(info.Camera),
            Actors = actors
        };
    }

    internal static char[] ConvertScript(Stream stream, int length)
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

    internal static int[] ConvertParameters(ReadOnlySpan<byte> parameters)
    {
        var result = new int[parameters.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = parameters[i];
        return result;
    }

    internal static Actor ReadZztActor(Stream stream)
    {
        var info = ZztActor.Read(stream);
        var script = ConvertScript(stream, info.Length);

        return new Actor
        {
            Position = Vec2.FromRawPosition(info.Position),
            Step = Vec2.FromRawVector(info.Step),
            Cycle = info.Cycle,
            Parameters = ConvertParameters(info.Parameters),
            Follower = info.Follower,
            Leader = info.Leader,
            Under = info.Under.ToTile(),
            Instruction = info.Instruction,
            Script = script
        };
    }

    internal static Actor ReadSuperZztActor(Stream stream)
    {
        var info = SuperZztActor.Read(stream);
        var script = ConvertScript(stream, info.Length);

        return new Actor
        {
            Position = Vec2.FromRawPosition(info.Position),
            Step = Vec2.FromRawVector(info.Step),
            Cycle = info.Cycle,
            Parameters = ConvertParameters(info.Parameters),
            Follower = info.Follower,
            Leader = info.Leader,
            Under = info.Under.ToTile(),
            Instruction = info.Instruction,
            Script = script
        };
    }

    public static void Write(Stream stream, WorldType worldType, Board board)
    {
        if (board.Actors.Count < 1)
            throw new LibRotonException(
                "At least 1 actor is required on a board.");

        switch (worldType)
        {
            case WorldType.Zzt:
                WriteZztBoard(stream, board);
                break;
            case WorldType.SuperZzt:
                WriteSuperZztBoard(stream, board);
                break;
            default:
                throw new LibRotonException(
                    $"Unknown world type {worldType}.");
        }
    }

    internal static byte[] ConvertExits(IReadOnlyDictionary<ExitDirection, int> exits, int length)
    {
        var result = new byte[length];
        foreach (var exit in exits)
            result[(int)exit.Key] = (byte)exit.Value;
        return result;
    }

    internal static RawTile[] ConvertTiles(ReadOnlySpan<Tile> tiles)
    {
        var result = new RawTile[tiles.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = RawTile.FromTile(tiles[i]);
        return result;
    }

    internal static Dictionary<Actor, int> ConvertActorBindings(IEnumerable<Actor> actors)
    {
        var index = 0;
        var scripts = new Dictionary<char[], int>();
        var result = new Dictionary<Actor, int>();

        foreach (var actor in actors)
        {
            if (actor.Script.Length > 0)
            {
                scripts.TryAdd(actor.Script, index);
                result.TryAdd(actor, scripts[actor.Script]);
            }
            else
            {
                result.TryAdd(actor, 0);
            }
            
            index++;
        }

        return result;
    }

    internal static void WriteZztBoard(Stream stream, Board board)
    {
        var dataStream = new MemoryStream();

        new ZztBoardHeader
        {
            Name = board.Name
        }.Write(dataStream);

        Rle.Pack(dataStream, ConvertTiles(board.Tiles));

        new ZztBoardInfo
        {
            MaxShots = (byte)board.MaxShots,
            DarkBit = board.IsDark ? (byte)1 : (byte)0,
            Exits = ConvertExits(board.Exits, 4),
            RestartOnZapBit = board.RestartOnZap ? (byte)1 : (byte)0,
            Message = board.Message,
            Enter = board.Enter.ToRawPosition(),
            TimeLimit = (short)board.TimeLimit.TotalSeconds,
            ActorCount = (short)(board.Actors.Count - 1)
        }.Write(dataStream);

        var binds = ConvertActorBindings(board.Actors);

        foreach (var actor in board.Actors)
            WriteZztActor(dataStream, binds[actor], actor);

        Span<byte> sizeBuf = stackalloc byte[2];
        WriteInt16LittleEndian(sizeBuf, (short)dataStream.Length);
        stream.Write(sizeBuf);
        dataStream.Position = 0;
        dataStream.CopyTo(stream);
    }

    internal static void WriteSuperZztBoard(Stream stream, Board board)
    {
        var dataStream = new MemoryStream();

        new SuperZztBoardHeader
        {
            Name = board.Name
        }.Write(dataStream);

        Rle.Pack(dataStream, ConvertTiles(board.Tiles));

        new SuperZztBoardInfo
        {
            MaxShots = (byte)board.MaxShots,
            Exits = ConvertExits(board.Exits, 4),
            RestartOnZapBit = board.RestartOnZap ? (byte)1 : (byte)0,
            Enter = board.Enter.ToRawPosition(),
            TimeLimit = (short)board.TimeLimit.TotalSeconds,
            ActorCount = (short)(board.Actors.Count - 1),
            Camera = board.Camera.ToRawVector()
        }.Write(dataStream);

        var binds = ConvertActorBindings(board.Actors);

        foreach (var actor in board.Actors)
            WriteZztActor(dataStream, binds[actor], actor);

        Span<byte> sizeBuf = stackalloc byte[2];
        WriteInt16LittleEndian(sizeBuf, (short)dataStream.Length);
        stream.Write(sizeBuf);
        dataStream.Position = 0;
        dataStream.CopyTo(stream);
    }

    internal static byte[] ConvertParameters(ReadOnlySpan<int> parameters)
    {
        var result = new byte[parameters.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = (byte)parameters[i];
        return result;
    }

    internal static void ConvertScript(Stream stream, ReadOnlySpan<char> script)
    {
        // var length = CodePage437.Encoding.GetByteCount(script);
        Span<byte> buf = stackalloc byte[script.Length];
        CodePage437.Encoding.GetBytes(script, buf);
        stream.Write(buf);
        // return length;
    }

    internal static void WriteZztActor(Stream stream, int bind, Actor actor)
    {
        var info = new ZztActor
        {
            Position = actor.Position.ToRawPosition(),
            Step = actor.Step.ToRawVector(),
            Cycle = (short)actor.Cycle,
            Parameters = ConvertParameters(actor.Parameters),
            Follower = (short)actor.Follower,
            Leader = (short)actor.Leader,
            Under = RawTile.FromTile(actor.Under),
            Instruction = (short)actor.Instruction,
            Length = bind > 0 ? (short)-bind : (short)actor.Script.Length
        };

        info.Write(stream);
        if (info.Length > 0)
            ConvertScript(stream, actor.Script);
    }

    internal static void WriteSuperZztActor(Stream stream, int bind, Actor actor)
    {
        var info = new ZztActor
        {
            Position = actor.Position.ToRawPosition(),
            Step = actor.Step.ToRawVector(),
            Cycle = (short)actor.Cycle,
            Parameters = ConvertParameters(actor.Parameters),
            Follower = (short)actor.Follower,
            Leader = (short)actor.Leader,
            Under = RawTile.FromTile(actor.Under),
            Instruction = (short)actor.Instruction,
            Length = bind > 0 ? (short)-bind : (short)actor.Script.Length
        };

        info.Write(stream);
        if (info.Length > 0)
            ConvertScript(stream, actor.Script);
    }
}