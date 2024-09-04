namespace LibRoton.Structures;

public struct Tile
{
    public int Element { get; set; }
    public int Color { get; set; }

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
}