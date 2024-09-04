namespace LibRoton.Structures;

internal static class Mapper
{
    private static List<string> ToFlagsList(Flag[] flags) =>
        flags
            .Where(x => x.Length > 0)
            .Select(x => x.Text)
            .ToList();

    private static Flag[] FromFlagsList(List<string> flags, int count)
    {
        var result = new Flag[count];
        var i = 0;
        foreach (var flag in flags)
            result[i++] = new Flag
            {
                Text = flag
            };
        return result;
    }

    private static HashSet<KeyColor> ToKeyColorHashSet(byte[] keys)
    {
        var result = new HashSet<KeyColor>();
        for (var i = 0; i < keys.Length; i++)
            if (keys[i] != 0)
                result.Add((KeyColor)i);
        return result;
    }

    private static byte[] FromKeyColorHashSet(HashSet<KeyColor> keys, int count)
    {
        var result = new byte[count];
        foreach (var key in keys)
            result[(int)key] = 1;
        return result;
    }

    private static Tile[] ToTiles(RawTile[] tiles, Func<byte, Element> convertElement)
    {
        var result = new Tile[tiles.Length];
        for (var i = 0; i < tiles.Length; i++)
            result[i] = Tile.FromRawTile(tiles[i], convertElement);
        return result;
    }

    private static RawTile[] FromTiles(Tile[] tiles, Func<Element, byte> convertElement)
    {
        var result = new RawTile[tiles.Length];
        for (var i = 0; i < tiles.Length; i++)
            result[i] = tiles[i].ToRawTile(convertElement);
        return result;
    }

    private static int[] ToParameters(byte[] parameters) =>
        parameters.Select(x => (int)x).ToArray();

    private static byte[] FromParameters(int[] parameters) =>
        parameters.Select(x => (byte)x).ToArray();

    public static WorldData FromZzt(ZztWorldHeader header) =>
        new()
        {
            Type = header.Type,
            BoardCount = header.BoardCount,
            Ammo = header.Ammo,
            Gems = header.Gems,
            Keys = ToKeyColorHashSet(header.Keys),
            Health = header.Health,
            Board = header.Board,
            Torches = header.Torches,
            TorchCycles = header.TorchCycles,
            EnergyCycles = header.EnergyCycles,
            Score = header.Score,
            Name = header.Name,
            Flags = ToFlagsList(header.Flags),
            TimePassed = header.TimePassed,
            Locked = header.Locked
        };

    public static BoardData FromZzt(ZztBoardHeader header, ZztBoardInfo info) =>
        new()
        {
            Name = header.Name,
            MaxShots = info.MaxShots,
            DarkBit = info.DarkBit,
            Exits = info.Exits,
            RestartOnZapBit = info.RestartOnZapBit,
            Message = info.Message,
            Enter = info.Enter,
            TimeLimit = info.TimeLimit,
            ActorCount = info.ActorCount
        };

    public static ActorData FromZzt(ZztActor actor, byte[] script) =>
        new()
        {
            Position = actor.Position,
            Step = actor.Step,
            Cycle = actor.Cycle,
            Parameters = actor.Parameters,
            Follower = actor.Follower,
            Leader = actor.Leader,
            Under = actor.Under,
            Instruction = actor.Instruction,
            Length = actor.Length,
            Script = script
        };

    public static ZztWorldHeader ToZzt(WorldData data) =>
        new()
        {
            Type = data.Type,
            BoardCount = data.BoardCount,
            Ammo = data.Ammo,
            Gems = data.Gems,
            Keys = FromKeyColorHashSet(data.Keys, 7),
            Health = data.Health,
            Board = data.Board,
            Torches = data.Torches,
            TorchCycles = data.TorchCycles,
            EnergyCycles = data.EnergyCycles,
            Score = data.Score,
            Name = data.Name,
            Flags = FromFlagsList(data.Flags, 10),
            TimePassed = data.TimePassed,
            Locked = data.Locked
        };

    public static (ZztBoardHeader Header, ZztBoardInfo Info) ToZzt(BoardData data) =>
        (new ZztBoardHeader
        {
            Name = data.Name
        }, new ZztBoardInfo
        {
            MaxShots = data.MaxShots,
            DarkBit = data.DarkBit,
            Exits = data.Exits,
            RestartOnZapBit = data.RestartOnZapBit,
            Message = data.Message,
            Enter = data.Enter,
            TimeLimit = data.TimeLimit,
            ActorCount = data.ActorCount
        });

    public static ZztActor ToZzt(ActorData data) =>
        new()
        {
            Position = data.Position,
            Step = data.Step,
            Cycle = data.Cycle,
            Parameters = data.Parameters,
            Follower = data.Follower,
            Leader = data.Leader,
            Under = data.Under,
            Instruction = data.Instruction,
            Length = data.Length
        };

    public static WorldData FromSuperZzt(SuperZztWorldHeader header) =>
        new()
        {
            Type = header.Type,
            BoardCount = header.BoardCount,
            Ammo = header.Ammo,
            Gems = header.Gems,
            Keys = ToKeyColorHashSet(header.Keys),
            Health = header.Health,
            Board = header.Board,
            EnergyCycles = header.EnergyCycles,
            Score = header.Score,
            Name = header.Name,
            Flags = ToFlagsList(header.Flags),
            TimePassed = header.TimePassed,
            Locked = header.Locked,
            Stones = header.Stones
        };

    public static BoardData FromSuperZzt(SuperZztBoardHeader header, SuperZztBoardInfo info) =>
        new()
        {
            Name = header.Name,
            MaxShots = info.MaxShots,
            Exits = info.Exits,
            RestartOnZapBit = info.RestartOnZapBit,
            Enter = info.Enter,
            TimeLimit = info.TimeLimit,
            ActorCount = info.ActorCount,
            Camera = info.Camera
        };

    public static ActorData FromSuperZzt(SuperZztActor actor, byte[] script) =>
        new()
        {
            Position = actor.Position,
            Step = actor.Step,
            Cycle = actor.Cycle,
            Parameters = actor.Parameters,
            Follower = actor.Follower,
            Leader = actor.Leader,
            Under = actor.Under,
            Instruction = actor.Instruction,
            Length = actor.Length,
            Script = script
        };

    public static SuperZztWorldHeader ToSuperZzt(WorldData data) =>
        new()
        {
            Type = data.Type,
            BoardCount = data.BoardCount,
            Ammo = data.Ammo,
            Gems = data.Gems,
            Keys = FromKeyColorHashSet(data.Keys, 7),
            Health = data.Health,
            Board = data.Board,
            EnergyCycles = data.EnergyCycles,
            Score = data.Score,
            Name = data.Name,
            Flags = FromFlagsList(data.Flags, 16),
            TimePassed = data.TimePassed,
            Locked = data.Locked,
            Stones = data.Stones
        };

    public static (SuperZztBoardHeader Header, SuperZztBoardInfo Info) ToSuperZzt(BoardData data) =>
        (new SuperZztBoardHeader
        {
            Name = data.Name
        }, new SuperZztBoardInfo
        {
            MaxShots = data.MaxShots,
            Exits = data.Exits,
            RestartOnZapBit = data.RestartOnZapBit,
            Enter = data.Enter,
            TimeLimit = data.TimeLimit,
            ActorCount = data.ActorCount,
            Camera = data.Camera
        });

    public static SuperZztActor ToSuperZzt(ActorData data) =>
        new()
        {
            Position = data.Position,
            Step = data.Step,
            Cycle = data.Cycle,
            Parameters = data.Parameters,
            Follower = data.Follower,
            Leader = data.Leader,
            Under = data.Under,
            Instruction = data.Instruction,
            Length = data.Length
        };

    public static World FromCommon(WorldData world, List<(BoardData Board, List<ActorData> Actors)> boards,
        Func<byte, Element> convertElement) =>
        new()
        {
            Type = world.Type,
            Ammo = world.Ammo,
            Gems = world.Gems,
            Health = world.Health,
            StartBoard = world.Board,
            Torches = world.Torches,
            TorchCycles = world.TorchCycles,
            EnergyCycles = world.EnergyCycles,
            Score = world.Score,
            Name = world.Name,
            Flags = world.Flags,
            TimePassed = world.TimePassed.ToTimeSpan(),
            Locked = world.Locked != 0,
            Boards = boards.Select(x => FromCommon(x.Board, x.Actors, convertElement)).ToList()
        };

    public static Board FromCommon(BoardData board, List<ActorData> actors, Func<byte, Element> convertElement)
    {
        var (resultActors, resultScripts) = FromCommon(actors, convertElement);

        var result = new Board
        {
            Name = board.Name,
            Tiles = ToTiles(board.Tiles, convertElement),
            MaxShots = 0,
            IsDark = false,
            Exits = Exits.FromBytes(board.Exits),
            RestartOnZap = false,
            Message = board.Message,
            Enter = Vec2.FromRawPosition(board.Enter),
            TimeLimit = TimeSpan.FromSeconds(board.TimeLimit),
            Camera = Vec2.FromRawVector(board.Camera),
            Actors = resultActors,
            Scripts = resultScripts
        };

        return result;
    }

    private static (List<Actor> Actors, List<string> Scripts) FromCommon(List<ActorData> data,
        Func<byte, Element> convertElement)
    {
        var actors = new List<Actor>();
        var scripts = new List<string>();

        for (var i = 0; i < data.Count; i++)
        {
            var source = data[i];
            var scriptIndex = -1;

            if (source.Script is { Length: > 0 } script)
            {
                scriptIndex = scripts.Count;
                scripts.Add(CodePage437.Encoding.GetString(script));
            }
            else if (source.Length < 0)
            {
                scriptIndex = source.Length;
            }

            actors.Add(new Actor
            {
                Position = Vec2.FromRawPosition(source.Position),
                Step = Vec2.FromRawVector(source.Step),
                Cycle = source.Cycle,
                Parameters = ToParameters(source.Parameters),
                Follower = source.Follower,
                Leader = source.Leader,
                Under = Tile.FromRawTile(source.Under, convertElement),
                Instruction = source.Instruction,
                Script = scriptIndex
            });
        }

        // Handle #bind.
        foreach (var actor in actors)
        {
            if (actor.Script < 0)
                actor.Script = actors[-actor.Script].Script;
        }

        return (actors, scripts);
    }

    public static (WorldData World, List<(BoardData Board, List<ActorData> Actors)> Boards) ToCommon(World world,
        Func<Element, byte> convertElement) =>
    (
        World: new WorldData
        {
            Type = (short)world.Type,
            BoardCount = (short)(world.Boards.Count - 1),
            Ammo = (short)world.Ammo,
            Gems = (short)world.Gems,
            Keys = world.Keys,
            Health = (short)world.Health,
            Board = (short)world.StartBoard,
            Torches = (short)world.Torches,
            TorchCycles = (short)world.TorchCycles,
            EnergyCycles = (short)world.EnergyCycles,
            Score = (short)world.Score,
            Name = world.Name,
            Flags = world.Flags,
            TimePassed = DosTime.FromTimeSpan(world.TimePassed),
            Locked = world.Locked ? (byte)1 : (byte)0,
            Stones = (short)world.Stones
        },
        Boards: world.Boards.Select(x => ToCommon(x, convertElement)).ToList()
    );

    public static (BoardData Board, List<ActorData> Actors) ToCommon(Board board, Func<Element, byte> convertElement) =>
    (
        Board: new BoardData
        {
            Name = board.Name,
            MaxShots = (byte)board.MaxShots,
            DarkBit = board.IsDark ? (byte)1 : (byte)0,
            Exits = board.Exits.ToBytes(),
            RestartOnZapBit = board.RestartOnZap ? (byte)1 : (byte)0,
            Message = board.Message,
            Enter = board.Enter.ToRawPosition(),
            TimeLimit = (short)board.TimeLimit.TotalSeconds,
            ActorCount = (short)(board.Actors.Count - 1),
            Camera = board.Camera.ToRawVector(),
            Width = board.Width,
            Height = board.Height,
            MaxActorCount = board.MaxActorCount,
            Tiles = FromTiles(board.Tiles, convertElement)
        },
        Actors: ToCommon(board.Actors, board.Scripts, convertElement)
    );

    public static List<ActorData> ToCommon(List<Actor> actors, List<string> scripts, Func<Element, byte> convertElement)
    {
        var scriptMap = new Dictionary<int, int>();
        var result = new List<ActorData>();
        var actorIndex = 0;

        foreach (var actor in actors)
        {
            var length = 0;
            var script = string.Empty;

            if (actor.Script >= 0)
            {
                var scriptIndex = actor.Script;
                if (scriptMap.TryGetValue(scriptIndex, out var scriptTarget))
                {
                    length = -scriptTarget;
                }
                else
                {
                    script = scripts[scriptIndex];
                    length = script.Length;
                    scriptMap[scriptIndex] = actorIndex;
                }
            }

            result.Add(new ActorData
            {
                Position = actor.Position.ToRawPosition(),
                Step = actor.Step.ToRawVector(),
                Cycle = (short)actor.Cycle,
                Parameters = FromParameters(actor.Parameters),
                Follower = (short)actor.Follower,
                Leader = (short)actor.Leader,
                Under = actor.Under.ToRawTile(convertElement),
                Instruction = (short)actor.Instruction,
                Length = (short)length,
                Script = !string.IsNullOrEmpty(script) ? CodePage437.Encoding.GetBytes(script) : []
            });
        }

        return result;
    }
}