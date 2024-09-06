using JetBrains.Annotations;

namespace ZztFormat;

/// <summary>
/// A .DAT format archive.
/// </summary>
[PublicAPI]
public partial class ZztDat
{
    /// <summary>
    /// A single file in a .DAT archive.
    /// </summary>
    /// <param name="Name">
    /// Name of the file in the archive.
    /// </param>
    /// <param name="Text">
    /// Text content of the file.
    /// </param>
    public record struct Entry(string Name, string Text);

    /// <summary>
    /// Entries stored within the archive.
    /// </summary>
    public List<Entry> Entries { get; set; } = [];
}