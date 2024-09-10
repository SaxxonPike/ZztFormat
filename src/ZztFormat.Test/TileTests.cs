using NUnit.Framework.Internal;

namespace ZztFormat.Test;

[TestFixture]
public class TileTests
{
    [Test]
    [TestCase(0x40, '@')]
    [TestCase(0x41, 'A')]
    [TestCase(0x30, '0')]
    public void Char_ShouldReturnCorrectValue(byte input, char expected)
    {
        var tile = new Tile
        {
            Color = input
        };
        
        Assert.That(tile.Char, Is.EqualTo(expected));
    }

    [Test]
    [TestCase(0x40, '@')]
    [TestCase(0x41, 'A')]
    [TestCase(0x30, '0')]
    public void Char_ShouldSetCorrectValue(byte expected, char input)
    {
        var tile = new Tile
        {
            Char = input
        };
        
        Assert.That(tile.Color, Is.EqualTo(expected));
    }

    [Test]
    public void Equals()
    {
        var element = Testing.Random.NextByte();
        var color = Testing.Random.NextByte();
        
        var tile0 = new Tile
        {
            Element = element,
            Color = color
        };
        
        Assert.That(tile0, Is.Not.EqualTo(new Tile
        {
            Element = Testing.Random.NextByte(),
            Color = color
        }));

        Assert.That(tile0, Is.Not.EqualTo(new Tile
        {
            Element = element,
            Color = Testing.Random.NextByte()
        }));

        Assert.That(tile0, Is.EqualTo(new Tile
        {
            Element = element,
            Color = color
        }));
    }
}