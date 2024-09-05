using System.Diagnostics;

namespace LibRoton.Test;

[TestFixture]
public class WorldTests
{
    [Test]
    // Standard worlds
    [TestCase("TOWN.ZZT")]
    [TestCase("ENIGMA.ZZT")]
    // Corrupt boards
    [TestCase("BOOFX-D2.ZZT")]
    // Super ZZT
    [TestCase("MONSTER.SZT")]
    public void TestWorldLoad(string fileName)
    {
        var worldStream = File.OpenRead(Path.Combine(TestPaths.Files, fileName));

        var sw = new Stopwatch();
        sw.Start();
        var world = World.Read(worldStream);
        sw.Stop();

        TestContext.Out.WriteLine("Elapsed: {0}", sw.Elapsed);
        TestContext.Out.WriteLine("Boards: {0}", world.Boards.Count);
        foreach (var board in world.Boards)
            TestContext.Out.WriteLine(board.Name);
    }

    [Test]
    [TestCase("TOWN.BRD", WorldType.Zzt)]
    public void TestBoardLoad(string fileName, WorldType type)
    {
        var boardStream = File.OpenRead(Path.Combine(TestPaths.Files, fileName));

        var sw = new Stopwatch();
        sw.Start();
        var board = Board.Read(boardStream, type);
        sw.Stop();

        TestContext.Out.WriteLine("Elapsed: {0}", sw.Elapsed);
        TestContext.Out.WriteLine("Board: {0}", board.Name);
    }
}