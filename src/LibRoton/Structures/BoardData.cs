namespace LibRoton.Structures;

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