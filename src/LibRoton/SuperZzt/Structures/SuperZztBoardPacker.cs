using LibRoton.Shared;

namespace LibRoton.SuperZzt.Structures;

public class SuperZztBoardPacker : IBoardPacker
{
    public Board Unpack(ReadOnlySpan<byte> raw)
    {
        throw new NotImplementedException();
    }

    public Memory<byte> Pack(Board board)
    {
        throw new NotImplementedException();
    }
}