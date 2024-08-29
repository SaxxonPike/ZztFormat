using System.Runtime.InteropServices;
using LibRoton.CodeGen;
using LibRoton.Shared;
using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.Zzt.Structures;

public class ZztWorldPacker : IWorldPacker
{
    public World Unpack(ReadOnlySpan<byte> raw)
    {
        throw new NotImplementedException();
    }

    public World Unpack(Stream stream)
    {
        throw new NotImplementedException();
    }

    public Memory<byte> Pack(World world)
    {
        throw new NotImplementedException();
    }

    public int Pack(World world, Stream stream)
    {
        throw new NotImplementedException();
    }
}