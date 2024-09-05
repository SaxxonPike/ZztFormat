namespace ZztFormat;

internal partial struct Time
{
    public static Time FromTimeSpan(TimeSpan timeSpan)
    {
        var totalSeconds = timeSpan.TotalSeconds;
        var seconds = Math.Floor(totalSeconds);
        var fraction = timeSpan.TotalSeconds - seconds;

        return new Time
        {
            Seconds = (short)seconds,
            Hundredths = (short)(fraction * 100)
        };
    }

    public TimeSpan ToTimeSpan() =>
        TimeSpan.FromSeconds(Seconds + Hundredths / 100d);
}