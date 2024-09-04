using JetBrains.Annotations;

namespace LibRoton.Structures;

internal partial struct RawTile : IEquatable<RawTile>
{
    [Pure]
    public static RawTile FromTile(Tile tile) =>
        new()
        {
            ElementId = (byte)tile.Element,
            Color = (byte)tile.Color
        };

    [Pure]
    public Tile ToTile() =>
        new()
        {
            Element = ElementId,
            Color = Color
        };

    public bool Equals(RawTile other) =>
        ElementId == other.ElementId && Color == other.Color;

    public override bool Equals(object? obj) =>
        obj is RawTile other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(ElementId, Color);
    
    public static bool operator ==(RawTile left, RawTile right) => 
        left.Equals(right);

    public static bool operator !=(RawTile left, RawTile right) => 
        !(left == right);
}