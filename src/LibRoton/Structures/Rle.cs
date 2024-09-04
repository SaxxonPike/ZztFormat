namespace LibRoton.Structures;

internal static class Rle
{
    public static void Unpack(Stream stream, Span<RawTile> tiles)
    {
        try
        {
            var tileIndex = 0;

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
        }
        catch (IndexOutOfRangeException)
        {
            // Do nothing.
            // Might have a corrupt board.
        }
    }

    public static void Pack(Stream stream, ReadOnlySpan<RawTile> tiles)
    {
        var runData = tiles[0];
        var runCount = 0;

        foreach (var tile in tiles)
        {
            if (tile == runData)
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

        return;

        void Commit(byte elementId, byte color, int count)
        {
            new TileRle
            {
                ElementId = elementId,
                Color = color,
                Count = unchecked((byte)count)
            }.Write(stream);
        }
    }
}