namespace LibRoton.Structures;

public struct Vec2
{
    public int X;
    public int Y;

    public RawPosition ToRawPosition() =>
        new() { X = (byte)X, Y = (byte)Y };

    public RawVector ToRawVector() =>
        new() { X = (short)X, Y = (short)Y };

    public static Vec2 FromRawPosition(RawPosition position) =>
        new() { X = position.X, Y = position.Y };

    public static Vec2 FromRawVector(RawVector vector) =>
        new() { X = vector.X, Y = vector.Y };

    public override string ToString() =>
        $"({X}, {Y})";
}