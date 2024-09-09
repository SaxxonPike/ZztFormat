using System.Collections;
using System.Globalization;

namespace ZztFormat;

public class FlagList : IList<string>
{
    private const StringComparison IgnoreCase = StringComparison.OrdinalIgnoreCase;
    private const StringComparison UseCase = StringComparison.Ordinal;

    private readonly List<string> _list = [];

    public FlagList()
    {
    }

    public FlagList(IEnumerable<string> values)
    {
        foreach (var item in values)
            Add(item);
    }

    public FlagOptions Options { get; set; }

    private StringComparison Comparison =>
        !Options.HasFlag(FlagOptions.CaseSensitive)
            ? IgnoreCase
            : UseCase;

    private string FixName(string item) =>
        !Options.HasFlag(FlagOptions.PreserveCase)
            ? item.ToUpper(CultureInfo.InvariantCulture)
            : item;

    public IEnumerator<string> GetEnumerator() =>
        _list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        _list.GetEnumerator();

    public void Add(string item)
    {
        if (!Options.HasFlag(FlagOptions.AllowDuplicates) && Contains(item))
            return;

        _list.Add(Options.HasFlag(FlagOptions.PreserveCase)
            ? item
            : item.ToUpper());
    }

    public void Clear() =>
        _list.Clear();

    public bool Contains(string item) =>
        IndexOf(item) >= 0;

    public void CopyTo(string[] array, int arrayIndex) =>
        _list.CopyTo(array, arrayIndex);

    public bool Remove(string item)
    {
        var index = IndexOf(item);
        if (index >= 0)
            RemoveAt(index);
        return index >= 0;
    }

    public int Count => _list.Count;

    public bool IsReadOnly => false;

    public int IndexOf(string item)
    {
        var name = FixName(item);

        for (var i = 0; i < _list.Count; i++)
            if (string.Equals(_list[i], name, Comparison))
                return i;

        return -1;
    }

    public void Insert(int index, string item)
    {
        if (!Options.HasFlag(FlagOptions.AllowDuplicates) && Contains(item))
            return;

        _list.Insert(index, Options.HasFlag(FlagOptions.PreserveCase)
            ? item
            : item.ToUpper());
    }

    public void RemoveAt(int index) =>
        _list.RemoveAt(index);

    public string this[int index]
    {
        get => _list[index];
        set => _list[index] = Options.HasFlag(FlagOptions.PreserveCase)
            ? value
            : value.ToUpper();
    }
}