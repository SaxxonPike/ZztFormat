using LibRoton.CodeGen;
using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.Shared;

public delegate void TileDecompressFunc(int x, int y, RawZztTile tile);

public delegate RawZztTile TileCompressFunc(int x, int y);

public static class TileCompression
{
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