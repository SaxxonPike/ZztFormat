using JetBrains.Annotations;

namespace LibRoton;

[PublicAPI]
public partial class Actor
{
    public Vec2 Position { get; set; }
    public Vec2 Step { get; set; }
    public int Cycle { get; set; }
    public int[] Parameters { get; init; } = new int[3];
    public int Follower { get; set; }
    public int Leader { get; set; }
    public Tile Under { get; set; }
    public int Instruction { get; set; }
    public int Length { get; set; }
    public char[] Script { get; set; } = [];
}