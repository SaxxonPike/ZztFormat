namespace ZztFormat;

public partial class HighScoreList
{
    public static HighScoreList Read(Stream stream, WorldType worldType) =>
        worldType switch
        {
            WorldType.Zzt => ReadZztHighScoreList(stream),
            WorldType.SuperZzt => ReadSuperZztHighScoreList(stream),
            _ => throw new ZztFormatException(
                $"Unknown world type {worldType}.")
        };

    internal static HighScoreList ReadZztHighScoreList(Stream stream)
    {
        var result = new List<Entry>();
        var data = ZztHighScoreList.Read(stream);

        foreach (var item in data.Scores)
        {
            if (item.Name.Length < 1)
                continue;

            result.Add(new Entry
            {
                Name = item.Name,
                Score = item.Score
            });
        }

        return new HighScoreList
        {
            Scores = result,
        };
    }

    internal static HighScoreList ReadSuperZztHighScoreList(Stream stream)
    {
        var result = new List<Entry>();
        var data = SuperZztHighScoreList.Read(stream);

        foreach (var item in data.Scores)
        {
            if (item.Name.Length < 1)
                continue;

            result.Add(new Entry
            {
                Name = item.Name,
                Score = item.Score
            });
        }

        return new HighScoreList
        {
            Scores = result,
        };
    }

    public static void Write(Stream stream, WorldType worldType, HighScoreList list)
    {
        switch (worldType)
        {
            case WorldType.Zzt:
                WriteZztHighScoreList(stream, list);
                break;
            case WorldType.SuperZzt:
                WriteSuperZztHighScoreList(stream, list);
                break;
            default:
                throw new ZztFormatException(
                    $"Unknown world type {worldType}.");
        }
    }

    internal static void WriteZztHighScoreList(Stream stream, HighScoreList list)
    {
        var result = new ZztHighScoreList();
        for (var i = 0; i < result.Scores.Length; i++)
            result.Scores[i] = i < list.Scores.Count
                ? new ZztHighScore
                {
                    Name = list.Scores[i].Name,
                    Score = (short)list.Scores[i].Score
                }
                : new ZztHighScore
                {
                    Name = string.Empty,
                    Score = -1
                };

        result.Write(stream);
    }

    internal static void WriteSuperZztHighScoreList(Stream stream, HighScoreList list)
    {
        var result = new SuperZztHighScoreList();
        for (var i = 0; i < result.Scores.Length; i++)
            result.Scores[i] = i < list.Scores.Count
                ? new SuperZztHighScore
                {
                    Name = list.Scores[i].Name,
                    Score = (short)list.Scores[i].Score
                }
                : new SuperZztHighScore
                {
                    Name = string.Empty,
                    Score = -1
                };

        result.Write(stream);
    }
}