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