        using LibRoton.Structures;

namespace LibRoton.Test;

[TestFixture]
public class ElementTests
{
    private void Test_Elements(IEnumerable<IElementProperties> elements)
    {
        foreach (var element in elements)
        {
            Console.Write($"new {element.GetType().Name} {{ ");
            if (element.Name.Length > 0)
                Console.Write($"Name = \"{element.Name}\", ");
            if (element.Character != 0)
                Console.Write($"Character = 0x{element.Character:X2}, ");
            if (element.Color != 0)
                Console.Write($"Color = 0x{element.Color:X2}, ");
            if (element.IsDestructible)
                Console.Write($"IsDestructible = {element.IsDestructible}, ");
            if (element.IsPushable)
                Console.Write($"IsPushable = {element.IsPushable}, ");
            if (element.IsEditorFloor)
                Console.Write($"IsEditorFloor = {element.IsEditorFloor}, ");
            if (element.IsFloor)
                Console.Write($"IsFloor = {element.IsFloor}, ");
            if (element.IsAlwaysVisible)
                Console.Write($"IsAlwaysVisible = {element.IsAlwaysVisible}, ");
            if (element.Cycle != 0)
                Console.Write($"Cycle = {element.Cycle}, ");
            if (element.Menu != 0)
                Console.Write($"Menu = {element.Menu}, ");
            if (element.MenuKey != 0) 
                Console.Write($"MenuKey = {element.MenuKey}, ");
            if (element.EditorCategoryText.Length != 0)
                Console.Write($"EditorCategoryText = \"{element.EditorCategoryText}\", ");
            if (element.EditorP1Text.Length != 0)
                Console.Write($"EditorP1Text = \"{element.EditorP1Text}\", ");
            if (element.EditorP2Text.Length != 0)
                Console.Write($"EditorP2Text = \"{element.EditorP2Text}\", ");
            if (element.EditorP3Text.Length != 0)
                Console.Write($"EditorP3Text = \"{element.EditorP3Text}\", ");
            if (element.EditorBoardText.Length != 0)
                Console.Write($"EditorBoardText = \"{element.EditorBoardText}\", ");
            if (element.EditorStepText.Length != 0)
                Console.Write($"EditorStepText = \"{element.EditorStepText}\", ");
            if (element.EditorCodeText.Length != 0)
                Console.Write($"EditorCodeText = \"{element.EditorCodeText}\", ");
            if (element.Score != 0)
                Console.Write($"Score = {element.Score}, ");
            Console.WriteLine("},");
        }
    }
    
    [Explicit]
    [Test]
    public void Test_ZztElements()
    {
        using var stream = File.OpenRead(Path.Combine(TestPaths.Files, "ZZT.ELE"));
        var total = stream.Length / ZztElementProperties.Size;
        var elements = new List<ZztElementProperties>();
        for (var i = 0; i < total; i++)
            elements.Add(ZztElementProperties.Read(stream));
        Test_Elements(elements);
    }
    
    [Explicit]
    [Test]
    public void Test_SuperZztElements()
    {
        using var stream = File.OpenRead(Path.Combine(TestPaths.Files, "SUPERZ.ELE"));
        var total = stream.Length / SuperZztElementProperties.Size;
        var elements = new List<SuperZztElementProperties>();
        for (var i = 0; i < total; i++)
            elements.Add(SuperZztElementProperties.Read(stream));
        Test_Elements(elements);
    }
}