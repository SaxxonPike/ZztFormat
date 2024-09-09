using JetBrains.Annotations;

namespace ZztFormat;

/// <summary>
/// Contains actor data, including position and attached script.
/// </summary>
[PublicAPI]
public partial class Actor
{
    /// <summary>
    /// Position on the board where this actor is located.
    /// </summary>
    public Vec2 Position { get; set; }

    /// <summary>
    /// For elements that use it, this indicates a 2D vector.
    /// </summary>
    public Vec2 Step { get; set; }

    /// <summary>
    /// A frequency divider for processing this actor.
    /// A cycle value of 1 indicates processing every cycle,
    /// while a value of 2 indicates every other.
    /// </summary>
    public int Cycle { get; set; } = 1;

    /// <summary>
    /// Element-specific properties.
    /// </summary>
    public int[] Parameters { get; init; } = new int[3];

    /// <summary>
    /// For centipedes, this is the index of the actor that is
    /// following behind this one.
    /// </summary>
    public int Follower { get; set; } = -1;

    /// <summary>
    /// For centipedes, this is the index of the actor that this
    /// actor is following behind.
    /// </summary>
    public int Leader { get; set; } = -1;

    /// <summary>
    /// Tile data underneath this actor.
    /// </summary>
    public Tile Under { get; set; }

    /// <summary>
    /// Offset within the script that the next instruction will be read from.
    /// A value of -1 indicates script execution is suspended.
    /// </summary>
    public int Instruction { get; set; }

    /// <summary>
    /// If greater than zero, this indicates which actor's script to use
    /// instead of this actor's own <see cref="Script"/>. If this Actor has
    /// both a Bind value and a Script when saving game worlds, the Bind value
    /// will be discarded in the saved world.
    /// </summary>
    public int Bind { get; set; }

    /// <summary>
    /// Contains the full text of the script owned by this actor. If this
    /// actor is bound to another actor's script, then that actor's script
    /// is what will actually run, and <see cref="Script"/> is ignored. If both
    /// <see cref="Script"/> and <see cref="Bind"/> are populated when saving
    /// game worlds, <see cref="Script"/> will be discarded.
    /// </summary>
    public char[] Script { get; set; } = [];
}