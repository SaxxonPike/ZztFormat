using System.Buffers.Binary;

namespace ZztFormat.Test;

[TestFixture]
public class DatTests
{
    [Test]
    public void Read_ShouldReturnCorrectData()
    {
        // Arrange.

        var inStream = File.OpenRead(Path.Combine(TestPaths.Files, "ZZT.DAT"));

        var expectedFileNames = new[]
        {
            "ABOUT.HLP",
            "EDITOR.HLP",
            "CREATURE.HLP",
            "TERRAIN.HLP",
            "ITEM.HLP",
            "LANG.HLP",
            "LANGTUT.HLP",
            "LANGREF.HLP",
            "INFO.HLP",
            "GAME.HLP"
        };

        // Act.

        var entries = Dat.Read(inStream);

        // Assert.

        Assert.That(entries.Select(x => x.Name),
            Is.EquivalentTo(expectedFileNames));
    }

    [Test]
    public void Write_ShouldFailWhenTooManyFiles()
    {
        var entries = Enumerable
            .Range(0, 25)
            .Select(_ => new Dat.Entry())
            .ToList();

        var stream = new MemoryStream();

        Assert.That(() => Dat.Write(stream, entries),
            Throws.TypeOf<ZztFormatException>());
    }

    [Test]
    public void Write_ShouldWriteCorrectData()
    {
        // Arrange: expected data.

        var entries = new List<Dat.Entry>
        {
            new("ABC.DEF", "ghi"),
            new("123.456", "789")
        };

        var expected = new byte[0x0536];

        // Arrange: header

        BinaryPrimitives.WriteInt16LittleEndian(expected.AsSpan(), (short)entries.Count);

        // Arrange: entry 1

        expected[0x002] = 0x07;
        "ABC.DEF"u8.CopyTo(expected.AsSpan(0x003));
        BinaryPrimitives.WriteInt32LittleEndian(expected.AsSpan(0x4CA), 0x052A);

        // Arrange: entry 2

        expected[0x035] = 0x07;
        "123.456"u8.CopyTo(expected.AsSpan(0x036));
        BinaryPrimitives.WriteInt32LittleEndian(expected.AsSpan(0x4CE), 0x0530);

        // Arrange: content 1

        expected[0x052A] = 0x03;
        "ghi"u8.CopyTo(expected.AsSpan(0x052B));
        expected[0x052E] = 0x01;
        expected[0x052F] = 0x40;

        // Arrange: content 2

        expected[0x0530] = 0x03;
        "789"u8.CopyTo(expected.AsSpan(0x0531));
        expected[0x0534] = 0x01;
        expected[0x0535] = 0x40;

        // Arrange: input/output objects.

        var outStream = new MemoryStream();

        // Act.

        Dat.Write(outStream, entries);
        outStream.Flush();

        // Assert.

        var observed = outStream.ToArray();
        Assert.That(observed,
            Is.EquivalentTo(expected));
    }
}