namespace LibRoton.Structures;

public class SuperZztBoard : IBoard
{
    public SuperZztBoardHeader Header { get; set; } = new();

    IBoardHeader IBoard.Header => Header;

    public int Width => 96;

    public int Height => 80;
}