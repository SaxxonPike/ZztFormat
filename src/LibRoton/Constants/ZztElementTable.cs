using LibRoton.Structures;

namespace LibRoton.Constants;

public class ZztElementTable : IElementTable
{
    private static readonly ZztElementProperties Default = new()
    {
        Character = 0x20,
        Color = 0xFF,
        Cycle = -1
    };

    private readonly Lazy<List<IElementProperties>> _properties = new(() =>
    [
        new ZztElementProperties
        {
            Name = "Empty",
            Character = 0x20,
            Color = 0x70,
            PushableBit = 1,
            FloorBit = 1,
            Cycle = -1
        },
        Default,
        Default,
        new ZztElementProperties
        {
            Name = "Monitor",
            Character = 0x20,
            Color = 0x07,
            Cycle = 1
        },
        new ZztElementProperties
        {
            Name = "Player",
            Character = 0x02,
            Color = 0x1F,
            DestructibleBit = 1,
            PushableBit = 1,
            AlwaysVisibleBit = 1,
            Cycle = 1,
            Menu = 1,
            MenuKey = 90,
            EditorCategoryText = "Items:"
        },
        new ZztElementProperties
        {
            Name = "Ammo",
            Character = 0x84,
            Color = 0x03,
            PushableBit = 1,
            Cycle = -1,
            Menu = 1,
            MenuKey = 65
        },
        new ZztElementProperties
        {
            Name = "Torch",
            Character = 0x9D,
            Color = 0x06,
            AlwaysVisibleBit = 1,
            Cycle = -1,
            Menu = 1,
            MenuKey = 84
        },
        new ZztElementProperties
        {
            Name = "Gem",
            Character = 0x04,
            Color = 0xFF,
            DestructibleBit = 1,
            PushableBit = 1,
            Cycle = -1,
            Menu = 1,
            MenuKey = 71
        },
        new ZztElementProperties
        {
            Name = "Key",
            Character = 0x0C,
            Color = 0xFF,
            PushableBit = 1,
            Cycle = -1,
            Menu = 1,
            MenuKey = 75
        },
        new ZztElementProperties
        {
            Name = "Door",
            Character = 0x0A,
            Color = 0xFE,
            Cycle = -1,
            Menu = 1,
            MenuKey = 68
        },
        new ZztElementProperties
        {
            Name = "Scroll",
            Character = 0xE8,
            Color = 0x0F,
            PushableBit = 1,
            Cycle = 1,
            Menu = 1,
            MenuKey = 83,
            EditorCodeText = "Edit text of scroll"
        },
        new ZztElementProperties
        {
            Name = "Passage",
            Character = 0xF0,
            Color = 0xFE,
            AlwaysVisibleBit = 1,
            Cycle = 0,
            Menu = 1,
            MenuKey = 80,
            EditorBoardText = "Room thru passage?"
        },
        new ZztElementProperties
        {
            Name = "Duplicator",
            Character = 0xFA,
            Color = 0x0F,
            Cycle = 2,
            Menu = 1,
            MenuKey = 85,
            EditorP2Text = "Duplication rate?;SF",
            EditorStepText = "Source direction?"
        },
        new ZztElementProperties
        {
            Name = "Bomb",
            Character = 0x0B,
            Color = 0xFF,
            PushableBit = 1,
            Cycle = 6,
            Menu = 1,
            MenuKey = 66
        },
        new ZztElementProperties
        {
            Name = "Energizer",
            Character = 0x7F,
            Color = 0x05,
            Cycle = -1,
            Menu = 1,
            MenuKey = 69
        },
        new ZztElementProperties
        {
            Name = "Star",
            Character = 0x53,
            Color = 0x0F,
            Cycle = 1
        },
        new ZztElementProperties
        {
            Name = "Clockwise",
            Character = 0x2F,
            Color = 0xFF,
            Cycle = 3,
            Menu = 1,
            MenuKey = 49,
            EditorCategoryText = "Conveyors:"
        },
        new ZztElementProperties
        {
            Name = "Counter",
            Character = 0x5C,
            Color = 0xFF,
            Cycle = 2,
            Menu = 1,
            MenuKey = 50
        },
        new ZztElementProperties
        {
            Name = "Bullet",
            Character = 0xF8,
            Color = 0x0F,
            DestructibleBit = 1,
            Cycle = 1
        },
        new ZztElementProperties
        {
            Name = "Water",
            Character = 0xB0,
            Color = 0xF9,
            EditorFloorBit = 1,
            Cycle = -1,
            Menu = 3,
            MenuKey = 87,
            EditorCategoryText = "Terrains:"
        },
        new ZztElementProperties
        {
            Name = "Forest",
            Character = 0xB0,
            Color = 0x20,
            Cycle = -1,
            Menu = 3,
            MenuKey = 70
        },
        new ZztElementProperties
        {
            Name = "Solid",
            Character = 0xDB,
            Color = 0xFF,
            Cycle = -1,
            Menu = 3,
            MenuKey = 83,
            EditorCategoryText = "Walls:"
        },
        new ZztElementProperties
        {
            Name = "Normal",
            Character = 0xB2,
            Color = 0xFF,
            Cycle = -1,
            Menu = 3,
            MenuKey = 78
        },
        new ZztElementProperties
        {
            Name = "Breakable",
            Character = 0xB1,
            Color = 0xFF,
            Cycle = -1,
            Menu = 3,
            MenuKey = 66
        },
        new ZztElementProperties
        {
            Name = "Boulder",
            Character = 0xFE,
            Color = 0xFF,
            PushableBit = 1,
            Cycle = -1,
            Menu = 3,
            MenuKey = 79
        },
        new ZztElementProperties
        {
            Name = "Slider (NS)",
            Character = 0x12,
            Color = 0xFF,
            Cycle = -1,
            Menu = 3,
            MenuKey = 49
        },
        new ZztElementProperties
        {
            Name = "Slider (EW)",
            Character = 0x1D,
            Color = 0xFF,
            Cycle = -1,
            Menu = 3,
            MenuKey = 50
        },
        new ZztElementProperties
        {
            Name = "Fake",
            Character = 0xB2,
            Color = 0xFF,
            EditorFloorBit = 1,
            FloorBit = 1,
            Cycle = -1,
            Menu = 3,
            MenuKey = 65
        },
        new ZztElementProperties
        {
            Name = "Invisible",
            Character = 0xB0,
            Color = 0xFF,
            Cycle = -1,
            Menu = 3,
            MenuKey = 73
        },
        new ZztElementProperties
        {
            Name = "Blink wall",
            Character = 0xCE,
            Color = 0xFF,
            Cycle = 1,
            Menu = 3,
            MenuKey = 76,
            EditorP1Text = "Starting time",
            EditorP2Text = "Period",
            EditorStepText = "Wall direction"
        },
        new ZztElementProperties
        {
            Name = "Transporter",
            Character = 0xC5,
            Color = 0xFF,
            Cycle = 2,
            Menu = 3,
            MenuKey = 84,
            EditorStepText = "Direction?"
        },
        new ZztElementProperties
        {
            Name = "Line",
            Character = 0xCE,
            Color = 0xFF,
            Cycle = -1
        },
        new ZztElementProperties
        {
            Name = "Ricochet",
            Character = 0x2A,
            Color = 0x0A,
            Cycle = -1,
            Menu = 3,
            MenuKey = 82
        },
        new ZztElementProperties
        {
            Character = 0xCD,
            Color = 0xFF,
            Cycle = -1
        },
        new ZztElementProperties
        {
            Name = "Bear",
            Character = 0x99,
            Color = 0x06,
            DestructibleBit = 1,
            PushableBit = 1,
            Cycle = 3,
            Menu = 2,
            MenuKey = 66,
            EditorCategoryText = "Creatures:",
            EditorP1Text = "Sensitivity?",
            Score = 1
        },
        new ZztElementProperties
        {
            Name = "Ruffian",
            Character = 0x05,
            Color = 0x0D,
            DestructibleBit = 1,
            PushableBit = 1,
            Cycle = 1,
            Menu = 2,
            MenuKey = 82,
            EditorP1Text = "Intelligence?",
            EditorP2Text = "Resting time?",
            Score = 2
        },
        new ZztElementProperties
        {
            Name = "Object",
            Character = 0x02,
            Color = 0xFF,
            Cycle = 3,
            Menu = 2,
            MenuKey = 79,
            EditorP1Text = "Character?",
            EditorCodeText = "Edit Program"
        },
        new ZztElementProperties
        {
            Name = "Slime",
            Character = 0x2A,
            Color = 0xFF,
            Cycle = 3,
            Menu = 2,
            MenuKey = 86,
            EditorP2Text = "Movement speed?;FS"
        },
        new ZztElementProperties
        {
            Name = "Shark",
            Character = 0x5E,
            Color = 0x07,
            Cycle = 3,
            Menu = 2,
            MenuKey = 89,
            EditorP1Text = "Intelligence?"
        },
        new ZztElementProperties
        {
            Name = "Spinning gun",
            Character = 0x18,
            Color = 0xFF,
            Cycle = 2,
            Menu = 2,
            MenuKey = 71,
            EditorP1Text = "Intelligence?",
            EditorP2Text = "Firing rate?",
            EditorP3Text = "Firing type?"
        },
        new ZztElementProperties
        {
            Name = "Pusher",
            Character = 0x10,
            Color = 0xFF,
            Cycle = 4,
            Menu = 2,
            MenuKey = 80,
            EditorStepText = "Push direction?"
        },
        new ZztElementProperties
        {
            Name = "Lion",
            Character = 0xEA,
            Color = 0x0C,
            DestructibleBit = 1,
            PushableBit = 1,
            Cycle = 2,
            Menu = 2,
            MenuKey = 76,
            EditorCategoryText = "Beasts:",
            EditorP1Text = "Intelligence?",
            Score = 1
        },
        new ZztElementProperties
        {
            Name = "Tiger",
            Character = 0xE3,
            Color = 0x0B,
            DestructibleBit = 1,
            PushableBit = 1,
            Cycle = 2,
            Menu = 2,
            MenuKey = 84,
            EditorP1Text = "Intelligence?",
            EditorP2Text = "Firing rate?",
            EditorP3Text = "Firing type?",
            Score = 2
        },
        new ZztElementProperties
        {
            Character = 0xBA,
            Color = 0xFF,
            Cycle = -1
        },
        new ZztElementProperties
        {
            Name = "Head",
            Character = 0xE9,
            Color = 0xFF,
            DestructibleBit = 1,
            Cycle = 2,
            Menu = 2,
            MenuKey = 72,
            EditorCategoryText = "Centipedes",
            EditorP1Text = "Intelligence?",
            EditorP2Text = "Deviance?",
            Score = 1
        },
        new ZztElementProperties
        {
            Name = "Segment",
            Character = 0x4F,
            Color = 0xFF,
            DestructibleBit = 1,
            Cycle = 2,
            Menu = 2,
            MenuKey = 83,
            Score = 3
        },
        Default,
        Default,
        Default,
        Default,
        Default,
        Default,
        Default,
        Default
    ]);

    public IElementProperties this[int index] => 
        index is < 0 or > 53
            ? Default
            : _properties.Value[index];

    public int Count =>
        _properties.Value.Count;
}