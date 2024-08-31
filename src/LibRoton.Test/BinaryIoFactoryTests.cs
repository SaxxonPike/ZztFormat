using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LibRoton.Serialization;
using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.Test;

[TestFixture]
public class BinaryIoFactoryTests
{
    [StructLayout(LayoutKind.Sequential)]
    private struct OuterStruct
    {
        public byte a;
        public byte b;
        public InnerStruct c;
        public InnerByteArray i;
        public InnerStructArray j;
    }

    [StructLayout(LayoutKind.Explicit, Size = 32)]
    private struct InnerStruct
    {
        [FieldOffset(0)] public byte d;
        [FieldOffset(2)] public byte e;
        [FieldOffset(4)] public int f;
    }

    [InlineArray(10)]
    private struct InnerByteArray
    {
        private byte _zero;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct InnerStructItem
    {
        public byte g;
        public byte h;
    }
    
    [InlineArray(2)]
    private struct InnerStructArray
    {
        private InnerStructItem _zero;
    }
    
    [Test]
    public void test1()
    {
        var subject = new BinaryIoFactory();
        var reader = subject.CreateReaderExpression<OuterStruct>();
        Console.WriteLine(typeof(Expression).GetProperty("DebugView", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(reader));
        var readerFunc = reader.Compile();

        var testBytes = new byte[8192];
        Random.Shared.NextBytes(testBytes);
        var testStream = new MemoryStream(testBytes);
        var output = readerFunc(testStream);
    }
}