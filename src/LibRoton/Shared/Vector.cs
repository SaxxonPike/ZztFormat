using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.Shared;

public record struct Vector(int X, int Y)
{
    public static Vector CopyFrom(RawZztVector raw) =>
        new(raw.X, raw.Y);

    public void CopyTo(ref RawZztVector raw) =>
        raw = ToRaw();

    public void CopyTo(Span<RawZztVector> raw) =>
        CopyTo(ref raw[0]);

    public RawZztVector ToRaw() =>
        new() { X = (short)X, Y = (short)Y };
}