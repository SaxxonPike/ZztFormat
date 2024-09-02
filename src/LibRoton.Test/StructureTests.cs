using System.Text;
using LibRoton.Structures;

namespace LibRoton.Test;

[TestFixture]
[Parallelizable]
public class StructureTests
{
    [Test]
    public void Test_StringExtension_Ascii()
    {
        var subject = new ZztWorldString
        {
            Text = "testing"
        };

        Assert.That(subject.Length, Is.EqualTo(7));
        Assert.That(subject.Bytes.AsSpan()[..7].SequenceEqual("testing"u8));
    }

    [Test]
    public void Test_StringExtension_NonAscii()
    {
        var subject = new ZztWorldString
        {
            Text = "\u2591\u2592\u2593"
        };

        Assert.That(subject.Length, Is.EqualTo(3));
        Assert.That(subject.Bytes.AsSpan()[..3].SequenceEqual(new byte[]
        {
            0xB0, 0xB1, 0xB2
        }));
    }
}