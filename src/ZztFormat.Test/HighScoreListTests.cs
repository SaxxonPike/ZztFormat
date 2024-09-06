using System.Buffers.Binary;

namespace ZztFormat.Test;

[TestFixture]
public class HighScoreListTests
{
    [Test]
    [TestCase("TOWN.HI", WorldType.Zzt)]
    public void Read_ReturnsCorrectScores(string fileName, WorldType worldType)
    {
        var inStream = File.OpenRead(Path.Combine(TestPaths.Files, fileName));
        var list = HighScoreList.Read(inStream, worldType);

        Assert.That(list.Scores, Is.EquivalentTo(new List<HighScoreList.Entry>
        {
            new() { Name = "Tim Sweeney", Score = 14690 },
            new() { Name = "Neil Tender", Score = 12783 }
        }));
    }

    [Test]
    [TestCase(WorldType.Zzt)]
    [TestCase(WorldType.SuperZzt)]
    public void Write_WritesCorrectScores(WorldType worldType)
    {
        var (expectedBytes, scoreLength) = worldType switch
        {
            WorldType.Zzt => (new byte[ZztHighScoreList.Size], ZztHighScore.Size),
            WorldType.SuperZzt => (new byte[SuperZztHighScoreList.Size], SuperZztHighScore.Size)
        };

        var outStream = new MemoryStream();
        var list = new HighScoreList
        {
            Scores =
            [
                new() { Name = "Saxxon Fox", Score = 420 },
                new() { Name = "Noxxas Xof", Score = 69 }
            ]
        };

        for (int i = 0, j = 0; i < 30; i++, j += scoreLength)
        {
            if (i < list.Scores.Count)
            {
                expectedBytes[j] = (byte)list.Scores[i].Name.Length;
                CodePage437.Encoding.GetBytes(list.Scores[i].Name, expectedBytes.AsSpan(j + 1));
                BinaryPrimitives.WriteInt16LittleEndian(expectedBytes.AsSpan(j + scoreLength - 4),
                    (short)list.Scores[i].Score);
            }
            else
            {
                expectedBytes[j] = 0;
                BinaryPrimitives.WriteInt16LittleEndian(expectedBytes.AsSpan(j + scoreLength - 4), -1);
            }
        }

        HighScoreList.Write(outStream, worldType, list);
        outStream.Flush();

        outStream.Position = 0;
        var outBytes = outStream.ToArray();

        Assert.That(outBytes, Is.EquivalentTo(expectedBytes));
    }
}