using System.Buffers.Binary;

namespace ZztFormat;

public partial class World(WorldType worldType)
{
    /// <summary>
    /// World type ID. This will be -1 for ZZT worlds, and -2 for Super ZZT
    /// worlds. By default, this will be <see cref="WorldType.Zzt"/>.
    /// </summary>
    public WorldType Type { get; } = worldType;

    /// <summary>
    /// Creates a blank <see cref="World"/> with one title screen
    /// <see cref="Board"/> and typical defaults.
    /// </summary>
    /// <param name="type">
    /// Type of world to create.
    /// </param>
    /// <returns></returns>
    public static World Create(WorldType type)
    {
        var world = type switch
        {
            WorldType.Zzt => CreateZztWorld(),
            WorldType.SuperZzt => CreateSuperZztWorld(),
            _ => throw new ZztFormatException(
                $"Unknown world type {type}.")
        };

        world.Boards[0].Name = "Title Screen";
        return world;
    }

    /// <summary>
    /// Create a blank ZZT world.
    /// </summary>
    internal static World CreateZztWorld()
    {
        var world = new World(WorldType.Zzt);
        world.Boards.Add(Board.Create(WorldType.Zzt));
        return world;
    }

    /// <summary>
    /// Create a blank Super ZZT world.
    /// </summary>
    internal static World CreateSuperZztWorld()
    {
        var world = new World(WorldType.SuperZzt);
        world.Boards.Add(Board.Create(WorldType.SuperZzt));
        return world;
    }

    /// <summary>
    /// Reads a <see cref="World"/> from a <see cref="Stream"/>.
    /// </summary>
    /// <param name="stream">
    /// The stream that the world data will be loaded from.
    /// </param>
    /// <param name="options">
    /// Optional flags that alter the load process.
    /// </param>
    /// <returns>
    /// World that was loaded from the stream.
    /// </returns>
    /// <exception cref="ZztFormatException">
    /// Thrown if the world file is unrecognized or corrupt, of if there were
    /// problems writing to the stream.
    /// </exception>
    public static World Read(Stream stream, ReadOptions options = default)
    {
        // First two bytes of a world file are used to identify its type.

        Span<byte> typeBuf = stackalloc byte[2];
        stream.ReadExactly(typeBuf);
        var type = BinaryPrimitives.ReadInt16LittleEndian(typeBuf);

        return type switch
        {
            -1 => ReadZztWorld(stream, options),
            -2 => ReadSuperZztWorld(stream, options),
            _ => throw new ZztFormatException(
                $"Unknown world type {type}.")
        };
    }

    /// <summary>
    /// Deserializes world keys.
    /// </summary>
    internal static HashSet<KeyColor> ConvertKeys(ReadOnlySpan<byte> keys)
    {
        var result = new HashSet<KeyColor>();
        for (var i = 0; i < keys.Length; i++)
            if (keys[i] != 0)
                result.Add((KeyColor)i);
        return result;
    }

    /// <summary>
    /// Deserializes world flags.
    /// </summary>
    internal static FlagList ConvertFlags(ReadOnlySpan<Flag> flags, ReadOptions options)
    {
        var result = new FlagList();

        if (options.HasFlag(ReadOptions.CaseSensitiveFlags))
            result.Options |= FlagOptions.CaseSensitive;

        if (options.HasFlag(ReadOptions.PreserveFlagCase))
            result.Options |= FlagOptions.PreserveCase;

        if (options.HasFlag(ReadOptions.AllowDuplicateFlags))
            result.Options |= FlagOptions.AllowDuplicates;

        for (var i = 0; i < flags.Length; i++)
            if (flags[i].Length > 0)
                result.Add(flags[i].Text);
        return result;
    }

    /// <summary>
    /// Deserializes a ZZT world.
    /// The world type should already have been read before calling this.
    /// </summary>
    internal static World ReadZztWorld(Stream stream, ReadOptions options)
    {
        var header = ZztWorldHeader.Read(stream);

        var world = new World(WorldType.Zzt)
        {
            Ammo = header.Ammo,
            Gems = header.Gems,
            Keys = ConvertKeys(header.Keys),
            Health = header.Health,
            StartBoard = header.Board,
            Torches = header.Torches,
            TorchCycles = header.TorchCycles,
            EnergyCycles = header.EnergyCycles,
            Score = header.Score,
            Name = header.Name,
            Flags = ConvertFlags(header.Flags, options),
            TimePassed = header.TimePassed.ToTimeSpan(),
            Locked = header.Locked != 0
        };

        for (var i = 0; i <= header.BoardCount; i++)
            world.Boards.Add(Board.Read(stream, WorldType.Zzt));

        return world;
    }

    /// <summary>
    /// Deserializes a Super ZZT world.
    /// The world type should already have been read before calling this.
    /// </summary>
    internal static World ReadSuperZztWorld(Stream stream, ReadOptions options)
    {
        var header = SuperZztWorldHeader.Read(stream);

        var world = new World(WorldType.SuperZzt)
        {
            Ammo = header.Ammo,
            Gems = header.Gems,
            Keys = ConvertKeys(header.Keys),
            Health = header.Health,
            StartBoard = header.Board,
            EnergyCycles = header.EnergyCycles,
            Score = header.Score,
            Name = header.Name,
            Flags = ConvertFlags(header.Flags, options),
            TimePassed = header.TimePassed.ToTimeSpan(),
            Locked = header.Locked != 0,
            Stones = header.Stones
        };

        for (var i = 0; i <= header.BoardCount; i++)
            world.Boards.Add(Board.Read(stream, WorldType.SuperZzt));

        return world;
    }

    /// <summary>
    /// Serialize world keys.
    /// </summary>
    internal static byte[] ConvertKeys(HashSet<KeyColor> keys, int length)
    {
        var result = new byte[length];
        foreach (var key in keys)
            result[(int)key] = 1;
        return result;
    }

    /// <summary>
    /// Serialize world flags.
    /// </summary>
    internal static Flag[] ConvertFlags(FlagList flags, int length)
    {
        var result = new Flag[length];
        for (var i = 0; i < length; i++)
            result[i] = new Flag();

        var index = 0;
        foreach (var flag in flags)
            result[index++].Text = flag;

        return result;
    }

    /// <summary>
    /// Writes a <see cref="World"/> to a <see cref="Stream"/>.
    /// </summary>
    /// <param name="stream">
    /// Stream to which the data will be written.
    /// </param>
    /// <param name="world">
    /// World data to write.
    /// </param>
    /// <exception cref="ZztFormatException">
    /// Thrown if there are no boards in the world data, if the world type is
    /// unrecognized, or if there were problems writing to the stream.
    /// </exception>
    public static void Write(Stream stream, World world)
    {
        if (world.Boards.Count < 1)
            throw new ZztFormatException(
                "A minimum of 1 board is required.");

        Span<byte> lenBuf = stackalloc byte[2];
        BinaryPrimitives.WriteInt16LittleEndian(lenBuf, (short)world.Type);
        stream.Write(lenBuf);

        switch (world.Type)
        {
            case WorldType.Zzt:
                WriteZztWorld(stream, world);
                break;
            case WorldType.SuperZzt:
                WriteSuperZztWorld(stream, world);
                break;
            default:
                throw new ZztFormatException(
                    $"Unknown world type {world.Type}.");
        }
    }

    /// <summary>
    /// Serialize a ZZT world.
    /// The world type should already have been written before calling this.
    /// </summary>
    internal static void WriteZztWorld(Stream stream, World world)
    {
        new ZztWorldHeader
        {
            BoardCount = (short)(world.Boards.Count - 1),
            Ammo = (short)world.Ammo,
            Gems = (short)world.Gems,
            Keys = ConvertKeys(world.Keys, 7),
            Health = (short)world.Health,
            Board = (short)world.StartBoard,
            Torches = (short)world.Torches,
            TorchCycles = (short)world.TorchCycles,
            EnergyCycles = (short)world.EnergyCycles,
            Score = (short)world.Score,
            Name = world.Name,
            Flags = ConvertFlags(world.Flags, 10),
            TimePassed = Time.FromTimeSpan(world.TimePassed),
            Locked = world.Locked ? (byte)1 : (byte)0
        }.Write(stream);

        foreach (var board in world.Boards)
            Board.Write(stream, WorldType.Zzt, board);
    }

    /// <summary>
    /// Serialize a Super ZZT world.
    /// The world type should already have been written before calling this.
    /// </summary>
    internal static void WriteSuperZztWorld(Stream stream, World world)
    {
        new SuperZztWorldHeader
        {
            BoardCount = (short)(world.Boards.Count - 1),
            Ammo = (short)world.Ammo,
            Gems = (short)world.Gems,
            Keys = ConvertKeys(world.Keys, 7),
            Health = (short)world.Health,
            Board = (short)world.StartBoard,
            Score = (short)world.Score,
            EnergyCycles = (short)world.EnergyCycles,
            Name = world.Name,
            Flags = ConvertFlags(world.Flags, 16),
            TimePassed = Time.FromTimeSpan(world.TimePassed),
            Locked = world.Locked ? (byte)1 : (byte)0,
            Stones = (short)world.Stones
        }.Write(stream);

        foreach (var board in world.Boards)
            Board.Write(stream, WorldType.SuperZzt, board);
    }
}