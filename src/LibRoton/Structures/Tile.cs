using JetBrains.Annotations;

namespace LibRoton.Structures;

[PublicAPI]
public partial struct Tile
{
    public int Element { get; set; }
    public int Color { get; set; }
}