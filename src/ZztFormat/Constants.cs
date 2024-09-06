using System.Text;

namespace ZztFormat;

/// <summary>
/// Contains character sets and other static data that doesn't belong to any
/// specific data model.
/// </summary>
public static class Constants
{
    private static readonly Lazy<byte[]> TransporterVCharsData = new(() =>
    [
        0x5E, 0x7E, 0x5E, 0x2D, 0x76, 0x5F, 0x76, 0x2D
    ]);

    /// <summary>
    /// Character set to use for rendering a <see cref="ElementType.Transporter"/>
    /// pointed north, south, or indeterminately.
    /// </summary>
    public static ReadOnlySpan<byte> TransporterVChars => TransporterVCharsData.Value;

    private static readonly Lazy<byte[]> TransporterHCharsData = new(() =>
    [
        0x28, 0x3C, 0x28, 0xB3, 0x29, 0x3E, 0x29, 0xB3
    ]);

    /// <summary>
    /// Character set to use for rendering a <see cref="ElementType.Transporter"/>
    /// pointed west or east.
    /// </summary>
    public static ReadOnlySpan<byte> TransporterHChars => TransporterHCharsData.Value;

    private static readonly Lazy<byte[]> StarCharsData = new(() =>
    [
        0xB3, 0x2F, 0xC4, 0x5C
    ]);

    /// <summary>
    /// Character set to use for rendering the <see cref="ElementType.Star"/> element.
    /// </summary>
    public static ReadOnlySpan<byte> StarChars => StarCharsData.Value;

    private static readonly Lazy<byte[]> LineCharsData = new(() =>
    [
        0xF9, 0xD0, 0xD2, 0xBA, 0xB5, 0xBC, 0xBB, 0xB9,
        0xC6, 0xC8, 0xC9, 0xCC, 0xCD, 0xCA, 0xCB, 0xCE
    ]);

    /// <summary>
    /// Character set to use for rendering the <see cref="ElementType.Line"/> element.
    /// The index can be determined by adding the following values together:
    /// north +1, south +2, west +4, east +8.
    /// </summary>
    public static ReadOnlySpan<byte> LineChars => LineCharsData.Value;

    private static readonly Lazy<byte[]> WebCharsData = new(() =>
    [
        0xFA, 0xB3, 0xB3, 0xB3, 0xC4, 0xD9, 0xBF, 0xB4,
        0xC4, 0xC0, 0xDA, 0xC3, 0xC4, 0xC1, 0xC2, 0xC5
    ]);

    /// <summary>
    /// Character set to use for rendering the <see cref="ElementType.Web"/> element.
    /// The index can be determined by adding the following values together:
    /// north +1, south +2, west +4, east +8.
    /// </summary>
    public static ReadOnlySpan<byte> WebChars => WebCharsData.Value;
}