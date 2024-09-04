namespace LibRoton.Structures;

public class WorldData
{
    public short Type { get; set; }
    public short BoardCount { get; set; }
    public short Ammo { get; set; }
    public short Gems { get; set; }
    public HashSet<KeyColor> Keys { get; init; } = [];
    public short Health { get; set; }
    public short Board { get; set; }
    public short Torches { get; set; }
    public short TorchCycles { get; set; }
    public short EnergyCycles { get; set; }
    public short Score { get; set; }
    public string Name { get; set; }
    public List<string> Flags { get; init; } = [];
    public DosTime TimePassed { get; set; }
    public byte Locked { get; set; }
    public short Stones { get; set; }
}

public class BoardData
{
    public string Name { get; set; }
    public byte MaxShots { get; set; }
    public byte DarkBit { get; set; }
    public byte[] Exits { get; init; } = new byte[4];
    public byte RestartOnZapBit { get; set; }
    public string Message { get; set; }
    public RawPosition Enter { get; set; }
    public short TimeLimit { get; set; }
    public short ActorCount { get; set; }
    public RawVector Camera { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int MaxActorCount { get; set; }
    public RawTile[] Tiles { get; init; } = [];
}

public class ActorData
{
    public RawPosition Position { get; set; }
    public RawVector Step { get; set; }
    public short Cycle { get; set; }
    public byte[] Parameters { get; init; } = new byte[3];
    public short Follower { get; set; }
    public short Leader { get; set; }
    public RawTile Under { get; set; }
    public short Instruction { get; set; }
    public short Length { get; set; }
    public byte[] Script { get; set; } = [];
}