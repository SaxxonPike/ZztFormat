using System.Buffers;
using static System.Buffers.Binary.BinaryPrimitives;

namespace ZztFormat;

public partial class Board(WorldType worldType)
{
    private const int ZztBoardWidth = 60;
    private const int ZztBoardHeight = 25;
    private const int ZztBoardTileCount = ZztBoardWidth * ZztBoardHeight;
    private const int ZztActorCount = 151;
    private const int SuperZztBoardWidth = 96;
    private const int SuperZztBoardHeight = 80;
    private const int SuperZztBoardTileCount = SuperZztBoardWidth * SuperZztBoardHeight;
    private const int SuperZztActorCount = 129;

    /// <summary>
    /// World type associated with this board.
    /// </summary>
    public WorldType WorldType { get; } = worldType;

    /// <summary>
    /// Gets or sets the tile at the specified coordinates. These coordinates match
    /// in-game coordinates: the upper-left corner is (X = 1, Y = 1).
    /// </summary>
    /// <param name="x">
    /// X coordinate of the tile.
    /// </param>
    /// <param name="y">
    /// Y coordinate of the tile.
    /// </param>
    public Tile this[int x, int y]
    {
        get => Tiles[(y - 1) * Width + (x - 1)];
        set => Tiles[(y - 1) * Width + (x - 1)] = value;
    }

    /// <summary>
    /// Gets or sets the width of the board. This also assigns the
    /// <see cref="Size"/> property.
    /// </summary>
    public int Width
    {
        get => Size.X;
        set => Size = Size with { X = value };
    }

    /// <summary>
    /// Gets or sets the height of the board. This also assigns the
    /// <see cref="Size"/> property.
    /// </summary>
    public int Height
    {
        get => Size.Y;
        set => Size = Size with { Y = value };
    }

    /// <summary>
    /// Creates a new board with the specified world type, with sensible
    /// defaults: empty board with one Tile and Actor for the player.
    /// </summary>
    /// <param name="worldType">
    /// World type to create the board with. This impacts board size and
    /// other initialization.
    /// </param>
    /// <returns>
    /// The created board.
    /// </returns>
    /// <exception cref="ZztFormatException">
    /// The world type is not valid.
    /// </exception>
    /// <remarks>
    /// A blank <see cref="World"/> is created to be the parent.
    /// </remarks>
    public static Board Create(WorldType worldType) =>
        worldType switch
        {
            WorldType.Zzt => CreateZztBoard(),
            WorldType.SuperZzt => CreateSuperZztBoard(),
            _ => throw new ZztFormatException(
                $"Unknown world type {worldType}.")
        };

    /// <summary>
    /// Creates a default board for ZZT.
    /// </summary>
    internal static Board CreateZztBoard()
    {
        var center = new Vec2(
            ZztBoardWidth / 2 + 1,
            ZztBoardHeight / 2 + 1);

        var board = new Board(WorldType.Zzt)
        {
            Tiles = new Tile[ZztBoardTileCount],
            Enter = center,
            Size = new Vec2(ZztBoardWidth, ZztBoardHeight),
            [center.X, center.Y] = new()
            {
                Element = 0x04,
                Color = 0x1F
            }
        };

        var player = new Actor(WorldType.Zzt)
        {
            Position = center,
            Cycle = 1
        };

        board.Actors.Add(player);
        return board;
    }

    /// <summary>
    /// Creates a default board for Super ZZT.
    /// </summary>
    internal static Board CreateSuperZztBoard()
    {
        var center = new Vec2(
            SuperZztBoardWidth / 2 + 1,
            SuperZztBoardHeight / 2 + 1);

        var board = new Board(WorldType.SuperZzt)
        {
            Tiles = new Tile[SuperZztBoardTileCount],
            Enter = center,
            Size = new Vec2(SuperZztBoardWidth, SuperZztBoardHeight),
            [center.X, center.Y] = new()
            {
                Element = 0x04,
                Color = 0x1F
            }
        };

        var player = new Actor(WorldType.SuperZzt)
        {
            Position = center,
            Cycle = 1
        };

        board.Actors.Add(player);
        return board;
    }

    /// <summary>
    /// Reads a board from a <see cref="Stream"/>.
    /// </summary>
    /// <param name="stream">
    /// Stream where the board data will be read from.
    /// </param>
    /// <param name="worldType">
    /// World type associated with the board.
    /// </param>
    /// <param name="options">
    /// Additional loading options.
    /// </param>
    /// <returns>
    /// The loaded board.
    /// </returns>
    /// <exception cref="ZztFormatException">
    /// The world type is invalid, or a corrupt board was found with the
    /// <see cref="ReadOptions.ThrowOnCorruptBoards"/> option specified.
    /// </exception>
    /// <remarks>
    /// A blank <see cref="World"/> is created to be the parent.
    /// </remarks>
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
                _ => throw new ZztFormatException(
                    $"Unknown world type {worldType}.")
            };

            if (board == null)
            {
                if (options.HasFlag(ReadOptions.ThrowOnCorruptBoards))
                    throw new ZztFormatException(
                        "A corrupt board was found.");

                if (!options.HasFlag(ReadOptions.KeepCorruptBoards))
                {
                    board = Create(worldType);
                    return board;
                }

                // The board data needs to be duplicated in order to be
                // able to free the temporary board memory. We still grab
                // the board name when available, because it is a fixed offset
                // from the beginning of the data.

                board = new Board(worldType)
                {
                    Name = worldType switch
                    {
                        WorldType.Zzt => boardBuf.Length >= ZztBoardHeader.Size
                            ? ZztBoardHeader.Read(boardBuf).Name
                            : string.Empty,
                        WorldType.SuperZzt => boardBuf.Length >= SuperZztBoardHeader.Size
                            ? SuperZztBoardHeader.Read(boardBuf).Name
                            : string.Empty,
                        _ => throw new ZztFormatException(
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

    /// <summary>
    /// Deserialize exit bytes.
    /// </summary>
    internal static Dictionary<ExitDirection, int> ConvertExits(ReadOnlySpan<byte> exits)
    {
        var result = new Dictionary<ExitDirection, int>();
        for (var i = 0; i < exits.Length; i++)
            result[(ExitDirection)i] = exits[i];
        return result;
    }

    /// <summary>
    /// Deserialize tiles.
    /// </summary>
    internal static Tile[] ConvertTiles(ReadOnlySpan<RawTile> tiles)
    {
        var result = new Tile[tiles.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = tiles[i].ToTile();
        return result;
    }

    /// <summary>
    /// Read a ZZT board.
    /// </summary>
    /// <param name="stream">
    /// Stream from which the board data will be read.
    /// </param>
    /// <returns>
    /// The loaded board.
    /// </returns>
    internal static Board? ReadZztBoard(Stream stream)
    {
        Span<RawTile> tiles = stackalloc RawTile[ZztBoardTileCount];
        var header = ZztBoardHeader.Read(stream);

        if (!Rle.Unpack(stream, tiles))
            return null;

        var info = ZztBoardInfo.Read(stream);

        var board = new Board(WorldType.Zzt)
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
            TimeLimit = TimeSpan.FromSeconds(info.TimeLimit)
        };

        for (var i = 0; i <= info.ActorCount; i++)
            board.Actors.Add(ReadZztActor(stream));

        return board;
    }

    /// <summary>
    /// Read a Super ZZT board.
    /// </summary>
    /// <param name="stream">
    /// Stream from which the board data will be read.
    /// </param>
    /// <returns>
    /// The loaded board.
    /// </returns>
    internal static Board? ReadSuperZztBoard(Stream stream)
    {
        Span<RawTile> tiles = stackalloc RawTile[SuperZztBoardTileCount];
        var header = SuperZztBoardHeader.Read(stream);

        if (!Rle.Unpack(stream, tiles))
            return null;

        var info = SuperZztBoardInfo.Read(stream);

        var board = new Board(WorldType.SuperZzt)
        {
            Size = new Vec2(SuperZztBoardWidth, SuperZztBoardHeight),
            Name = header.Name,
            Tiles = ConvertTiles(tiles),
            MaxShots = info.MaxShots,
            Exits = ConvertExits(info.Exits),
            RestartOnZap = info.RestartOnZapBit != 0,
            Enter = Vec2.FromRawPosition(info.Enter),
            TimeLimit = TimeSpan.FromSeconds(info.TimeLimit),
            Camera = Vec2.FromRawVector(info.Camera)
        };

        for (var i = 0; i <= info.ActorCount; i++)
            board.Actors.Add(ReadSuperZztActor(stream));

        return board;
    }

    /// <summary>
    /// Deserialize Actor script.
    /// </summary>
    /// <param name="stream">
    /// Stream from which the script data will be read.
    /// </param>
    /// <param name="length">
    /// Length value from the Actor record.
    /// </param>
    /// <returns>
    /// Char[] representation of the script.
    /// </returns>
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

    /// <summary>
    /// Deserialize P1/P2/P3.
    /// </summary>
    internal static int[] ConvertParameters(ReadOnlySpan<byte> parameters)
    {
        var result = new int[parameters.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = parameters[i];
        return result;
    }

    /// <summary>
    /// Reads an actor from a <see cref="Stream"/> in the ZZT format.
    /// </summary>
    /// <param name="stream">
    /// Stream from which the actor data will be read.
    /// </param>
    /// <returns>
    /// The loaded actor.
    /// </returns>
    internal static Actor ReadZztActor(Stream stream)
    {
        var info = ZztActor.Read(stream);
        var script = ConvertScript(stream, info.Length);

        return new Actor(WorldType.Zzt)
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

    /// <summary>
    /// Reads an actor from a <see cref="Stream"/> in the Super ZZT format.
    /// </summary>
    /// <param name="stream">
    /// Stream from which the actor data will be read.
    /// </param>
    /// <returns>
    /// The loaded actor.
    /// </returns>
    internal static Actor ReadSuperZztActor(Stream stream)
    {
        var info = SuperZztActor.Read(stream);
        var script = ConvertScript(stream, info.Length);

        return new Actor(WorldType.SuperZzt)
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

    /// <summary>
    /// Writes a board to a <see cref="Stream"/>.
    /// </summary>
    /// <param name="stream">
    /// Stream to which the board data will be written.
    /// </param>
    /// <param name="worldType">
    /// World type associated with the board data.
    /// </param>
    /// <param name="board">
    /// Board data to write.
    /// </param>
    /// <exception cref="ZztFormatException">
    /// World type is invalid, or the actor list does not contain the minimum
    /// required number of actors (one.)
    /// </exception>
    public static void Write(Stream stream, WorldType worldType, Board board)
    {
        if (board.Actors.Count < 1)
            throw new ZztFormatException(
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
                throw new ZztFormatException(
                    $"Unknown world type {worldType}.");
        }
    }

    /// <summary>
    /// Serialize board exits.
    /// </summary>
    internal static byte[] ConvertExits(IReadOnlyDictionary<ExitDirection, int> exits, int length)
    {
        var result = new byte[length];
        foreach (var exit in exits)
            result[(int)exit.Key] = (byte)exit.Value;
        return result;
    }

    /// <summary>
    /// Serialize tiles.
    /// </summary>
    internal static RawTile[] ConvertTiles(ReadOnlySpan<Tile> tiles)
    {
        var result = new RawTile[tiles.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = RawTile.FromTile(tiles[i]);
        return result;
    }

    /// <summary>
    /// Determine actor script bindings.
    /// </summary>
    /// <param name="actors">
    /// Actors for which to find binding values for.
    /// </param>
    /// <returns>
    /// A dictionary that maps an actor to its bound actor index.
    /// </returns>
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

    /// <summary>
    /// Writes a board to a <see cref="Stream"/> in ZZT format.
    /// </summary>
    /// <param name="stream">
    /// Stream to which the board data will be written.
    /// </param>
    /// <param name="board">
    /// Board data to write.
    /// </param>
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

    /// <summary>
    /// Writes a board to a <see cref="Stream"/> in Super ZZT format.
    /// </summary>
    /// <param name="stream">
    /// Stream to which the board data will be written.
    /// </param>
    /// <param name="board">
    /// Board data to write.
    /// </param>
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

    /// <summary>
    /// Serialize P1/P2/P3.
    /// </summary>
    internal static byte[] ConvertParameters(ReadOnlySpan<int> parameters)
    {
        var result = new byte[parameters.Length];
        for (var i = 0; i < result.Length; i++)
            result[i] = (byte)parameters[i];
        return result;
    }

    /// <summary>
    /// Serialize actor script.
    /// </summary>
    /// <param name="stream">
    /// Stream to which the script data will be written.
    /// </param>
    /// <param name="script">
    /// Script data to write.
    /// </param>
    internal static void ConvertScript(Stream stream, ReadOnlySpan<char> script)
    {
        // var length = CodePage437.Encoding.GetByteCount(script);
        Span<byte> buf = stackalloc byte[script.Length];
        CodePage437.Encoding.GetBytes(script, buf);
        stream.Write(buf);
    }

    /// <summary>
    /// Write an actor to a <see cref="Stream"/> in ZZT format.
    /// </summary>
    /// <param name="stream">
    /// Stream to which the actor data will be written.
    /// </param>
    /// <param name="bind">
    /// Index of the actor to bind to.
    /// </param>
    /// <param name="actor">
    /// Actor that will be written.
    /// </param>
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

    /// <summary>
    /// Write an actor to a <see cref="Stream"/> in Super ZZT format.
    /// </summary>
    /// <param name="stream">
    /// Stream to which the actor data will be written.
    /// </param>
    /// <param name="bind">
    /// Index of the actor to bind to.
    /// </param>
    /// <param name="actor">
    /// Actor that will be written.
    /// </param>
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

    /// <summary>
    /// Binds an actor's script to another actor.
    /// </summary>
    /// <param name="binder">
    /// Actor that will be bound to another.
    /// </param>
    /// <param name="owner">
    /// Actor that has the script to bind to.
    /// </param>
    /// <exception cref="ZztFormatException">
    /// The actor was not found in <see cref="Actors"/>.
    /// </exception>
    public void BindScript(Actor binder, Actor owner)
    {
        var ownerIndex = Actors.IndexOf(owner);
        if (ownerIndex < 0)
            throw new ZztFormatException("Owner actor was not found in the actor list.");

        binder.Bind = -ownerIndex;
        binder.Script = owner.Script;
    }

    /// <summary>
    /// Sets two actors' leader/follower values accordingly.
    /// </summary>
    /// <param name="leader">
    /// Actor that will receive the new <see cref="Actor.Follower"/> value.
    /// </param>
    /// <param name="follower">
    /// Actor that will receive the new <see cref="Actor.Leader"/> value.
    /// </param>
    /// <exception cref="ZztFormatException">
    /// The actor was not found in <see cref="Actors"/>.
    /// </exception>
    public void SetLeaderFollower(Actor leader, Actor follower)
    {
        var leaderIndex = Actors.IndexOf(leader);
        if (leaderIndex < 0)
            throw new ZztFormatException("Leader actor was not found in the actor list.");

        var followerIndex = Actors.IndexOf(follower);
        if (followerIndex < 0)
            throw new ZztFormatException("Follower actor was not found in the actor list.");

        leader.Follower = followerIndex;
        follower.Leader = leaderIndex;
    }

    /// <summary>
    /// After adding a board, fix board exit and passage links.
    /// </summary>
    /// <param name="addedIndex">
    /// Board index of the board that was added.
    /// </param>
    /// <param name="worldType">
    /// World type associated with the board.
    /// </param>
    internal void FixRefsAfterAdd(int addedIndex)
    {
        // Fix board links.

        var exitsToChange = new Dictionary<ExitDirection, int>();

        foreach (var (direction, target) in Exits)
            if (target >= addedIndex)
                exitsToChange.Add(direction, target + 1);

        foreach (var (direction, newTarget) in exitsToChange)
            Exits[direction] = newTarget;

        // Fix passage links.

        var passageId = WorldType switch
        {
            WorldType.Zzt => ElementList.UnmapZztElement(ElementType.Passage),
            WorldType.SuperZzt => ElementList.UnmapSuperZztElement(ElementType.Passage),
            _ => -1
        };

        if (passageId < 0)
            return;

        for (var i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i].Element != passageId)
                continue;

            var x = i % Width + 1;
            var y = i / Width + 1;

            foreach (var actor in Actors)
            {
                if (actor.Position.X != x || actor.Position.Y != y)
                    continue;

                var p2 = actor.Parameters[2];
                if (p2 >= addedIndex)
                    actor.Parameters[2] = p2 + 1;
            }
        }
    }

    /// <summary>
    /// After removing a board, fix board exit and passage links.
    /// </summary>
    /// <param name="removedIndex">
    /// Board index of the board that was removed.
    /// </param>
    internal void FixRefsBeforeRemove(int removedIndex)
    {
        // Fix board links.

        var exitsToChange = new Dictionary<ExitDirection, int>();

        foreach (var (direction, target) in Exits)
            if (target > removedIndex)
                exitsToChange.Add(direction, target - 1);
            else if (target == removedIndex)
                exitsToChange.Add(direction, -1);

        foreach (var (direction, newTarget) in exitsToChange)
            if (newTarget >= 0)
                Exits[direction] = newTarget;
            else
                Exits.Remove(direction);

        // Fix passage links.

        var passageId = WorldType switch
        {
            WorldType.Zzt => ElementList.UnmapZztElement(ElementType.Passage),
            WorldType.SuperZzt => ElementList.UnmapSuperZztElement(ElementType.Passage),
            _ => -1
        };

        if (passageId < 0)
            return;

        for (var i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i].Element != passageId)
                continue;

            var x = i % Width + 1;
            var y = i / Width + 1;

            foreach (var actor in Actors)
            {
                if (actor.Position.X != x || actor.Position.Y != y)
                    continue;

                var p2 = actor.Parameters[2];
                if (p2 > removedIndex)
                    actor.Parameters[2] = p2 - 1;
                else if (p2 == removedIndex)
                    actor.Parameters[2] = 0;
            }
        }
    }
}