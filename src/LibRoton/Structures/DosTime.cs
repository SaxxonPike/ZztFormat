namespace LibRoton.Structures;

public partial struct DosTime
{
    public static DosTime FromTimeSpan(TimeSpan timeSpan)
    {
        var totalSeconds = timeSpan.TotalSeconds;
        var seconds = Math.Floor(totalSeconds);
        var fraction = timeSpan.TotalSeconds - seconds;

        return new DosTime
        {
            Seconds = (short)seconds,
            Hundredths = (short)(fraction * 100)
        };
    }
    
    public TimeSpan ToTimeSpan() => 
        TimeSpan.FromSeconds(Seconds + Hundredths / 100d);
}