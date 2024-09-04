namespace LibRoton.Structures;

public class ActorData
{
    public RawPosition Position { get; set; }
    public RawVector Step { get; set; }
    public short Cycle { get; set; }
    public byte[] Parameters { get; init; } = new byte[3];
    public short Follower { get; set; }
    public short Leader { get; set; }
    public RawTile Under { get; set; }
    public short Instruction { get; set; }
    public short Length { get; set; }
    public byte[] Script { get; set; } = [];
}