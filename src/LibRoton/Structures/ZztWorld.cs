namespace LibRoton.Structures;

public class ZztWorld
{
    public ZztWorldHeader Header { get; set; }
    public List<ZztPackedBoard> PackedBoards { get; set; }

    public static ZztWorld Read(Stream stream)
    {
        var result = new ZztWorld
        {
            Header = ZztWorldHeader.Read(stream)
        };

        if (result.Header.Type != -1)
            throw new Exception("Not a ZZT world.");

        for (var i = 0; i <= result.Header.BoardCount; i++)
        {
            var packedBoard = new ZztPackedBoard
            {
                Header = ZztBoardHeader.Read(stream)
            };

            var boardDataSize = packedBoard.Header.Length - ZztBoardHeader.Size;
            packedBoard.Data = new byte[boardDataSize];
            stream.ReadExactly(packedBoard.Data);
            result.PackedBoards.Add(packedBoard);
        }

        return result;
    }

    public static void Write(Stream stream, ZztWorld world)
    {
        if (world.PackedBoards.Count < 1)
            throw new Exception("Can't write world without any boards.");

        var header = world.Header;
        header.BoardCount = (short)(world.PackedBoards.Count - 1);
        ZztWorldHeader.Write(stream, world.Header);

        foreach (var packedBoard in world.PackedBoards)
        {
            var boardHeader = packedBoard.Header;
            boardHeader.Length = (short)(packedBoard.Data.Length + ZztBoardHeader.Size);
            ZztBoardHeader.Write(stream, boardHeader);
            stream.Write(packedBoard.Data);
        }
    }
}