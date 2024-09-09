namespace ZztFormat;

public partial class Actor(WorldType worldType)
{
    public WorldType Type { get; } = worldType;

    /// <summary>
    /// Creates a copy of the specified actor.
    /// </summary>
    /// <remarks>
    /// The clone's script will be copied and will not be bound to another
    /// actor or board.
    /// </remarks>
    public Actor CloneDetached() =>
        new(Type)
        {
            Position = Position,
            Step = Step,
            Cycle = Cycle,
            Parameters = Parameters.ToArray(),
            Follower = Follower,
            Leader = Leader,
            Under = Under,
            Instruction = Instruction,
            Bind = 0,
            Script = Script.ToArray()
        };

    /// <summary>
    /// Copies all properties from this actor into another.
    /// </summary>
    /// <param name="other">
    /// Actor that will be copied onto.
    /// </param>
    /// <remarks>
    /// Script bindings are unaffected.
    /// </remarks>
    public void CopyTo(Actor other)
    {
        other.Position = Position;
        other.Step = Step;
        other.Cycle = Cycle;
        Parameters.CopyTo(other.Parameters.AsSpan());
        other.Follower = Follower;
        other.Leader = Leader;
        other.Under = Under;
        other.Instruction = Instruction;
        other.Bind = Bind;
        other.Script = Script;
    }
}