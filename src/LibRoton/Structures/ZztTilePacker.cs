namespace LibRoton.Structures;

internal static class ZztTilePacker
{
    public static int Pack(Stream stream, ReadOnlySpan<ZztTile> tiles)
    {
        var outCursor = 0;
        var current = new ZztTileRle
        {
            Element = tiles[0].Element,
            Color = tiles[0].Color
        };

        foreach (var tile in tiles)
        {
            if (current.Count == 255 ||
                tile.Element != current.Element ||
                tile.Color != current.Color)
            {
                ZztTileRle.Write(stream, current);
                outCursor += 1;
                current = new ZztTileRle
                {
                    Element = tile.Element,
                    Color = tile.Color
                };
            }

            current.Count++;
        }

        ZztTileRle.Write(stream, current);
        return outCursor * ZztTileRle.Size;
    }

    public static void Unpack(Stream stream, Span<ZztTile> tiles)
    {
        var tileCursor = 0;

        while (true)
        {
            var rle = ZztTileRle.Read(stream);
            // Looks silly, but works for 0 = 256
            for (var i = rle.Count; --i != 0;)
            {
                tiles[tileCursor++] = new ZztTile
                {
                    Element = rle.Element,
                    Color = rle.Color
                };
                
                if (tileCursor >= tiles.Length)
                    break;
            }

            if (tileCursor >= tiles.Length)
                break;
        }
    }
}