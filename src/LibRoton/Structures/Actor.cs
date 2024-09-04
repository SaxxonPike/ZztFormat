namespace LibRoton.Structures;

public class Actor
{
    public Vec2 Position { get; set; }
    public Vec2 Step { get; set; }
    public int Cycle { get; set; } = -1;
    public int[] Parameters { get; set; } = new int[3];
    public int Follower { get; set; } = -1;
    public int Leader { get; set; } = -1;
    public Tile Under { get; set; }
    public int Instruction { get; set; }
    public Memory<char> Script { get; set; }

    public override string ToString() => 
        $"{Position}";
}