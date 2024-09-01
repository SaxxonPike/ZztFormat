using System.Buffers;
using System.Buffers.Binary;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Expression = System.Linq.Expressions.Expression;

namespace LibRoton.Serialization;

public class BinaryIoFactory
{
    private static readonly Lazy<HashSet<Type>> _swapPrimTypes = new([
        typeof(ushort),
        typeof(short),
        typeof(uint),
        typeof(int),
        typeof(ulong),
        typeof(long),
        typeof(nint),
        typeof(nuint),
        typeof(Int128),
        typeof(UInt128)
    ]);

    private Expression GetEndianFixupExpression<T>(Expression field)
    {
        if (typeof(T).GetCustomAttribute<InlineArrayAttribute>() is { } inlineArray)
        {
            var span = Expression.Variable(typeof(Span<T>), "span");
            var zeroField = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance).First();
            
            var createSpan = Expression.Assign(
                span,
                Expression.Call(
                    typeof(MemoryMarshal),
                    nameof(MemoryMarshal.CreateSpan),
                    [zeroField.FieldType],
                    Expression.Field(field, zeroField),
                    Expression.Constant(inlineArray.Length)
                )
            );

            if (zeroField.FieldType == typeof(byte) || zeroField.FieldType == typeof(sbyte))
                return Expression.Empty();

            if (_swapPrimTypes.Value.Contains(zeroField.FieldType))
            {
                return Expression.Block([span],
                    // span = MemoryMarshal.CreateSpan<T>(ref zeroField, inlineArray.Length);
                    Expression.Assign(
                        span,
                        createSpan
                    ),
                    // BinaryPrimitives.ReverseEndianness((ReadOnlySpan<T>)span, span);
                    Expression.Call(
                        typeof(BinaryPrimitives),
                        nameof(BinaryPrimitives.ReverseEndianness),
                        Type.EmptyTypes,
                        Expression.Convert(
                            span,
                            typeof(ReadOnlySpan<T>)
                        ),
                        span
                    )
                );
            }

            if (!zeroField.FieldType.IsPrimitive)
                throw new NotSupportedException("Not yet supported (todo)");
            
            throw new NotSupportedException($"Unsupported inline array field type {zeroField.FieldType}");
        }

        return Expression.Block(
            typeof(T).GetFields().SelectMany<FieldInfo, Expression>(fieldInfo =>
            {
                if (fieldInfo.GetCustomAttribute<NonSerializedAttribute>() is not null)
                    return [];

                if (_swapPrimTypes.Value.Contains(fieldInfo.FieldType))
                {
                    // field = BinaryPrimitives.ReverseEndianness(field);
                    return
                    [
                        Expression.Assign(
                            field,
                            Expression.Call(
                                typeof(BinaryPrimitives),
                                nameof(BinaryPrimitives.ReverseEndianness),
                                Type.EmptyTypes,
                                field
                            )
                        )
                    ];
                }

                throw new NotSupportedException($"Unsupported field type {fieldInfo.FieldType}");
            })
        );
    }

    public Expression<Func<Stream, T>> CreateReaderExpression<T>()
    {
        var buffer = Expression.Variable(typeof(Span<byte>), "buffer");
        var mem = Expression.Variable(typeof(IMemoryOwner<byte>), "mem");
        var size = Marshal.SizeOf<T>();
        var stream = Expression.Parameter(typeof(Stream), "stream");

        // public T ReadValue(Stream stream) {
        return Expression.Lambda<Func<Stream, T>>(
            Expression.Block([buffer, mem],
                // try {
                Expression.TryFinally(
                    Expression.Block(
                        // var mem = MemoryPool<byte>.Shared.Rent(size);
                        Expression.Assign(
                            mem,
                            Expression.Call(
                                Expression.Constant(
                                    MemoryPool<byte>.Shared
                                ),
                                nameof(MemoryPool<byte>.Shared.Rent),
                                Type.EmptyTypes,
                                Expression.Constant(size)
                            )
                        ),
                        // var buffer = mem.Memory.Span.Slice(0, size);
                        Expression.Assign(
                            buffer,
                            Expression.Call(
                                Expression.Property(
                                    Expression.Property(
                                        mem,
                                        nameof(IMemoryOwner<byte>.Memory)
                                    ),
                                    nameof(Memory<byte>.Span)
                                ),
                                nameof(Span<byte>.Slice),
                                Type.EmptyTypes,
                                Expression.Constant(0),
                                Expression.Constant(size)
                            )
                        ),
                        // stream.ReadExactly(buffer);
                        Expression.Call(
                            stream,
                            nameof(Stream.ReadExactly),
                            Type.EmptyTypes,
                            buffer
                        ),
                        // return MemoryMarshal.Read<T>((ReadOnlySpan<byte>)buffer);
                        Expression.Call(
                            typeof(MemoryMarshal),
                            nameof(MemoryMarshal.Read),
                            [typeof(T)],
                            Expression.Convert(
                                buffer,
                                typeof(ReadOnlySpan<byte>)
                            )
                        )
                    ),
                    // } finally {
                    Expression.Block(
                        // if (mem != default) mem.Dispose();
                        Expression.IfThen(
                            Expression.NotEqual(
                                mem,
                                Expression.Constant(null)
                            ),
                            Expression.Call(
                                Expression.Convert(
                                    mem,
                                    typeof(IDisposable)
                                ),
                                nameof(IDisposable.Dispose),
                                Type.EmptyTypes
                            )
                        )
                    )
                    // }
                )
            ),
            stream
        );
    }

    public Func<Stream, T> CreateReader<T>() =>
        CreateReaderExpression<T>().Compile();
}