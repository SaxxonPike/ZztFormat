using System.Buffers.Binary;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.Serialization;

public delegate T BinaryReadFunc<T>(BinaryReader reader);

public delegate void BinaryWriteFunc<T>(BinaryWriter writer, T value);

public class BinaryIoFactory
{
    private record struct FieldLayout(
        FieldInfo Field,
        Type ItemType,
        int? Offset,
        bool Primitive,
        bool InlineArray,
        int FieldPack = 0,
        int ItemSize = 0,
        int ItemCount = 1,
        int ItemPack = 0
    );

    private static Span<T> InlineArrayToSpan<T>(ref T item0) =>
        MemoryMarshal.CreateSpan(ref item0, typeof(T).GetCustomAttribute<InlineArrayAttribute>()!.Length);

    private static FieldLayout? CreateFieldLayout(FieldInfo field)
    {
        if (field.FieldType.GetCustomAttribute<NonSerializedAttribute>() is not null)
            return null;

        var offset = field.GetCustomAttribute<FieldOffsetAttribute>()?.Value;

        if (field.FieldType.GetCustomAttribute<InlineArrayAttribute>() is { } inlineArray)
        {
            var fieldType = field.FieldType
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .First()
                .FieldType;

            var itemPack = fieldType.StructLayoutAttribute?.Pack;

            return new FieldLayout(
                Field: field,
                ItemType: fieldType,
                Offset: offset,
                Primitive: false,
                InlineArray: true,
                ItemCount: inlineArray.Length,
                ItemPack: itemPack ?? 0
            );
        }

        if (field.FieldType.IsPrimitive)
        {
            var fieldType = field.FieldType;

            return new FieldLayout(
                Field: field,
                ItemType: fieldType,
                Offset: offset,
                Primitive: true,
                InlineArray: false,
                ItemSize: Marshal.SizeOf(fieldType)
            );
        }

        if (field.FieldType.IsEnum)
        {
            var fieldType = field.FieldType.GetEnumUnderlyingType();

            return new FieldLayout(
                Field: field,
                ItemType: fieldType,
                Offset: offset,
                Primitive: true,
                InlineArray: false,
                ItemSize: Marshal.SizeOf(fieldType)
            );
        }

        if (field.FieldType.IsValueType)
        {
            var fieldType = field.FieldType;

            return new FieldLayout(
                Field: field,
                ItemType: fieldType,
                Offset: offset,
                Primitive: false,
                InlineArray: false
            );
        }

        throw new Exception($"Unsupported field type {field.FieldType}");
    }

    private static IEnumerable<FieldLayout> GetFieldLayouts(Type type)
    {
        var fields = type
            .GetFields(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance
            );

        var layouts = fields
            .Select(CreateFieldLayout)
            .Where(l => l != null)
            .Select(l => (FieldLayout)l!);

        return layouts;
    }

    private static byte ReadByte(BinaryReader reader) =>
        reader.ReadByte();

    private static sbyte ReadSByte(BinaryReader reader) =>
        reader.ReadSByte();

    private static ushort ReadUInt16(BinaryReader reader)
    {
        Span<byte> buf = stackalloc byte[sizeof(ushort)];
        reader.Read(buf);
        return BinaryPrimitives.ReadUInt16LittleEndian(buf);
    }

    private static short ReadInt16(BinaryReader reader)
    {
        Span<byte> buf = stackalloc byte[sizeof(short)];
        reader.Read(buf);
        return BinaryPrimitives.ReadInt16LittleEndian(buf);
    }

    private static uint ReadUInt32(BinaryReader reader)
    {
        Span<byte> buf = stackalloc byte[sizeof(uint)];
        reader.Read(buf);
        return BinaryPrimitives.ReadUInt32LittleEndian(buf);
    }

    private static int ReadInt32(BinaryReader reader)
    {
        Span<byte> buf = stackalloc byte[sizeof(int)];
        reader.Read(buf);
        return BinaryPrimitives.ReadInt32LittleEndian(buf);
    }

    private static ulong ReadUInt64(BinaryReader reader)
    {
        Span<byte> buf = stackalloc byte[sizeof(ulong)];
        reader.Read(buf);
        return BinaryPrimitives.ReadUInt64LittleEndian(buf);
    }

    private static long ReadInt64(BinaryReader reader)
    {
        Span<byte> buf = stackalloc byte[sizeof(long)];
        reader.Read(buf);
        return BinaryPrimitives.ReadInt64LittleEndian(buf);
    }

    private static float ReadSingle(BinaryReader reader)
    {
        Span<byte> buf = stackalloc byte[sizeof(float)];
        reader.Read(buf);
        return BinaryPrimitives.ReadSingleLittleEndian(buf);
    }

    private static double ReadDouble(BinaryReader reader)
    {
        Span<byte> buf = stackalloc byte[sizeof(double)];
        reader.Read(buf);
        return BinaryPrimitives.ReadDoubleLittleEndian(buf);
    }

    private static void ReadBytes(BinaryReader reader, Span<byte> buf)
    {
        reader.Read(buf);
    }

    private static bool ReadBoolean(BinaryReader reader) =>
        reader.ReadBoolean();

    private static IEnumerable<Expression> GetReadExpressions(
        ParameterExpression reader,
        Expression target)
    {
        if (reader.Type != typeof(BinaryReader))
            throw new Exception("Type of reader expression must be BinaryReader.");

        foreach (var layout in GetFieldLayouts(target.Type))
        {
            if (layout.InlineArray)
            {
                continue;
            }
            else if (!layout.Primitive)
            {
                foreach (var expr in GetReadExpressions(reader, Expression.Field(target, layout.Field)))
                    yield return expr;
                continue;
            }

            var targetField = Expression.Field(target, layout.Field);
            string? methodName = default;

            if (layout.ItemType == typeof(byte))
                methodName = nameof(ReadByte);
            if (layout.ItemType == typeof(sbyte))
                methodName = nameof(ReadSByte);
            if (layout.ItemType == typeof(ushort))
                methodName = nameof(ReadUInt16);
            if (layout.ItemType == typeof(short))
                methodName = nameof(ReadInt16);
            if (layout.ItemType == typeof(uint))
                methodName = nameof(ReadUInt32);
            if (layout.ItemType == typeof(int))
                methodName = nameof(ReadInt32);
            if (layout.ItemType == typeof(ulong))
                methodName = nameof(ReadUInt64);
            if (layout.ItemType == typeof(long))
                methodName = nameof(ReadInt64);
            if (layout.ItemType == typeof(float))
                methodName = nameof(ReadSingle);
            if (layout.ItemType == typeof(double))
                methodName = nameof(ReadDouble);
            if (layout.ItemType == typeof(bool))
                methodName = nameof(ReadBoolean);

            if (methodName == default)
                throw new Exception($"Unsupported primitive type {layout.ItemType.Name}");

            var methodInfo = typeof(BinaryIoFactory).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            yield return Expression.Assign(targetField, Expression.Call(methodInfo!, reader));
        }

        yield break;
    }

    public Func<BinaryReader, T> CreateReader<T>()
    {
        var type = typeof(T);
        var reader = Expression.Parameter(typeof(BinaryReader), "reader");
        var value = Expression.Variable(typeof(T));
        List<Expression> reads = [];

        // Create the local variable.
        reads.Add(Expression.Assign(value, Expression.New(typeof(T))));

        // Perform the read operations.
        reads.AddRange(GetReadExpressions(reader, value));

        // Return the local variable.
        reads.Add(value);

        var lambda = Expression.Lambda<Func<BinaryReader, T>>(Expression.Block([value], reads), reader);
        return lambda.Compile();
    }
}