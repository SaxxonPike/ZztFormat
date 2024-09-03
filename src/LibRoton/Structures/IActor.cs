namespace LibRoton.Structures;

public interface IActor
{
    Position Position { get; set; }
    Vector Step { get; set; }
    short Cycle { get; set; }
    byte[] Parameters { get; }
    short Follower { get; set; }
    short Leader { get; set; }
    Tile Under { get; set; }
    int Pointer { get; set; }
    short Instruction { get; set; }
    short Length { get; set; }
}