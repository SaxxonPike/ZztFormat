using LibRoton.SuperZzt.Structures.LowLevel;
using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.Shared;

public class Actor
{
    public Point Position { get; set; }
    public Vector Vector { get; set; }
    public int Cycle { get; set; }
    public Memory<int> Parameters { get; set; }
    public int Follower { get; set; }
    public int Leader { get; set; }
    public Tile Under { get; set; }
    public Memory<char> Script { get; set; }
    public Actor? Bind { get; set; }
    public int Instruction { get; set; }

    public static Actor CopyFrom(RawZztActor raw) =>
        new()
        {
            Position = Point.CopyFrom(raw.Position),
            Vector = Vector.CopyFrom(raw.Step),
            Cycle = raw.Cycle,
            Parameters = new int[] { raw.Parameters[0], raw.Parameters[1], raw.Parameters[2] },
            Follower = raw.Follower,
            Leader = raw.Leader,
            Under = Tile.CopyFrom(raw.Under),
            Script = default,
            Bind = null,
            Instruction = 0
        };

    public static Actor CopyFrom(RawSuperZztActor raw) =>
        new()
        {
            Position = Point.CopyFrom(raw.Position),
            Vector = Vector.CopyFrom(raw.Step),
            Cycle = raw.Cycle,
            Parameters = new int[] { raw.Parameters[0], raw.Parameters[1], raw.Parameters[2] },
            Follower = raw.Follower,
            Leader = raw.Leader,
            Under = Tile.CopyFrom(raw.Under),
            Script = default,
            Bind = null,
            Instruction = 0
        };

    private RawZztActorParameters CopyParameters()
    {
        var result = new RawZztActorParameters();
        for (var i = 0; i < 3; i++)
            result[i] = (byte)Parameters.Span[i];
        return result;
    }

    public void CopyTo(ref RawZztActor raw) =>
        raw = new RawZztActor
        {
            Position = Position.ToRaw(),
            Step = Vector.ToRaw(),
            Cycle = (short)Cycle,
            Parameters = CopyParameters(),
            Follower = (short)Follower,
            Leader = (short)Leader,
            Under = Under.ToRaw(),
            Pointer = new RawZztPointer { Value = uint.MaxValue },
            Instruction = (short)Instruction,
            Length = (short)Script.Length
        };

    public void CopyTo(ref RawSuperZztActor raw) =>
        raw = new RawSuperZztActor
        {
            Position = Position.ToRaw(),
            Step = Vector.ToRaw(),
            Cycle = (short)Cycle,
            Parameters = CopyParameters(),
            Follower = (short)Follower,
            Leader = (short)Leader,
            Under = Under.ToRaw(),
            Pointer = new RawZztPointer { Value = uint.MaxValue },
            Instruction = (short)Instruction,
            Length = (short)Script.Length
        };
}