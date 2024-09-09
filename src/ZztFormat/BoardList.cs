using System.Collections;

namespace ZztFormat;

/// <summary>
/// A list of boards. Changing this list will also change the respective
/// board exit indices and passage destinations.
/// </summary>
public class BoardList(WorldType worldType) : IList<Board>
{
    private readonly List<Board> _list = [];

    public WorldType WorldType { get; } = worldType;
    
    public IEnumerator<Board> GetEnumerator() =>
        _list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        _list.GetEnumerator();

    public void Add(Board item) =>
        _list.Add(item);

    public void Clear() =>
        _list.Clear();

    public bool Contains(Board item) =>
        _list.Contains(item);

    public void CopyTo(Board[] array, int arrayIndex) =>
        _list.CopyTo(array, arrayIndex);

    public bool Remove(Board item)
    {
        var index = _list.IndexOf(item);
        if (index < 0)
            return false;

        RemoveAt(index);
        return true;
    }

    public int Count => _list.Count;

    public bool IsReadOnly => false;

    public int IndexOf(Board item) =>
        _list.IndexOf(item);

    public void Insert(int index, Board item)
    {
        _list.Insert(index, item);
        foreach (var board in _list)
            board.FixRefsAfterAdd(index);
    }

    public void RemoveAt(int index)
    {
        foreach (var board in _list)
            board.FixRefsBeforeRemove(index);
        _list.RemoveAt(index);
    }

    public Board this[int index]
    {
        get => _list[index];
        set => _list[index] = value;
    }
}