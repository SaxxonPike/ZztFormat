using System.Buffers.Binary;
using LibRoton.CodeGen;
using LibRoton.Shared;
using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.Zzt.Structures;

public class ZztBoardPacker : IBoardPacker
{
    public Board Unpack(ReadOnlySpan<byte> raw)
    {
        var cursor = raw;
        var board = BinarySerialization.Deserialize<RawZztBoardHeader>(raw);
        cursor = cursor[RawZztBoardHeader.Size..];
        
        var rle = default(RawZztTileRle);
        var total = 0;
        while (total < 1500)
        {
            var advance = BinarySerialization.Deserialize(cursor, ref rle);
            cursor = cursor[advance..];
            
        }
    }

    public Board Unpack(Stream stream)
    {
        Span<byte> lenBytes = stackalloc byte[2];
        stream.ReadAtLeast(lenBytes, 2);
        var len = BinaryPrimitives.ReadInt16LittleEndian(lenBytes);
        Span<byte> raw = stackalloc byte[len + 2];
        lenBytes.CopyTo(raw);
        stream.ReadAtLeast(raw[2..], len);
        return Unpack(raw);
    }

    public Memory<byte> Pack(Board board)
    {
        throw new NotImplementedException();
    }

    public int Pack(Board board, Stream stream)
    {
        throw new NotImplementedException();
    }
}