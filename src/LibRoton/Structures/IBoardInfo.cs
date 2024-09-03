namespace LibRoton.Structures;

public interface IBoardInfo
{
    byte MaxShots { get; set; }
    bool IsDark { get; set; }
    byte[] Exits { get; }
    bool RestartOnZap { get; set; }
    string Message { get; set; }
    Position Enter { get; set; }
    short TimeLimit { get; set; }
    byte[] Extra { get; }
    short ActorCount { get; set; }
    Vector Camera { get; set; }
}