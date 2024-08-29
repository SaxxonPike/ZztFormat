namespace LibRoton.Shared;

public interface IBoardPacker
{
    Board Unpack(ReadOnlySpan<byte> raw);
    Board Unpack(Stream stream);
    Memory<byte> Pack(Board board);
    int Pack(Board board, Stream stream);
}