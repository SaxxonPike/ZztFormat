namespace LibRoton.Structures;

public interface IWorld
{
    IWorldHeader Header { get; }
    Element GetElement(byte elementId);
    byte GetElementId(Element element);
}