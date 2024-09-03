using LibRoton.Structures;

namespace LibRoton.Constants;

public interface IElementTable
{
    IElementProperties this[int index] { get; }
    int Count { get; }
}