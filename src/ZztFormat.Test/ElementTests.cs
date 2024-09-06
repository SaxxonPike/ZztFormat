namespace ZztFormat.Test;

[TestFixture]
public class ElementTests
{
    [Test]
    public void test1()
    {
        var elements = ElementList.Load(WorldType.SuperZzt);
    }
}