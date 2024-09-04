using LibRoton.Structures;

namespace LibRoton.Test;

[TestFixture]
public class WorldTests
{
    [Test]
    public void test1()
    {
        var worldStream = File.OpenRead(Path.Combine(TestPaths.Files, "TOWN.ZZT"));
        var world = ZztFile.ReadWorld(worldStream);
    }
}