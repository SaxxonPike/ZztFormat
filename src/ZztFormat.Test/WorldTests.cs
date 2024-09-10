using System.Diagnostics;

namespace ZztFormat.Test;

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
        using var worldStream = File.OpenRead(Path.Combine(TestPaths.Files, fileName));

        var sw = new Stopwatch();
        sw.Start();
        var world = World.Read(worldStream);
        sw.Stop();

        TestContext.Out.WriteLine("Elapsed: {0}", sw.Elapsed);
        TestContext.Out.WriteLine("Boards: {0}", world.Boards.Count);
        foreach (var board in world.Boards)
            TestContext.Out.WriteLine(board.Name);
    }

    [TestCase("TOWN.ZZT")]
    [TestCase("ENIGMA.ZZT")]
    [TestCase("BOOFX-D2.ZZT")]
    [TestCase("MONSTER.SZT")]
    public void TestWorldResave(string fileName)
    {
        using var worldStream = File.OpenRead(Path.Combine(TestPaths.Files, fileName));
        var world = World.Read(worldStream);

        var stream = new MemoryStream();
        World.Write(stream, world);
        stream.Flush();
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

    [Test]
    [TestCase(WorldType.Zzt)]
    [TestCase(WorldType.SuperZzt)]
    public void TestWorldSave(WorldType type)
    {
        var world = World.Create(type);
        var stream = new MemoryStream();
        World.Write(stream, world);
        stream.Flush();
    }

    [Test]
    [TestCase(WorldType.Zzt)]
    [TestCase(WorldType.SuperZzt)]
    public void TestWorldSaveFlags(WorldType type)
    {
        var flag = Testing.Random.GetString();
        var expected = flag.ToUpper();
        var world = World.Create(type);
        world.Flags.Add(flag);

        var stream = new MemoryStream();
        World.Write(stream, world);
        stream.Flush();

        stream.Position = 0;

        var readBack = World.Read(stream);
        Assert.That(readBack.Flags, Contains.Item(expected[..20]));
    }

    [Test]
    [TestCase(WorldType.Zzt)]
    [TestCase(WorldType.SuperZzt)]
    public void TestWorldSaveKeys(WorldType type)
    {
        var world = World.Create(type);
        var color = Testing.Random.NextEnum<KeyColor>();
        world.Keys.Add(color);

        var stream = new MemoryStream();
        World.Write(stream, world);
        stream.Flush();

        stream.Position = 0;

        var readBack = World.Read(stream);
        Assert.That(readBack.Keys, Contains.Item(color));
    }
}