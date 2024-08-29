namespace LibRoton.Shared;

public interface IWorldPacker
{
    World Unpack(ReadOnlySpan<byte> raw);
    World Unpack(Stream stream);
    Memory<byte> Pack(World world);
    int Pack(World world, Stream stream);
}

public interface IElementConverter
{
    Element 
}