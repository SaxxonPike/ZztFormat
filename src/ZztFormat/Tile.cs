using JetBrains.Annotations;

namespace ZztFormat;

[PublicAPI]
public partial struct Tile
{
    public int Element { get; set; }
    public int Color { get; set; }
}