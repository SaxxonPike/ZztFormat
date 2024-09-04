namespace LibRoton.Structures;

public class Exits
{
    public int North { get; set; }
    public int South { get; set; }
    public int West { get; set; }
    public int East { get; set; }

    public Board? GetNorthBoard(World world) =>
        North < 0 || North >= world.Boards.Count 
            ? null 
            : world.Boards[North];

    public Board? GetSouthBoard(World world) =>
        South < 0 || South >= world.Boards.Count 
            ? null 
            : world.Boards[South];

    public Board? GetWestBoard(World world) =>
        West < 0 || West >= world.Boards.Count 
            ? null 
            : world.Boards[West];

    public Board? GetEastBoard(World world) =>
        East < 0 || East >= world.Boards.Count 
            ? null 
            : world.Boards[East];

    public static Exits FromBytes(ReadOnlySpan<byte> bytes) =>
        new()
        {
            North = bytes[0],
            South = bytes[1],
            West = bytes[2],
            East = bytes[3]
        };

    public byte[] ToBytes() =>
    [
        (byte)North,
        (byte)South,
        (byte)West,
        (byte)East
    ];
}