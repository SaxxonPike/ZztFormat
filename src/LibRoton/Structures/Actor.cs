namespace LibRoton.Structures;

public struct Vec2
{
    public int X;
    public int Y;

    public RawPosition ToRawPosition() =>
        new() { X = (byte)X, Y = (byte)Y };

    public RawVector ToRawVector() =>
        new() { X = (short)X, Y = (short)Y };

    public static Vec2 FromRawPosition(RawPosition position) =>
        new() { X = position.X, Y = position.Y };

    public static Vec2 FromRawVector(RawVector vector) =>
        new() { X = vector.X, Y = vector.Y };
}

public struct Tile
{
    public Element Element;
    public DosColor ForegroundColor;
    public DosColor BackgroundColor;
    public bool Blink;

    public RawTile ToRawTile(Func<Element, byte> elementMap) =>
        new()
        {
            ElementId = elementMap(Element),
            Color = unchecked((byte)(((int)ForegroundColor & 0xF) |
                                     (((int)BackgroundColor & 0x7) << 4) |
                                     (Blink ? 0x80 : 0x00)))
        };

    public static Tile FromRawTile(RawTile tile, Func<byte, Element> elementMap) =>
        new()
        {
            Element = elementMap(tile.ElementId),
            ForegroundColor = (DosColor)(tile.Color & 0xF),
            BackgroundColor = (DosColor)((tile.Color >> 4) & 0x7),
            Blink = (tile.Color & 0x80) != 0
        };
}

public class Actor
{
    public Vec2 Position { get; set; }
    public Vec2 Step { get; set; }
    public int Cycle { get; set; } = -1;
    public int[] Parameters { get; set; } = new int[3];
    public int Follower { get; set; } = -1;
    public int Leader { get; set; } = -1;
    public Tile Under { get; set; }
    public int Instruction { get; set; }
    public int Script { get; set; } = -1;
}

public class World
{
    public int Type { get; set; }
    public int Ammo { get; set; }
    public int Gems { get; set; }
    public HashSet<KeyColor> Keys { get; set; } = [];
    public int Health { get; set; }
    public int StartBoard { get; set; }
    public int Torches { get; set; }
    public int TorchCycles { get; set; }
    public int EnergyCycles { get; set; }
    public int Score { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Flags { get; set; } = [];
    public TimeSpan TimePassed { get; set; }
    public bool Locked { get; set; }
    public int Stones { get; set; }
    public List<Board> Boards { get; set; }
}

public class Board
{
    public string Name { get; set; } = string.Empty;
    public Tile[] Tiles { get; set; } = [];
    public int MaxShots { get; set; }
    public bool IsDark { get; set; }
    public Exits Exits { get; set; } = new();
    public bool RestartOnZap { get; set; }
    public string Message { get; set; } = string.Empty;
    public Vec2 Enter { get; set; }
    public TimeSpan TimeLimit { get; set; }
    public Vec2 Camera { get; set; }
    public List<Actor> Actors { get; set; } = [];
    public List<string> Scripts { get; set; } = [];
    public int Width { get; set; }
    public int Height { get; set; }
    public int MaxActorCount { get; set; }
}

public enum DrawBehavior
{
}

public enum ActBehavior
{
}

public enum InteractBehavior
{
}

public class ElementProperties
{
    public required Element Element { get; init; }
    public int Character { get; set; }
    public DosColor ForegroundColor { get; set; }
    public DosColor BackgroundColor { get; set; }
    public bool Blink { get; set; }
    public bool EditorForceExactColor { get; set; }
    public bool EditorForceWhite { get; set; }
    public bool IsDestructible { get; set; }
    public bool IsPushable { get; set; }
    public bool IsAlwaysVisible { get; set; }
    public bool IsEditorFloor { get; set; }
    public bool IsFloor { get; set; }
    public bool HasDrawFunc { get; set; }
    public DrawBehavior DrawFunc { get; set; }
    public int Cycle { get; set; }
    public ActBehavior ActFunc { get; set; }
    public InteractBehavior InteractFunc { get; set; }
    public int Menu { get; set; }
    public char MenuKey { get; set; }
    public int Score { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EditorCategory { get; set; } = string.Empty;
    public string EditorP1Text { get; set; } = string.Empty;
    public string EditorP2Text { get; set; } = string.Empty;
    public string EditorP3Text { get; set; } = string.Empty;
    public string EditorBoardText { get; set; } = string.Empty;
    public string EditorStepText { get; set; } = string.Empty;
    public string EditorCodeText { get; set; } = string.Empty;
}