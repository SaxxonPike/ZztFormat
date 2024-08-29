using LibRoton.CodeGen;
using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.Shared;

public delegate void TileDecompressFunc(int x, int y, RawZztTile tile);

public delegate RawZztTile TileCompressFunc(int x, int y);

public static class TileCompression
{
    public static int Compress(
        ReadOnlySpan<byte> target,
        int width,
        int height,
        TileCompressFunc func)
    {
        var x = 2;
        var y = 1;
        var capacity = width * height;
        var progress = 1;
        var result = 0;
        var lastTile = func(1, 1);
        var count = 1;

        while (progress < capacity)
        {
            var tile = func(x, y);
            if (lastTile.Value != tile.Value)
            {
                
            }
        }
    }
    
    public static int Decompress(
        ReadOnlySpan<byte> source,
        int width,
        int height,
        TileDecompressFunc func)
    {
        var cursor = source;
        var progress = 0;
        var x = 1;
        var y = 1;
        var rle = default(RawZztTileRle);
        var capacity = width * height;
        var result = 0;

        while (progress < capacity)
        {
            var advance = BinarySerialization.Deserialize(cursor, ref rle);
            result += advance;
            cursor = cursor[advance..];
            progress += rle.Count;
            var c = rle.Count;

            while (true)
            {
                func(x++, y, new RawZztTile
                {
                    Element = rle.Element,
                    Color = rle.Color
                });

                if (x > width)
                {
                    x = 1;
                    y++;
                }

                if (--c == 0)
                    break;
            }
        }

        return result;
    }
}