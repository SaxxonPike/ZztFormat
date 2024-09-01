using System.Buffers;
using System.Buffers.Binary;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Expression = System.Linq.Expressions.Expression;

namespace LibRoton.Serialization;

public delegate T BinaryReadFunc<T>(BinaryReader reader);

public delegate void BinaryWriteFunc<T>(BinaryWriter writer, T value);

public class BinaryIoFactory
{
    private static readonly Type _tReadOnlyByteSpan =
        typeof(ReadOnlySpan<byte>);

    private static readonly Type _tByteSpan =
        typeof(ReadOnlySpan<byte>);

    private static readonly Dictionary<Type, MethodInfo> _readMethods = new()
    {
        {
            typeof(byte),
            typeof(BinaryIoFactory).GetMethod(
                nameof(ReadByte), StaticFlags, [_tReadOnlyByteSpan])!
        },
        {
            typeof(sbyte),
            typeof(BinaryIoFactory).GetMethod(
                nameof(ReadSByte), StaticFlags, [_tReadOnlyByteSpan])!
        },
        {
            typeof(short),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.ReadInt16LittleEndian), StaticFlags, [_tReadOnlyByteSpan])!
        },
        {
            typeof(ushort),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.ReadUInt16LittleEndian), StaticFlags, [_tReadOnlyByteSpan])!
        },
        {
            typeof(int),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.ReadInt32LittleEndian), StaticFlags, [_tReadOnlyByteSpan])!
        },
        {
            typeof(uint),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.ReadUInt32LittleEndian), StaticFlags, [_tReadOnlyByteSpan])!
        },
        {
            typeof(long),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.ReadInt64LittleEndian), StaticFlags, [_tReadOnlyByteSpan])!
        },
        {
            typeof(ulong),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.ReadUInt64LittleEndian), StaticFlags, [_tReadOnlyByteSpan])!
        },
        {
            typeof(float),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.ReadSingleLittleEndian), StaticFlags, [_tReadOnlyByteSpan])!
        },
        {
            typeof(double),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.ReadDoubleBigEndian), StaticFlags, [_tReadOnlyByteSpan])!
        }
    };

    private static readonly Dictionary<Type, MethodInfo> _writeMethods = new()
    {
        {
            typeof(byte),
            typeof(BinaryIoFactory).GetMethod(
                nameof(WriteByte), StaticFlags, [_tByteSpan, typeof(byte)])!
        },
        {
            typeof(sbyte),
            typeof(BinaryIoFactory).GetMethod(
                nameof(WriteSByte), StaticFlags, [_tByteSpan, typeof(sbyte)])!
        },
        {
            typeof(short),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.WriteInt16LittleEndian), StaticFlags, [_tByteSpan, typeof(short)])!
        },
        {
            typeof(ushort),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.WriteUInt16LittleEndian), StaticFlags, [_tByteSpan, typeof(ushort)])!
        },
        {
            typeof(int),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.WriteInt32LittleEndian), StaticFlags, [_tByteSpan, typeof(int)])!
        },
        {
            typeof(uint),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.WriteUInt32LittleEndian), StaticFlags, [_tByteSpan, typeof(uint)])!
        },
        {
            typeof(long),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.WriteInt64LittleEndian), StaticFlags, [_tByteSpan, typeof(long)])!
        },
        {
            typeof(ulong),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.WriteUInt64LittleEndian), StaticFlags, [_tByteSpan, typeof(ulong)])!
        },
        {
            typeof(float),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.WriteSingleLittleEndian), StaticFlags, [_tByteSpan, typeof(float)])!
        },
        {
            typeof(double),
            typeof(BinaryPrimitives).GetMethod(
                nameof(BinaryPrimitives.WriteDoubleLittleEndian), StaticFlags, [_tByteSpan, typeof(double)])!
        }
    };

    private const BindingFlags InstanceFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
    private const BindingFlags StaticFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

    private static byte ReadByte(ReadOnlySpan<byte> span) =>
        span[0];

    private static sbyte ReadSByte(ReadOnlySpan<byte> span) =>
        unchecked((sbyte)span[0]);

    private static void WriteByte(Span<byte> span, byte value) =>
        span[0] = value;

    private static void WriteSByte(Span<sbyte> span, byte value) =>
        span[0] = unchecked((sbyte)value);

    private static int CalculateSize(Type t)
    {
        if (t.IsPrimitive || t.IsEnum)
            return Marshal.SizeOf(t);

        if (t.GetCustomAttribute<InlineArrayAttribute>() is { } inlineArray)
        {
            var elementType = t.GetFields(InstanceFlags).First().FieldType;
            return CalculateSize(elementType) * inlineArray.Length;
        }

        var structLayout = t.StructLayoutAttribute;
        if (structLayout?.Size is { } size and > 0)
            return size;

        var offset = 0;
        var maxOffset = 0;
        foreach (var field in t.GetFields(InstanceFlags))
        {
            if (field.GetCustomAttribute<NonSerializedAttribute>() is not null)
                continue;

            if (field.GetCustomAttribute<FieldOffsetAttribute>() is { } fieldOffset)
                offset = fieldOffset.Value;

            offset += CalculateSize(field.FieldType);
            if (offset > maxOffset)
                maxOffset = offset;
        }

        return maxOffset;
    }

    private static Expression GetReadExpression(
        ParameterExpression stream,
        Type fieldType)
    {
        var buffer = Expression.Variable(typeof(System.Buffers.IMemoryOwner<byte>), "buffer");
        var offset = Expression.Variable(typeof(int), "offset");
        var target = Expression.Variable(fieldType, "target");
        var span = Expression.Variable(typeof(Span<byte>), "span");
        var size = CalculateSize(fieldType);
        var body = new List<Expression>();

        body.AddRange([
            Expression.Assign(
                buffer,
                Expression.Call(
                    Expression.Constant(MemoryPool<byte>.Shared),
                    nameof(MemoryPool<byte>.Rent),
                    Type.EmptyTypes,
                    [Expression.Constant(size)]
                )
            ),
            Expression.Assign(
                span,
                Expression.Call(
                    Expression.Property(
                        Expression.Property(
                            buffer,
                            nameof(IMemoryOwner<byte>.Memory)
                        ),
                        nameof(Memory<byte>.Span)
                    ),
                    nameof(Span<byte>.Slice),
                    Type.EmptyTypes,
                    Expression.Constant(size)
                )
            ),
            Expression.Assign(
                offset,
                Expression.Constant(0)
            ),
            Expression.Call(
                stream,
                nameof(Stream.Read),
                Type.EmptyTypes,
                span
            )
        ]);

        body.Add(
            Expression.Assign(
                target,
                Expression.New(fieldType)
            )
        );

        body.AddRange(
            GetParseExpressions(
                0,
                fieldType,
                span,
                offset,
                target,
                fieldType.StructLayoutAttribute?.Value ?? LayoutKind.Sequential
            )
        );

        body.Add(target);

        return Expression.Block(
            [span, buffer, offset, target],
            Expression.TryFinally(
                Expression.Block(body),
                Expression.IfThen(
                    Expression.NotEqual(
                        buffer,
                        Expression.Default(typeof(System.Buffers.IMemoryOwner<byte>))
                    ),
                    Expression.Call(
                        Expression.Constant(MemoryPool<byte>.Shared),
                        nameof(MemoryPool<byte>.Dispose),
                        Type.EmptyTypes
                    )
                )
            )
        );
    }

    private static IEnumerable<Expression> GetParseExpressions(
        int depth,
        Type fieldType,
        ParameterExpression span,
        ParameterExpression offset,
        Expression target,
        LayoutKind layoutKind)
    {
        if (fieldType.GetCustomAttribute<InlineArrayAttribute>() is { } inlineArray)
        {
            var zeroField = fieldType.GetFields(InstanceFlags).First();
            var itemType = zeroField.FieldType;
            if (itemType == typeof(byte))
            {
                // Creates a span out of the array.
                var createSpanMethod = typeof(MemoryMarshal)
                    .GetMethod(nameof(MemoryMarshal.CreateSpan), StaticFlags, [itemType, typeof(int)])!;

                // Holds the length of the array.
                var arrayLength = Expression.Constant(inlineArray.Length);

                // Holds the current span - position 0 is the current element.
                var arraySpan = Expression.Call(
                    createSpanMethod,
                    [Expression.Field(target, zeroField), arrayLength]
                );

                var arraySpanVar = Expression.Variable(typeof(Span<>)
                    .MakeGenericType(itemType));

                var loopBreak = Expression.Label();
                var loopCount = Expression.Constant(inlineArray.Length);
                var loopCounter = Expression.Variable(typeof(int));
                var itemVal = Expression.Variable(itemType);
                var itemParseExpressions = GetParseExpressions(
                    depth + 1,
                    itemType,
                    arraySpanVar,
                    offset,
                    itemVal,
                    itemType.StructLayoutAttribute?.Value ?? LayoutKind.Sequential
                );

                Expression.Block(
                    [loopCounter, arraySpanVar, itemVal],
                    Expression.Assign(
                        loopCounter,
                        Expression.Constant(0)
                    ),
                    Expression.Assign(
                        arraySpanVar,
                        arraySpan
                    ),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.GreaterThanOrEqual(
                                loopCounter,
                                loopCount
                            ),
                            Expression.Break(loopBreak),
                            Expression.Block(itemParseExpressions
                                .Concat([
                                    Expression.Assign()
                                ]))
                        )
                    ));
            }

            yield break;
        }

        var dataType = fieldType.IsEnum
            ? fieldType.GetEnumUnderlyingType()
            : fieldType;

        Func<Expression, Expression> fieldConvert = fieldType.IsEnum
            ? e => Expression.Convert(e, fieldType)
            : e => e;

        if (_readMethods.TryGetValue(dataType, out var readMethod))
        {
            yield return Expression.Assign(
                target,
                fieldConvert(
                    Expression.Call(
                        readMethod,
                        Expression.Convert(
                            Expression.Call(
                                span,
                                nameof(Span<byte>.Slice),
                                Type.EmptyTypes,
                                offset
                            ),
                            typeof(ReadOnlySpan<byte>)
                        )
                    )
                )
            );

            if (layoutKind != LayoutKind.Explicit)
            {
                yield return Expression.AddAssign(
                    offset,
                    Expression.Constant(CalculateSize(dataType))
                );
            }

            yield break;
        }

        if (dataType.IsPrimitive)
            throw new Exception($"Primitive type {dataType.Name} not supported.");

        var structLayout = dataType.StructLayoutAttribute;
        var body = new List<Expression>();
        var subOffset = Expression.Variable(typeof(int), $"offset{depth}");
        var subSpan = Expression.Variable(typeof(Span<byte>), $"span{depth}");

        body.AddRange([
            Expression.Assign(
                subOffset,
                Expression.Constant(0)
            ),
            Expression.Assign(
                subSpan,
                Expression.Call(
                    span,
                    nameof(Span<byte>.Slice),
                    Type.EmptyTypes,
                    offset
                )
            )
        ]);

        foreach (var field in fieldType.GetFields(InstanceFlags))
        {
            if (field.GetCustomAttribute<FieldOffsetAttribute>() is { } fieldOffset)
            {
                body.Add(
                    Expression.Assign(
                        subOffset,
                        Expression.Constant(fieldOffset.Value)
                    )
                );
            }

            body.AddRange(
                GetParseExpressions(
                    depth + 1,
                    field.FieldType,
                    subSpan,
                    subOffset,
                    Expression.Field(target, field),
                    structLayout?.Value ?? LayoutKind.Sequential
                )
            );
        }

        yield return Expression.Block(
            [subOffset, subSpan],
            body
        );
    }

    public Expression<Func<Stream, T>> CreateReaderExpression<T>()
    {
        var type = typeof(T);
        var stream = Expression.Parameter(typeof(Stream), "stream");
        return Expression.Lambda<Func<Stream, T>>(GetReadExpression(stream, type), stream);
    }

    public Func<Stream, T> CreateReader<T>()
    {
        var lambda = CreateReaderExpression<T>();
        return lambda.Compile();
    }
}