using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.Shared;

public record struct Point(int X, int Y)
{
    public static Point CopyFrom(RawZztPosition raw) =>
        new(raw.X, raw.Y);

    public void CopyTo(ref RawZztPosition raw) =>
        raw = ToRaw();

    public void CopyTo(Span<RawZztPosition> raw) =>
        CopyTo(ref raw[0]);

    public RawZztPosition ToRaw() =>
        new() { X = (byte)X, Y = (byte)Y };
}