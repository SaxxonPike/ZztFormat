namespace LibRoton.Structures;

internal partial struct RawTile
{
    public static RawTile FromTile(Tile tile) =>
        new()
        {
            ElementId = (byte)tile.Element,
            Color = (byte)tile.Color
        };

    public Tile ToTile() =>
        new()
        {
            Element = ElementId,
            Color = Color
        };
}