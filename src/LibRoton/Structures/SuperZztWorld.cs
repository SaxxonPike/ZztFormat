namespace LibRoton.Structures;

public class SuperZztWorld : IWorld
{
    public SuperZztWorldHeader Header { get; set; } = new();

    IWorldHeader IWorld.Header => Header;

    public Element GetElement(byte elementId)
    {
        throw new NotImplementedException();
    }

    public byte GetElementId(Element element)
    {
        throw new NotImplementedException();
    }
}