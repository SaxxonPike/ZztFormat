using JetBrains.Annotations;

namespace LibRoton;

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
    public int Cycle { get; set; }
    
    /// <summary>
    /// Element-specific properties.
    /// </summary>
    public int[] Parameters { get; init; } = new int[3];
    
    /// <summary>
    /// For centipedes, this is the index of the actor that is
    /// following behind this one.
    /// </summary>
    public int Follower { get; set; }
    
    /// <summary>
    /// For centipedes, this is the index of the actor that this
    /// actor is following behind.
    /// </summary>
    public int Leader { get; set; }
    
    /// <summary>
    /// Tile data underneath this actor.
    /// </summary>
    public Tile Under { get; set; }
    
    /// <summary>
    /// Offset within the script that the next instruction will be read from.
    /// A value of -1 indicates script execution is suspended.
    /// </summary>
    public int Instruction { get; set; }
    
    // /// <summary>
    // /// This value serves a dual purpose: for nonnegative values, this is the
    // /// length of the script. For negative values, it indicates the (negated)
    // /// index of the actor on the board to "bind" to, or borrow script from.
    // /// </summary>
    // public int Length { get; set; }
    
    /// <summary>
    /// Contains the full text of the attached script. In the
    /// </summary>
    public char[] Script { get; set; } = [];
}