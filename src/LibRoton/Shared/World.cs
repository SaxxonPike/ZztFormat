namespace LibRoton.Shared;

public class World
{
    public WorldType Type { get; set; }
    public Player Player { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public List<string> Flags { get; set; } = [];
    public bool IsLocked { get; set; }
    public Memory<byte> ExtraData { get; set; }

    public int FlagLimit { get; set; }
    public int FlagMaxLength { get; set; }

    public List<Memory<byte>> PackedBoards { get; set; } = [];
    public IBoardPacker? BoardPacker { get; set; }
    public IWorldPacker? WorldPacker { get; set; }
}