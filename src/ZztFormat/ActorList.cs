using System.Collections;

namespace ZztFormat;

/// <summary>
/// A list of actors. Changing this list also adjusts the properties on all
/// actors contained in the list - shifting indices for these properties
/// automatically:
/// <see cref="Actor.Follower"/>,
/// <see cref="Actor.Leader"/>,
/// <see cref="Actor.Bind"/>.
/// If an actor with a bound script is removed, the owner of the script is
/// changed to the lowest index Actor bound to that script.
/// </summary>
public class ActorList(WorldType worldType) : IList<Actor>
{
    private readonly List<Actor> _list = [];

    public WorldType WorldType { get; } = worldType;
    
    /// <inheritdoc />
    public IEnumerator<Actor> GetEnumerator() =>
        _list.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() =>
        _list.GetEnumerator();

    /// <inheritdoc />
    public void Add(Actor item)
    {
        if (_list.Contains(item))
            throw new InvalidOperationException("List already contains this actor.");

        _list.Add(item);
    }

    /// <inheritdoc />
    public void Clear() =>
        _list.Clear();

    /// <inheritdoc />
    public bool Contains(Actor item) =>
        _list.Contains(item);

    /// <inheritdoc />
    public void CopyTo(Actor[] array, int arrayIndex) =>
        _list.CopyTo(array, arrayIndex);

    /// <inheritdoc />
    /// <remarks>
    /// This affects the following properties of all actors:
    /// <see cref="Actor.Follower"/>,
    /// <see cref="Actor.Leader"/>,
    /// <see cref="Actor.Bind"/>
    /// </remarks>
    public bool Remove(Actor item)
    {
        var index = _list.IndexOf(item);
        if (index < 0)
            return false;
        RemoveAt(index);
        return true;
    }

    /// <inheritdoc />
    public int Count => _list.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public int IndexOf(Actor item) =>
        _list.IndexOf(item);

    /// <inheritdoc />
    /// <remarks>
    /// This affects the following properties of all actors:
    /// <see cref="Actor.Follower"/>,
    /// <see cref="Actor.Leader"/>,
    /// <see cref="Actor.Bind"/>
    /// </remarks>
    public void Insert(int index, Actor item)
    {
        _list.Insert(index, item);

        // Post-insert fix-up of indices.
        for (var i = 0; i < _list.Count; i++)
        {
            var other = _list[i];
            if (other.Follower >= index)
                other.Follower++;
            if (other.Leader >= index)
                other.Leader++;
            if (other.Bind >= index)
                other.Bind++;
        }
    }

    /// <inheritdoc />
    /// <remarks>
    /// This affects the following properties of all actors:
    /// <see cref="Actor.Follower"/>,
    /// <see cref="Actor.Leader"/>,
    /// <see cref="Actor.Bind"/>
    /// </remarks>
    public void RemoveAt(int index)
    {
        var reboundScriptIndex = 0;
        var removed = _list[index];
        _list.RemoveAt(index);

        // Post-remove fix-up of indices.
        for (var i = 0; i < _list.Count; i++)
        {
            var other = _list[i];

            if (other.Follower > index)
                other.Follower--;
            else if (other.Follower == index)
                other.Follower = -1;

            if (other.Leader > index)
                other.Leader--;
            else if (other.Leader == index)
                other.Leader = -1;

            if (other.Bind > index)
                other.Bind--;
            else if (other.Bind == index)
            {
                if (reboundScriptIndex == 0)
                    reboundScriptIndex = i;

                other.Bind = -reboundScriptIndex;
            }

            other.Script = removed.Script;
        }
    }

    /// <inheritdoc />
    public Actor this[int index]
    {
        get => _list[index];
        set => _list[index] = value;
    }
}