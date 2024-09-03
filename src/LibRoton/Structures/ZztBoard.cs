namespace LibRoton.Structures;

public class ZztBoard : IBoard
{
    public ZztBoardHeader Header { get; set; } = new();

    IBoardHeader IBoard.Header => Header;

    public int Width => 60;

    public int Height => 25;
}