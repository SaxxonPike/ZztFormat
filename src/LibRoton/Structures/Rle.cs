namespace LibRoton.Structures;

internal static class Rle
{
    public static int Unpack(Stream stream, Span<RawTile> tiles)
    {
        var tileIndex = 0;

        try
        {
            while (tileIndex < tiles.Length)
            {
                var rle = TileRle.Read(stream);
                var count = rle.Count;

                while (true)
                {
                    tiles[tileIndex++] = new RawTile
                    {
                        ElementId = rle.ElementId,
                        Color = rle.Color
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

    public static int Pack(Stream stream, ReadOnlySpan<RawTile> tiles)
    {
        var result = 0;
        var runData = tiles[0];
        var runCount = 0;

        foreach (var tile in tiles)
        {
            if ((tile.ElementId, tile.Color) == (runData.ElementId, runData.Color))
            {
                runCount++;
                if (runCount < byte.MaxValue)
                    continue;
            }

            Commit(runData.ElementId, runData.Color, runCount);
            runCount = 0;
            runData = tile;
        }

        if (runCount > 0)
            Commit(runData.ElementId, runData.Color, runCount);

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
    }
}