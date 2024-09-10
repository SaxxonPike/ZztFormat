namespace ZztFormat.Test;

[TestFixture]
public class ElementTests
{
    [Test]
    public void Load_ShouldLoadZztElements()
    {
        var elements = ElementList.Load(WorldType.Zzt);
        Assert.That(elements, Has.Count.EqualTo(54));

        Assert.That(elements[ElementType.Empty]!.Id, Is.EqualTo(0));
        Assert.That(elements[ElementType.Stone], Is.Null);
        Assert.That(elements[ElementType.Player]!.Id, Is.EqualTo(4));
        Assert.That(elements[4]!.Type, Is.EqualTo(ElementType.Player));
        Assert.That(elements[ElementType.BlackText]!.Id, Is.EqualTo(53));
        Assert.That(elements[53]!.Type, Is.EqualTo(ElementType.BlackText));
        
        Assert.That(elements[ElementType.Empty]!.Name, Is.EqualTo("Empty"));
        Assert.That(elements[ElementType.Player]!.EditorCategoryText, Is.EqualTo("Items:"));
        Assert.That(elements[ElementType.BlinkWall]!.EditorP1Text, Is.EqualTo("Starting time"));
        Assert.That(elements[ElementType.BlinkWall]!.EditorP2Text, Is.EqualTo("Period"));
        Assert.That(elements[ElementType.SpinningGun]!.EditorP3Text, Is.EqualTo("Firing type?"));
        Assert.That(elements[ElementType.Passage]!.EditorBoardText, Is.EqualTo("Room thru passage?"));
        Assert.That(elements[ElementType.Duplicator]!.EditorStepText, Is.EqualTo("Source direction?"));
        Assert.That(elements[ElementType.Scroll]!.EditorCodeText, Is.EqualTo("Edit text of scroll"));
        
    }
    
    [Test]
    public void Load_ShouldLoadSuperZztElements()
    {
        var elements = ElementList.Load(WorldType.SuperZzt);
        Assert.That(elements, Has.Count.EqualTo(80));

        Assert.That(elements[ElementType.Empty]!.Id, Is.EqualTo(0));
        Assert.That(elements[ElementType.Torch], Is.Null);
        Assert.That(elements[ElementType.Player]!.Id, Is.EqualTo(4));
        Assert.That(elements[4]!.Type, Is.EqualTo(ElementType.Player));
        Assert.That(elements[ElementType.BlackText]!.Id, Is.EqualTo(79));
        Assert.That(elements[79]!.Type, Is.EqualTo(ElementType.BlackText));
        
        Assert.That(elements[ElementType.Empty]!.Name, Is.EqualTo("Empty"));
        Assert.That(elements[ElementType.Player]!.EditorCategoryText, Is.EqualTo("Items:"));
        Assert.That(elements[ElementType.BlinkWall]!.EditorP1Text, Is.EqualTo("Starting time"));
        Assert.That(elements[ElementType.BlinkWall]!.EditorP2Text, Is.EqualTo("Period"));
        Assert.That(elements[ElementType.SpinningGun]!.EditorP3Text, Is.EqualTo("Firing type?"));
        Assert.That(elements[ElementType.Passage]!.EditorBoardText, Is.EqualTo("Room thru passage?"));
        Assert.That(elements[ElementType.Duplicator]!.EditorStepText, Is.EqualTo("Source direction?"));
        Assert.That(elements[ElementType.Scroll]!.EditorCodeText, Is.EqualTo("Edit text of scroll"));
    }
}