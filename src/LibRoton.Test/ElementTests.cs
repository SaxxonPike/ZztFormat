        using LibRoton.Structures;

namespace LibRoton.Test;

[TestFixture]
public class ElementTests
{
    [Explicit]
    [Test]
    public void Test_ZztElements()
    {
        using var stream = File.OpenRead(Path.Combine(TestPaths.Files, "ZZT.ELE"));
        var total = stream.Length / ZztElementProperties.Size;
        var inElements = new List<ZztElementProperties>();
        for (var i = 0; i < total; i++)
            inElements.Add(ZztElementProperties.Read(stream));

        var outStream = File.Open(Path.Combine(TestPaths.Files, "ZZT.ELE.OUT"), FileMode.Create);
        foreach (var element in inElements)
        {
            new ZztElementProperties
            {
                Character = element.Character,
                Color = element.Color,
                DestructibleBit = element.DestructibleBit,
                PushableBit = element.PushableBit,
                AlwaysVisibleBit = element.AlwaysVisibleBit,
                EditorFloorBit = element.EditorFloorBit,
                FloorBit = element.FloorBit,
                DrawFuncBit = element.DrawFuncBit,
                DrawFunc = element.DrawFunc,
                Cycle = element.Cycle,
                ActFunc = element.ActFunc,
                InteractFunc = element.InteractFunc,
                Menu = element.Menu,
                MenuKey = element.MenuKey,
                Score = 0,
                Name = element.Name,
                EditorCategoryText = element.EditorCategoryText,
                EditorP1Text = element.EditorP1Text,
                EditorP2Text = element.EditorP2Text,
                EditorP3Text = element.EditorP3Text,
                EditorBoardText = element.EditorBoardText,
                EditorStepText = element.EditorStepText,
                EditorCodeText = element.EditorCodeText
            }.Write(outStream);
        }
        
        outStream.Flush();
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
    }
}