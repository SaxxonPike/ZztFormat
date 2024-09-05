namespace LibRoton;

public partial struct Tile : IEquatable<Tile>
{
    public char Char
    {
        get
        {
            Span<char> chars = stackalloc char[1];
            Span<byte> bytes = stackalloc byte[1];
            bytes[0] = unchecked((byte)Color);
            CodePage437.Encoding.GetChars(bytes, chars);
            return chars[0];
        }
        set
        {
            Span<char> chars = stackalloc char[1];
            Span<byte> bytes = stackalloc byte[1];
            chars[0] = value;
            CodePage437.Encoding.GetBytes(chars, bytes);
            Color = bytes[0];
        }
    }

    public bool Equals(Tile other) => 
        Element == other.Element && Color == other.Color;

    public override bool Equals(object? obj) => 
        obj is Tile other && Equals(other);

    public override int GetHashCode() => 
        HashCode.Combine(Element, Color);

    public static bool operator ==(Tile left, Tile right) => 
        left.Equals(right);

    public static bool operator !=(Tile left, Tile right) => 
        !(left == right);
}