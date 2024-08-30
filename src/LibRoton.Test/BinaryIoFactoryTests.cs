using System.Runtime.InteropServices;
using LibRoton.Serialization;
using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.Test;

[TestFixture]
public class BinaryIoFactoryTests
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Abc123
    {
        public byte a;
        public byte b;
    }
    
    [Test]
    public void test1()
    {
        var subject = new BinaryIoFactory();
        var reader = subject.CreateReader<Abc123>();
    }
}