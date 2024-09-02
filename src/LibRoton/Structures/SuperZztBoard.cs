namespace LibRoton.Structures;

public class SuperZztBoard
{
    public SuperZztBoardName Name { get; set; }
    public SuperZztBoardInfo Info { get; set; }
    public ZztTile[] Tiles { get; set; }
    public List<SuperZztActor> Actors { get; set; }
    public List<byte[]> Scripts { get; set; }
}