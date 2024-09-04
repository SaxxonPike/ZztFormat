namespace LibRoton.Structures;

public class World
{
    public int Type { get; set; }
    public int Ammo { get; set; }
    public int Gems { get; set; }
    public HashSet<KeyColor> Keys { get; set; } = [];
    public int Health { get; set; }
    public int StartBoard { get; set; }
    public int Torches { get; set; }
    public int TorchCycles { get; set; }
    public int EnergyCycles { get; set; }
    public int Score { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Flags { get; set; } = [];
    public TimeSpan TimePassed { get; set; }
    public bool Locked { get; set; }
    public int Stones { get; set; }
    public List<Board> Boards { get; set; }

    public override string ToString() => Name;
}