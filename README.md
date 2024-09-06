# ZztFormat

Class library for reading and manipulating ZZT and Super ZZT worlds.

### Create game worlds

```csharp
// Create a ZZT world with one blank board for the title screen
var zztWorld = World.Create(WorldType.Zzt);

// Create a Super ZZT world with one blank board for the title screen
var superZztWorld = World.Create(WorldType.SuperZzt);
```

### Load game worlds

```csharp
// Open file stream for TOWN.ZZT, for reading
using var stream = File.OpenRead("TOWN.ZZT");

// Read the game world
var world = World.Load(stream);
```

### Save game worlds

You must call `.Flush()` on the stream in order to ensure that all the data
finishes writing. `Save` does not automatically flush the stream.

```csharp
// Open file stream for MY-WORLD.ZZT, for writing
using var stream = File.Open("MY-WORLD.ZZT", FileMode.Create);

// Write the game world
World.Save(stream, world);

// Ensure all stream data is finished being written to disk before closing
stream.Flush();
```

### Edit the code of an object on a board

Scripts are represented as `char[]` so that separate instances with the same
textual content are not considered the same (where `#BIND` is concerned.)

Also remember: the line separator character in ZZT is `\r`.

```csharp
// Assume world has been loaded already

// Edit the code of an actor on the title screen
var titleBoard = world.Boards[0];
var firstNonPlayerActor = titleBoard.Actors[1];
firstNonPlayerActor.Script = "@guy\r#end\r:touch\rHello!".ToCharArray();
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
