namespace LibRoton.Structures;

public class ZztBoard
{
    public ZztBoardName Name { get; set; }
    public ZztBoardInfo Info { get; set; }
    public ZztTile[] Tiles { get; set; }
    public List<ZztActor> Actors { get; set; }
    public List<byte[]> Scripts { get; set; }
}