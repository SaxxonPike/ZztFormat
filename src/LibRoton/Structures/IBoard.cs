namespace LibRoton.Structures;

public interface IBoard
{
    IBoardHeader Header { get; }
    int Width { get; }
    int Height { get; }
}