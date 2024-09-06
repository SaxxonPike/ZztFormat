namespace ZztFormat;

public partial struct Vec2 : IEquatable<Vec2>
{
    public Vec2()
    {
    }

    public Vec2(int x, int y)
    {
        X = x;
        Y = y;
    }
    
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

    public bool Equals(Vec2 other) => 
        X == other.X && Y == other.Y;

    public override bool Equals(object? obj) => 
        obj is Vec2 other && Equals(other);

    public override int GetHashCode() => 
        HashCode.Combine(X, Y);

    public static bool operator ==(Vec2 left, Vec2 right) => 
        left.Equals(right);

    public static bool operator !=(Vec2 left, Vec2 right) => 
        !(left == right);
}