using LibRoton.Zzt.Structures.LowLevel;

namespace LibRoton.Shared;

public record struct Tile(int Element, Color Color, Background Background, Effect Effect)
{
    public static Tile CopyFrom(RawZztTile raw) =>
        new(raw.Element,
            (Color)(raw.Color & 0xF),
            (Background)((raw.Color >> 4) & 0x7),
            (Effect)(raw.Color & 0x80));

    public void CopyTo(ref RawZztTile raw) =>
        raw = ToRaw();

    public RawZztTile ToRaw() =>
        new()
        {
            Element = (byte)Element,
            Color = (byte)((((int)Background & 0x07) << 4) |
                           ((int)Color & 0x0F) |
                           (int)Effect)
        };
}