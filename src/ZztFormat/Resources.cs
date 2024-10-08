﻿// Automatically generated from Resources.tt

using JetBrains.Annotations;
using System.IO.Compression;

namespace ZztFormat;


[PublicAPI]
internal static class Resources
{
    private static byte[] Decompress(byte[] compressed, int decompressedSize)
    {
        Span<byte> decompressed = stackalloc byte[decompressedSize];
        using var mem = new MemoryStream(compressed);
        using var decomp = new GZipStream(mem, CompressionMode.Decompress);
        using var result = new MemoryStream();
        decomp.ReadExactly(decompressed);
        result.Write(decompressed);
        result.Flush();
        return result.ToArray();
    }

    private static Lazy<byte[]> _rawZztElements = new(() => Decompress(new byte[]
    {
        0x1F, 0x8B, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00,  // 0x0000
        0x00, 0x13, 0xED, 0x9A, 0x4F, 0x6C, 0xDB, 0x54,  // 0x0008
        0x1C, 0xC7, 0x9F, 0x9D, 0xC4, 0x21, 0x69, 0xBA,  // 0x0010
        0x6E, 0x1D, 0xA8, 0x45, 0xEA, 0x84, 0x81, 0x1D,  // 0x0018
        0xA0, 0x15, 0x94, 0x96, 0x4D, 0x60, 0x2A, 0x51,  // 0x0020
        0x35, 0x49, 0xD3, 0x56, 0xEB, 0xDA, 0x10, 0x07,  // 0x0028
        0x2A, 0x10, 0x20, 0xB9, 0xC9, 0x6B, 0xFA, 0xA8,  // 0x0030
        0xED, 0x17, 0x3D, 0x3B, 0x1D, 0xE5, 0x82, 0x90,  // 0x0038
        0xD8, 0x01, 0x38, 0x73, 0xE0, 0x80, 0x38, 0x21,  // 0x0040
        0x6E, 0xA8, 0x70, 0x00, 0x89, 0x0B, 0xA7, 0x69,  // 0x0048
        0x27, 0xD0, 0xCE, 0xE3, 0xB8, 0x0B, 0x43, 0x9A,  // 0x0050
        0x34, 0x09, 0x21, 0x01, 0x87, 0x86, 0xF7, 0xFC,  // 0x0058
        0x27, 0xCE, 0x92, 0xA6, 0x73, 0xDB, 0x65, 0xB6,  // 0x0060
        0x51, 0xBF, 0x6A, 0xFD, 0xAA, 0xC8, 0x71, 0x7E,  // 0x0068
        0x9F, 0xFE, 0xFE, 0xBE, 0xD7, 0x8A, 0x75, 0xC0,  // 0x0070
        0x01, 0xFA, 0x25, 0x02, 0x89, 0x6F, 0x36, 0x01,  // 0x0078
        0xBD, 0x9E, 0xA6, 0xDF, 0x00, 0x80, 0xC4, 0xBC,  // 0x0080
        0x56, 0x37, 0x77, 0x40, 0x04, 0x24, 0x36, 0x9D,  // 0x0088
        0xB5, 0x85, 0x60, 0x48, 0x16, 0x42, 0x74, 0x74,  // 0x0090
        0x2F, 0x82, 0xE4, 0x79, 0x21, 0x3A, 0x12, 0x93,  // 0x0098
        0xCE, 0x4A, 0x0D, 0xE7, 0xC0, 0x47, 0xF9, 0x16,  // 0x00A0
        0x42, 0xF2, 0x32, 0xD6, 0x91, 0x89, 0x49, 0xA0,  // 0x00A8
        0xD6, 0xF9, 0x12, 0xFF, 0x04, 0xC7, 0xD1, 0x64,  // 0x00B0
        0x70, 0x10, 0x26, 0x5F, 0xB5, 0x11, 0x38, 0xF0,  // 0x00B8
        0x96, 0x50, 0x54, 0x95, 0x1D, 0xD8, 0x41, 0x20,  // 0x00C0
        0x2C, 0x99, 0x50, 0x33, 0x5E, 0x09, 0xC4, 0xD2,  // 0x00C8
        0x9E, 0xFA, 0x38, 0xC6, 0xD2, 0xB9, 0x3D, 0x17,  // 0x00D0
        0x3E, 0x99, 0x66, 0x08, 0x73, 0xF1, 0x39, 0x4D,  // 0x00D8
        0xC3, 0x41, 0x5B, 0xE7, 0x4B, 0x5F, 0x0A, 0x00,  // 0x00E0
        0xB8, 0x5E, 0xB0, 0x11, 0x7E, 0x78, 0x89, 0x21,  // 0x00E8
        0x94, 0x13, 0x65, 0x4C, 0x2A, 0x9B, 0x41, 0x9B,  // 0x00F0
        0xE7, 0x47, 0xF1, 0x26, 0xD7, 0xE1, 0x05, 0xE1,  // 0x00F8
        0x45, 0x86, 0xB0, 0x10, 0x5B, 0x80, 0x5A, 0xD0,  // 0x0100
        0xC6, 0xF9, 0x53, 0xA6, 0xD9, 0x19, 0x48, 0x68,  // 0x0108
        0x8A, 0x21, 0x5C, 0x8A, 0x5D, 0x82, 0x91, 0xE8,  // 0x0110
        0x0A, 0x00, 0xA4, 0xF7, 0xEC, 0xD5, 0x43, 0xB8,  // 0x0118
        0x69, 0x79, 0x21, 0x1F, 0xCF, 0xE3, 0x28, 0x94,  // 0x0120
        0x23, 0xAA, 0xDF, 0x87, 0x3C, 0x2F, 0x70, 0xE0,  // 0x0128
        0xAB, 0x49, 0x89, 0xFF, 0xEC, 0x05, 0xF6, 0x93,  // 0x0130
        0x2C, 0xC8, 0x15, 0x82, 0x55, 0x35, 0x68, 0xFB,  // 0x0138
        0xDA, 0x75, 0x76, 0xBE, 0x8A, 0x4C, 0xD1, 0x84,  // 0x0140
        0xEF, 0x9B, 0x22, 0xDE, 0x10, 0x0D, 0xD7, 0xBE,  // 0x0148
        0xBB, 0x7B, 0x5E, 0x3A, 0xB3, 0xDB, 0x24, 0x9E,  // 0x0150
        0x58, 0x5E, 0x28, 0x26, 0x8B, 0x8A, 0x61, 0x28,  // 0x0158
        0x35, 0xD8, 0x77, 0xC3, 0x86, 0x4B, 0x18, 0x6B,  // 0x0160
        0xA2, 0xB9, 0x49, 0x1A, 0x62, 0xDD, 0xFE, 0xC8,  // 0x0168
        0xD9, 0xC3, 0x3D, 0xE0, 0xDF, 0x21, 0x6B, 0xE1,  // 0x0170
        0x1E, 0x1B, 0x97, 0x78, 0x1E, 0x34, 0x27, 0xDC,  // 0x0178
        0xBE, 0xF0, 0x7A, 0x3A, 0xDF, 0xA8, 0xAB, 0xA8,  // 0x0180
        0xA2, 0x1C, 0xB1, 0xBB, 0x3D, 0xEA, 0xBE, 0x1D,  // 0x0188
        0x61, 0x5D, 0x24, 0x8A, 0x09, 0x67, 0x67, 0xE4,  // 0x0190
        0x82, 0xFF, 0xB7, 0x9F, 0x91, 0x71, 0x83, 0x54,  // 0x0198
        0xA0, 0x58, 0x45, 0x04, 0x56, 0xD8, 0x33, 0x0E,  // 0x01A0
        0xC0, 0x1A, 0xB0, 0xD3, 0x99, 0x23, 0xA3, 0xB4,  // 0x01A8
        0x16, 0x81, 0x9B, 0xF4, 0xFA, 0xE7, 0xE3, 0x0C,  // 0x01B0
        0x21, 0x1B, 0xCF, 0x62, 0x6D, 0xFD, 0x28, 0xC6,  // 0x01B8
        0x3F, 0x74, 0x7D, 0x98, 0xB0, 0x57, 0x2F, 0x9D,  // 0x01C0
        0x2F, 0x88, 0x0C, 0x61, 0x3E, 0x35, 0xAF, 0x43,  // 0x01C8
        0x52, 0x43, 0x1F, 0x74, 0x36, 0xE8, 0xF0, 0x49,  // 0x01D0
        0x76, 0x02, 0xE9, 0x9D, 0x73, 0xCC, 0xF0, 0xDB,  // 0x01D8
        0xF4, 0xFA, 0x97, 0x9D, 0x15, 0x71, 0xD9, 0x54,  // 0x01E0
        0x42, 0x6F, 0xBD, 0xA5, 0x49, 0x7B, 0x52, 0xE5,  // 0x01E8
        0xC6, 0x46, 0x24, 0x3E, 0x06, 0xD4, 0x11, 0x37,  // 0x01F0
        0x17, 0xA6, 0x52, 0x39, 0x15, 0x57, 0xB6, 0xAE,  // 0x01F8
        0x20, 0xA3, 0x3D, 0xA1, 0xD3, 0x39, 0xAC, 0x6F,  // 0x0200
        0xC3, 0x1D, 0x4C, 0xC2, 0x34, 0x27, 0xBD, 0xED,  // 0x0208
        0x20, 0x5C, 0x1F, 0x61, 0xE9, 0x3C, 0x32, 0xEA,  // 0x0210
        0x22, 0x4C, 0x27, 0x73, 0xB8, 0xA1, 0x9B, 0xE1,  // 0x0218
        0x0F, 0x23, 0x00, 0xFE, 0x1E, 0xB2, 0xDA, 0x82,  // 0x0220
        0xD3, 0x17, 0xE0, 0x50, 0x2B, 0x90, 0x84, 0x6C,  // 0x0228
        0x43, 0x55, 0xA1, 0x19, 0xAC, 0x75, 0xBE, 0xB4,  // 0x0230
        0xFB, 0x0F, 0xB0, 0x1A, 0x83, 0x97, 0xCE, 0xB1,  // 0x0238
        0x19, 0x16, 0x52, 0x6B, 0x89, 0x35, 0xA5, 0xDB,  // 0x0240
        0x07, 0xA9, 0x32, 0x24, 0x44, 0x41, 0x7A, 0x98,  // 0x0248
        0xE2, 0x08, 0xEC, 0x8A, 0xF6, 0xEA, 0x21, 0xDC,  // 0x0250
        0x78, 0x99, 0x21, 0x14, 0x84, 0x02, 0x26, 0xD0,  // 0x0258
        0x88, 0x82, 0x17, 0x7E, 0xEB, 0xDA, 0x3B, 0xB3,  // 0x0260
        0x5C, 0x88, 0x01, 0x39, 0x21, 0x63, 0x15, 0x55,  // 0x0268
        0x3B, 0xEE, 0x16, 0xD6, 0x14, 0x55, 0x0D, 0x95,  // 0x0270
        0x0B, 0xA8, 0xBE, 0xEF, 0x81, 0xB0, 0x22, 0xAC,  // 0x0278
        0x60, 0xA2, 0x29, 0xA1, 0x9A, 0x91, 0x7A, 0xE8,  // 0x0280
        0xBB, 0x1E, 0x08, 0xD9, 0x54, 0x96, 0x40, 0x65,  // 0x0288
        0x4B, 0x59, 0x57, 0xFB, 0x3F, 0x25, 0x1D, 0x53,  // 0x0290
        0x7B, 0x5D, 0xFB, 0x85, 0x5B, 0x17, 0x18, 0xC2,  // 0x0298
        0x6A, 0x32, 0x8B, 0x1B, 0x6A, 0x35, 0x0A, 0x45,  // 0x02A0
        0x75, 0xB8, 0xCB, 0x0B, 0x36, 0xC2, 0xD4, 0x80,  // 0x02A8
        0x4C, 0x53, 0x01, 0x12, 0xF1, 0x99, 0x15, 0xF9,  // 0x02B0
        0xD9, 0x60, 0x4D, 0xBC, 0x9F, 0xCE, 0xF5, 0x40,  // 0x02B8
        0x98, 0x6E, 0x21, 0xCC, 0xAF, 0x85, 0x1C, 0xC1,  // 0x02C0
        0x4A, 0x67, 0xAE, 0xFD, 0x40, 0x72, 0x49, 0x62,  // 0x02C8
        0x08, 0x73, 0xF1, 0x82, 0xB2, 0x15, 0xFA, 0x34,  // 0x02D0
        0xB0, 0xB4, 0xDB, 0xE5, 0x85, 0x45, 0xAB, 0x2F,  // 0x02D8
        0x2C, 0xA5, 0x96, 0xF4, 0x6D, 0x64, 0xA0, 0x08,  // 0x02E0
        0xA4, 0xF3, 0xAF, 0xCE, 0x8C, 0xF4, 0xFC, 0x79,  // 0x02E8
        0x6B, 0xC4, 0x3E, 0xEF, 0x56, 0xA4, 0xE5, 0x74,  // 0x02F0
        0x56, 0x45, 0xFA, 0x96, 0x78, 0x45, 0xB9, 0xEF,  // 0x02F8
        0xE6, 0x73, 0x90, 0xCD, 0xB4, 0x26, 0xD2, 0x6B,  // 0x0300
        0xA2, 0x89, 0x34, 0x17, 0x58, 0x28, 0x42, 0x82,  // 0x0308
        0x70, 0x67, 0x5F, 0xF1, 0xAD, 0x53, 0xAC, 0x01,  // 0x0310
        0x79, 0x1B, 0x9E, 0x03, 0xEF, 0xBD, 0xE6, 0x20,  // 0x0318
        0x5C, 0x1B, 0x63, 0x63, 0xDE, 0x55, 0x7A, 0x9D,  // 0x0320
        0x1B, 0x63, 0x08, 0xE5, 0x81, 0x32, 0x51, 0x74,  // 0x0328
        0xA3, 0x8E, 0x49, 0x20, 0xA3, 0x5E, 0x3A, 0xEF,  // 0x0330
        0x63, 0xBB, 0xE6, 0xC8, 0xF5, 0xC2, 0xF8, 0x70,  // 0x0338
        0xC7, 0xE1, 0x7C, 0x7C, 0x19, 0xE9, 0xA1, 0x8F,  // 0x0340
        0x21, 0x4B, 0xE3, 0x69, 0x7B, 0xED, 0x6C, 0x6D,  // 0x0348
        0xA5, 0x47, 0x4A, 0xA8, 0x82, 0x2B, 0x9B, 0x11,  // 0x0350
        0x98, 0x55, 0x7F, 0xD9, 0xB7, 0x3B, 0x07, 0x6B,  // 0x0358
        0xD3, 0x21, 0xF5, 0x85, 0xE0, 0x1D, 0x48, 0xD2,  // 0x0360
        0x9E, 0x9C, 0xB0, 0xF7, 0x0B, 0x3C, 0xDB, 0xFE,  // 0x0368
        0xC3, 0xEE, 0x8D, 0x67, 0x3A, 0x47, 0xA7, 0x0E,  // 0x0370
        0xB3, 0x41, 0x47, 0xD8, 0xB6, 0x51, 0x2F, 0x23,  // 0x0378
        0x43, 0xDD, 0x40, 0x26, 0xDA, 0x46, 0xE6, 0xCE,  // 0x0380
        0x21, 0xCF, 0x4F, 0x1E, 0x80, 0x38, 0x90, 0x18,  // 0x0388
        0xE4, 0xDA, 0x8E, 0xC2, 0x5E, 0x8B, 0xB9, 0x08,  // 0x0390
        0xA5, 0x64, 0xA9, 0xB1, 0xB1, 0x81, 0x94, 0x83,  // 0x0398
        0x8B, 0x81, 0xA5, 0xC1, 0x25, 0xBA, 0xBB, 0x53,  // 0x03A0
        0x55, 0x54, 0x83, 0x7A, 0xA5, 0x75, 0x06, 0x34,  // 0x03A8
        0x58, 0xA2, 0x93, 0xBA, 0x5B, 0xA5, 0xFA, 0x09,  // 0x03B0
        0xC6, 0x03, 0xDE, 0x49, 0xE7, 0xAB, 0x13, 0xCC,  // 0x03B8
        0x0B, 0x5F, 0x8F, 0x4B, 0xFC, 0x8F, 0x13, 0x0C,  // 0x03C0
        0x61, 0x55, 0x58, 0x5D, 0x7F, 0x8F, 0xD6, 0x04,  // 0x03C8
        0x1F, 0x0F, 0x49, 0xE7, 0x36, 0x15, 0xA2, 0x54,  // 0x03D0
        0x68, 0xE5, 0xEA, 0xB3, 0x0F, 0x32, 0xD6, 0x69,  // 0x03D8
        0x5E, 0x91, 0xE0, 0x1A, 0x51, 0xDA, 0x8E, 0xAC,  // 0x03E0
        0xC7, 0xDB, 0x72, 0x21, 0x06, 0xBE, 0x15, 0x25,  // 0x03E8
        0xFE, 0xF3, 0xA7, 0x18, 0xC2, 0x1B, 0x09, 0x3A,  // 0x03F0
        0x60, 0x68, 0x47, 0x2F, 0x49, 0xC3, 0x97, 0xF1,  // 0x03F8
        0x36, 0xD4, 0xA0, 0x6E, 0x8A, 0x46, 0x1D, 0xC2,  // 0x0400
        0xEA, 0xEC, 0x4C, 0x41, 0x3E, 0x26, 0x40, 0x4F,  // 0x0408
        0xBD, 0xDB, 0xF6, 0x87, 0xAA, 0x18, 0x78, 0xF2,  // 0x0410
        0x69, 0x3B, 0x9D, 0x79, 0xF0, 0x66, 0x42, 0xA6,  // 0x0418
        0xBF, 0xDB, 0x2D, 0x3F, 0x8F, 0xD8, 0x37, 0x90,  // 0x0420
        0x1E, 0xA2, 0x46, 0x9D, 0x40, 0xBA, 0x71, 0x86,  // 0x0428
        0x19, 0xFE, 0xCD, 0xB0, 0x8B, 0xB0, 0x90, 0x91,  // 0x0430
        0xEB, 0x48, 0xD7, 0x59, 0x34, 0xD7, 0x1A, 0x07,  // 0x0438
        0x27, 0xC4, 0xBE, 0x08, 0x99, 0x02, 0x22, 0xEC,  // 0x0440
        0xCD, 0xD6, 0x59, 0x24, 0xE8, 0x78, 0xD1, 0xDC,  // 0x0448
        0xA9, 0x3F, 0x40, 0xD6, 0xD3, 0x0E, 0xC2, 0xC8,  // 0x0450
        0x45, 0x89, 0x8F, 0x83, 0x4F, 0x2F, 0xBA, 0x08,  // 0x0458
        0x45, 0xA1, 0xD8, 0x30, 0x36, 0x03, 0xDB, 0x2E,  // 0x0460
        0x0C, 0xB1, 0x4F, 0xF7, 0x75, 0xA2, 0x0A, 0xC0,  // 0x0468
        0x1F, 0x19, 0xAF, 0x22, 0xF1, 0xE0, 0x2C, 0xE7,  // 0x0470
        0x56, 0xA4, 0x65, 0xDA, 0x9D, 0xBB, 0x67, 0x93,  // 0x0478
        0x24, 0xAD, 0xB4, 0x86, 0xD9, 0xB1, 0x79, 0x0E,  // 0x0480
        0x36, 0x90, 0x38, 0x70, 0x6B, 0xA0, 0x1D, 0xE1,  // 0x0488
        0x39, 0xDE, 0x45, 0x28, 0x27, 0xCA, 0xD4, 0x26,  // 0x0490
        0x5F, 0x4E, 0x08, 0x36, 0x90, 0x78, 0xF0, 0x53,  // 0x0498
        0xF4, 0xBB, 0xF3, 0xED, 0xA6, 0x77, 0x9A, 0xC7,  // 0x04A0
        0x83, 0x9F, 0x05, 0xD7, 0x0B, 0x8B, 0xF1, 0x45,  // 0x04A8
        0xA8, 0x74, 0xCD, 0xCA, 0xE9, 0x1C, 0x2D, 0x93,  // 0x04B0
        0xA8, 0x0E, 0xAB, 0xD0, 0xF0, 0x5E, 0xDC, 0xD7,  // 0x04B8
        0x0B, 0xA9, 0x3C, 0xDC, 0xA6, 0x8D, 0xB1, 0xFF,  // 0x04C0
        0x91, 0xC5, 0x81, 0xD5, 0x7B, 0x10, 0xEE, 0x9C,  // 0x04C8
        0x72, 0x11, 0xE4, 0xA4, 0x0C, 0x6B, 0xAC, 0xAE,  // 0x04D0
        0xF7, 0xD9, 0x82, 0x63, 0x2B, 0xB6, 0xCF, 0xBF,  // 0x04D8
        0x91, 0x44, 0x2E, 0x90, 0x4E, 0x10, 0xC2, 0xA0,  // 0x04E0
        0x13, 0x84, 0x30, 0xE8, 0x04, 0x21, 0x0C, 0xFA,  // 0x04E8
        0x1F, 0x20, 0xFC, 0x07, 0xA5, 0x26, 0x68, 0x24,  // 0x04F0
        0x22, 0x29, 0x00, 0x00, 
    }, 0x2922));

    public static ReadOnlySpan<byte> ZztElements => _rawZztElements.Value;

    private static Lazy<byte[]> _rawSuperZztElements = new(() => Decompress(new byte[]
    {
        0x1F, 0x8B, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00,  // 0x0000
        0x00, 0x13, 0xED, 0x9A, 0x4F, 0x6C, 0xDB, 0x54,  // 0x0008
        0x1C, 0xC7, 0xDF, 0xCB, 0x1F, 0x87, 0xA4, 0x29,  // 0x0010
        0xFD, 0x33, 0xB4, 0x0D, 0x69, 0x5D, 0xBD, 0x4D,  // 0x0018
        0x13, 0x65, 0x87, 0x8D, 0x56, 0x30, 0xBA, 0x0E,  // 0x0020
        0x51, 0x35, 0x4D, 0xB2, 0x55, 0xEB, 0xDA, 0x28,  // 0x0028
        0x6E, 0x29, 0x2A, 0x08, 0xC9, 0x75, 0xDF, 0xD2,  // 0x0030
        0xB7, 0x3A, 0x7E, 0xD1, 0xB3, 0x93, 0x52, 0x24,  // 0x0038
        0x84, 0x90, 0x76, 0xE6, 0x80, 0x10, 0x77, 0x84,  // 0x0040
        0x90, 0x38, 0xC1, 0xE0, 0x82, 0x84, 0xC4, 0x01,  // 0x0048
        0xA6, 0x1D, 0x81, 0x1B, 0x17, 0x8E, 0x08, 0x18,  // 0x0050
        0x9B, 0x2A, 0x21, 0x4E, 0x11, 0x12, 0x2B, 0xEF,  // 0x0058
        0xD9, 0x75, 0xE2, 0x3A, 0x69, 0xE3, 0xAD, 0xCB,  // 0x0060
        0x1C, 0x4F, 0xF9, 0x46, 0x8D, 0x25, 0x2B, 0x71,  // 0x0068
        0x7E, 0x9F, 0xFE, 0xFE, 0xBC, 0xDF, 0xEF, 0xD9,  // 0x0070
        0x62, 0x09, 0x40, 0xF6, 0x12, 0x41, 0x15, 0x6E,  // 0x0078
        0x6F, 0x03, 0xF6, 0xDE, 0xCF, 0xFE, 0x00, 0x00,  // 0x0080
        0xD1, 0x4C, 0xB1, 0x64, 0x6C, 0x82, 0x00, 0x48,  // 0x0088
        0xDC, 0xB6, 0x0E, 0x35, 0x82, 0xCF, 0x26, 0x4D,  // 0x0090
        0x82, 0xE0, 0x68, 0x17, 0xC1, 0x85, 0xBA, 0x0F,  // 0x0098
        0x82, 0xA3, 0xD0, 0x30, 0xE0, 0x06, 0x73, 0x02,  // 0x00A0
        0x08, 0x2A, 0x57, 0x6A, 0x04, 0xB1, 0xAB, 0x44,  // 0x00A8
        0xC3, 0x06, 0xA1, 0x7E, 0xDB, 0xD7, 0x5A, 0xA1,  // 0x00B0
        0x61, 0x58, 0x27, 0xF8, 0x38, 0x63, 0x11, 0x40,  // 0x00B8
        0xB0, 0x2C, 0xE4, 0x54, 0x79, 0x13, 0xB9, 0x00,  // 0x00C0
        0x84, 0x19, 0x03, 0x15, 0xF5, 0x09, 0x5F, 0x0C,  // 0x00C8
        0xDD, 0x53, 0x37, 0xC2, 0x35, 0x1F, 0x58, 0x79,  // 0x00D0
        0xF0, 0xE7, 0xCB, 0x9C, 0x60, 0x2A, 0x32, 0x55,  // 0x00D8
        0x2C, 0x12, 0xBF, 0x8D, 0xF3, 0xA4, 0x86, 0x4C,  // 0x00E0
        0x0E, 0x5C, 0x1E, 0x44, 0xB6, 0xE1, 0x6E, 0x1F,  // 0x00E8
        0xBC, 0x7B, 0x81, 0xFB, 0xE0, 0x52, 0xF8, 0x12,  // 0x00F0
        0x2A, 0xFA, 0x6D, 0x9B, 0x37, 0x25, 0xB7, 0x5D,  // 0x00F8
        0x51, 0xF4, 0xCF, 0x79, 0x4E, 0x70, 0x25, 0x7C,  // 0x0100
        0x05, 0x05, 0x62, 0x35, 0x00, 0x20, 0x71, 0xDF,  // 0x0108
        0x3C, 0xD4, 0x09, 0x94, 0x09, 0x4E, 0x90, 0x8E,  // 0x0110
        0xA4, 0x49, 0x10, 0x0A, 0x11, 0xD3, 0x9D, 0x3E,  // 0x0118
        0x47, 0x35, 0x3D, 0xF2, 0x52, 0x15, 0x86, 0x4D,  // 0x0120
        0x1F, 0x48, 0x82, 0xA4, 0x50, 0xA2, 0xAA, 0x7E,  // 0x0128
        0x9B, 0xE7, 0xD4, 0xA1, 0xCC, 0x2A, 0x36, 0x44,  // 0x0130
        0x03, 0xBD, 0x6D, 0x88, 0xE4, 0x9A, 0xA8, 0xDB,  // 0x0138
        0xF6, 0xFD, 0x5D, 0xF7, 0x01, 0x3F, 0x56, 0xE1,  // 0x0140
        0x96, 0x99, 0x07, 0xB9, 0x58, 0x4E, 0xD6, 0x75,  // 0x0148
        0xB9, 0x80, 0xDA, 0x6E, 0xD7, 0x60, 0x9E, 0x90,  // 0x0150
        0xA2, 0x68, 0xAC, 0xD1, 0xB2, 0x58, 0xB2, 0x7E,  // 0x0158
        0x72, 0xF2, 0xC1, 0x2E, 0xF0, 0x6F, 0x1F, 0x7F,  // 0x0160
        0x87, 0x63, 0x67, 0xAB, 0x30, 0x04, 0x36, 0x46,  // 0x0168
        0xED, 0xF5, 0x60, 0x31, 0x91, 0x2E, 0x97, 0x54,  // 0x0170
        0xAC, 0xC8, 0x0F, 0xB9, 0xA8, 0x3D, 0x63, 0x7F,  // 0x0178
        0x1D, 0x13, 0x4D, 0xA4, 0xB2, 0x81, 0x26, 0x2F,  // 0x0180
        0x4A, 0x59, 0xEF, 0x5F, 0x1F, 0x90, 0x48, 0x99,  // 0x0188
        0x2A, 0x48, 0x5C, 0xC5, 0x14, 0x29, 0xFC, 0x1A,  // 0x0190
        0xFB, 0x50, 0xF5, 0x98, 0x99, 0x0C, 0xBF, 0x1B,  // 0x0198
        0xAA, 0x42, 0x01, 0x9C, 0x3E, 0x5E, 0x85, 0x93,  // 0x01A0
        0xC3, 0x9C, 0x20, 0x15, 0x49, 0x91, 0xE2, 0xCA,  // 0x01A8
        0xC3, 0xD8, 0xFE, 0xD8, 0xF5, 0x5E, 0xD4, 0x3C,  // 0x01B0
        0xD4, 0x33, 0xF9, 0xFD, 0xD3, 0x9C, 0x20, 0x13,  // 0x01B8
        0xCF, 0x68, 0x88, 0x16, 0xF0, 0x3B, 0xEE, 0x65,  // 0x01C0
        0xB9, 0xF3, 0x14, 0xFC, 0x15, 0xED, 0x9C, 0x49,  // 0x01C8
        0x00, 0x0B, 0xC7, 0x58, 0x15, 0x02, 0xDF, 0x1C,  // 0x01D0
        0xB3, 0xF3, 0x60, 0x34, 0x3E, 0xAD, 0x12, 0x65,  // 0x01D8
        0x7D, 0x03, 0xEB, 0xCE, 0x5C, 0x4E, 0x4C, 0x13,  // 0x01E0
        0xAD, 0x82, 0x36, 0x09, 0xED, 0xA4, 0xDE, 0xE8,  // 0x01E8
        0x4D, 0x8B, 0x60, 0x70, 0x88, 0x67, 0xF2, 0xCA,  // 0x01F0
        0x90, 0x4D, 0x30, 0x16, 0x9B, 0x26, 0x65, 0xCD,  // 0x01F8
        0xE8, 0xFC, 0x18, 0x7A, 0x12, 0xA2, 0x88, 0xCC,  // 0x0200
        0x01, 0x5E, 0x8C, 0xEA, 0x04, 0x8B, 0x53, 0x3C,  // 0x0208
        0x9E, 0x66, 0x23, 0xB3, 0x72, 0x45, 0x76, 0x7F,  // 0x0210
        0x38, 0xBE, 0x80, 0x28, 0x95, 0xB1, 0xD6, 0x49,  // 0x0218
        0x41, 0x04, 0x6E, 0x8A, 0xE6, 0xA1, 0x4E, 0xF0,  // 0x0220
        0xE5, 0xAB, 0x9C, 0x20, 0x2B, 0x64, 0x09, 0x45,  // 0x0228
        0xBA, 0xE1, 0xAF, 0x71, 0x9E, 0xF4, 0x6B, 0xB3,  // 0x0230
        0x28, 0x0A, 0x03, 0x29, 0x2A, 0x11, 0x15, 0xAF,  // 0x0238
        0xBA, 0x3E, 0x2C, 0x2C, 0xC9, 0xAA, 0xDA, 0x51,  // 0x0240
        0x0E, 0x60, 0xFA, 0xBA, 0x39, 0xC1, 0x9C, 0x30,  // 0x0248
        0x47, 0x68, 0x51, 0xEE, 0xA8, 0xBE, 0x68, 0x0F,  // 0x0250
        0x7D, 0xD5, 0x9C, 0x20, 0x15, 0x4F, 0x51, 0x24,  // 0x0258
        0xAF, 0xCB, 0x2B, 0x6A, 0xFB, 0x3B, 0xA3, 0x03,  // 0x0260
        0xEA, 0xBE, 0x7B, 0x3E, 0x58, 0xBE, 0xC8, 0x09,  // 0x0268
        0xE6, 0x63, 0x29, 0x52, 0x56, 0x57, 0x83, 0x50,  // 0x0270
        0x4D, 0x07, 0xDD, 0x3E, 0xB0, 0x08, 0x46, 0x7B,  // 0x0278
        0x24, 0x96, 0x06, 0x88, 0x8A, 0x23, 0x73, 0xD2,  // 0x0280
        0xF3, 0x3E, 0x9B, 0xD8, 0x42, 0x43, 0xCD, 0x09,  // 0x0288
        0xC6, 0x6A, 0x04, 0x99, 0xA5, 0x0E, 0x27, 0xE0,  // 0x0290
        0x99, 0x0C, 0x9D, 0xBB, 0x8E, 0xEA, 0x24, 0x27,  // 0x0298
        0x98, 0x8A, 0x64, 0xE5, 0xF5, 0x8E, 0x4F, 0x01,  // 0x02A0
        0x53, 0x0D, 0x6B, 0xF2, 0x29, 0x73, 0x3D, 0x98,  // 0x02A8
        0x89, 0xCF, 0x68, 0x15, 0xAC, 0xE3, 0x00, 0x64,  // 0x02B0
        0xF2, 0x4F, 0x56, 0x5F, 0xF4, 0xC6, 0x08, 0xEF,  // 0x02B8
        0x86, 0xE8, 0x88, 0x5D, 0x8B, 0x5E, 0x4F, 0xA4,  // 0x02C0
        0x54, 0xAC, 0xAD, 0x8B, 0x1B, 0x72, 0xCB, 0x49,  // 0x02C8
        0xB3, 0x57, 0x32, 0x64, 0x6A, 0x60, 0xAD, 0x20,  // 0x02D0
        0x1A, 0xB8, 0x68, 0xF3, 0x0A, 0x39, 0x44, 0x31,  // 0x02D8
        0x71, 0xAF, 0x27, 0x9E, 0xF5, 0x34, 0x5F, 0x78,  // 0x02E0
        0xEA, 0xF3, 0xCD, 0xBE, 0x9F, 0xBD, 0x6D, 0x11,  // 0x02E8
        0x1C, 0x3E, 0xC9, 0x3B, 0xBB, 0x5F, 0x4E, 0x54,  // 0x02F0
        0xE1, 0x87, 0x27, 0x38, 0xC1, 0x42, 0xCF, 0x02,  // 0x02F8
        0x95, 0x35, 0xBD, 0x44, 0xA8, 0x2F, 0xDD, 0x5D,  // 0x0300
        0x22, 0xED, 0x61, 0x38, 0xDB, 0xD1, 0x8E, 0x0F,  // 0x0308
        0x6E, 0x1D, 0x71, 0x75, 0x76, 0x91, 0x59, 0xAC,  // 0x0310
        0x75, 0x7C, 0x00, 0x99, 0x3A, 0x93, 0x30, 0x0F,  // 0x0318
        0xEE, 0x15, 0x2D, 0xFF, 0x54, 0x1E, 0x2B, 0x44,  // 0x0320
        0x59, 0x43, 0x9D, 0xDF, 0x19, 0x05, 0xBF, 0xBB,  // 0x0328
        0xBE, 0x17, 0xAA, 0xED, 0x3A, 0x86, 0xC1, 0xF7,  // 0x0330
        0xF1, 0x2A, 0x9C, 0x85, 0x3C, 0x23, 0xD8, 0xA4,  // 0x0338
        0x8F, 0xE4, 0x86, 0x04, 0x48, 0x4C, 0xB3, 0x56,  // 0x0340
        0xC3, 0x28, 0xB3, 0xA6, 0xD5, 0xD1, 0xDD, 0x25,  // 0x0348
        0x25, 0xA4, 0xE9, 0xD8, 0xC0, 0x15, 0x6C, 0x6C,  // 0x0350
        0x3E, 0xE0, 0x46, 0xC9, 0x23, 0x10, 0x04, 0xD1,  // 0x0358
        0x5E, 0xC7, 0xEE, 0xFB, 0xAD, 0x98, 0x4D, 0x90,  // 0x0360
        0x8F, 0xE5, 0xCB, 0xD7, 0xAE, 0x61, 0x79, 0xFF,  // 0x0368
        0x32, 0x60, 0xAA, 0x77, 0x86, 0xCD, 0x72, 0xAA,  // 0x0370
        0x8A, 0x0B, 0x48, 0x53, 0x6A, 0x7B, 0x3D, 0xBD,  // 0x0378
        0x79, 0xD6, 0x9A, 0xDB, 0xF5, 0xA9, 0x9D, 0x5C,  // 0x0380
        0x21, 0x10, 0xB2, 0x32, 0xF9, 0xBF, 0x17, 0xB8,  // 0x0388
        0x0F, 0x7E, 0x38, 0x5B, 0x85, 0xE7, 0x46, 0x39,  // 0x0390
        0xC1, 0xBC, 0x30, 0xBF, 0x72, 0x9D, 0x55, 0x03,  // 0x0398
        0x0F, 0xD7, 0x48, 0x4C, 0xAF, 0xC9, 0x54, 0x56,  // 0x03A0
        0x58, 0xCD, 0x6A, 0xB3, 0x07, 0x92, 0xE6, 0x9E,  // 0x03A8
        0x5D, 0x8E, 0x92, 0x02, 0x95, 0x1D, 0xBB, 0xD2,  // 0x03B0
        0x67, 0xEA, 0x79, 0x10, 0x06, 0x77, 0x4E, 0x57,  // 0x03B8
        0xE1, 0xCF, 0xCF, 0x71, 0x82, 0xD7, 0xA2, 0xAC,  // 0x03C0
        0xAB, 0x28, 0x3E, 0x7C, 0x31, 0x1A, 0xBC, 0x4A,  // 0x03C8
        0x2A, 0xA8, 0x88, 0x34, 0x43, 0xD4, 0x4B, 0x08,  // 0x03D0
        0xAD, 0x4E, 0x5E, 0xCC, 0x4A, 0x07, 0x05, 0xD8,  // 0x03D8
        0x4B, 0xC1, 0xCF, 0xE4, 0xA3, 0x56, 0x14, 0x29,  // 0x03E0
        0x87, 0xF9, 0x7F, 0x7E, 0xE0, 0xA8, 0x45, 0x10,  // 0x03E8
        0x02, 0x97, 0x92, 0x52, 0x09, 0x6B, 0x1A, 0x8F,  // 0x03F0
        0xE4, 0x42, 0x79, 0xFF, 0x64, 0x68, 0x9A, 0x07,  // 0x03F8
        0xC9, 0x2C, 0xA6, 0xFC, 0xCB, 0xE6, 0x86, 0x23,  // 0x0400
        0x70, 0x9D, 0x34, 0x36, 0x4B, 0x8F, 0x30, 0x37,  // 0x0408
        0xFA, 0x2D, 0x82, 0x0F, 0x58, 0x47, 0x1A, 0x01,  // 0x0410
        0xF0, 0x15, 0x9B, 0x20, 0x27, 0xE4, 0xCA, 0xFA,  // 0x0418
        0x9A, 0x6F, 0xE3, 0x41, 0x1F, 0xFF, 0x75, 0x4F,  // 0x0420
        0xBB, 0xA6, 0x00, 0xDC, 0x4D, 0xD6, 0x6A, 0x51,  // 0x0428
        0x08, 0x5C, 0x87, 0x76, 0x2D, 0x9A, 0x65, 0x6B,  // 0x0430
        0x72, 0x63, 0x3F, 0x12, 0x63, 0x25, 0x56, 0x37,  // 0x0438
        0x5C, 0x83, 0x72, 0x53, 0x1F, 0x3C, 0x36, 0x41,  // 0x0440
        0xF0, 0x5B, 0x8F, 0x83, 0xE0, 0x46, 0xC8, 0x26,  // 0x0448
        0x58, 0x88, 0x2E, 0x30, 0x93, 0x3C, 0xB9, 0xC0,  // 0x0450
        0xDF, 0x28, 0x0A, 0x3D, 0x01, 0x99, 0xFC, 0xD7,  // 0x0458
        0xB6, 0x69, 0xAF, 0xE5, 0x83, 0xD1, 0x1E, 0xDB,  // 0x0460
        0x07, 0x97, 0x23, 0x97, 0x91, 0xDC, 0xD0, 0x1C,  // 0x0468
        0x27, 0xA6, 0x59, 0x7D, 0xC4, 0x25, 0xB4, 0x8A,  // 0x0470
        0xF4, 0xFA, 0xC9, 0xA6, 0x3E, 0x88, 0xA7, 0x51,  // 0x0478
        0x85, 0xAD, 0x87, 0xED, 0x0F, 0x2B, 0x08, 0xE6,  // 0x0480
        0x9D, 0x04, 0xE5, 0x43, 0x36, 0x81, 0x14, 0x93,  // 0x0488
        0x50, 0x81, 0xD7, 0xF3, 0x36, 0x1B, 0x70, 0x60,  // 0x0490
        0x85, 0x9F, 0x80, 0x28, 0xBA, 0xE9, 0x9E, 0x93,  // 0x0498
        0x39, 0x41, 0x14, 0x64, 0xA3, 0x59, 0xB5, 0xF1,  // 0x04A0
        0x66, 0x66, 0x47, 0xEE, 0x9B, 0x1E, 0x7F, 0xB6,  // 0x04A8
        0x29, 0xC1, 0x78, 0x6C, 0x89, 0x55, 0x11, 0x2A,  // 0x04B0
        0xCE, 0xF9, 0x6D, 0x5F, 0x6B, 0x0D, 0x37, 0x27,  // 0x04B8
        0x18, 0xDB, 0x21, 0x68, 0x5B, 0x37, 0xF3, 0xE8,  // 0x04C0
        0x34, 0xD0, 0x9C, 0xE0, 0xC5, 0x1D, 0x82, 0x25,  // 0x04C8
        0xBF, 0xED, 0x6B, 0xAD, 0xFE, 0xE6, 0x04, 0xE7,  // 0x04D0
        0x77, 0x08, 0x32, 0x7E, 0xDB, 0xD7, 0x5A, 0xC1,  // 0x04D8
        0xAF, 0x45, 0x5D, 0x02, 0xFF, 0xD5, 0x25, 0xF0,  // 0x04E0
        0x5F, 0x5D, 0x02, 0xFF, 0xF5, 0x91, 0x73, 0xBF,  // 0x04E8
        0xE8, 0xF3, 0xB0, 0xD5, 0x9B, 0x46, 0x40, 0x3E,  // 0x04F0
        0x9A, 0x27, 0x46, 0xC3, 0x90, 0x16, 0x5B, 0x2C,  // 0x04F8
        0xA8, 0x18, 0x79, 0x99, 0xD1, 0x92, 0xD2, 0x06,  // 0x0500
        0x36, 0x94, 0x35, 0x31, 0xEF, 0x9C, 0x70, 0xDA,  // 0x0508
        0xA1, 0x10, 0xD8, 0x8A, 0x70, 0x02, 0xF8, 0x69,  // 0x0510
        0x94, 0xF7, 0xD4, 0x4A, 0xD4, 0x26, 0x48, 0x27,  // 0x0518
        0xD2, 0x54, 0x2E, 0x10, 0x4D, 0xCC, 0x95, 0x4B,  // 0x0520
        0x2D, 0xAE, 0xE1, 0x2F, 0x01, 0x04, 0xBF, 0x43,  // 0x0528
        0xC7, 0x94, 0xF9, 0x47, 0x8D, 0x80, 0x4D, 0xFA,  // 0x0530
        0x32, 0x9B, 0xB3, 0xBD, 0x8C, 0x99, 0xFE, 0xCE,  // 0x0538
        0xC9, 0x21, 0xD0, 0x57, 0x9F, 0x70, 0x20, 0xB8,  // 0x0540
        0x27, 0xD8, 0x04, 0x92, 0x20, 0x95, 0xB0, 0xB7,  // 0x0548
        0x5B, 0x99, 0xFE, 0x12, 0x84, 0xCD, 0x3B, 0x20,  // 0x0550
        0xCC, 0x0D, 0x77, 0x8F, 0xEC, 0xEE, 0x8B, 0x96,  // 0x0558
        0xC2, 0x4B, 0x28, 0x18, 0x8F, 0x78, 0x81, 0x65,  // 0x0560
        0xEB, 0x39, 0xBB, 0x4F, 0xC6, 0xCD, 0x9D, 0x5F,  // 0x0568
        0xF6, 0xFE, 0xD6, 0x38, 0x27, 0x58, 0x8E, 0x4A,  // 0x0570
        0x2C, 0x91, 0x03, 0x71, 0x0B, 0x24, 0xF8, 0xD5,  // 0x0578
        0xB4, 0x4B, 0xE0, 0xBF, 0x82, 0x4F, 0x50, 0xED,  // 0x0580
        0x73, 0x54, 0xD3, 0xAD, 0x9D, 0xFD, 0x22, 0x76,  // 0x0588
        0x42, 0x48, 0x95, 0x55, 0x35, 0x00, 0xF7, 0x01,  // 0x0590
        0x01, 0xF8, 0x31, 0xF0, 0x3E, 0xF8, 0x36, 0xF0,  // 0x0598
        0x04, 0x92, 0xB5, 0x1E, 0x7C, 0x71, 0x92, 0x47,  // 0x05A0
        0xD1, 0xC4, 0xA9, 0x5A, 0x14, 0x45, 0xF8, 0xB3,  // 0x05A8
        0x06, 0x3E, 0xDB, 0xE6, 0x4D, 0xC1, 0xCF, 0xE4,  // 0x05B0
        0x2E, 0x81, 0xFF, 0xEA, 0x12, 0xF8, 0xAF, 0x2E,  // 0x05B8
        0x81, 0xFF, 0xFA, 0x1F, 0x5A, 0x70, 0xD4, 0x89,  // 0x05C0
        0xA0, 0x3C, 0x00, 0x00, 
    }, 0x3CA0));

    public static ReadOnlySpan<byte> SuperZztElements => _rawSuperZztElements.Value;

}