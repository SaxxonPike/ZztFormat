namespace LibRoton.Structures;

public struct Tile
{
    public Element Element;
    public DosColor ForegroundColor;
    public DosColor BackgroundColor;
    public bool Blink;

    public char Character
    {
        get
        {
            Span<char> c = stackalloc char[1];
            Span<byte> b = stackalloc byte[1];
            b[0] = GetColorValue();
            CodePage437.Encoding.GetChars(b, c);
            return c[0];
        }
        set
        {
            Span<char> c = stackalloc char[1];
            Span<byte> b = stackalloc byte[1];
            c[0] = value;
            CodePage437.Encoding.GetBytes(c, b);
            (ForegroundColor, BackgroundColor, Blink) = GetColorValue(b[0]);
        }
    }

    private byte GetColorValue() =>
        unchecked((byte)(((int)ForegroundColor & 0xF) |
                         (((int)BackgroundColor & 0x7) << 4) |
                         (Blink ? 0x80 : 0x00)));

    private static (DosColor Foreground, DosColor Background, bool Blink) GetColorValue(byte raw) =>
    (
        Foreground: (DosColor)(raw & 0xF),
        Background: (DosColor)((raw >> 4) & 0x7),
        Blink: (raw & 0x80) != 0
    );

    public RawTile ToRawTile(Func<Element, byte> elementMap) =>
        new()
        {
            ElementId = elementMap(Element),
            Color = GetColorValue()
        };

    public static Tile FromRawTile(RawTile tile, Func<byte, Element> elementMap) =>
        new()
        {
            Element = elementMap(tile.ElementId),
            ForegroundColor = (DosColor)(tile.Color & 0xF),
            BackgroundColor = (DosColor)((tile.Color >> 4) & 0x7),
            Blink = (tile.Color & 0x80) != 0
        };

    public override string ToString() =>
        Element >= Element.BlueText && Element <= Element.BlackText
            ? $"{Element}; '{Character}'"
            : $"{Element}; 0x{GetColorValue():X2}";
}