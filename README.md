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