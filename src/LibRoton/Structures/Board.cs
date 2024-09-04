using JetBrains.Annotations;

namespace LibRoton.Structures;

[PublicAPI]
public partial class Board
{
    public string Name { get; set; } = string.Empty;
    public Tile[] Tiles { get; set; } = [];
    public int MaxShots { get; set; }
    public bool IsDark { get; set; }
    public int[] Exits { get; set; } = [];
    public bool RestartOnZap { get; set; }
    public string Message { get; set; } = string.Empty;
    public Vec2 Enter { get; set; }
    public TimeSpan TimeLimit { get; set; }
    public Vec2 Camera { get; set; }
    public List<Actor> Actors { get; set; } = [];
    public Vec2 Size { get; set; }
    public bool IsCorrupt { get; set; }
}