using LibRoton.Structures;

namespace LibRoton.Resources;

public static class Elements
{
    public static List<Element> Load(int worldType)
    {
        return worldType switch
        {
            -1 => LoadZztElements(),
            -2 => LoadSuperZztElements(),
            _ => throw new Exception($"Unknown world type {worldType}.")
        };
    }

    private static List<Element> LoadZztElements()
    {
        var bytes = Resources.ZztElements;
        var stream = new MemoryStream(bytes);
        var count = bytes.Length / ZztElementProperties.Size;
        var result = new List<Element>();

        for (var i = 0; i < count; i++)
        {
            var element = Element.Read(stream, -1);
            element.Type = MapZztElement(i);
            result.Add(element);
        }

        return result;
    }
    
    private static List<Element> LoadSuperZztElements()
    {
        var bytes = Resources.SuperZztElements;
        var stream = new MemoryStream(bytes);
        var count = bytes.Length / SuperZztElementProperties.Size;
        var result = new List<Element>();

        for (var i = 0; i < count; i++)
        {
            var element = Element.Read(stream, -2);
            element.Type = MapSuperZztElement(i);
            result.Add(element);
        }

        return result;
    }

    private static ElementType MapZztElement(int i) =>
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
    
    private static ElementType MapSuperZztElement(int i) =>
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
}