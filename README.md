# ZztFormat

Class library for reading and manipulating ZZT and Super ZZT worlds.

### Load ZZT or Super ZZT worlds

```csharp
using ZztFormat;

using var stream = File.OpenRead("TOWN.ZZT");
var world = World.Load(stream);
```

### Save ZZT or Super ZZT worlds

You must call `.Flush()` on the stream in order to ensure that all the data
finishes writing. `Save` does not automatically flush the stream.

```csharp
using var stream = File.Open("MY-WORLD.ZZT", FileMode.Create);
World.Save(stream, world);
stream.Flush();
```

### Edit the code of an object on a board

Scripts are represented as `char[]` so that separate instances with the same
textual content are not considered the same (where `#BIND` is concerned.)

Also remember: the line separator character in ZZT is `\r`.

```csharp
world.Boards[0].Actors[1].Script = "@guy\r#end\r:touch\rHello!".ToCharArray();
```

## Developer Notes

T4 templates are used to automatically generate static data and data models.

- `Resources.tt`
  - This template takes binary blobs for static data (such as font and palette)
    and builds them into the assembly as lazy-initialized byte arrays.
    - Font: 8x14 as used by ZZT 3.2
    - Palette: standard IBM VGA palette
- `Structures.tt`
  - This template builds data model classes and structs for the raw binary
    content. The data model is populated using the definitions in
    `Structures.txt`. The file format documentation used to verify these
    structures is here:
    [ZZT Format @ ModdingWiki](https://moddingwiki.shikadi.net/wiki/ZZT_Format)
