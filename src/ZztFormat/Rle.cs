namespace ZztFormat;

internal static class Rle
{
    public static bool Unpack(
        Stream stream,
        Span<RawTile> tiles, 
        ReadOptions options = default)
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

            return true;
        }
        catch (Exception)
        {
            if (options.HasFlag(ReadOptions.ThrowOnCorruptBoards))
                throw;
            return false;
        }
    }

    public static void Pack(
        Stream stream, 
        ReadOnlySpan<RawTile> tiles,
        WriteOptions options = default)
    {
        var runData = tiles[0];
        var runCount = 0;
        
        // This determines the maximum length of runs. A run of 256 is encoded
        // as 0x00.
        
        var runMax = options.HasFlag(WriteOptions.Allow256RleCount)
            ? 256
            : 255;

        foreach (var tile in tiles)
        {
            if (tile == runData)
            {
                runCount++;
                if (runCount < runMax)
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