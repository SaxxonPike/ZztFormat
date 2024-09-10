using JetBrains.Annotations;

namespace ZztFormat;

/// <summary>
/// Contains a ZZT or Super ZZT world.
/// </summary>
[PublicAPI]
public partial class World
{
    /// <summary>
    /// Ammunition count in the player's inventory.
    /// </summary>
    public int Ammo { get; set; }

    /// <summary>
    /// Gem count in the player's inventory.
    /// </summary>
    public int Gems { get; set; }

    /// <summary>
    /// Keys in the player's inventory.
    /// </summary>
    public HashSet<KeyColor> Keys { get; set; } = [];

    /// <summary>
    /// Health of the player.
    /// </summary>
    public int Health { get; set; } = 100;

    /// <summary>
    /// Starting board when the world is loaded in-game. For saved games,
    /// this will be the board that the game was saved on.
    /// </summary>
    public int StartBoard { get; set; }

    /// <summary>
    /// Torch count in the player's inventory.
    /// </summary>
    public int Torches { get; set; }

    /// <summary>
    /// Number of torchlight cycles remaining before expiration.
    /// </summary>
    public int TorchCycles { get; set; }

    /// <summary>
    /// Number of energy cycles remaining before expiration.
    /// </summary>
    public int EnergyCycles { get; set; }

    /// <summary>
    /// Player score.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Internal name of the world. This is used in-game to re-load the correct
    /// world once a game has been started and ended.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// World flags that are set.
    /// </summary>
    public FlagList Flags { get; set; } = [];

    /// <summary>
    /// Amount of time that has passed on the current board.
    /// </summary>
    public TimeSpan TimePassed { get; set; }

    /// <summary>
    /// If true, the world is locked and cannot be opened by the in-game
    /// editor.
    /// </summary>
    public bool Locked { get; set; }

    /// <summary>
    /// Number of power stones in the player's inventory.
    /// </summary>
    public int Stones { get; set; }

    /// <summary>
    /// Boards that are contained within this game world.
    /// </summary>
    public BoardList Boards { get; set; } = new(worldType);
}