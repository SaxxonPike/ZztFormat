using LibRoton.Structures;

namespace LibRoton.Constants;

public class SuperZztElementTable : IElementTable
{
    private static readonly SuperZztElementProperties Default = new()
    {
        Character = 0x20,
        Color = 0xFF,
        Cycle = -1
    };

    private readonly Lazy<List<IElementProperties>> _properties = new(() =>
    [
        new SuperZztElementProperties
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
        new SuperZztElementProperties
        {
            Name = "Monitor",
            Character = 0x02,
            Color = 0x1F,
            PushableBit = 1,
            Cycle = 1
        },
        new SuperZztElementProperties
        {
            Name = "Player",
            Character = 0x02,
            Color = 0x1F,
            DestructibleBit = 1,
            PushableBit = 1,
            Cycle = 1,
            Menu = 1,
            MenuKey = 90,
            EditorCategoryText = "Items:"
        },
        new SuperZztElementProperties
        {
            Name = "Ammo",
            Character = 0x84,
            Color = 0x03,
            PushableBit = 1,
            Cycle = -1,
            Menu = 1,
            MenuKey = 65
        },
        Default,
        new SuperZztElementProperties
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
        new SuperZztElementProperties
        {
            Name = "Key",
            Character = 0x0C,
            Color = 0xFF,
            PushableBit = 1,
            Cycle = -1,
            Menu = 1,
            MenuKey = 75
        },
        new SuperZztElementProperties
        {
            Name = "Door",
            Character = 0x0A,
            Color = 0xFE,
            Cycle = -1,
            Menu = 1,
            MenuKey = 68
        },
        new SuperZztElementProperties
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
        new SuperZztElementProperties
        {
            Name = "Passage",
            Character = 0xF0,
            Color = 0xFE,
            Menu = 1,
            MenuKey = 80,
            EditorBoardText = "Room thru passage?"
        },
        new SuperZztElementProperties
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
        new SuperZztElementProperties
        {
            Name = "Bomb",
            Character = 0x0B,
            Color = 0xFF,
            PushableBit = 1,
            Cycle = 6,
            Menu = 1,
            MenuKey = 66
        },
        new SuperZztElementProperties
        {
            Name = "Energizer",
            Character = 0x7F,
            Color = 0x05,
            Cycle = -1,
            Menu = 1,
            MenuKey = 69
        },
        Default,
        new SuperZztElementProperties
        {
            Name = "Clockwise",
            Character = 0x2F,
            Color = 0xFF,
            Cycle = 3,
            Menu = 1,
            MenuKey = 49,
            EditorCategoryText = "Conveyors:"
        },
        new SuperZztElementProperties
        {
            Name = "Counter",
            Character = 0x5C,
            Color = 0xFF,
            Cycle = 2,
            Menu = 1,
            MenuKey = 50
        },
        Default,
        new SuperZztElementProperties
        {
            Name = "Lava",
            Character = 0x6F,
            Color = 0x4E,
            EditorFloorBit = 1,
            Cycle = -1,
            Menu = 3,
            MenuKey = 76,
            EditorCategoryText = "Terrains:"
        },
        new SuperZztElementProperties
        {
            Name = "Forest",
            Character = 0xB0,
            Color = 0x20,
            Cycle = -1,
            Menu = 3,
            MenuKey = 70
        },
        new SuperZztElementProperties
        {
            Name = "Solid",
            Character = 0xDB,
            Color = 0xFF,
            Cycle = -1,
            Menu = 3,
            MenuKey = 83,
            EditorCategoryText = "Walls:"
        },
        new SuperZztElementProperties
        {
            Name = "Normal",
            Character = 0xB2,
            Color = 0xFF,
            Cycle = -1,
            Menu = 3,
            MenuKey = 78
        },
        new SuperZztElementProperties
        {
            Name = "Breakable",
            Character = 0xB1,
            Color = 0xFF,
            Cycle = -1,
            Menu = 3,
            MenuKey = 66
        },
        new SuperZztElementProperties
        {
            Name = "Boulder",
            Character = 0xFE,
            Color = 0xFF,
            PushableBit = 1,
            Cycle = -1,
            Menu = 3,
            MenuKey = 79
        },
        new SuperZztElementProperties
        {
            Name = "Slider (NS)",
            Character = 0x12,
            Color = 0xFF,
            Cycle = -1,
            Menu = 3,
            MenuKey = 49
        },
        new SuperZztElementProperties
        {
            Name = "Slider (EW)",
            Character = 0x1D,
            Color = 0xFF,
            Cycle = -1,
            Menu = 3,
            MenuKey = 50
        },
        new SuperZztElementProperties
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
        new SuperZztElementProperties
        {
            Name = "Invisible",
            Character = 0x20,
            Color = 0xFF,
            Cycle = -1,
            Menu = 3,
            MenuKey = 73
        },
        new SuperZztElementProperties
        {
            Name = "Blink wall",
            Character = 0xCE,
            Color = 0xFF,
            Cycle = 1,
            Menu = 3,
            MenuKey = 88,
            EditorP1Text = "Starting time",
            EditorP2Text = "Period",
            EditorStepText = "Wall direction"
        },
        new SuperZztElementProperties
        {
            Name = "Transporter",
            Character = 0xC5,
            Color = 0xFF,
            Cycle = 2,
            Menu = 3,
            MenuKey = 84,
            EditorStepText = "Direction?"
        },
        new SuperZztElementProperties
        {
            Name = "Line",
            Character = 0xCE,
            Color = 0xFF,
            Cycle = -1
        },
        new SuperZztElementProperties
        {
            Name = "Ricochet",
            Character = 0x2A,
            Color = 0x0A,
            Cycle = -1,
            Menu = 3,
            MenuKey = 82
        },
        Default,
        new SuperZztElementProperties
        {
            Name = "Bear",
            Character = 0xEB,
            Color = 0x02,
            DestructibleBit = 1,
            PushableBit = 1,
            Cycle = 3,
            Menu = 2,
            MenuKey = 66,
            EditorCategoryText = "Creatures:",
            EditorP1Text = "Sensitivity?",
            Score = 1
        },
        new SuperZztElementProperties
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
        new SuperZztElementProperties
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
        new SuperZztElementProperties
        {
            Name = "Slime",
            Character = 0x2A,
            Color = 0xFF,
            Cycle = 3,
            Menu = 2,
            MenuKey = 86,
            EditorP2Text = "Movement speed?;FS"
        },
        Default,
        new SuperZztElementProperties
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
        new SuperZztElementProperties
        {
            Name = "Pusher",
            Character = 0x10,
            Color = 0xFF,
            Cycle = 4,
            Menu = 2,
            MenuKey = 80,
            EditorStepText = "Push direction?"
        },
        new SuperZztElementProperties
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
        new SuperZztElementProperties
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
        Default,
        new SuperZztElementProperties
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
        new SuperZztElementProperties
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
        new SuperZztElementProperties
        {
            Name = "Floor",
            Character = 0xB0,
            Color = 0xFF,
            EditorFloorBit = 1,
            FloorBit = 1,
            Cycle = -1,
            Menu = 5,
            MenuKey = 70,
            EditorCategoryText = "Terrains:"
        },
        new SuperZztElementProperties
        {
            Name = "Water N",
            Character = 0x1E,
            Color = 0x19,
            EditorFloorBit = 1,
            FloorBit = 1,
            Cycle = -1,
            Menu = 5,
            MenuKey = 56
        },
        new SuperZztElementProperties
        {
            Name = "Water S",
            Character = 0x1F,
            Color = 0x19,
            EditorFloorBit = 1,
            FloorBit = 1,
            Cycle = -1,
            Menu = 5,
            MenuKey = 50
        },
        new SuperZztElementProperties
        {
            Name = "Water W",
            Character = 0x11,
            Color = 0x19,
            EditorFloorBit = 1,
            FloorBit = 1,
            Cycle = -1,
            Menu = 5,
            MenuKey = 52
        },
        new SuperZztElementProperties
        {
            Name = "Water E",
            Character = 0x10,
            Color = 0x19,
            EditorFloorBit = 1,
            FloorBit = 1,
            Cycle = -1,
            Menu = 5,
            MenuKey = 54
        },
        Default,
        Default,
        Default,
        Default,
        Default,
        Default,
        Default,
        new SuperZztElementProperties
        {
            Name = "Roton",
            Character = 0x94,
            Color = 0x0D,
            DestructibleBit = 1,
            PushableBit = 1,
            Cycle = 1,
            Menu = 4,
            MenuKey = 82,
            EditorCategoryText = "Uglies:",
            EditorP1Text = "Intelligence?",
            EditorP2Text = "Switch Rate?",
            Score = 2
        },
        new SuperZztElementProperties
        {
            Name = "Dragon Pup",
            Character = 0xED,
            Color = 0x04,
            DestructibleBit = 1,
            PushableBit = 1,
            Cycle = 2,
            Menu = 4,
            MenuKey = 68,
            EditorP1Text = "Intelligence?",
            EditorP2Text = "Switch Rate?",
            Score = 1
        },
        new SuperZztElementProperties
        {
            Name = "Pairer",
            Character = 0xE5,
            Color = 0x01,
            DestructibleBit = 1,
            PushableBit = 1,
            Cycle = 2,
            Menu = 4,
            MenuKey = 80,
            EditorP1Text = "Intelligence?",
            Score = 2
        },
        new SuperZztElementProperties
        {
            Name = "Spider",
            Character = 0x0F,
            Color = 0xFF,
            DestructibleBit = 1,
            Cycle = 1,
            Menu = 4,
            MenuKey = 83,
            EditorP1Text = "Intelligence?",
            Score = 3
        },
        new SuperZztElementProperties
        {
            Name = "Web",
            Character = 0xC5,
            Color = 0xFF,
            EditorFloorBit = 1,
            FloorBit = 1,
            Cycle = -1,
            Menu = 5,
            MenuKey = 87
        },
        new SuperZztElementProperties
        {
            Name = "Stone",
            Character = 0x5A,
            Color = 0x0F,
            Cycle = 1,
            Menu = 5,
            MenuKey = 90
        },
        Default,
        Default,
        Default,
        Default,
        new SuperZztElementProperties
        {
            Name = "Bullet",
            Character = 0xF8,
            Color = 0x0F,
            DestructibleBit = 1,
            Cycle = 1
        },
        new SuperZztElementProperties
        {
            Character = 0xCD,
            Color = 0xFF,
            Cycle = -1
        },
        new SuperZztElementProperties
        {
            Character = 0xBA,
            Color = 0xFF,
            Cycle = -1
        },
        new SuperZztElementProperties
        {
            Name = "Star",
            Character = 0x53,
            Color = 0x0F,
            Cycle = 1
        },
        Default,
        Default,
        Default,
        Default,
        Default,
        Default,
        new SuperZztElementProperties
        {
            Character = 0x20,
            Color = 0xFF,
            Cycle = -1
        }
    ]);

    public IElementProperties this[int index] => 
        index is < 0 or > 79
            ? Default
            : _properties.Value[index];

    public int Count =>
        _properties.Value.Count;
}