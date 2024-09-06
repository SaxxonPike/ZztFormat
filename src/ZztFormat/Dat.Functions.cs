using System.Text;

namespace ZztFormat;

public partial class Dat
{
    /// <summary>
    /// Read a ZZT.DAT archive.
    /// </summary>
    /// <param name="stream">
    /// Stream to read the data from.
    /// </param>
    /// <returns>
    /// Archive read from the stream.
    /// </returns>
    public static List<Entry> Read(Stream stream)
    {
        // Read the header and extract name/offset data.

        Span<byte> buf = stackalloc byte[ZztDatHeader.Size];
        stream.ReadExactly(buf);

        var header = ZztDatHeader.Read(buf);
        var entries = new List<(string Name, int Offset)>();

        for (var i = 0; i < header.Count; i++)
            if (header.Entries[i].Length > 0)
                entries.Add((header.Entries[i].Name, Offset: header.Offsets[i]));

        if (entries.Count < 1)
            return [];

        using var tempStream = new MemoryStream();
        header.Write(tempStream);

        // Determine the total length of data. This is done by starting at the
        // highest offset file, and looking for the "end of file" marker.

        var toCopy = entries.Max(x => x.Offset) - ZztDatHeader.Size;
        while (toCopy > 0)
        {
            var amount = stream.Read(buf[..Math.Min(ZztDatHeader.Size, toCopy)]);
            if (amount < 1)
                break;

            tempStream.Write(buf[..amount]);
            toCopy -= amount;
        }

        while (true)
        {
            stream.ReadExactly(buf[..1]);
            tempStream.Write(buf[..1]);
            var lineLength = buf[0];

            stream.ReadExactly(buf[..lineLength]);
            tempStream.Write(buf[..lineLength]);
            if (lineLength == 1 && buf[0] == 0x40)
                break;
        }

        // Seek through the temp stream and translate each file.

        var files = new List<Entry>();
        var sb = new StringBuilder();
        Span<char> chars = stackalloc char[256];

        foreach (var entry in entries)
        {
            tempStream.Position = entry.Offset;

            while (true)
            {
                tempStream.ReadExactly(buf[..1]);
                var lineLength = buf[0];
                tempStream.ReadExactly(buf[..lineLength]);

                if (lineLength == 1 && buf[0] == 0x40)
                    break;

                if (sb.Length > 0)
                    sb.AppendLine();

                var charCount = CodePage437.Encoding.GetChars(buf[..lineLength], chars);
                sb.Append(chars[..charCount]);
            }

            files.Add(new Entry(entry.Name, sb.ToString()));
            sb.Clear();
        }

        return files;
    }

    /// <summary>
    /// Write a ZZT.DAT archive.
    /// </summary>
    /// <param name="stream">
    /// Stream to write the data to.
    /// </param>
    /// <param name="entries">
    /// Archive to write to the stream.
    /// </param>
    public static void Write(Stream stream, List<Entry> entries)
    {
        if (entries.Count > 24)
            throw new ZztFormatException(
                "Archive cannot contain more than 24 files.");

        Span<byte> buf = stackalloc byte[256];
        var header = new ZztDatHeader();
        var tempStream = new MemoryStream();

        for (var i = 0; i < entries.Count; i++)
        {
            var offset = (int)(tempStream.Position + ZztDatHeader.Size);
            using var reader = new StringReader(entries[i].Text);
            while (true)
            {
                var line = reader.ReadLine();
                if (line is null)
                    break;

                var size = CodePage437.Encoding.GetBytes(line, buf[1..]);
                buf[0] = (byte)size;
                tempStream.Write(buf[..(size + 1)]);
            }

            buf[0] = 0x01;
            buf[1] = 0x40;
            tempStream.Write(buf[..2]);

            header.Entries[i] = new ZztDatEntry
            {
                Name = entries[i].Name
            };
            header.Offsets[i] = offset;
        }

        header.Count = (short)entries.Count;
        header.Write(stream);
        tempStream.Position = 0;
        tempStream.CopyTo(stream);
    }
}