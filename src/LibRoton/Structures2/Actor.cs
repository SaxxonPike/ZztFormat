using System.Buffers;
using System.Buffers.Binary;

namespace LibRoton.Structures;

public interface IWorldHeader
{
    short BoardCount { get; set; }
    short Ammo { get; set; }
    short Gems { get; set; }
    byte[] Keys { get; }
    short Health { get; set; }
    short Board { get; set; }
    short Torches { get; set; }
    short TorchCycles { get; set; }
    short EnergyCycles { get; set; }
    short Score { get; set; }
    string Name { get; set; }
    Flag[] Flags { get; }
    DosTime TimePassed { get; set; }
    byte Locked { get; set; }
    short Stones { get; set; }
}

public partial class ZztWorldHeader
{
    short IWorldHeader.Stones
    {
        get => default;
        set { }
    }
}

public partial class SuperZztWorldHeader
{
    short IWorldHeader.Torches
    {
        get => default;
        set { }
    }

    short IWorldHeader.TorchCycles
    {
        get => default;
        set { }
    }
}

public class WorldData2
{
    public int Type { get; set; }
    public int Ammo { get; set; }
    public int Gems { get; set; }
    public HashSet<int> Keys { get; set; } = [];
    public int Health { get; set; }
    public int Board { get; set; }
    public int Torches { get; set; }
    public int TorchCycles { get; set; }
    public int EnergyCycles { get; set; }
    public int Score { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Flags { get; set; } = [];
    public TimeSpan TimePassed { get; set; }
    public bool Locked { get; set; }
    public int Stones { get; set; }
    public List<BoardData2> Boards { get; set; } = [];

    public static WorldData2 Read(Stream stream)
    {
        Span<byte> typeBuf = stackalloc byte[2];
        stream.ReadExactly(typeBuf);
        var type = BinaryPrimitives.ReadInt16LittleEndian(typeBuf);

        return type switch
        {
            -1 => ReadZztWorld(stream),
            -2 => ReadSuperZztWorld(stream),
            _ => throw new Exception($"Unknown world type {type}.")
        };
    }

    private static HashSet<int> ReadKeys(byte[] keys)
    {
        var result = new HashSet<int>();
        for (var i = 0; i < keys.Length; i++)
            if (keys[i] != 0)
                result.Add(i);
        return result;
    }

    private static List<string> ReadFlags(Flag[] flags)
    {
        var result = new List<string>();
        for (var i = 0; i < flags.Length; i++)
            if (flags[i].Length > 0)
                result.Add(flags[i].Text);
        return result;
    }

    private static WorldData2 ReadZztWorld(Stream stream)
    {
        var header = ZztWorldHeader.Read(stream);

        var boards = new List<BoardData2>();
        for (var i = 0; i <= header.BoardCount; i++)
            boards.Add(BoardData2.Read(stream, -1));

        return new WorldData2
        {
            Type = -1,
            Ammo = header.Ammo,
            Gems = header.Gems,
            Keys = ReadKeys(header.Keys),
            Health = header.Health,
            Board = header.Board,
            Torches = header.Torches,
            TorchCycles = header.TorchCycles,
            EnergyCycles = header.EnergyCycles,
            Score = header.Score,
            Name = header.Name,
            Flags = ReadFlags(header.Flags),
            TimePassed = header.TimePassed.ToTimeSpan(),
            Locked = header.Locked != 0,
            Boards = boards
        };
    }

    private static WorldData2 ReadSuperZztWorld(Stream stream)
    {
        var header = SuperZztWorldHeader.Read(stream);

        var boards = new List<BoardData2>();
        for (var i = 0; i <= header.BoardCount; i++)
            boards.Add(BoardData2.Read(stream, -2));

        return new WorldData2
        {
            Type = -2,
            Ammo = header.Ammo,
            Gems = header.Gems,
            Keys = ReadKeys(header.Keys),
            Health = header.Health,
            Board = header.Board,
            EnergyCycles = header.EnergyCycles,
            Score = header.Score,
            Name = header.Name,
            Flags = ReadFlags(header.Flags),
            TimePassed = header.TimePassed.ToTimeSpan(),
            Locked = header.Locked != 0,
            Stones = header.Stones,
            Boards = boards
        };
    }

    private static byte[] WriteKeys(HashSet<int> keys, int length)
    {
        var result = new byte[length];
        foreach (var key in keys)
            result[key] = 1;
        return result;
    }

    private static Flag[] WriteFlags(List<string> flags, int length)
    {
        var result = new Flag[length];
        var index = 0;
        foreach (var flag in flags)
            result[index++].Text = flag;
        return result;
    }

    public void Write(Stream stream, WorldData2 world)
    {
        if (world.Boards.Count < 1)
            throw new Exception("A minimum of 1 board is required.");

        switch (world.Type)
        {
            case -1:
                WriteZztWorld(stream, world);
                break;
            case -2:
                WriteSuperZztWorld(stream, world);
                break;
            default:
                throw new Exception($"Unknown world type {world.Type}.");
        }
    }

    private void WriteZztWorld(Stream stream, WorldData2 world)
    {
        new ZztWorldHeader
        {
            BoardCount = (short)(world.Boards.Count - 1),
            Ammo = (short)world.Ammo,
            Gems = (short)world.Gems,
            Keys = WriteKeys(world.Keys, 7),
            Health = (short)world.Health,
            Board = (short)world.Board,
            Torches = (short)world.Torches,
            TorchCycles = (short)world.TorchCycles,
            EnergyCycles = (short)world.EnergyCycles,
            Score = (short)world.Score,
            Name = world.Name,
            Flags = WriteFlags(world.Flags, 10),
            TimePassed = DosTime.FromTimeSpan(world.TimePassed),
            Locked = world.Locked ? (byte)1 : (byte)0
        }.Write(stream);

        foreach (var board in world.Boards)
            BoardData2.Write(stream, -1, board);
    }

    private void WriteSuperZztWorld(Stream stream, WorldData2 world)
    {
        new SuperZztWorldHeader
        {
            BoardCount = (short)(world.Boards.Count - 1),
            Ammo = (short)world.Ammo,
            Gems = (short)world.Gems,
            Keys = WriteKeys(world.Keys, 7),
            Health = (short)world.Health,
            Board = (short)world.Board,
            Score = (short)world.Score,
            EnergyCycles = (short)world.EnergyCycles,
            Name = world.Name,
            Flags = WriteFlags(world.Flags, 16),
            TimePassed = DosTime.FromTimeSpan(world.TimePassed),
            Locked = world.Locked ? (byte)1 : (byte)0,
            Stones = (short)world.Stones
        }.Write(stream);

        foreach (var board in world.Boards)
            BoardData2.Write(stream, -2, board);
    }
}

public interface IBoardHeader
{
    string Name { get; set; }
}

public interface IBoardInfo
{
    byte MaxShots { get; set; }
    byte DarkBit { get; set; }
    byte[] Exits { get; }
    byte RestartOnZapBit { get; set; }
    byte MessageLength { get; set; }
    byte[] MessageBytes { get; }
    RawPosition Enter { get; set; }
    short TimeLimit { get; set; }
    short ActorCount { get; set; }
    RawVector Camera { get; set; }
}

public partial class ZztBoardInfo
{
    RawVector IBoardInfo.Camera
    {
        get => default;
        set { }
    }
}

public partial class SuperZztBoardInfo
{
    byte IBoardInfo.DarkBit
    {
        get => default;
        set { }
    }

    byte[] IBoardInfo.MessageBytes =>
        Array.Empty<byte>();

    byte IBoardInfo.MessageLength
    {
        get => default;
        set { }
    }
}

public class BoardData2
{
    public string Name { get; set; } = string.Empty;
    public RawTile[] TileData { get; set; } = [];
    public int MaxShots { get; set; }
    public bool IsDark { get; set; }
    public int[] Exits { get; set; } = [];
    public bool RestartOnZap { get; set; }
    public string Message { get; set; } = string.Empty;
    public Vec2 Enter { get; set; }
    public TimeSpan TimeLimit { get; set; }
    public Vec2 Camera { get; set; }
    public List<ActorData2> Actors { get; set; } = [];

    public Vec2 Size { get; set; }
    public bool IsCorrupt { get; set; }

    public RawTile this[int x, int y]
    {
        get => TileData[y * Width + x];
        set => TileData[y * Width + x] = value;
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

    public static BoardData2 Read(Stream stream, short worldType)
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

    private static BoardData2 ReadZztBoard(Stream stream)
    {
        var header = ZztBoardHeader.Read(stream);

        try
        {
            var tiles = new RawTile[60 * 25];
            RleCompression.Unpack(stream, tiles);

            var info = ZztBoardInfo.Read(stream);

            var actors = new List<ActorData2>();
            for (var i = 0; i <= info.ActorCount; i++)
                actors.Add(ReadZztActor(stream));

            return new BoardData2
            {
                Width = 60,
                Height = 25,
                Name = header.Name,
                TileData = tiles,
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
            return new BoardData2
            {
                IsCorrupt = true,
                Name = header.Name
            };
        }
    }

    private static BoardData2 ReadSuperZztBoard(Stream stream)
    {
        var header = SuperZztBoardHeader.Read(stream);

        try
        {
            var tiles = new RawTile[96 * 80];
            RleCompression.Unpack(stream, tiles);

            var info = SuperZztBoardInfo.Read(stream);

            var actors = new List<ActorData2>();
            for (var i = 0; i <= info.ActorCount; i++)
                actors.Add(ReadSuperZztActor(stream));

            return new BoardData2
            {
                Width = 96,
                Height = 80,
                Name = header.Name,
                TileData = tiles,
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
            return new BoardData2
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

    private static ActorData2 ReadZztActor(Stream stream)
    {
        var info = ZztActor.Read(stream);
        var script = ReadScript(stream, info.Length);

        return new ActorData2
        {
            Position = Vec2.FromRawPosition(info.Position),
            Step = Vec2.FromRawVector(info.Step),
            Cycle = info.Cycle,
            Parameters = ReadParameters(info.Parameters),
            Follower = info.Follower,
            Leader = info.Leader,
            Under = info.Under,
            Instruction = info.Instruction,
            Length = info.Length > 0 ? script.Length : info.Length,
            Script = script
        };
    }

    private static ActorData2 ReadSuperZztActor(Stream stream)
    {
        var info = SuperZztActor.Read(stream);
        var script = ReadScript(stream, info.Length);

        return new ActorData2
        {
            Position = Vec2.FromRawPosition(info.Position),
            Step = Vec2.FromRawVector(info.Step),
            Cycle = info.Cycle,
            Parameters = ReadParameters(info.Parameters),
            Follower = info.Follower,
            Leader = info.Leader,
            Under = info.Under,
            Instruction = info.Instruction,
            Length = info.Length > 0 ? script.Length : info.Length,
            Script = script
        };
    }
    
    public static void Write(Stream stream, short worldType, BoardData2 board)
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

    private static void WriteZztBoard(Stream stream, BoardData2 board)
    {
        var dataStream = new MemoryStream();
        
        new ZztBoardHeader
        {
            Name = board.Name
        }.Write(dataStream);
        
        RleCompression.Pack(dataStream, board.TileData);
        
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

    private static void WriteSuperZztBoard(Stream stream, BoardData2 board)
    {
        var dataStream = new MemoryStream();
        
        new SuperZztBoardHeader
        {
            Name = board.Name
        }.Write(dataStream);
        
        RleCompression.Pack(dataStream, board.TileData);
        
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

    private static void WriteZztActor(Stream stream, ActorData2 actor)
    {
        var info = new ZztActor
        {
            Position = actor.Position.ToRawPosition(),
            Step = actor.Step.ToRawVector(),
            Cycle = (short)actor.Cycle,
            Parameters = WriteParameters(actor.Parameters),
            Follower = (short)actor.Follower,
            Leader = (short)actor.Leader,
            Under = actor.Under,
            Instruction = (short)actor.Instruction,
            Length = (short)actor.Length
        };
        
        info.Write(stream);
        WriteScript(stream, info.Length, actor.Script);
    }

    private static void WriteSuperZztActor(Stream stream, ActorData2 actor)
    {
        var info = new ZztActor
        {
            Position = actor.Position.ToRawPosition(),
            Step = actor.Step.ToRawVector(),
            Cycle = (short)actor.Cycle,
            Parameters = WriteParameters(actor.Parameters),
            Follower = (short)actor.Follower,
            Leader = (short)actor.Leader,
            Under = actor.Under,
            Instruction = (short)actor.Instruction,
            Length = (short)actor.Length
        };
        
        info.Write(stream);
        WriteScript(stream, info.Length, actor.Script);
    }
}

public interface IActorInfo
{
    RawPosition Position { get; set; }
    RawVector Step { get; set; }
    short Cycle { get; set; }
    byte[] Parameters { get; init; }
    short Follower { get; set; }
    short Leader { get; set; }
    RawTile Under { get; set; }
    short Instruction { get; set; }
    short Length { get; set; }
}

public class ActorData2
{
    public Vec2 Position { get; set; }
    public Vec2 Step { get; set; }
    public int Cycle { get; set; }
    public int[] Parameters { get; init; } = new int[3];
    public int Follower { get; set; }
    public int Leader { get; set; }
    public RawTile Under { get; set; }
    public int Instruction { get; set; }
    public int Length { get; set; }
    public char[] Script { get; set; } = [];

    public bool IsBound => Length < 0;

    public void Bind(int index)
    {
        Length = -index;
        Script = [];
    }

    public void Unbind()
    {
        if (Length < 0)
            return;

        Length = 0;
        Script = [];
    }
}

public interface IElementInfo
{
    byte Character { get; set; }
    byte Color { get; set; }
    byte DestructibleBit { get; set; }
    byte PushableBit { get; set; }
    byte AlwaysVisibleBit { get; set; }
    byte EditorFloorBit { get; set; }
    byte FloorBit { get; set; }
    byte DrawFuncBit { get; set; }
    int DrawFunc { get; set; }
    short Cycle { get; set; }
    int ActFunc { get; set; }
    int InteractFunc { get; set; }
    short Menu { get; set; }
    byte MenuKey { get; set; }
    string Name { get; set; }
    string EditorCategoryText { get; set; }
    string EditorP1Text { get; set; }
    string EditorP2Text { get; set; }
    string EditorP3Text { get; set; }
    string EditorBoardText { get; set; }
    string EditorStepText { get; set; }
    string EditorCodeText { get; set; }
    short Score { get; set; }
}

public partial class SuperZztElementProperties
{
    byte IElementInfo.AlwaysVisibleBit
    {
        get => default;
        set { }
    }
}
