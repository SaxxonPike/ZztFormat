using JetBrains.Annotations;

namespace ZztFormat;

/// <summary>
/// A list of high scores.
/// </summary>
[PublicAPI]
public static partial class HighScoreList
{
    /// <summary>
    /// One entry in a high score list.
    /// </summary>
    public record struct Entry(string Name, int Score);
}