namespace LibRoton.Structures;

public class SuperZztWorld
{
    public SuperZztWorldHeader Header { get; set; }
    public List<SuperZztPackedBoard> PackedBoards { get; set; }

    public static SuperZztWorld Read(Stream stream)
    {
        var result = new SuperZztWorld
        {
            Header = SuperZztWorldHeader.Read(stream)
        };

        if (result.Header.Type != -2)
            throw new Exception("Not a Super ZZT world.");

        for (var i = 0; i <= result.Header.BoardCount; i++)
        {
            var packedBoard = new SuperZztPackedBoard
            {
                Header = SuperZztBoardHeader.Read(stream)
            };

            var boardDataSize = packedBoard.Header.Length - SuperZztBoardHeader.Size;
            packedBoard.Data = new byte[boardDataSize];
            stream.ReadExactly(packedBoard.Data);
            result.PackedBoards.Add(packedBoard);
        }

        return result;
    }

    public static void Write(Stream stream, SuperZztWorld world)
    {
        if (world.PackedBoards.Count < 1)
            throw new Exception("Can't write world without any boards.");

        var header = world.Header;
        header.BoardCount = (short)(world.PackedBoards.Count - 1);
        SuperZztWorldHeader.Write(stream, world.Header);

        foreach (var packedBoard in world.PackedBoards)
        {
            var boardHeader = packedBoard.Header;
            boardHeader.Length = (short)(packedBoard.Data.Length + SuperZztBoardHeader.Size);
            SuperZztBoardHeader.Write(stream, boardHeader);
            stream.Write(packedBoard.Data);
        }
    }
}