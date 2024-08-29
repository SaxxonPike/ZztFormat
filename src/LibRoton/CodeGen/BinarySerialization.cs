using System.Runtime.InteropServices;

namespace LibRoton.CodeGen;

internal static class BinarySerialization
{
    private static void FixEndian(Type type, Span<byte> data)
    {
        // TODO
        throw new NotSupportedException("Fix for non little endian systems.");
    }
    
    public static int Serialize<T>(ReadOnlySpan<T> source, Span<byte> target)
        where T : struct
    {
        var srcVals = MemoryMarshal.Cast<T, byte>(source);
        srcVals.CopyTo(target);
        var dstVals = MemoryMarshal.Cast<byte, T>(target);
        if (!BitConverter.IsLittleEndian)
            FixEndian(typeof(T), target[..srcVals.Length]);
        return dstVals.Length;
    }

    public static int Serialize<T>(ref T source, Span<byte> target)
        where T : struct =>
        Serialize(MemoryMarshal.CreateReadOnlySpan(ref source, 1), target);

    public static byte[] Serialize<T>(T source)
        where T : struct
    {
        var result = new byte[Marshal.SizeOf<T>()];
        Serialize(ref source, result);
        return result;
    }
    
    public static int Serialize<T>(ReadOnlySpan<T> source, Stream target)
        where T : struct
    {
        var srcVals = MemoryMarshal.Cast<T, byte>(source);
        Span<byte> dst = stackalloc byte[srcVals.Length];
        srcVals.CopyTo(dst);
        if (!BitConverter.IsLittleEndian)
            FixEndian(typeof(T), dst);
        target.Write(dst);
        return dst.Length;
    }

    public static int Serialize<T>(ref T source, Stream target)
        where T : struct =>
        Serialize(MemoryMarshal.CreateReadOnlySpan(ref source, 1), target);

    public static int Deserialize<T>(ReadOnlySpan<byte> source, Span<T> target)
        where T : struct
    {
        var srcVals = MemoryMarshal.Cast<byte, T>(source);
        srcVals.CopyTo(target);
        var dstVals = MemoryMarshal.Cast<T, byte>(target);
        if (!BitConverter.IsLittleEndian)
            FixEndian(typeof(T), dstVals);
        return dstVals.Length;
    }

    public static int Deserialize<T>(ReadOnlySpan<byte> source, ref T target)
        where T : struct =>
        Deserialize(source, MemoryMarshal.CreateSpan(ref target, 1));

    public static T Deserialize<T>(ReadOnlySpan<byte> source)
        where T : struct
    {
        var result = default(T);
        Deserialize(source, ref result);
        return result;
    }

    public static int Deserialize<T>(Stream source, Span<T> target)
        where T : struct
    {
        var dstVals = MemoryMarshal.Cast<T, byte>(target);
        source.ReadAtLeast(dstVals, dstVals.Length);
        if (!BitConverter.IsLittleEndian)
            FixEndian(typeof(T), dstVals);
        return dstVals.Length;
    }

    public static int Deserialize<T>(Stream source, ref T target)
        where T : struct =>
        Deserialize(source, MemoryMarshal.CreateSpan(ref target, 1));
    
    public static T Deserialize<T>(Stream source)
        where T : struct
    {
        var result = default(T);
        Deserialize(source, ref result);
        return result;
    }

}