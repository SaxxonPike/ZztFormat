namespace LibRoton.Structures;

public static class RleCompression
{
    public static int Unpack(Stream stream, Span<Tile> tiles, Func<byte, Element> elementMap)
    {
        var tileIndex = 0;

        try
        {
            while (tileIndex < tiles.Length)
            {
                var rle = TileRle.Read(stream);
                var element = elementMap(rle.ElementId);
                var foreColor = (DosColor)(rle.Color & 0xF);
                var backColor = (DosColor)((rle.Color >> 4) & 0x7);
                var blink = (rle.Color & 0x80) != 0;
                var count = rle.Count;

                while (true)
                {
                    tiles[tileIndex++] = new Tile
                    {
                        Element = element,
                        ForegroundColor = foreColor,
                        BackgroundColor = backColor,
                        Blink = blink
                    };

                    if (--count == 0)
                        break;
                }
            }

            return tileIndex;
        }
        catch
        {
            return tileIndex;
        }
    }

    public static int Pack(Stream stream, ReadOnlySpan<Tile> tiles, Func<Element, byte> elementMap)
    {
        var result = 0;
        var runData = Convert(tiles[0]);
        var runCount = 0;

        foreach (var tile in tiles)
        {
            var tileData = Convert(tile);
            if (tileData == runData)
            {
                runCount++;
                if (runCount < byte.MaxValue)
                    continue;
            }

            Commit(runData.Element, runData.Color, runCount);
            runCount = 0;
            runData = tileData;
        }

        if (runCount > 0)
            Commit(runData.Element, runData.Color, runCount);

        return result;

        void Commit(byte elementId, byte color, int count)
        {
            new TileRle
            {
                ElementId = elementId,
                Color = color,
                Count = unchecked((byte)count)
            }.Write(stream);
            result++;
        }

        (byte Element, byte Color) Convert(Tile tile) =>
        (
            Element: elementMap(tile.Element),
            Color: unchecked((byte)(((int)tile.ForegroundColor & 0xF) |
                                    (((int)tile.BackgroundColor & 0x7) << 4) |
                                    (tile.Blink ? 0x80 : 0x00)))
        );
    }
}