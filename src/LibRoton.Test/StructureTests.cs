using LibRoton.Structures;

namespace LibRoton.Test;

[TestFixture]
public class StructureTests
{
    [Test]
    public void test1()
    {
        var world = new ZztWorldHeader();
        world.WorldName = new ZztWorldString();
        var other = world.WorldName.Text;
        world.WorldName.Text[0] = 1;
        var outp = new byte[100];
    }
}