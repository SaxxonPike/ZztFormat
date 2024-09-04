namespace LibRoton.Structures;

public struct Vec2
{
    public int X;
    public int Y;

    internal RawPosition ToRawPosition() =>
        new() { X = (byte)X, Y = (byte)Y };

    internal RawVector ToRawVector() =>
        new() { X = (short)X, Y = (short)Y };

    internal static Vec2 FromRawPosition(RawPosition position) =>
        new() { X = position.X, Y = position.Y };

    internal static Vec2 FromRawVector(RawVector vector) =>
        new() { X = vector.X, Y = vector.Y };

    public override string ToString() =>
        $"({X}, {Y})";
}