namespace LibRoton;

public static class ElementList
{
    public static List<Element> Load(WorldType worldType)
    {
        return worldType switch
        {
            WorldType.Zzt => LoadZztElements(),
            WorldType.SuperZzt => LoadSuperZztElements(),
            _ => throw new Exception($"Unknown world type {worldType}.")
        };
    }

    internal static List<Element> LoadZztElements()
    {
        var bytes = Resources.ZztElements;
        var count = bytes.Length / ZztElementProperties.Size;
        var result = new List<Element>();
        var offset = 0;

        for (var i = 0; i < count; i++)
        {
            var element = Element.ReadZztElement(bytes[offset..]);
            offset += ZztElementProperties.Size;
            element.Type = MapZztElement(i);
            element.Id = i;
            result.Add(element);
        }

        return result;
    }

    internal static List<Element> LoadSuperZztElements()
    {
        var bytes = Resources.SuperZztElements;
        var count = bytes.Length / SuperZztElementProperties.Size;
        var result = new List<Element>();
        var offset = 0;

        for (var i = 0; i < count; i++)
        {
            var element = Element.ReadSuperZztElement(bytes[offset..]);
            offset += SuperZztElementProperties.Size;
            element.Type = MapSuperZztElement(i);
            element.Id = i;
            result.Add(element);
        }

        return result;
    }

    internal static ElementType MapZztElement(int i) =>
        i switch
        {
            0 => ElementType.Empty,
            1 => ElementType.BoardEdge,
            2 => ElementType.Messenger,
            3 => ElementType.Monitor,
            4 => ElementType.Player,
            5 => ElementType.Ammo,
            6 => ElementType.Torch,
            7 => ElementType.Gem,
            8 => ElementType.Key,
            9 => ElementType.Door,
            10 => ElementType.Scroll,
            11 => ElementType.Passage,
            12 => ElementType.Duplicator,
            13 => ElementType.Bomb,
            14 => ElementType.Energizer,
            15 => ElementType.Star,
            16 => ElementType.Clockwise,
            17 => ElementType.Counter,
            18 => ElementType.Bullet,
            19 => ElementType.Water,
            20 => ElementType.Forest,
            21 => ElementType.Solid,
            22 => ElementType.Normal,
            23 => ElementType.Breakable,
            24 => ElementType.Boulder,
            25 => ElementType.SliderNs,
            26 => ElementType.SliderEw,
            27 => ElementType.Fake,
            28 => ElementType.Invisible,
            29 => ElementType.BlinkWall,
            30 => ElementType.Transporter,
            31 => ElementType.Line,
            32 => ElementType.Ricochet,
            33 => ElementType.BlinkRayH,
            34 => ElementType.Bear,
            35 => ElementType.Ruffian,
            36 => ElementType.Object,
            37 => ElementType.Slime,
            38 => ElementType.Shark,
            39 => ElementType.SpinningGun,
            40 => ElementType.Pusher,
            41 => ElementType.Lion,
            42 => ElementType.Tiger,
            43 => ElementType.BlinkRayV,
            44 => ElementType.Head,
            45 => ElementType.Segment,
            47 => ElementType.BlueText,
            48 => ElementType.GreenText,
            49 => ElementType.CyanText,
            50 => ElementType.RedText,
            51 => ElementType.PurpleText,
            52 => ElementType.BrownText,
            53 => ElementType.BlackText,
            _ => ElementType.Unknown
        };

    internal static ElementType MapSuperZztElement(int i) =>
        i switch
        {
            0 => ElementType.Empty,
            1 => ElementType.BoardEdge,
            2 => ElementType.Messenger,
            3 => ElementType.Monitor,
            4 => ElementType.Player,
            5 => ElementType.Ammo,
            7 => ElementType.Gem,
            8 => ElementType.Key,
            9 => ElementType.Door,
            10 => ElementType.Scroll,
            11 => ElementType.Passage,
            12 => ElementType.Duplicator,
            13 => ElementType.Bomb,
            14 => ElementType.Energizer,
            16 => ElementType.Clockwise,
            17 => ElementType.Counter,
            19 => ElementType.Lava,
            20 => ElementType.Forest,
            21 => ElementType.Solid,
            22 => ElementType.Normal,
            23 => ElementType.Breakable,
            24 => ElementType.Boulder,
            25 => ElementType.SliderNs,
            26 => ElementType.SliderEw,
            27 => ElementType.Fake,
            28 => ElementType.Invisible,
            29 => ElementType.BlinkWall,
            30 => ElementType.Transporter,
            31 => ElementType.Line,
            32 => ElementType.Ricochet,
            34 => ElementType.Bear,
            35 => ElementType.Ruffian,
            36 => ElementType.Object,
            37 => ElementType.Slime,
            38 => ElementType.Shark,
            39 => ElementType.SpinningGun,
            40 => ElementType.Pusher,
            41 => ElementType.Lion,
            42 => ElementType.Tiger,
            44 => ElementType.Head,
            45 => ElementType.Segment,
            47 => ElementType.Floor,
            48 => ElementType.WaterN,
            49 => ElementType.WaterS,
            50 => ElementType.WaterW,
            51 => ElementType.WaterE,
            59 => ElementType.Roton,
            60 => ElementType.DragonPup,
            61 => ElementType.Pairer,
            62 => ElementType.Spider,
            63 => ElementType.Web,
            64 => ElementType.Stone,
            69 => ElementType.Bullet,
            70 => ElementType.BlinkRayH,
            71 => ElementType.BlinkRayV,
            72 => ElementType.Star,
            73 => ElementType.BlueText,
            74 => ElementType.GreenText,
            75 => ElementType.CyanText,
            76 => ElementType.RedText,
            77 => ElementType.PurpleText,
            78 => ElementType.BrownText,
            79 => ElementType.BlackText,
            _ => ElementType.Unknown
        };
    
    internal static int UnmapZztElement(ElementType i) =>
        i switch
        {
            ElementType.Empty => 0,
            ElementType.BoardEdge => 1,
            ElementType.Messenger => 2,
            ElementType.Monitor => 3,
            ElementType.Player => 4,
            ElementType.Ammo => 5,
            ElementType.Torch => 6,
            ElementType.Gem => 7,
            ElementType.Key => 8,
            ElementType.Door => 9,
            ElementType.Scroll => 10,
            ElementType.Passage => 11,
            ElementType.Duplicator => 12,
            ElementType.Bomb => 13,
            ElementType.Energizer => 14,
            ElementType.Star => 15,
            ElementType.Clockwise => 16,
            ElementType.Counter => 17,
            ElementType.Bullet => 18,
            ElementType.Water => 19,
            ElementType.Forest => 20,
            ElementType.Solid => 21,
            ElementType.Normal => 22,
            ElementType.Breakable => 23,
            ElementType.Boulder => 24,
            ElementType.SliderNs => 25,
            ElementType.SliderEw => 26,
            ElementType.Fake => 27,
            ElementType.Invisible => 28,
            ElementType.BlinkWall => 29,
            ElementType.Transporter => 30,
            ElementType.Line => 31,
            ElementType.Ricochet => 32,
            ElementType.BlinkRayH => 33,
            ElementType.Bear => 34,
            ElementType.Ruffian => 35,
            ElementType.Object => 36,
            ElementType.Slime => 37,
            ElementType.Shark => 38,
            ElementType.SpinningGun => 39,
            ElementType.Pusher => 40,
            ElementType.Lion => 41,
            ElementType.Tiger => 42,
            ElementType.BlinkRayV => 43,
            ElementType.Head => 44,
            ElementType.Segment => 45,
            ElementType.BlueText => 47,
            ElementType.GreenText => 48,
            ElementType.CyanText => 49,
            ElementType.RedText => 50,
            ElementType.PurpleText => 51,
            ElementType.BrownText => 52,
            ElementType.BlackText => 53,
            _ => -1
        };

    internal static int UnmapSuperZztElement(ElementType i) =>
        i switch
        {
            ElementType.Empty => 0,
            ElementType.BoardEdge => 1,
            ElementType.Messenger => 2,
            ElementType.Monitor => 3,
            ElementType.Player => 4,
            ElementType.Ammo => 5,
            ElementType.Gem => 7,
            ElementType.Key => 8,
            ElementType.Door => 9,
            ElementType.Scroll => 10,
            ElementType.Passage => 11,
            ElementType.Duplicator => 12,
            ElementType.Bomb => 13,
            ElementType.Energizer => 14,
            ElementType.Clockwise => 16,
            ElementType.Counter => 17,
            ElementType.Lava => 19,
            ElementType.Forest => 20,
            ElementType.Solid => 21,
            ElementType.Normal => 22,
            ElementType.Breakable => 23,
            ElementType.Boulder => 24,
            ElementType.SliderNs => 25,
            ElementType.SliderEw => 26,
            ElementType.Fake => 27,
            ElementType.Invisible => 28,
            ElementType.BlinkWall => 29,
            ElementType.Transporter => 30,
            ElementType.Line => 31,
            ElementType.Ricochet => 32,
            ElementType.Bear => 34,
            ElementType.Ruffian => 35,
            ElementType.Object => 36,
            ElementType.Slime => 37,
            ElementType.Shark => 38,
            ElementType.SpinningGun => 39,
            ElementType.Pusher => 40,
            ElementType.Lion => 41,
            ElementType.Tiger => 42,
            ElementType.Head => 44,
            ElementType.Segment => 45,
            ElementType.Floor => 47,
            ElementType.WaterN => 48,
            ElementType.WaterS => 49,
            ElementType.WaterW => 50,
            ElementType.WaterE => 51,
            ElementType.Roton => 59,
            ElementType.DragonPup => 60,
            ElementType.Pairer => 61,
            ElementType.Spider => 62,
            ElementType.Web => 63,
            ElementType.Stone => 64,
            ElementType.Bullet => 69,
            ElementType.BlinkRayH => 70,
            ElementType.BlinkRayV => 71,
            ElementType.Star => 72,
            ElementType.BlueText => 73,
            ElementType.GreenText => 74,
            ElementType.CyanText => 75,
            ElementType.RedText => 76,
            ElementType.PurpleText => 77,
            ElementType.BrownText => 78,
            ElementType.BlackText => 79,
            _ => -1
        };
}