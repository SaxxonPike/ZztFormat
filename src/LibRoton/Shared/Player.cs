namespace LibRoton.Shared;

public class Player
{
    public int Ammo { get; set; }
    public int Gems { get; set; }
    public HashSet<KeyColor> Keys { get; set; } = [];
    public int Health { get; set; }
    public int Board { get; set; }
    public int Torches { get; set; }
    public int TorchCycles { get; set; }
    public int EnergyCycles { get; set; }
    public int Score { get; set; }
    public TimeSpan TimePassed { get; set; }
    public int Stones { get; set; }
}