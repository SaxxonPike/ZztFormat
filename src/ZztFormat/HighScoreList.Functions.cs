namespace ZztFormat;

public static partial class HighScoreList
{
    public static List<Entry> Read(Stream stream, WorldType worldType) =>
        worldType switch
        {
            WorldType.Zzt => ReadZztHighScoreList(stream),
            WorldType.SuperZzt => ReadSuperZztHighScoreList(stream),
            _ => throw new ZztFormatException(
                $"Unknown world type {worldType}.")
        };

    internal static List<Entry> ReadZztHighScoreList(Stream stream)
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

        return result;
    }

    internal static List<Entry> ReadSuperZztHighScoreList(Stream stream)
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

        return result;
    }

    public static void Write(Stream stream, WorldType worldType, List<Entry> list)
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

    internal static void WriteZztHighScoreList(Stream stream, List<Entry> list)
    {
        var result = new ZztHighScoreList();
        for (var i = 0; i < result.Scores.Length; i++)
            result.Scores[i] = i < list.Count
                ? new ZztHighScore
                {
                    Name = list[i].Name,
                    Score = (short)list[i].Score
                }
                : new ZztHighScore
                {
                    Name = string.Empty,
                    Score = -1
                };

        result.Write(stream);
    }

    internal static void WriteSuperZztHighScoreList(Stream stream, List<Entry> list)
    {
        var result = new SuperZztHighScoreList();
        for (var i = 0; i < result.Scores.Length; i++)
            result.Scores[i] = i < list.Count
                ? new SuperZztHighScore
                {
                    Name = list[i].Name,
                    Score = (short)list[i].Score
                }
                : new SuperZztHighScore
                {
                    Name = string.Empty,
                    Score = -1
                };

        result.Write(stream);
    }
}