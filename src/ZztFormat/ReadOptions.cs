namespace ZztFormat;

/// <summary>
/// Flags that affect the way in which worlds and boards will be loaded.
/// </summary>
[Flags]
public enum ReadOptions
{
    None,

    /// <summary>
    /// By default, corrupt boards will be replaced with empty boards.
    /// If specified, will keep the raw content of the corrupt board data.
    /// This does not permit the board to be interacted with, and the
    /// <see cref="WriteOptions.KeepCorruptBoards"/> flag will need to be
    /// specified in order to preserve this data during save operations.
    /// </summary>
    KeepCorruptBoards = 1 << 0,
    
    /// <summary>
    /// By default, corrupt boards will be replaced with empty boards.
    /// If specified, encountering corrupt boards will instead result in an
    /// exception.
    /// </summary>
    ThrowOnCorruptBoards = 1 << 1,
    
    /// <inheritdoc cref="FlagOptions.CaseSensitive"/>
    CaseSensitiveFlags = 1 << 2,
    
    /// <inheritdoc cref="FlagOptions.PreserveCase"/>
    PreserveFlagCase = 1 << 3,
    
    /// <inheritdoc cref="FlagOptions.AllowDuplicates"/>
    AllowDuplicateFlags = 1 << 4
}

/// <summary>
/// Flags that affect the way in which worlds and boards will be saved.
/// </summary>
[Flags]
public enum WriteOptions
{
    None,

    /// <summary>
    /// If specified, will take advantage of a quirk in the RLE algorithm
    /// that allows a maximum run of 256 instead of 255. This is sometimes
    /// incompatible with editors, but will not cause issues in-game.
    /// </summary>
    Allow256RleCount = 1 << 0,

    /// <summary>
    /// By default, when encountering corrupt boards during a save operation,
    /// an exception is thrown. If specified, these boards will instead be
    /// saved to the output as-is.
    /// </summary>
    KeepCorruptBoards = 1 << 1,
}