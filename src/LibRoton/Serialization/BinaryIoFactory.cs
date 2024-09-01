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