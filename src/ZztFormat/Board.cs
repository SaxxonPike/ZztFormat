using JetBrains.Annotations;

namespace ZztFormat;

/// <summary>
/// Contains properties of a single board as well as its tiles and
/// scripted actors.
/// </summary>
[PublicAPI]
public partial class Board
{
    /// <summary>
    /// Name of the board.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Board tile data as a single contiguous data block.
    /// </summary>
    public Tile[] Tiles { get; set; } = [];

    /// <summary>
    /// Maximum number of bullets that are permitted on this board at one time.
    /// </summary>
    public int MaxShots { get; set; } = 255;

    /// <summary>
    /// If true, the board will be dark.
    /// </summary>
    public bool IsDark { get; set; }

    /// <summary>
    /// Adjacent boards when players walk off the edge of this board.
    /// </summary>
    public Dictionary<ExitDirection, int> Exits { get; set; } = [];

    /// <summary>
    /// If true, the player's position will be reset when taking damage.
    /// </summary>
    public bool RestartOnZap { get; set; }

    /// <summary>
    /// Most recently rendered message on the board. This will be empty for
    /// new worlds, but may have content when opening ZZT saved games.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Coordinates where the player has most recently entered the board.
    /// </summary>
    public Vec2 Enter { get; set; }

    /// <summary>
    /// Time limit for the board. When this elapses in-game, the player is
    /// dealt damage. When combined with <see cref="RestartOnZap"/>, the player
    /// will be teleported back to the <see cref="Enter"/> location.
    /// </summary>
    public TimeSpan TimeLimit { get; set; }

    /// <summary>
    /// In-game information for the camera in Super ZZT.
    /// </summary>
    public Vec2 Camera { get; set; }

    /// <summary>
    /// Objects on the board that have special or scripted behavior.
    /// </summary>
    public ActorList Actors { get; set; } = new(worldType);

    /// <summary>
    /// Dimensions of the board.
    /// </summary>
    public Vec2 Size { get; set; }

    /// <summary>
    /// Extra data. This field is generally only used for storing the raw
    /// packed data of a board that could not be read successfully.
    /// </summary>
    public byte[] Extra { get; set; } = [];
}