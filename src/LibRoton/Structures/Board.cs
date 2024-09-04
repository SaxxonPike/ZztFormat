namespace LibRoton.Structures;

public class Board
{
    public string Name { get; set; } = string.Empty;
    public Tile[] Tiles { get; set; } = [];
    public int MaxShots { get; set; }
    public bool IsDark { get; set; }
    public Exits Exits { get; set; } = new();
    public bool RestartOnZap { get; set; }
    public string Message { get; set; } = string.Empty;
    public Vec2 Enter { get; set; }
    public TimeSpan TimeLimit { get; set; }
    public Vec2 Camera { get; set; }
    public List<Actor> Actors { get; set; } = [];
    public List<string> Scripts { get; set; } = [];
    public int Width { get; set; }
    public int Height { get; set; }
    public int MaxActorCount { get; set; }

    public override string ToString() => Name;
}