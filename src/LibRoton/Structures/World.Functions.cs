using System.Buffers.Binary;

namespace LibRoton.Structures;

public partial class World
{
    public static World Read(Stream stream)
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

    private static HashSet<int> ReadKeys(ReadOnlySpan<byte> keys)
    {
        var result = new HashSet<int>();
        for (var i = 0; i < keys.Length; i++)
            if (keys[i] != 0)
                result.Add(i);
        return result;
    }

    private static List<string> ReadFlags(ReadOnlySpan<Flag> flags)
    {
        var result = new List<string>();
        for (var i = 0; i < flags.Length; i++)
            if (flags[i].Length > 0)
                result.Add(flags[i].Text);
        return result;
    }

    private static World ReadZztWorld(Stream stream)
    {
        var header = ZztWorldHeader.Read(stream);

        var boards = new List<Board>();
        for (var i = 0; i <= header.BoardCount; i++)
            boards.Add(Structures.Board.Read(stream, -1));

        return new World
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

    private static World ReadSuperZztWorld(Stream stream)
    {
        var header = SuperZztWorldHeader.Read(stream);

        var boards = new List<Board>();
        for (var i = 0; i <= header.BoardCount; i++)
            boards.Add(Structures.Board.Read(stream, -2));

        return new World
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

    public static void Write(Stream stream, World world)
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

    private static void WriteZztWorld(Stream stream, World world)
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
            TimePassed = Time.FromTimeSpan(world.TimePassed),
            Locked = world.Locked ? (byte)1 : (byte)0
        }.Write(stream);

        foreach (var board in world.Boards)
            Structures.Board.Write(stream, -1, board);
    }

    private static void WriteSuperZztWorld(Stream stream, World world)
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
            TimePassed = Time.FromTimeSpan(world.TimePassed),
            Locked = world.Locked ? (byte)1 : (byte)0,
            Stones = (short)world.Stones
        }.Write(stream);

        foreach (var board in world.Boards)
            Structures.Board.Write(stream, -2, board);
    }
}