using System.Buffers;

namespace LibRoton.Structures;

public static class ZztFile
{
    private static Element ConvertElement(byte e) =>
        e switch
        {
            0 => Element.Empty,
            1 => Element.BoardEdge,
            2 => Element.Messenger,
            3 => Element.Monitor,
            4 => Element.Player,
            5 => Element.Ammo,
            6 => Element.Torch,
            7 => Element.Gem,
            8 => Element.Key,
            9 => Element.Door,
            10 => Element.Scroll,
            11 => Element.Passage,
            12 => Element.Duplicator,
            13 => Element.Bomb,
            14 => Element.Energizer,
            15 => Element.Star,
            16 => Element.Clockwise,
            17 => Element.Counter,
            18 => Element.Bullet,
            19 => Element.Water,
            20 => Element.Forest,
            21 => Element.Solid,
            22 => Element.Normal,
            23 => Element.Breakable,
            24 => Element.Boulder,
            25 => Element.SliderNs,
            26 => Element.SliderEw,
            27 => Element.Fake,
            28 => Element.Invisible,
            29 => Element.BlinkWall,
            30 => Element.Transporter,
            31 => Element.Line,
            32 => Element.Ricochet,
            33 => Element.BlinkRayH,
            34 => Element.Bear,
            35 => Element.Ruffian,
            36 => Element.Object,
            37 => Element.Slime,
            38 => Element.Shark,
            39 => Element.SpinningGun,
            40 => Element.Pusher,
            41 => Element.Lion,
            42 => Element.Tiger,
            43 => Element.BlinkRayV,
            44 => Element.Head,
            45 => Element.Segment,
            47 => Element.BlueText,
            48 => Element.GreenText,
            49 => Element.CyanText,
            50 => Element.RedText,
            51 => Element.PurpleText,
            52 => Element.BrownText,
            53 => Element.BlackText,
            _ => (Element)e
        };

    private static byte ConvertElement(Element e) =>
        e switch
        {
            Element.Empty => 0,
            Element.BoardEdge => 1,
            Element.Messenger => 2,
            Element.Monitor => 3,
            Element.Player => 4,
            Element.Ammo => 5,
            Element.Torch => 6,
            Element.Gem => 7,
            Element.Key => 8,
            Element.Door => 9,
            Element.Scroll => 10,
            Element.Passage => 11,
            Element.Duplicator => 12,
            Element.Bomb => 13,
            Element.Energizer => 14,
            Element.Star => 15,
            Element.Clockwise => 16,
            Element.Counter => 17,
            Element.Bullet => 18,
            Element.Water => 19,
            Element.Forest => 20,
            Element.Solid => 21,
            Element.Normal => 22,
            Element.Breakable => 23,
            Element.Boulder => 24,
            Element.SliderNs => 25,
            Element.SliderEw => 26,
            Element.Fake => 27,
            Element.Invisible => 28,
            Element.BlinkWall => 29,
            Element.Transporter => 30,
            Element.Line => 31,
            Element.Ricochet => 32,
            Element.BlinkRayH => 33,
            Element.Bear => 34,
            Element.Ruffian => 35,
            Element.Object => 36,
            Element.Slime => 37,
            Element.Shark => 38,
            Element.SpinningGun => 39,
            Element.Pusher => 40,
            Element.Lion => 41,
            Element.Tiger => 42,
            Element.BlinkRayV => 43,
            Element.Head => 44,
            Element.Segment => 45,
            Element.BlueText => 47,
            Element.GreenText => 48,
            Element.CyanText => 49,
            Element.RedText => 50,
            Element.PurpleText => 51,
            Element.BrownText => 52,
            Element.BlackText => 53,
            _ => (byte)e
        };

    private static (BoardData Board, List<ActorData> Actors) ReadBoardInternal(
        Stream stream)
    {
        var boardHeader = ZztBoardHeader.Read(stream);
        var boardDataSize = boardHeader.DataSize - ZztBoardHeader.Size + 2;
        byte[]? boardBytes = default;
        try
        {
            boardBytes = ArrayPool<byte>.Shared.Rent(boardDataSize);
            stream.ReadExactly(boardBytes.AsSpan(0, boardDataSize));
            using var boardDataStream = new MemoryStream(boardBytes);

            var tiles = new RawTile[60 * 25];
            RleCompression.Unpack(boardDataStream, tiles);

            var boardInfo = ZztBoardInfo.Read(boardDataStream);
            var actorDatas = new List<ActorData>();

            for (var i = 0; i <= boardInfo.ActorCount; i++)
            {
                byte[] script = [];
                var actor = ZztActor.Read(boardDataStream);
                if (actor.Length > 0)
                {
                    script = new byte[actor.Length];
                    boardDataStream.ReadExactly(script);
                }
                actorDatas.Add(Mapper.FromZzt(actor, script));
            }

            var boardData = Mapper.FromZzt(boardHeader, boardInfo, tiles);
            return (Board: boardData, Actors: actorDatas);
        }
        finally
        {
            if (boardBytes != null)
                ArrayPool<byte>.Shared.Return(boardBytes);
        }
    }

    public static World ReadWorld(Stream stream)
    {
        var header = ZztWorldHeader.Read(stream);

        var boards = Enumerable
            .Range(0, header.BoardCount + 1)
            .Select(_ => ReadBoardInternal(stream))
            .ToList();

        return Mapper.FromCommon(Mapper.FromZzt(header), boards, ConvertElement);
    }

    public static Board ReadBoard(Stream stream)
    {
        var (board, actors) = ReadBoardInternal(stream);
        return Mapper.FromCommon(board, actors, ConvertElement);
    }

    public static void WriteWorld(Stream stream, World world)
    {
    }

    public static void WriteBoard(Stream stream, Board board)
    {
    }
}